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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using MediatR;
using ProjectManagementSystem.Infrastructure.Authentication;
using ProjectManagementSystem.Infrastructure.PasswordHasher;
using ProjectManagementSystem.Infrastructure.RefreshTokenStore;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.WebApi.Authorization;
using ProjectManagementSystem.WebApi.Extensions;
using ProjectManagementSystem.WebApi.Filters;
using ProjectManagementSystem.WebApi.Middlewares;

namespace ProjectManagementSystem.WebApi
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public IConfiguration Configuration { get; }

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            _logger = logger;
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

            services.Configure<JwtOptions>(Configuration.GetSection("Authentication:Jwt"));

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.Authentication.IUserRepository, Infrastructure.Authentication.UserRepository>();
            services.AddScoped<Domain.Authentication.IJwtAccessTokenFactory, JwtAccessTokenFactory>();
            services.AddScoped<Domain.Authentication.IPasswordHasher, PasswordHasher>();
            services.AddDbContext<RefreshTokenDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.Authentication.IRefreshTokenStore, RefreshTokenStore>();
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

            #region DbContexts, repositories and services

            #region User

            #region Accounts

            services.AddDbContext<Infrastructure.User.Accounts.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.User.Accounts.IUserRepository, Infrastructure.User.Accounts.UserRepository>();
            services.AddScoped<Domain.User.Accounts.IPasswordHasher, PasswordHasher>();
            services.AddScoped<Domain.User.Accounts.UserUpdateService>();

            #endregion

            #endregion

            #region Admin

            #region Users

            services.AddDbContext<Infrastructure.Admin.Users.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddScoped<Domain.Admin.CreateUsers.IUserRepository, Infrastructure.Admin.Users.UserRepository>();
            services.AddScoped<Domain.Admin.CreateUsers.IPasswordHasher, PasswordHasher>();

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
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            } 
            
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../v1/swagger.json", AppDomain.CurrentDomain.FriendlyName);
            });

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (int) HttpStatusCode.Redirect));
        }
    }
}