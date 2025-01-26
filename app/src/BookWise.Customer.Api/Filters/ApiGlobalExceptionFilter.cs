using BookWise.Customer.Application.Exceptions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookWise.Customer.Api.Middlewares;

public class ApiGlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var details = new ProblemDetails();
        var exception = context.Exception;
        
        if (exception is FluentValidation.ValidationException validationException)
        {
            details.Title = "Validation error";
            details.Status = StatusCodes.Status400BadRequest;
            details.Type = "ValidationError";
            details.Detail = "One or more validation errors occurred.";
            details.Extensions["errors"] = GetValidationErrors(validationException.Errors);
        }
        else if (exception is NotFoundException)
        {
            details.Title = "Not found";
            details.Status = StatusCodes.Status404NotFound;
            details.Type = "NotFound";
            details.Detail = exception.Message;
        }
        else
        {
            details.Title = "An unexpected error occurred";
            details.Status = StatusCodes.Status422UnprocessableEntity;
            details.Type = "Unexpected";
            details.Detail = exception.Message;
        }

        context.HttpContext.Response.StatusCode = (int)details.Status;
        context.Result = new ObjectResult(details);
        context.ExceptionHandled = true;
    }
    
    private IDictionary<string, string[]> GetValidationErrors(IEnumerable<ValidationFailure> errors)
    {
        return errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group.Select(e => e.ErrorMessage).ToArray()
            );
    }
}