using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProjectManagementSystem.WebApi.Filters;
using ProjectManagementSystem.WebApi.Middlewares;
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
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ErrorHandlingFilter));
                })
                .AddFluentValidation(configuration =>
                {
                    configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGen(ConfigureSwagger);
        }

        private void ConfigureSwagger(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = AppDomain.CurrentDomain.FriendlyName,
                Description = $"Swagger for {AppDomain.CurrentDomain.FriendlyName}",
            });

            options.CustomSchemaIds(type => type.FullName);

            options.MapType<Guid>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "uuid",
                Default = new OpenApiString(Guid.NewGuid().ToString())
            });

            options.DescribeAllEnumsAsStrings();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Description = "Insert the session ID with Bearer in the field\nExample:\nBearer ac17e09a-5257-4405-bff2-341f30f78aa2",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                
                    },
                    new List<string>()
                }
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
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