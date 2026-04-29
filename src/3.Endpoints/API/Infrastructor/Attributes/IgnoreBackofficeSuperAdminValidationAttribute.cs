namespace Vehicle.Insurance.Endpoints.API.Infrastructor.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class IgnoreBackofficeSuperAdminValidationAttribute : Attribute
{
}

