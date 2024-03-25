using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using FluentAssertions;

namespace ATS.MVP.Tests.Common.Models.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+12 (34) 5678-9011", "12", "34", "56789011")]
    [InlineData("+12 (34) 5678-9012", "12", "34", "56789012")]
    [InlineData("123456789012", "12", "34", "56789012")]
    public void Create_WithValidPhoneNumber_ReturnsPhoneNumber(string phoneNumber, string expectedCountryCode, string expectedAreaCode, string expectedNumber)
    {
        // Act
        var result = PhoneNumber.Create(phoneNumber);

        // Assert
        result.CountryCode.Should().Be(expectedCountryCode);
        result.AreaNumber.Should().Be(expectedAreaCode);
        result.Number.Should().Be(expectedNumber);
    }

    [Theory]
    [InlineData("+12 (34) 12345-123")] //Máscara / Tamanho abaixo
    [InlineData("+12 (34) 12345-12345")] // Máscara / Tamanho acima
    [InlineData("+12 (34) 123a-1234")] // Caracteres 
    [InlineData("+12 (34) 123a5-12b4")] // Caracteres 
    [InlineData("+12 34 12345-6789")] // Máscara errada
    [InlineData("+12 (34) 123456789")] // Máscara errada
    [InlineData("+12 34 12345-6789")] // Máscara errada
    public void Create_WithInvalidPhoneNumber_ThrowsDomainException(string invalidPhoneNumber)
    {
        // Act
        Action action = () => PhoneNumber.Create(invalidPhoneNumber);

        // Assert
        action.Should().Throw<DomainException>().WithMessage(CommonErrorMessages.InvalidPhoneNumber);
    }

    [Theory]
    [InlineData("+12 (34) 1234-5678", "12", "34", "12345678")]
    [InlineData("+12 (34) 12345-6789", "12", "34", "123456789")]
    [InlineData("123412345678", "12", "34", "12345678")]
    [InlineData("1234123456789", "12", "34", "123456789")]
    public void ParsePhoneNumber_WithValidPhoneNumber_ReturnsTupleWithCountryAreaAndNumber(string phoneNumber, string expectedCountryCode, string expectedAreaCode, string expectedNumber)
    {
        // Act
        var (countryCode, areaCode, number) = PhoneNumber.ParsePhoneNumber(phoneNumber);

        // Assert
        countryCode.Should().Be(expectedCountryCode);
        areaCode.Should().Be(expectedAreaCode);
        number.Should().Be(expectedNumber);
    }

    [Theory]
    [InlineData("+12 (34) 99999-8888", "12", "34", "99999", "8888")]
    [InlineData("1234999998888", "12", "34", "99999", "8888")]
    [InlineData("+12 (34) 9999-8888", "12", "34", "9999", "8888")]
    [InlineData("123499998888", "12", "34", "9999", "8888")]
    public void ToString_WhenCalled_ReturnsFormattedPhoneNumber(string phoneNumber, string expectedCountryCode, string expectedAreaCode, string expectedNumberLeftHalf, string expectedNumberRightHalf)
    {
        // Arrange
        var phone = PhoneNumber.Create(phoneNumber);

        // Act
        var result = phone.ToString();


        // Assert
        result.Should().Be($"+{expectedCountryCode} ({expectedAreaCode}) {expectedNumberLeftHalf}-{expectedNumberRightHalf}");
    }

    [Theory]
    [InlineData("1234567890")]
    [InlineData("123456789012345")]
    [InlineData("12345abc6789")]
    [InlineData("+12 (34) 11112222")]
    [InlineData("+12 34 11112222")]
    [InlineData("12 34 11112222")]
    [InlineData("12 (34) 11112222")]
    [InlineData("12 (34) 1111-2222")]
    public void ParsePhoneNumber_WithInvalidPhoneNumber_ThrowsDomainException(string invalidPhoneNumber)
    {
        // Act
        Action action = () => PhoneNumber.ParsePhoneNumber(invalidPhoneNumber);

        // Assert
        action.Should().Throw<DomainException>().WithMessage(expectedWildcardPattern: CommonErrorMessages.InvalidPhoneNumber);
    }
}
