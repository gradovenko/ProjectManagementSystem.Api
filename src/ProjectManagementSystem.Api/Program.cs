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
using Microsoft.IdentityModel.Tokens;
using ProjectManagementSystem.Api;
using ProjectManagementSystem.Api.Extensions;
using Prometheus;
using Prometheus.DotNetRuntime;
using Prometheus.SystemMetrics;

DotNetRuntimeStatsBuilder.Default().StartCollecting();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureServices((context, services) =>
{
    #region BasicSettings

    var npgsqlConnectionString = context.Configuration.GetConnectionString("ProjectMS")!;

    services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProjectManagementSystem.Queries.Infrastructure.Issues.IssueQueryHandler>());
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ProjectManagementSystem.Queries.User.Issues.IssueQuery>());

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
        .AddNpgSql(npgsqlConnectionString)
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
        ).RequireAuthenticatedUser().RequireRole(ProjectManagementSystem.Api.Authorization.UserRole.Admin, ProjectManagementSystem.Api.Authorization.UserRole.User);

        options.DefaultPolicy = authorizationPolicyBuilder.Build();
    });

    #endregion

    #region Commands

    #region Authentication

    services.Configure<ProjectManagementSystem.Infrastructure.Authentication.JwtAuthOptions>(
        context.Configuration.GetSection("Authentication:Jwt"));

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Authentication.UserDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IUserGetter, ProjectManagementSystem.Infrastructure.Authentication.UserRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IAccessTokenCreator, ProjectManagementSystem.Infrastructure.Authentication.JwtAccessTokenCreator>();
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStoreDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Authentication.IRefreshTokenStore, ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStore>();

    #endregion
    
    #region Issues
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Issues.IssueDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IIssueRepository, ProjectManagementSystem.Infrastructure.Issues.IssueRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.ILabelGetter, ProjectManagementSystem.Infrastructure.Issues.LabelGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IProjectGetter, ProjectManagementSystem.Infrastructure.Issues.ProjectGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IReactionGetter, ProjectManagementSystem.Infrastructure.Issues.ReactionGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IUserGetter, ProjectManagementSystem.Infrastructure.Issues.UserGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IIssueAssigneeGetter, ProjectManagementSystem.Infrastructure.Issues.IssueAssigneeGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IIssueLabelGetter, ProjectManagementSystem.Infrastructure.Issues.IssueLabelGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Issues.IIssueUserReactionGetter, ProjectManagementSystem.Infrastructure.Issues.IssueUserReactionGetter>();

    #endregion
    
    #region Labels

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Labels.LabelDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Labels.ILabelRepository, ProjectManagementSystem.Infrastructure.Labels.LabelRepository>();

    #endregion
    
    #region Projects

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Projects.ProjectDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Projects.IProjectRepository, ProjectManagementSystem.Infrastructure.Projects.ProjectRepository>();

    #endregion

    #region TimeEntries
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.TimeEntries.TimeEntryDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.ITimeEntryRepository, ProjectManagementSystem.Infrastructure.TimeEntries.TimeEntryRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.IIssueGetter, ProjectManagementSystem.Infrastructure.TimeEntries.IssueGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.TimeEntries.IUserGetter, ProjectManagementSystem.Infrastructure.TimeEntries.UserGetter>();

    #endregion

    #region Users

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Users.UserDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Users.IUserRepository, ProjectManagementSystem.Infrastructure.Users.UserRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.Users.IPasswordHasher, ProjectManagementSystem.Infrastructure.PasswordHasher.PasswordHasher>();
    services.AddScoped<ProjectManagementSystem.Domain.Users.IRefreshTokenStore, ProjectManagementSystem.Infrastructure.RefreshTokenStore.RefreshTokenStore>();

    #endregion
    
    #region Comments
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Comments.CommentDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Comments.ICommentRepository, ProjectManagementSystem.Infrastructure.Comments.CommentRepository>();
    services.AddScoped<ProjectManagementSystem.Domain.Comments.IIssueGetter, ProjectManagementSystem.Infrastructure.Comments.IssueGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Comments.IReactionGetter, ProjectManagementSystem.Infrastructure.Comments.ReactionGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Comments.ICommentUserReactionGetter, ProjectManagementSystem.Infrastructure.Comments.CommentUserReactionGetter>();
    services.AddScoped<ProjectManagementSystem.Domain.Comments.IUserGetter, ProjectManagementSystem.Infrastructure.Comments.UserGetter>();

    #endregion
    
    #region Reactions
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Infrastructure.Reactions.ReactionDbContext>(npgsqlConnectionString);
    services.AddScoped<ProjectManagementSystem.Domain.Reactions.IReactionRepository, ProjectManagementSystem.Infrastructure.Reactions.ReactionRepository>();

    #endregion

    #endregion

    #region Queries

    #region User

    #region Users

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Users.UserDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Profiles.UserQuery, 
            ProjectManagementSystem.Queries.User.Profiles.UserViewModel>, 
        ProjectManagementSystem.Queries.Infrastructure.Users.UserQueryHandler>();

    #endregion

    #region Projects
    
    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Projects.ProjectQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Projects.ProjectListQuery, 
            ProjectManagementSystem.Queries.Page<ProjectManagementSystem.Queries.User.Projects.ProjectListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.Projects.ProjectQueryHandler>();

    #endregion
    
    #region Issues

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Issues.IssueQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Issues.IssueQuery, 
            ProjectManagementSystem.Queries.User.Issues.IssueViewModel?>, 
        ProjectManagementSystem.Queries.Infrastructure.Issues.IssueQueryHandler>();

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Issues.IssueListQuery, 
            ProjectManagementSystem.Queries.Page<ProjectManagementSystem.Queries.User.Issues.IssueListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.Issues.IssueQueryHandler>();

    #endregion

    #region TimeEntries

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.IssueTimeEntries.TimeEntryQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListQuery, 
            IEnumerable<ProjectManagementSystem.Queries.User.IssueTimeEntries.TimeEntryListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.IssueTimeEntries.TimeEntryQueryHandler>();

    #endregion
    
    #region Labels

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Labels.LabelQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Labels.LabelListQuery, 
            ProjectManagementSystem.Queries.Page<ProjectManagementSystem.Queries.User.Labels.LabelListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.Labels.LabelQueryHandler>();

    #endregion
    
    #region Reactions

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.Reactions.ReactionQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.Reactions.ReactionListQuery, 
            IEnumerable<ProjectManagementSystem.Queries.User.Reactions.ReactionListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.Reactions.ReactionQueryHandler>();

    #endregion
    
    #region IssueComments

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.IssueComments.CommentQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueComments.CommentListQuery, 
            ProjectManagementSystem.Queries.Page<ProjectManagementSystem.Queries.User.IssueComments.CommentListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.IssueComments.CommentQueryHandler>();

    #endregion

    #region IssueLabels

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.IssueLabels.LabelQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueLabels.LabelListQuery, 
            IEnumerable<ProjectManagementSystem.Queries.User.IssueLabels.LabelListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.IssueLabels.LabelQueryHandler>();

    #endregion

    #region IssueReactions

    services.AddNpgsqlDbContextPool<ProjectManagementSystem.Queries.Infrastructure.IssueReactions.ReactionQueryDbContext>(npgsqlConnectionString);

    services.AddScoped<IRequestHandler<ProjectManagementSystem.Queries.User.IssueReactions.ReactionListQuery, 
            IEnumerable<ProjectManagementSystem.Queries.User.IssueReactions.ReactionListItemViewModel>>, 
        ProjectManagementSystem.Queries.Infrastructure.IssueReactions.ReactionQueryHandler>();

    #endregion

    #endregion

    #endregion

    #region DatabaseMigrations

    services.AddNpgsqlDbContext<ProjectManagementSystem.DatabaseMigrations.MigrationDbContext>(npgsqlConnectionString);

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