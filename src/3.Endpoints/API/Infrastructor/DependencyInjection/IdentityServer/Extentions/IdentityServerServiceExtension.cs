using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Filters;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Middlewares;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Extentions;

public static class IdentityServerServiceExtension
{
    public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        var oAuthOption = configuration.GetSection(sectionName).Get<OAuthOption>();

        if (oAuthOption != null && oAuthOption.Enabled)
        {
            services.AddProviderHttpClient(oAuthOption);

            var authenticationBuilder = services.AddAuthentication(oAuthOption.TokenType.ToString());

            if (oAuthOption.TokenType == TokenType.JWTToken)
            {
                authenticationBuilder.AddJwtTokenSupoort(oAuthOption);
            }
            else
            {
                authenticationBuilder.AddReferenceTokenSupoort(oAuthOption);

                services.AddControllers(o =>
                {
                    o.Filters.Add<CustomAuthorizationFilter>();
                });
            }
        }

        return services;
    }

    public static bool UseIdentityServer(this WebApplication app, string sectionName)
    {
        var oAuthOption = app.Configuration.GetSection(sectionName).Get<OAuthOption>();

        if (oAuthOption != null && oAuthOption.Enabled)
        {
            app.UseMiddleware<FakeAuthenticationMiddleware>();
            app.UseAuthentication();
            if (oAuthOption.AuthorizationConfigs is not null &&
                oAuthOption.AuthorizationConfigs.Enabled)
                app.UseAuthorization();
        }

        return oAuthOption != null && oAuthOption.Enabled;
    }
}

