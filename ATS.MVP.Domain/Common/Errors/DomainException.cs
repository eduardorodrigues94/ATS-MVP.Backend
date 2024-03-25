namespace ATS.MVP.Domain.Common.Errors;

// Em um sistema real, se eu tivesse mais tempo provavelmente iria criar algum sistema com código de erros
// Com mensagens com localização, etc
public class DomainException : Exception
{
    private const int _badRequest = 400;

    public int StatusCode { get; init; }

    public DomainException(int? statusCode = _badRequest) : base()
    {
        StatusCode = statusCode ?? _badRequest;
    }

    public DomainException(string? message, int? statusCode = _badRequest) : base(message)
    {
        StatusCode = statusCode ?? _badRequest;
    }

    public DomainException(string? message, Exception? innerException, int? statusCode = _badRequest) : base(message, innerException)
    {
        StatusCode = statusCode ?? _badRequest;
    }

    private DomainException() : base()
    {
    }

    private DomainException(string? message) : base(message)
    {
    }

    private DomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
