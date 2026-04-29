using Vehicle.Insurance.Core.Contracts.Common.Options;
using Vehicle.Insurance.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ValidateBackofficeSuperAdminAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // بررسی وجود ویژگی IgnoreBackofficeSuperAdminValidation روی اکشن
        var hasIgnoreAttribute = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(IgnoreBackofficeSuperAdminValidationAttribute));

        if (hasIgnoreAttribute)
        {
            return;
        }

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

