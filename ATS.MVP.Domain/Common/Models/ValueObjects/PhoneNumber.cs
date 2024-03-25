using ATS.MVP.Domain.Common.Errors;
using System.Text.RegularExpressions;

namespace ATS.MVP.Domain.Common.Models.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public const int MinLength = 12;
    public const int MaxLength = 13;
    public const int CountryCodeLength = 2;
    public const int AreaNumberLength = 2;

    public string CountryCode { get; }
    public string AreaNumber { get; }
    public string Number { get; }

    private PhoneNumber(string countryCode, string areaCode, string number)
    {
        if (!IsValid(countryCode, areaCode, number))
        {
            throw new DomainException(CommonErrorMessages.InvalidPhoneNumber);
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
        var unmaskedPhoneNumber = IsMasked(phoneNumber) ? RemoveMask(phoneNumber) : phoneNumber;

        if (!IsUnmaskedPhoneNumber(unmaskedPhoneNumber))
        {
            throw new DomainException(CommonErrorMessages.InvalidPhoneNumber);
        }

        var countryCode = unmaskedPhoneNumber.Substring(0, 2);

        var areaCode = unmaskedPhoneNumber.Substring(2, 2);

        var number = unmaskedPhoneNumber.Substring(4);

        return (countryCode, areaCode, number);
    }

    private static string RemoveMask(string phoneNumber)
    {
        return Regex.Replace(phoneNumber, @"[\+\-\s\(\)]", "");
    }

    private static bool IsMasked(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+\d{2} \(\d{2}\) \d{4,5}-\d{4}$");
    }

    private static bool IsUnmaskedPhoneNumber(string phoneNumber)
    {
        var pattern = @"^\d{" + MinLength + "," + MaxLength + "}$";

        return Regex.IsMatch(phoneNumber, pattern);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryCode;
        yield return AreaNumber;
        yield return Number;
    }

    public string Normalized() => CountryCode + AreaNumber + Number;

    public override string ToString()
    {
        var numberIndexToSplitAt = (int) Math.Ceiling((double) Number.Length / 2);

        return $"+{CountryCode} ({AreaNumber}) {Number[..numberIndexToSplitAt]}-{Number[numberIndexToSplitAt..]}";
    }

    private static bool IsValid(string countryCode, string areaCode, string number)
    {
        const int numberMinSize = MinLength - CountryCodeLength - AreaNumberLength;

        const int numberMaxSize = MaxLength - CountryCodeLength - AreaNumberLength;

        return countryCode.Length == 2 &&
               areaCode.Length == 2 &&
               (number.Length == numberMinSize || number.Length == numberMaxSize);
    }
}
