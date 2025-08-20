using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Master.Data.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ValidateBackofficeSuperAdminAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var _softwareManagementOption = context.HttpContext.RequestServices.GetService<SoftwareManagementOption>();

        var superAdminClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == (!string.IsNullOrEmpty(_softwareManagementOption?.BackofficeSuperAdminClaimName) ?
                                                                                             _softwareManagementOption?.BackofficeSuperAdminClaimName :
                                                                                             ProjectConsts.BACKOFFICE_SUPER_ADMIN_CLAIM_NAME));

        var superAdminClaimValue = !string.IsNullOrEmpty(_softwareManagementOption?.BackofficeSuperAdminClaimValue) ?
                                       _softwareManagementOption?.BackofficeSuperAdminClaimValue :
                                       ProjectConsts.BACKOFFICE_SUPER_ADMIN_CLAIM_VALUE;

        if (string.IsNullOrEmpty(superAdminClaim?.ToString()) && superAdminClaim?.ToString() == superAdminClaimValue)
            return;

        context.Result = new ForbidResult();
    }
}
