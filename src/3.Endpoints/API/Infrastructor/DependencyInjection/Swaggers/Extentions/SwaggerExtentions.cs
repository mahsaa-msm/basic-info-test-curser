using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Extentions;
using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Filters;
using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Extentions;

public static class SwaggerExtentions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        var swaggerOption = configuration.GetSection(sectionName).Get<SwaggerOption>();

        if (swaggerOption != null && swaggerOption.SwaggerDoc != null && swaggerOption.Enabled == true)
        {
            services.AddSwaggerGen(options =>
            {
                options.DocumentFilter<TenantHeadersOperationFilter>();

                options.EnableAnnotations();
                options.SwaggerDoc(swaggerOption.SwaggerDoc.Name, new OpenApiInfo
                {
                    Title = swaggerOption.SwaggerDoc.Title,
                    Version = swaggerOption.SwaggerDoc.Version
                });

                var oAuthOption = configuration.GetSection("Swagger").GetSection("OAuth").Get<SwaggerOAuthOption>();
                if (oAuthOption != null && oAuthOption.Enabled)
                {
                    options.AddSecurityDefinition("Oauth2", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = "Oauth2",
                        BearerFormat = "Bearer <token>",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(oAuthOption.AuthorizationUrl),
                                TokenUrl = new Uri(oAuthOption.TokenUrl),
                                Scopes = oAuthOption.Scopes,
                            }
                        },
                    }); ;

                    options.OperationFilter<AddParamsToHeader>();
                }
                else
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Enter 'Bearer' followed by space and JWT token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
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
                                   }
                               },
                               new string[] {}
                           }
                       }
                    );
                }
            });
        }

        return services;
    }

    public static void UseSwaggerUI(this WebApplication app, string sectionName)
    {
        var swaggerOption = app.Configuration.GetSection(sectionName).Get<SwaggerOption>();

        if (swaggerOption != null && swaggerOption.SwaggerDoc != null && swaggerOption.Enabled == true)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint(swaggerOption.SwaggerDoc.URL, swaggerOption.SwaggerDoc.Title);
                options.RoutePrefix = string.Empty;
                options.OAuthUsePkce();
            });
        }
    }
}
