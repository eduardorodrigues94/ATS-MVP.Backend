namespace ATS.MVP.Api.Middlewares.Exceptions.Models;

public class ApiErrorDetails(string message)
{
    public string Message { get; } = message;
}
