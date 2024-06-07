using FluentValidation;
using Serilog;
using System.Net;
using System.Text.Json;

namespace candidatehub.Web.Middleware
{
    public class GlobalErrorHandler : IMiddleware
    {
        public GlobalErrorHandler()
        {
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException e)
            { 
                await HandleValidationExceptionAsync(context, e);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errors = validationException.Errors
        .Select(error => new { fieldName = error.PropertyName, message = error.ErrorMessage })
        .ToList();
            string json = JsonSerializer.Serialize(errors);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorDetails = new
            {
                statusCode = (int)HttpStatusCode.InternalServerError,
                type = "Error",
                title = "Server Error",
                message = "Error Occured, Please Contact Administrator"
            };
            string json = JsonSerializer.Serialize(errorDetails);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
