using IdentityModel.AspNetCore.OAuth2Introspection;
using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Security.Claims;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer;

public static class ReferenceTokenExtensions
{
    public static AuthenticationBuilder AddReferenceTokenSupoort(this AuthenticationBuilder authenticationBuilder,
                                                                 OAuthOption oAuthOption)
    {
        if (string.IsNullOrWhiteSpace(oAuthOption.RefrenceTokenConfig.ClientId) ||
            string.IsNullOrWhiteSpace(oAuthOption.RefrenceTokenConfig.ClientSecret))
            throw new ArgumentNullException($"{oAuthOption.TokenType.ToString()} {oAuthOption.Authority} , ClientId or ClientSecret is null or white space or empty");

        authenticationBuilder.AddOAuth2Introspection(oAuthOption.TokenType.ToString(), o =>
        {
            o.Authority = oAuthOption.Authority;

            if (!string.IsNullOrEmpty(oAuthOption.EndpointsPath?.IntrospectionEndpoint))
            {
                o.IntrospectionEndpoint = $"{oAuthOption.Authority}{oAuthOption.EndpointsPath.IntrospectionEndpoint}";
            }

            o.ClientId = oAuthOption.RefrenceTokenConfig.ClientId;
            o.ClientSecret = oAuthOption.RefrenceTokenConfig.ClientSecret;

            o.ForwardDefaultSelector = context =>
            {
                if (context.Request.HttpContext.Items.TryGetValue(ProjectConsts.FAKE_AUTHENTICATION_ITEM_NAME, out var isFake) && (bool)isFake)
                    return null;

                var actionDescriptor = context.Request.HttpContext.GetEndpoint()?.Metadata?.GetMetadata<ControllerActionDescriptor>();
                if (actionDescriptor is not null && actionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
                    return null;
                else
                    return oAuthOption.TokenType.ToString();
            };

            o.Events = new OAuth2IntrospectionEvents()
            {
                OnTokenValidated = async (context) =>
                {
                    if (context.HttpContext.Items.TryGetValue(ProjectConsts.FAKE_AUTHENTICATION_ITEM_NAME, out var isFake) && (bool)isFake)
                        return;

                    if (context.Principal is null)
                        throw new ArgumentNullException($"{oAuthOption.TokenType.ToString()} ({oAuthOption.Authority}) , principal is null");

                    if (oAuthOption.RegisterUserInfoClaims.Enabled && context.Principal.HasSubClaim(oAuthOption.UserIdentifierClaimType))
                    {
                        List<Claim> claims = await oAuthOption.GetUserInfoClaims(context.HttpContext, "ProviderHttpClient");
                        context.Principal.AddIdentity(context.Principal.CreateClaimsIdentity(claims));
                    }

                    if (oAuthOption.UserClaimRules.Count != 0)
                    {
                        context.Principal = context.Principal.ClonePrincipalWithConvertedClaims(oAuthOption);
                    }
                },
                OnAuthenticationFailed = async context =>
                {
                    if (context.HttpContext.Items.TryGetValue(ProjectConsts.FAKE_AUTHENTICATION_ITEM_NAME, out var isFake) && (bool)isFake)
                        return;

                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid Token");
                }
            };
        });

        return authenticationBuilder;
    }
}