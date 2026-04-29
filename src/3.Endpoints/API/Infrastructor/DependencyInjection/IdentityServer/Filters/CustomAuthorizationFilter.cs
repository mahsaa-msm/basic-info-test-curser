using Vehicle.Insurance.Core.Contracts.PodSsoApis.UserInfo;
using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Filters;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    private readonly IModernUserInfoService _userInfoService;
    private readonly OAuthOption _oAuthOption;
    public CustomAuthorizationFilter(IModernUserInfoService userInfoService,
                                     OAuthOption oAuthOption)
    {
        _userInfoService = userInfoService;
        _oAuthOption = oAuthOption;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any())
            return;

        if (context.HttpContext.Items.TryGetValue(ProjectConsts.FAKE_AUTHENTICATION_ITEM_NAME, out var isFake) && (bool)isFake)
            return;

        if (!IsForcedToAuthorize(context) && !_oAuthOption.AuthorizationConfigs.Enabled)
            return;

        var actionName = context.HttpContext.Request.RouteValues["action"].ToString();
        var action = $"Action_{actionName}";

        if (!_userInfoService.HasAccess(action))
            context.Result = new ForbidResult();
    }

    private bool IsForcedToAuthorize(AuthorizationFilterContext context)
    {
        var isForced = false;
        foreach (var headerName in _oAuthOption.AuthorizationConfigs.HeaderNamesToForceAuthorize)
        {
            isForced = isForced || context.HttpContext.Request.Headers.ContainsKey(headerName);
        }
        return isForced;
    }
}

