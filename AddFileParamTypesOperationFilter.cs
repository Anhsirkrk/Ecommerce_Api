using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Api
{
    public class AddFileParamTypesOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var attributes = context.MethodInfo.GetCustomAttributes(true);

            foreach (var attribute in attributes)
            {
                if (attribute is FromFormAttribute)
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "file",
                        In = ParameterLocation.Query,
                        Description = "Upload File",
                        Required = true,
                        Schema = new OpenApiSchema
                        {
                            Type = "IFormFile"
                        }
                    });
                }
            }
        }
    }
}
