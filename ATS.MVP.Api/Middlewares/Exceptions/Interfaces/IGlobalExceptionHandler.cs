namespace ATS.MVP.Api.Middlewares.Exceptions.Interfaces;

internal interface IGlobalExceptionHandler
{
    ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);
}
