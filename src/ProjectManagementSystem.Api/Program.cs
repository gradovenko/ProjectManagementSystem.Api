using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Api;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Domain.Users;
using ProjectManagementSystem.Infrastructure.Users;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.Queries.User.Profiles;
using Prometheus;
using Prometheus.DotNetRuntime;
using Prometheus.SystemMetrics;
using IPasswordHasher = ProjectManagementSystem.Domain.Users.IPasswordHasher;
using IUserRepository = ProjectManagementSystem.Domain.Users.IUserRepository;
using UserRepository = ProjectManagementSystem.Infrastructure.Users.UserRepository;

DotNetRuntimeStatsBuilder.Default().StartCollecting();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureServices((context, services) =>
{
    #region BasicSettings

    var npgsqlConnectionString = context.Configuration.GetConnectionString("ProjectMS");

    services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    #endregion

    #region OpenApi

    services.AddSwagger();

    #endregion
    
    #region Validation

    services
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddValidatorsFromAssembly(typeof(Program).Assembly);

    #endregion
    
    #region Monitoring

    services.AddHealthChecks()
        .AddNpgSql(npgsqlConnectionString!)
        .ForwardToPrometheus();
    services.AddSystemMetrics();
    services.AddHttpContextAccessor();

    #endregion

    #region AuthenticationAndAuthorization

    services.AddAuthentication(configureOptions =>
    {
        configureOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        configureOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = context.Configuration["Authentication:Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = context.Configuration["Authentication:Jwt:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.Configuration["Authentication:Jwt:SecretKey"]!)),
            ValidateIssuerSigningKey = true,
        };
    });
    
    services.AddAuthorization(options =>
    {
        var authorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme
            ).RequireAuthenticatedUser()
            .RequireRole(ProjectManagementSystem.Api.Authorization.UserRole.Admin, ProjectManagementSystem.Api.Authorization.UserRole.User);

        options.DefaultPolicy = authorizationPolicyBuilder.Build();
    });

    services.Configure<ProjectManagementSystem.Infrastructure.Authentication.JwtAuthOptions>(
        context.Configuration.GetSection("Authentication:Jwt"));

    services.AddDbContext<ProjectManagementSystem.Infrastructure.Authentication.UserDbContext>(options =>
        options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IUserGetter, ProjectManagementSystem.Infrastructure.Authentication.UserRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IAccessTokenCreator, ProjectManagementSystem.Infrastructure.Authentication.JwtAccessTokenCreator>();
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();
    services.AddDbContext<ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStoreDbContext>(options =>
        options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IRefreshTokenStore, ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStore>();
    
    // services.AddScoped<IRequestHandler<ProjectManagementSystem.Domain.Authentication.Commands.AuthenticateUserByPasswordCommand, 
    //         ProjectManagementSystem.Domain.CommandResult<ProjectManagementSystem.Domain.Authentication.Token, 
    //             ProjectManagementSystem.Domain.Authentication.Commands.AuthenticateUserByPasswordCommandResultState>>, 
    //     ProjectManagementSystem.Api.Handlers.AuthenticateUserByPasswordCommandHandler>();

    #endregion

    #region Commands

    #region Users

    services.AddDbContext<UserDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<UserCreator>();
    services.AddScoped<IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();

    #endregion

    // #region Issues
    //
    // services.AddDbContext<ProjectDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    // services.AddScoped<IProjectRepository, ProjectRepository>();
    // services.AddScoped<ITrackerRepository, TrackerRepository>();
    // services.AddScoped<IIssueStatusRepository, IssueStatusRepository>();
    // services.AddScoped<IIssuePriorityRepository, IssuePriorityRepository>();
    // services.AddScoped<ProjectManagementSystem.Domain.Issues2.IUserRepository, ProjectManagementSystem.Infrastructure.Issues.UserRepository>();
    // services.AddScoped<IIssueRepository, IssueRepository>();
    // services.AddScoped<IssueCreationService>();
    //
    // #endregion
    //
    // #region TimeEntries
    //
    // services.AddDbContext<IssueDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    // services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.IProjectRepository, ProjectManagementSystem.Infrastructure.TimeEntries.ProjectRepository>();
    // services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.IIssueRepository, ProjectManagementSystem.Infrastructure.TimeEntries.IssueRepository>();
    // services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.IUserRepository, ProjectManagementSystem.Infrastructure.TimeEntries.UserRepository>();
    // services.AddScoped<ITimeEntryActivityRepository, TimeEntryActivityRepository>();
    // services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
    // services.AddScoped<TimeEntryCreationService>();
    //
    // #endregion
        
    #region Projects

    services.AddDbContext<ProjectManagementSystem.Infrastructure.Projects.ProjectDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
    services.AddScoped<ProjectManagementSystem.Domain.Projects.IProjectRepository, ProjectManagementSystem.Infrastructure.Projects.ProjectRepository>();

    #endregion

    #endregion

    #region Queries

    #region Admin

    #region Users

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Users.UserQuery, ProjectManagementSystem.Queries.Admin.Users.UserView?>, ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Users.UserQuery).Assembly);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Users.UserListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.Users.UserListItemViewModel>>, ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Users.UserListQuery));

    #endregion

    #region IssuePriorities

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityQuery, ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityView>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListQuery));

    #endregion

    #region IssueStatuses

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusQuery, ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusView>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusListQueryHandler>();

    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListQuery));

    #endregion

    #region Projects

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Projects.ProjectQuery, ProjectManagementSystem.Queries.Admin.Projects.ProjectView>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Projects.ProjectQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Projects.ProjectListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.Projects.ProjectListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Projects.ProjectListQuery));

    #endregion

    #region Trackers

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Trackers.TrackerQuery, ProjectManagementSystem.Queries.Admin.Trackers.TrackerView>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Trackers.TrackerQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Trackers.TrackerListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.Trackers.TrackerListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Trackers.TrackerListQuery));

    #endregion

    #region TimeEntryActivities

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityQuery, ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityView>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListQuery, PageViewModel<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListQuery));

    #endregion

    #endregion
        
    #region User

    #region Accounts

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.Accounts.UserDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<UserQuery, UserViewModel>, ProjectManagementSystem.Queries.Infrastructure.User.Accounts.UserQueryHandler>();
        
    //services.AddMediatR(typeof(UserQuery));

    #endregion

    #region Projects
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.Projects.ProjectDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Projects.ProjectListQuery, PageViewModel<ProjectManagementSystem.Queries.User.Projects.ProjectListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.User.Projects.ProjectsQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.Projects.ProjectListQuery));

    #endregion

    #region ProjectIssues
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectIssues.IssueListQuery, PageViewModel<ProjectManagementSystem.Queries.User.ProjectIssues.IssueListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectIssues.IssueListQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectIssues.IssueQuery, ProjectManagementSystem.Queries.User.ProjectIssues.IssueViewModel>, 
        ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectIssues.IssueQuery));

    #endregion

    #region IssueTimeEntries

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListQuery, PageViewModel<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListQuery));

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryView>, 
        ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryQuery));

    #endregion

    #region ProjectTimeEntries

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListQuery, PageViewModel<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListQuery));
        
    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryView>, 
        ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryQuery));

    #endregion
        
    #region TimeEntries

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListQuery, PageViewModel<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListItemView>>, 
        ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryListQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListQuery));
        
    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryView>, 
        ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryQueryHandler>();
        
    //services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryQuery));

    #endregion

    #endregion

    #endregion

    #region DatabaseMigrations

    services.AddNpgsqlDbContext<ProjectManagementSystem.DatabaseMigrations.MigrationDbContext>(npgsqlConnectionString!);

    #endregion
});

if (builder.Environment.IsDevelopment())
    builder.Logging.AddSimpleConsole(options => { options.IncludeScopes = false; });
else
    builder.Logging.AddJsonConsole(options =>
    {
        options.IncludeScopes = true;
        options.JsonWriterOptions = new JsonWriterOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

// app.UseAspNetCorePathBase();
//
// app.UseHttpRequestResponseServerLogging();
//
// app.UseDbConcurrencyExceptionHandling();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpMetrics();

app.MapControllers();
app.MapMetrics();
app.MapSwagger("{documentName}/swagger.json");
app.MapHealthChecks("hc");

app.UseSwaggerUI(options => { options.SwaggerEndpoint("../v1/swagger.json", "Project Management System Api v1"); });

app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (int)HttpStatusCode.Redirect));

app.RunDatabaseMigrations();

app.Run();