using FluentValidation;
using System.Text.Json;

namespace WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await CatchException(context, ex);
            }
        }

        private static Task CatchException(HttpContext context, Exception ex)
        {
            var result = string.Empty;
            int statusCode = StatusCodes.Status500InternalServerError;

            if (ex is ValidationException)
            {
                var validation = ex as ValidationException;
                result = JsonSerializer.Serialize(new { validation!.Errors });
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                result = JsonSerializer.Serialize(new { ex.Message }); ;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}