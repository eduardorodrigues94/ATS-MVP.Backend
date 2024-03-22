using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models;

namespace ATS.MVP.Domain.Common.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string CountryCode { get; }
    public string AreaNumber { get; }
    public string Number { get; }

    private PhoneNumber(string countryCode, string areaCode, string number)
    {
        if (!IsValid(countryCode, areaCode, number))
        {
            throw new DomainException(CommomErrorMessages.InvalidPhoneNumber);
        }

        CountryCode = countryCode;
        AreaNumber = areaCode;
        Number = number;
    }

    public static PhoneNumber Create(string phoneNumber)
    {
        var (countryCode, areaCode, number) = ParsePhoneNumber(phoneNumber);

        return new PhoneNumber(countryCode, areaCode, number);
    }

    public static (string, string, string) ParsePhoneNumber(string phoneNumber)
    {
        var numericPhoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

        var countryCode = numericPhoneNumber.Substring(0, 2);
        var areaCode = numericPhoneNumber.Substring(2, 2);
        var number = numericPhoneNumber.Substring(4);

        return (countryCode, areaCode, number);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return AreaNumber;
        yield return Number;
    }

    public string Normalized() => $"{CountryCode}{AreaNumber}{Number}";

    public override string ToString()
    {
        var numberIndexToSplitAt = (int) Math.Ceiling((double) Number.Length / 2);

        return $"+{CountryCode} ({AreaNumber}) {Number[..numberIndexToSplitAt]}-{Number[numberIndexToSplitAt..]}";
    }

    private static bool IsValid(string countryCode, string areaCode, string number)
    {
        return countryCode.Length == 2 &&
               areaCode.Length == 2 &&
               (number.Length == 8 || number.Length == 9);
    }
}
