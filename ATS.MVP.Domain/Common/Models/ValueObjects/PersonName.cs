﻿using ATS.MVP.Domain.Common.Errors;
using System.Text.RegularExpressions;

namespace ATS.MVP.Domain.Common.Models.ValueObjects;

public class PersonName : ValueObject
{
    public const string pattern = "^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$";

    public string Value { get; }

    private PersonName(string value)
    {
        if (value is null || !Regex.IsMatch(value, pattern))
        {
            throw new DomainException(CommonErrorMessages.InvalidName);
        }

        Value = value;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static PersonName Create(string value)
    {
        return new PersonName(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
