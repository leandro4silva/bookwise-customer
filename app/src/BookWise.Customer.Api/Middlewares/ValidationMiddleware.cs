using System.Text.Json;
using FluentValidation;

namespace BookWise.Customer.Api.Middlewares;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationMiddleware> _logger;

    public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException ex)
        {
            _logger.LogError($"Validation failed: {ex.Message}");
            
            var errors = ex.Errors.Select(e =>  e.ErrorMessage).First();
                
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                Errors = errors
            };

            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}