using ATS.MVP.Api.Middlewares.Exceptions.Models;
using ATS.MVP.Domain.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ATS.MVP.Api.Middlewares.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly IHostEnvironment _hostEnvironment;

    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(IHostEnvironment hostEnvironment, ILogger<GlobalExceptionHandler> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        var response = new ApiErrorResponse(HttpStatusCode.InternalServerError);

        if (exception is DomainException)
        {
            var domainException = (DomainException) exception;

            httpContext.Response.StatusCode = domainException.StatusCode;

            response = new ApiErrorResponse(domainException.StatusCode);

            response.AddError(domainException.Message);
        }
        else
        {
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            if (_hostEnvironment.IsProduction())
            {
                response.AddError(CommonErrorMessages.UnhandledException);
            }
            else
            {
                var errorMessage = string.Join(' ', exception?.Message, exception?.InnerException?.Message).TrimEnd();

                response.AddError(string.IsNullOrWhiteSpace(errorMessage) ? CommonErrorMessages.UnhandledException : errorMessage);
            }
        }

        _logger.LogError(exception, exception?.Message);

        httpContext.Response.WriteAsJsonAsync(response).Wait();

        return ValueTask.FromResult(true);
    }
}
