namespace Master.Data.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class IgnoreTenantHeaderValidationAttribute : Attribute
{
}