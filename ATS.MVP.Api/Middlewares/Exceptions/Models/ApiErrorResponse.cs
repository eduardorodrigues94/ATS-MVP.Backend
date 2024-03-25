using System.Net;

namespace ATS.MVP.Api.Middlewares.Exceptions.Models;

public class ApiErrorResponse
{
    public int StatusCode { get; }


    private readonly List<ApiErrorDetails> _errors;

    public IReadOnlyList<ApiErrorDetails> Errors { get => _errors; }

    public ApiErrorResponse(int httpStatusCode)
    {
        StatusCode = httpStatusCode;

        _errors = [];
    }

    public ApiErrorResponse(HttpStatusCode httpStatusCode)
    {
        StatusCode = (int) httpStatusCode;

        _errors = [];
    }

    public void AddError(string message)
    {
        _errors.Add(new ApiErrorDetails(message));
    }

    public void AddError(IEnumerable<string> messages)
    {
        _errors.AddRange(messages.Select(x => new ApiErrorDetails(x)));
    }
}

