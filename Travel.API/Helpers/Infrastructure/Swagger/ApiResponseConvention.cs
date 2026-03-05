using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Travel.API.Helpers.Infrastructure.Swagger
{
    public class ApiResponseConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    // Skip if already defined manually
                    if (action.Filters.OfType<ProducesResponseTypeAttribute>().Any())
                        continue;

                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(JsonResponse), StatusCodes.Status400BadRequest));
                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(JsonResponse), StatusCodes.Status401Unauthorized));
                    action.Filters.Add(new ProducesResponseTypeAttribute(typeof(JsonResponse), StatusCodes.Status500InternalServerError));
                }
            }
        }
    }
}
