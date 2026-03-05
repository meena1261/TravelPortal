using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Travel.API.Helpers.Infrastructure.Swagger
{
    public class DefaultResponseOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Responses.ContainsKey("default"))
            {
                operation.Responses.Add("default", new OpenApiResponse
                {
                    Description = "Unexpected error",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(typeof(JsonResponse), context.SchemaRepository)
                        }
                    }
                });
            }
        }
    }
}
