using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Microsoft.AspNetCore.Authentication;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer;

public static class JwtTokenExtensions
{
    public static AuthenticationBuilder AddJwtTokenSupoort(this AuthenticationBuilder authenticationBuilder, OAuthOption oAuthOption)
    {
        authenticationBuilder
                    .AddJwtBearer(oAuthOption.TokenType.ToString(), o =>
                    {
                        o.Authority = oAuthOption.Authority;
                        o.Audience = oAuthOption.Audience;
                        o.RequireHttpsMetadata = oAuthOption.RequireHttpsMetadata;
                        if (oAuthOption.IgnoreSSL)
                            o.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };

                        o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateAudience = oAuthOption.ValidateAudience,
                            ValidateIssuer = oAuthOption.ValidateIssuer,
                            ValidateIssuerSigningKey = oAuthOption.ValidateIssuerSigningKey
                        };
                    });

        return authenticationBuilder;
    }
}
