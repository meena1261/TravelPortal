using System.Text.Json;
using Travel.API.Helpers;

namespace Travel.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteResponse(context, ex.Message, StatusCodes.Status500InternalServerError);
            }
        }

        private static async Task WriteResponse(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new JsonResponse<object>
            {
                Status = 0,
                Message = message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
