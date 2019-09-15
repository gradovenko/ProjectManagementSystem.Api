using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EventFlow;
using EventFlow.Autofac.Extensions;
using EventFlow.Extensions;
using FluentValidation.AspNetCore;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ProjectManagementSystem.Infrastructure.Authentication;
using ProjectManagementSystem.Infrastructure.PasswordHasher;
using ProjectManagementSystem.Infrastructure.RefreshTokenStore;
using ProjectManagementSystem.Queries;
using ProjectManagementSystem.WebApi.Authorization;
using ProjectManagementSystem.WebApi.Filters;
using ProjectManagementSystem.WebApi.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services
                .AddMvc(options => { options.Filters.Add(typeof(ErrorHandlingFilter)); })
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            ProcessDictionaryKeys = true
                        }
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(ConfigureSwagger);

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

            #endregion

            #region Queries
            
            var containerBuilder = new ContainerBuilder();

            services.AddDbContext<Queries.Infrastructure.Admin.Users.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            services.AddDbContext<Queries.Infrastructure.User.Accounts.UserDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("ProjectMS")));
            
            var container = EventFlowOptions.New
                .UseAutofacContainerBuilder(containerBuilder)
                .AddQueryHandler<Queries.Infrastructure.Admin.Users.UserQueryHandler, Queries.Admin.Users.UserQuery,
                    Queries.Admin.Users.ShortUserView>()
                .AddQueryHandler<Queries.Infrastructure.Admin.Users.UsersQueryHandler, Queries.Admin.Users.UsersQuery,
                    Page<Queries.Admin.Users.FullUserView>>()
                .AddQueryHandler<Queries.Infrastructure.User.Accounts.UserQueryHandler, Queries.User.Accounts.UserQuery,
                    Queries.User.Accounts.UserView>();

            containerBuilder.Populate(services);

            return new AutofacServiceProvider(containerBuilder.Build());

            #endregion
        }

        private void ConfigureSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info
            {
                Version = "v1",
                Title = AppDomain.CurrentDomain.FriendlyName,
                Description = $"Swagger for {AppDomain.CurrentDomain.FriendlyName}",
            });

            options.CustomSchemaIds(type => type.FullName);

            options.MapType<Guid>(() => new Schema
            {
                Type = "string",
                Format = "uuid",
                Default = Guid.NewGuid()
            });

            options.DescribeAllEnumsAsStrings();

            options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            {
                In = "header",
                Description =
                    "Insert the access token with Bearer in the field",
                Name = "Authorization",
                Type = "apiKey",
            });

            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            {
                {"Bearer", new string[] { }}
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
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