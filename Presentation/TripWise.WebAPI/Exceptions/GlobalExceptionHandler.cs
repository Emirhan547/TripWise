using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TripWise.WebAPI.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,Exception exception,CancellationToken cancellationToken)
    {
        if (exception is ValidationException validationException)
        {
            var errors = validationException.Errors.GroupBy(x => x.PropertyName) .ToDictionary(x => x.Key,x => x.Select(e => e.ErrorMessage).ToArray());

            var problemDetails = new ValidationProblemDetails(errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validasyon hatası oluştu."
            };

            httpContext.Response.StatusCode =StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
            return true;
        }

        logger.LogError(exception,"Beklenmeyen bir hata oluştu.");

        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Beklenmeyen bir hata oluştu."
        };

        httpContext.Response.StatusCode =StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problem,cancellationToken);
        return true;
    }
}
