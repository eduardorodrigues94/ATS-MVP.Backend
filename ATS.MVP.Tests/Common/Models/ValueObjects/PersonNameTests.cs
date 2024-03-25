using ATS.MVP.Domain.Common.Errors;
using FluentAssertions;
using ATS.MVP.Domain.Common.Models.ValueObjects;

namespace ATS.MVP.Tests.Common.Models.ValueObjects;

public class PersonNameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("!@#")]
    public void Create_InvalidName_ThrowsDomainException(string invalidName)
    {
        // Act
        Action action = () => PersonName.Create(invalidName);

        // Assert
        action.Should().Throw<DomainException>().WithMessage(CommonErrorMessages.InvalidName);
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Doe")]
    [InlineData("John Doe")]
    [InlineData("John-Doe")]
    [InlineData("John.Doe")]
    [InlineData("John, Doe")]
    public void Create_ValidName_ReturnsPersonName(string validName)
    {
        // Act
        var personName = PersonName.Create(validName);

        // Assert
        personName.Value.Should().Be(validName);
    }

    [Theory]
    [InlineData("John", "John", true)]
    [InlineData("John", "Doe", false)]
    [InlineData("John Doe", "John Doe", true)]
    [InlineData("John Doe", "John", false)]
    public void Equals_WhenCalled_ReturnsExpectedResult(string name1, string name2, bool expectedResult)
    {
        // Arrange
        var personName1 = PersonName.Create(name1);
        var personName2 = PersonName.Create(name2);

        // Act
        var result = personName1.Equals(personName2);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("John", "John", true)]
    [InlineData("John", "Doe", false)]
    [InlineData("John Doe", "John Doe", true)]
    [InlineData("John Doe", "John", false)]
    public void OperatorEquals_WhenCalled_ReturnsExpectedResult(string name1, string name2, bool expectedResult)
    {
        // Arrange
        var personName1 = PersonName.Create(name1);
        var personName2 = PersonName.Create(name2);

        // Act
        var result = personName1 == personName2;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("John", "John", false)]
    [InlineData("John", "Doe", true)]
    [InlineData("John Doe", "John Doe", false)]
    [InlineData("John Doe", "John", true)]
    public void OperatorNotEquals_WhenCalled_ReturnsExpectedResult(string name1, string name2, bool expectedResult)
    {
        // Arrange
        var personName1 = PersonName.Create(name1);
        var personName2 = PersonName.Create(name2);

        // Act
        var result = personName1 != personName2;

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Doe")]
    [InlineData("John Doe")]
    [InlineData("John-Doe")]
    [InlineData("John.Doe")]
    [InlineData("John, Doe")]
    public void ToString_WhenCalled_ReturnsValue(string name)
    {
        // Arrange
        var personName = PersonName.Create(name);

        // Act
        var result = personName.ToString();

        // Assert
        result.Should().Be(name);
    }
}
