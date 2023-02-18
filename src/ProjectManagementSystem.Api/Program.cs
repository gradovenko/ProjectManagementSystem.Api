using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Api;
using ProjectManagementSystem.Api.Extensions;
using ProjectManagementSystem.Queries;
using Prometheus;
using Prometheus.DotNetRuntime;
using Prometheus.SystemMetrics;

DotNetRuntimeStatsBuilder.Default().StartCollecting();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureServices((context, services) =>
{
    var npgsqlConnectionString = context.Configuration.GetConnectionString("ProjectMS");

    services
        .AddControllers()
        .AddFluentValidation(cfg =>
        {
            //cfg.RegisterValidatorsFromAssemblyContaining<AuthenticationBinding>();
            cfg.LocalizationEnabled = false;
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    services.AddSwagger();

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
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(context.Configuration["Authentication:Jwt:SecretKey"])),
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

        services.Configure<ProjectManagementSystem.Infrastructure.Authentication.JwtOptions>(
            context.Configuration.GetSection("Authentication:Jwt"));

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Authentication.UserDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Authentication.IUserRepository, ProjectManagementSystem.Infrastructure.Authentication.UserRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.Authentication.IJwtAccessTokenFactory, ProjectManagementSystem.Infrastructure.Authentication.JwtAccessTokenFactory>();
        services.AddScoped<ProjectManagementSystem.Domain.Authentication.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();
        services.AddDbContext<ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Authentication.IRefreshTokenStore, ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStore>();
        services.AddScoped<ProjectManagementSystem.Domain.Authentication.UserAuthenticationService>();

    #endregion

    #region DbContexts, Repositories, Services

        #region Admin

        #region Users

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.Users.UserDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.Users.IUserRepository, ProjectManagementSystem.Infrastructure.Admin.Users.UserRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.Admin.Users.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();

        #endregion

        #region IssuePriorities

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.IssuePriorities.IssuePriorityDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.IssuePriorities.IIssuePriorityRepository, ProjectManagementSystem.Infrastructure.Admin.IssuePriorities.IssuePriorityRepository>();

        #endregion

        #region IssueStatuses

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.IssueStatuses.IssueStatusDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.IssueStatuses.IIssueStatusRepository, ProjectManagementSystem.Infrastructure.Admin.IssueStatuses.IssueStatusRepository>();

        #endregion

        #region Projects

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.Projects.ProjectDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.Projects.IProjectRepository, ProjectManagementSystem.Infrastructure.Admin.Projects.ProjectRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.Admin.Projects.ITrackerRepository, ProjectManagementSystem.Infrastructure.Admin.Projects.TrackerRepository>();

        #endregion

        #region Trackers

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.Trackers.TrackerDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.Trackers.ITrackerRepository, ProjectManagementSystem.Infrastructure.Admin.Trackers.TrackerRepository>();

        #endregion

        #region TimeEntryActivities

        services.AddDbContext<ProjectManagementSystem.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.Admin.TimeEntryActivities.ITimeEntryActivityRepository, ProjectManagementSystem.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityRepository>();

        #endregion

        #endregion

        #region User

        #region Accounts

        services.AddDbContext<ProjectManagementSystem.Infrastructure.User.Accounts.UserDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.User.Accounts.IUserRepository, ProjectManagementSystem.Infrastructure.User.Accounts.UserRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Accounts.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Accounts.UserUpdateService>();

        #endregion

        #region Issues

        services.AddDbContext<ProjectManagementSystem.Infrastructure.User.Issues.ProjectDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IProjectRepository, ProjectManagementSystem.Infrastructure.User.Issues.ProjectRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.ITrackerRepository, ProjectManagementSystem.Infrastructure.User.Issues.TrackerRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IIssueStatusRepository, ProjectManagementSystem.Infrastructure.User.Issues.IssueStatusRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IIssuePriorityRepository, ProjectManagementSystem.Infrastructure.User.Issues.IssuePriorityRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IUserRepository, ProjectManagementSystem.Infrastructure.User.Issues.UserRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IIssueRepository, ProjectManagementSystem.Infrastructure.User.Issues.IssueRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Issues.IssueCreationService>();

        #endregion

        #region TimeEntries

        services.AddDbContext<ProjectManagementSystem.Infrastructure.User.TimeEntries.IssueDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.IProjectRepository, ProjectManagementSystem.Infrastructure.User.TimeEntries.ProjectRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.IIssueRepository, ProjectManagementSystem.Infrastructure.User.TimeEntries.IssueRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.IUserRepository, ProjectManagementSystem.Infrastructure.User.TimeEntries.UserRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.ITimeEntryActivityRepository, ProjectManagementSystem.Infrastructure.User.TimeEntries.TimeEntryActivityRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.ITimeEntryRepository, ProjectManagementSystem.Infrastructure.User.TimeEntries.TimeEntryRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.TimeEntries.TimeEntryCreationService>();

        #endregion
            
        #region Projects

        services.AddDbContext<ProjectManagementSystem.Infrastructure.User.Projects.ProjectDbContext>(options => options.UseNpgsql(context.Configuration.GetConnectionString("ProjectMS")));
        services.AddScoped<ProjectManagementSystem.Domain.User.Projects.IProjectRepository, ProjectManagementSystem.Infrastructure.User.Projects.ProjectRepository>();
        services.AddScoped<ProjectManagementSystem.Domain.User.Projects.ITrackerRepository, ProjectManagementSystem.Infrastructure.User.Projects.TrackerRepository>();

        #endregion

        #endregion

        #endregion

    #region Monitoring

    services.AddHealthChecks()
        .AddNpgSql(npgsqlConnectionString)
        .ForwardToPrometheus();
    services.AddSystemMetrics();
    services.AddHttpContextAccessor();

    #endregion

    #region ProjectManagementSystem.Queries

    #region Admin

        #region Users

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Users.UserQuery, ProjectManagementSystem.Queries.Admin.Users.UserView>, ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Users.UserQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Users.UserListQuery, Page<ProjectManagementSystem.Queries.Admin.Users.UserListItemView>>, ProjectManagementSystem.Queries.Infrastructure.Admin.Users.UserListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Users.UserListQuery));

        #endregion

        #region IssuePriorities

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityQuery, ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityView>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListQuery, Page<ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssuePriorities.IssuePriorityListQuery));

        #endregion

        #region IssueStatuses

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusQuery, ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusView>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListQuery, Page<ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses.IssueStatusListQueryHandler>();

        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.IssueStatuses.IssueStatusListQuery));

        #endregion

        #region Projects

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Projects.ProjectQuery, ProjectManagementSystem.Queries.Admin.Projects.ProjectView>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Projects.ProjectQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Projects.ProjectListQuery, Page<ProjectManagementSystem.Queries.Admin.Projects.ProjectListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.Projects.ProjectListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Projects.ProjectListQuery));

        #endregion

        #region Trackers

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Trackers.TrackerQuery, ProjectManagementSystem.Queries.Admin.Trackers.TrackerView>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Trackers.TrackerQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.Trackers.TrackerListQuery, Page<ProjectManagementSystem.Queries.Admin.Trackers.TrackerListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.Trackers.TrackerListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.Trackers.TrackerListQuery));

        #endregion

        #region TimeEntryActivities

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityQuery, ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityView>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListQuery, Page<ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.Admin.TimeEntryActivities.TimeEntryActivityListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.Admin.TimeEntryActivities.TimeEntryActivityListQuery));

        #endregion

        #endregion
        
        #region User

        #region Accounts

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.Accounts.UserDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Accounts.UserQuery, ProjectManagementSystem.Queries.User.Accounts.UserView>, ProjectManagementSystem.Queries.Infrastructure.User.Accounts.UserQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.Accounts.UserQuery));

        #endregion

        #region Projects
        
        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.Projects.ProjectDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Projects.ProjectListQuery, Page<ProjectManagementSystem.Queries.User.Projects.ProjectListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.User.Projects.ProjectsQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.Projects.ProjectListQuery));

        #endregion

        #region ProjectIssues
        
        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectIssues.IssueListQuery, Page<ProjectManagementSystem.Queries.User.ProjectIssues.IssueListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectIssues.IssueListQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectIssues.IssueQuery, ProjectManagementSystem.Queries.User.ProjectIssues.IssueView>, 
            ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues.IssueQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectIssues.IssueQuery));

        #endregion

        #region IssueTimeEntries

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListQuery, Page<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListQuery));

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryView>, 
            ProjectManagementSystem.Queries.Infrastructure.User.IssueTimeEntries.TimeEntryQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryQuery));

        #endregion

        #region ProjectTimeEntries

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListQuery, Page<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryListQuery));
            
        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryView>, 
            ProjectManagementSystem.Queries.Infrastructure.User.ProjectTimeEntries.TimeEntryQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.ProjectTimeEntries.TimeEntryQuery));

        #endregion
            
        #region TimeEntries

        services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryDbContext>(npgsqlConnectionString!);

        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListQuery, Page<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListItemView>>, 
            ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryListQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryListQuery));
            
        services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryQuery, ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryView>, 
            ProjectManagementSystem.Queries.Infrastructure.User.TimeEntries.TimeEntryQueryHandler>();
            
        services.AddMediatR(typeof(ProjectManagementSystem.Queries.User.TimeEntries.TimeEntryQuery));

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