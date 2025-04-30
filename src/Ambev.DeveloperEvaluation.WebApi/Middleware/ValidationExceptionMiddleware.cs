using Ambev.DeveloperEvaluation.Common.Exceptions;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, ex.Message); //trocar depois para erro generico e não mostrar a stack
            }
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }

        private static async Task HandleExceptionAsync(HttpContext context, int statusCode, object errorDetails)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = statusCode,
                Errors = errorDetails,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}
