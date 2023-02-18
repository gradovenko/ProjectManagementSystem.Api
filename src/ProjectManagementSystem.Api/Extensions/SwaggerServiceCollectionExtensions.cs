using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace ProjectManagementSystem.Api.Extensions;

internal static class SwaggerServiceCollectionExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Project Management System Api",
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

            options.CustomSchemaIds(type => type.FullName);

            options.MapType<Guid>(() => new OpenApiSchema
            {
                Type = "string", 
                Format = "uuid", 
                Default = new OpenApiString(Guid.NewGuid().ToString())
            });

            options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectManagementSystem.Api.xml"));
            options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectManagementSystem.Queries.xml"));
        });
    }
}