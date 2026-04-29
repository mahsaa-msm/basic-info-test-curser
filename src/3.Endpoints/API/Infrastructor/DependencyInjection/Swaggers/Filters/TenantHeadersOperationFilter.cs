using Vehicle.Insurance.Core.Resources;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Vehicle.Insurance.Endpoints.API.Infrastructor.DependencyInjection.Swaggers.Filters;

public class TenantHeadersOperationFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        foreach (var path in swaggerDoc.Paths.Values)
        {
            foreach (var operation in path.Operations.Values)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = ProjectConsts.TENANT_ID_X_HEADER_NAME,
                    In = ParameterLocation.Header,
                    Description = "شناسه تننت (عددی)",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                        Format = "int64"
                    }
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = ProjectConsts.TENANT_ID_HEADER_NAME,
                    In = ParameterLocation.Header,
                    Description = "شناسه تننت (عددی)",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "integer",
                        Format = "int64"
                    }
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = ProjectConsts.TENANT_KEY_X_HEADER_NAME,
                    In = ParameterLocation.Header,
                    Description = "شناسه تننت (Guid)",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    }
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = ProjectConsts.TENANT_KEY_HEADER_NAME,
                    In = ParameterLocation.Header,
                    Description = "شناسه تننت (Guid)",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                    }
                });
            }
        }
    }
}

