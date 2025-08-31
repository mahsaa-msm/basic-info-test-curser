using Master.Data.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Master.Data.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ValidateTenantHeaderAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {

        //TODO check tenant id 
        //if (!context.HttpContext.Request.Headers.TryGetValue(ProjectConsts.TENANT_ID_X_HEADER_NAME, out var tenantId))
        //{
        //    if (!context.HttpContext.Request.Headers.TryGetValue(ProjectConsts.TENANT_ID_HEADER_NAME, out tenantId))
        //    {
        //        if (!context.HttpContext.Request.Headers.TryGetValue(ProjectConsts.TENANT_KEY_X_HEADER_NAME, out var tenantKey))
        //        {
        //            if (!context.HttpContext.Request.Headers.TryGetValue(ProjectConsts.TENANT_KEY_HEADER_NAME, out tenantKey))
        //                context.Result = new ForbidResult();
        //        }
        //    }
        //}
        return;
    }
}
