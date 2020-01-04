using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace ProjectManagementSystem.WebApi.Extensions
{
    internal static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = AppDomain.CurrentDomain.FriendlyName,
                    Description = $"ProjectManagementSystem Api"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Insert a bearer with an access token in the field",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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

                options.CustomSchemaIds(type => type.FullName);

                options.MapType<Guid>(() => new OpenApiSchema
                {
                    Type = "string", 
                    Format = "uuid", 
                    Default = new OpenApiString(Guid.NewGuid().ToString())
                });

                options.DescribeAllEnumsAsStrings();

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                options.IncludeXmlComments(Path.Combine(basePath, "ProjectManagementSystem.WebApi.xml"));
                options.IncludeXmlComments(Path.Combine(basePath, "ProjectManagementSystem.Queries.xml"));
            });
        }
    }
}