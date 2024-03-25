using FluentAssertions;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;

namespace ATS.MVP.Tests.Common.Models.ValueObjects;
public class EmailTests
{
    [Theory]
    [InlineData("john.doe@example.com")]
    [InlineData("jane@example.co.uk")]
    [InlineData("user1234@test-domain.info")]
    public void Create_ValidEmail_ShouldNotThrowException(string email)
    {
        // Arrange & Act
        Action action = () => Email.Create(email);

        // Assert
        action.Should().NotThrow<DomainException>();
    }

    [Theory]
    [InlineData("invalid-email")]
    [InlineData("john.doe@example")]
    [InlineData("example.com")]
    [InlineData("user@.com")]
    public void Create_InvalidEmail_ShouldThrowException(string email)
    {
        // Arrange & Act
        Action action = () => Email.Create(email);

        // Assert
        action.Should().Throw<DomainException>()
            .WithMessage(CommonErrorMessages.InvalidEmail);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        // Arrange
        var emailValue = "john.doe@example.com";
        var email = Email.Create(emailValue);

        // Act
        var result = email.ToString();

        // Assert
        result.Should().Be(emailValue);
    }

    [Fact]
    public void GetEqualityComponents_ReturnsValue()
    {
        // Arrange
        var emailValue = "john.doe@example.com";
        var email = Email.Create(emailValue);

        // Act
        var result = email.GetEqualityComponents();

        // Assert
        result.Should().ContainSingle()
            .Which.Should().Be(emailValue);
    }
}
