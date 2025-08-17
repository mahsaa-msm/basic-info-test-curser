using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Options;
using System.Security.Claims;

namespace Master.Data.Endpoints.API.Infrastructor.DependencyInjection.IdentityServer.Middlewares;

public class FakeAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SoftwareManagementOption _softwareManagementOption;
    private readonly OAuthOption _oAuthOption;
    private readonly IHostEnvironment _environment;

    public FakeAuthenticationMiddleware(RequestDelegate next,
        SoftwareManagementOption softwareManagementOption,
                                      OAuthOption oAuthOption,
                                      IHostEnvironment environment)
    {
        _next = next;
        _softwareManagementOption = softwareManagementOption;
        _oAuthOption = oAuthOption;
        _environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        var fakeOuathOption = _oAuthOption.FakeAuthOption;

        if (fakeOuathOption is not null &&
        fakeOuathOption.Enabled &&
        _environment.IsDevelopment())
        {

            if (!string.IsNullOrEmpty(fakeOuathOption.FakeToken) &&
                context.Request.Headers.TryGetValue("Authorization", out var tokenValue) &&
                !string.IsNullOrWhiteSpace(tokenValue) &&
                tokenValue.Contains(fakeOuathOption.FakeToken))
            {
                // Mark the request as fake authenticated
                context.Items[ProjectConsts.FAKE_AUTHENTICATION_ITEM_NAME] = true;

                var identity = new ClaimsIdentity("FakeScheme");

                identity.AddClaim(new Claim(_softwareManagementOption.CustomerIdClaimName, fakeOuathOption.CustomerId));
                identity.AddClaim(new Claim(ProjectConsts.NATIONAL_CODE_CLAIM_NAME, fakeOuathOption.NationalCodeClaim));
                identity.AddClaim(new Claim(ProjectConsts.ZAMIN_NATIONAL_CODE_CLAIM_NAME, fakeOuathOption.ZaminNationalCodeClaim));
                identity.AddClaim(new Claim(ProjectConsts.CUSTOMER_TYPE_CLAIM_NAME, fakeOuathOption.CustomerTypeClaim));

                context.User = new ClaimsPrincipal(identity);
            }
        }

        await _next(context);
    }
}