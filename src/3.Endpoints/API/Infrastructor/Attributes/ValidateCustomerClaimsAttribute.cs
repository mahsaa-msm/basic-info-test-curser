using Master.Data.Core.Contracts.Common.Options;
using Master.Data.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zamin.Utilities.Extensions;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ValidateCustomerClaimsAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var _softwareManagementOption = context.HttpContext.RequestServices.GetService<SoftwareManagementOption>();

        var customerIdClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == (!string.IsNullOrEmpty(_softwareManagementOption?.CustomerIdClaimName) ?
                                                                                                _softwareManagementOption?.CustomerIdClaimName :
                                                                                                ProjectConsts.USER_CUSTOMER_ID_CLAIM_NAME));

        var customerNationalcodeClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ProjectConsts.NATIONAL_CODE_CLAIM_NAME);

        var customerTypecodeClaim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ProjectConsts.CUSTOMER_TYPE_CLAIM_NAME);

        if (customerIdClaim is not null && long.TryParse(customerIdClaim.Value, out long customerId) && customerId > 0 &&
            customerNationalcodeClaim is not null && customerTypecodeClaim is not null &&
            byte.TryParse(customerTypecodeClaim.Value, out byte customerType) &&
            (
                customerType == (byte)CustomerType.PERSON && customerNationalcodeClaim.Value.IsNationalCode() ||
                customerType == (byte)CustomerType.COMPANY && customerNationalcodeClaim.Value.Length == ProjectConsts.ECONOMIC_CODE_LENGTH && customerNationalcodeClaim.Value.IsNumeric()
            ))
            return;

        context.Result = new UnauthorizedResult();
    }
}
