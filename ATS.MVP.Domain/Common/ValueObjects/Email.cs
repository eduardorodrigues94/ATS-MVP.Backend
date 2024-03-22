using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models;
using System.Text.RegularExpressions;

namespace ATS.MVP.Domain.Common.ValueObjects;

public sealed class Email : ValueObject
{
    private const string _pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

    public string Value { get; }

    private Email(string value)
    {
        if (value is null || !Regex.IsMatch(value, _pattern))
        {
            throw new DomainException(CommomErrorMessages.InvalidEmail);
        }

        Value = value;
    }

    public static Email Create(string value) => new Email(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
