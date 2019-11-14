using System;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using MediatR;
using ProjectManagementSystem.DatabaseMigrations;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.WebApi.Authorization;
using ProjectManagementSystem.WebApi.Extensions;
using ProjectManagementSystem.WebApi.Filters;
using ProjectManagementSystem.WebApi.Middlewares;

namespace ProjectManagementSystem.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ErrorHandlingFilter));
                    options.EnableEndpointRouting = false;
                })
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwagger();

            #region Authentication

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
                    ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.Configure<Infrastructure.Authentication.JwtOptions>(
                Configuration.GetSection("Authentication:Jwt"));

            services.AddDbContext<Infrastructure.Authentication.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.Authentication.IUserRepository, Infrastructure.Authentication.UserRepository>();
            services
                .AddScoped<Domain.Authentication.IJwtAccessTokenFactory,
                    Infrastructure.Authentication.JwtAccessTokenFactory>();
            services.AddScoped<Domain.Authentication.IPasswordHasher, Infrastructure.PasswordHasher.PasswordHasher>();
            services.AddDbContext<Infrastructure.RefreshTokenStore.RefreshTokenDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.Authentication.IRefreshTokenStore, Infrastructure.RefreshTokenStore.RefreshTokenStore
                >();
            services.AddScoped<Domain.Authentication.UserAuthenticationService>();

            #endregion

            #region Authorization

            services.AddAuthorization(options =>
            {
                var authorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                        JwtBearerDefaults.AuthenticationScheme
                    ).RequireAuthenticatedUser()
                    .RequireRole(UserRole.Admin, UserRole.User);

                options.DefaultPolicy = authorizationPolicyBuilder.Build();
            });

            #endregion

            #region DatabaseMigrationsContext

            services.AddDbContext<ProjectManagementSystemDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            #endregion

            #region DbContextsRepositoriesServices

            #region Admin

            #region Users

            services.AddDbContext<Infrastructure.Admin.Users.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.Admin.CreateUsers.IUserRepository, Infrastructure.Admin.Users.UserRepository>();
            services.AddScoped<Domain.Admin.CreateUsers.IPasswordHasher, Infrastructure.PasswordHasher.PasswordHasher>();

            #endregion

            #region IssuePriorities

            services.AddDbContext<Infrastructure.Admin.IssuePriorities.IssuePriorityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.Admin.IssuePriorities.IIssuePriorityRepository,
                    Infrastructure.Admin.IssuePriorities.IssuePriorityRepository>();

            #endregion

            #region IssueStatuses

            services.AddDbContext<Infrastructure.Admin.IssueStatuses.IssueStatusDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.Admin.IssueStatuses.IIssueStatusRepository,
                    Infrastructure.Admin.IssueStatuses.IssueStatusRepository>();

            #endregion

            #region Projects

            services.AddDbContext<Infrastructure.Admin.CreateProjects.ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.Admin.CreateProjects.IProjectRepository,
                    Infrastructure.Admin.CreateProjects.ProjectRepository>();
            services
                .AddScoped<Domain.Admin.CreateProjects.ITrackerRepository,
                    Infrastructure.Admin.CreateProjects.TrackerRepository>();

            #endregion

            #region Trackers

            services.AddDbContext<Infrastructure.Admin.CreateTrackers.TrackerDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.Admin.CreateTrackers.ITrackerRepository,
                    Infrastructure.Admin.CreateTrackers.TrackerRepository>();

            #endregion

            #endregion

            #region User

            #region Accounts

            services.AddDbContext<Infrastructure.User.Accounts.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.User.Accounts.IUserRepository, Infrastructure.User.Accounts.UserRepository>();
            services.AddScoped<Domain.User.Accounts.IPasswordHasher, Infrastructure.PasswordHasher.PasswordHasher>();
            services.AddScoped<Domain.User.Accounts.UserUpdateService>();

            #endregion

            #region ProjectIssues

            services.AddDbContext<Infrastructure.User.CreateProjectIssues.ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services
                .AddScoped<Domain.User.CreateProjectIssues.IProjectRepository,
                    Infrastructure.User.CreateProjectIssues.ProjectRepository>();
            services
                .AddScoped<Domain.User.CreateProjectIssues.ITrackerRepository,
                    Infrastructure.User.CreateProjectIssues.TrackerRepository>();
            services
                .AddScoped<Domain.User.CreateProjectIssues.IIssueStatusRepository,
                    Infrastructure.User.CreateProjectIssues.IssueStatusRepository>();
            services
                .AddScoped<Domain.User.CreateProjectIssues.IIssuePriorityRepository,
                    Infrastructure.User.CreateProjectIssues.IssuePriorityRepository>();
            services
                .AddScoped<Domain.User.CreateProjectIssues.IUserRepository,
                    Infrastructure.User.CreateProjectIssues.UserRepository>();
            services
                .AddScoped<Domain.User.CreateProjectIssues.IIssueRepository,
                    Infrastructure.User.CreateProjectIssues.IssueRepository>();
            services.AddScoped<Domain.User.CreateProjectIssues.IssueCreationService>();

            #endregion

            #endregion

            #endregion

            #region Queries

            #region Admin

            #region Users

            services.AddDbContext<Queries.Infrastructure.Admin.Users.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services.AddScoped<IRequestHandler<Queries.Admin.Users.UserQuery, Queries.Admin.Users.ShortUserView>,
                Queries.Infrastructure.Admin.Users.UserQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Users.UserQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.Admin.Users.UsersQuery, Page<Queries.Admin.Users.FullUserView>>,
                    Queries.Infrastructure.Admin.Users.UsersQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Users.UsersQuery).Assembly);

            #endregion

            #region IssuePriorities

            services.AddDbContext<Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.Admin.IssuePriorities.IssuePriorityQuery,
                        Queries.Admin.IssuePriorities.ShortIssuePriorityView>,
                    Queries.Infrastructure.Admin.IssuePriorities.IssuePriorityQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.IssuePriorities.IssuePriorityQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.Admin.IssuePriorities.IssuePrioritiesQuery,
                        Page<Queries.Admin.IssuePriorities.FullIssuePriorityView>>,
                    Queries.Infrastructure.Admin.IssuePriorities.IssuePrioritiesQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.IssuePriorities.IssuePrioritiesQuery).Assembly);

            #endregion

            #region IssueStatuses

            services.AddDbContext<Queries.Infrastructure.Admin.IssueStatuses.IssueStatusDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.Admin.IssueStatuses.IssueStatusQuery,
                        Queries.Admin.IssueStatuses.ShortIssueStatusView>,
                    Queries.Infrastructure.Admin.IssueStatuses.IssueStatusQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.IssueStatuses.IssueStatusQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.Admin.IssueStatuses.IssueStatusesQuery,
                        Page<Queries.Admin.IssueStatuses.FullIssueStatusView>>,
                    Queries.Infrastructure.Admin.IssueStatuses.IssueStatusesQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.IssueStatuses.IssueStatusesQuery).Assembly);

            #endregion

            #region Projects

            services.AddDbContext<Queries.Infrastructure.Admin.Projects.ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.Admin.Projects.ProjectQuery,
                        Queries.Admin.Projects.ShortProjectView>,
                    Queries.Infrastructure.Admin.Projects.ProjectQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Projects.ProjectQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.Admin.Projects.ProjectsQuery,
                        Page<Queries.Admin.Projects.FullProjectView>>,
                    Queries.Infrastructure.Admin.Projects.ProjectsQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Projects.ProjectsQuery).Assembly);

            #endregion

            #region Trackers

            services.AddDbContext<Queries.Infrastructure.Admin.Trackers.TrackerDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.Admin.Trackers.TrackerQuery,
                        Queries.Admin.Trackers.ShortTrackerView>,
                    Queries.Infrastructure.Admin.Trackers.TrackerQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Trackers.TrackerQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.Admin.Trackers.TrackersQuery,
                        Page<Queries.Admin.Trackers.FullTrackerView>>,
                    Queries.Infrastructure.Admin.Trackers.TrackersQueryHandler>();
            services.AddMediatR(typeof(Queries.Admin.Trackers.TrackersQuery).Assembly);

            #endregion

            #endregion

            #region User

            #region Accounts

            services.AddDbContext<Queries.Infrastructure.User.Accounts.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.User.Accounts.UserQuery, Queries.User.Accounts.UserView>,
                    Queries.Infrastructure.User.Accounts.UserQueryHandler>();
            services.AddMediatR(typeof(Queries.User.Accounts.UserQuery).Assembly);

            #endregion

            #region Projects

            services.AddDbContext<Queries.Infrastructure.User.Projects.ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.User.Projects.ProjectsQuery,
                        Page<Queries.User.Projects.ProjectsView>>,
                    Queries.Infrastructure.User.Projects.ProjectsQueryHandler>();
            services.AddMediatR(typeof(Queries.User.Projects.ProjectsQuery).Assembly);

            #endregion

            #region ProjectIssues

            services.AddDbContext<Queries.Infrastructure.User.ProjectIssues.ProjectDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));

            services
                .AddScoped<IRequestHandler<Queries.User.ProjectIssues.IssueListQuery,
                        Page<Queries.User.ProjectIssues.IssueListView>>,
                    Queries.Infrastructure.User.ProjectIssues.IssueListQueryHandler>();
            services.AddMediatR(typeof(Queries.User.ProjectIssues.IssueListQuery).Assembly);

            services
                .AddScoped<IRequestHandler<Queries.User.ProjectIssues.IssueQuery,
                        Queries.User.ProjectIssues.IssueView>,
                    Queries.Infrastructure.User.ProjectIssues.IssueQueryHandler>();
            services.AddMediatR(typeof(Queries.User.ProjectIssues.IssueQuery).Assembly);

            #endregion

            #endregion

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
//            else
//            {
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }

            //app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../v1/swagger.json", AppDomain.CurrentDomain.FriendlyName);
            });

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (int) HttpStatusCode.Redirect));
        }
    }
}