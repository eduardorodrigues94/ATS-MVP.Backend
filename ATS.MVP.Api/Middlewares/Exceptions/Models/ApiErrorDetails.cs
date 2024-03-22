namespace ATS.MVP.Api.Middlewares.Exceptions.Models;

internal class ApiErrorDetails(string message)
{
    public string Message { get; } = message;
}
