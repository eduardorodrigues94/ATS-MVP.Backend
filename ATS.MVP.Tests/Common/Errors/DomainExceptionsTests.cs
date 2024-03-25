using ATS.MVP.Domain.Common.Errors;
using FluentAssertions;

namespace ATS.MVP.Tests.Common.Errors;
public class DomainExceptionTests
{
    [Fact]
    public void Constructor_WithoutParameters_ShouldSetStatusCodeToBadRequest()
    {
        // Act
        var exception = new DomainException();

        // Assert
        exception.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Constructor_WithMessage_ShouldSetMessageAndStatusCodeToBadRequest()
    {
        // Arrange
        var message = "Example message";

        // Act
        var exception = new DomainException(message);

        // Assert
        exception.Message.Should().Be(message);
        exception.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Constructor_WithMessageAndStatusCode_ShouldSetMessageAndStatusCode()
    {
        // Arrange
        var message = "Example message";
        var statusCode = 404;

        // Act
        var exception = new DomainException(message, statusCode);

        // Assert
        exception.Message.Should().Be(message);
        exception.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public void Constructor_WithInnerException_ShouldSetInnerExceptionAndStatusCodeToBadRequest()
    {
        // Arrange
        var innerException = new Exception("Inner exception");

        // Act
        var exception = new DomainException("Example message", innerException);

        // Assert
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Constructor_WithMessageAndInnerExceptionAndStatusCode_ShouldSetMessageAndInnerExceptionAndStatusCode()
    {
        // Arrange
        var message = "Example message";
        var innerException = new Exception("Inner exception");
        var statusCode = 404;

        // Act
        var exception = new DomainException(message, innerException, statusCode);

        // Assert
        exception.Message.Should().Be(message);
        exception.InnerException.Should().Be(innerException);
        exception.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public void Constructor_WithNullStatusCode_ShouldSetStatusCodeToBadRequest()
    {
        // Act
        var exception = new DomainException(null);

        // Assert
        exception.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Constructor_WithNullMessageAndNullStatusCode_ShouldSetStatusCodeToBadRequest()
    {
        // Act
        var exception = new DomainException(null, null);

        // Assert
        exception.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Constructor_WithNullMessageAndStatusCode_ShouldSetStatusCode()
    {
        // Arrange
        var statusCode = 404;

        // Act
        var exception = new DomainException(null, statusCode);

        // Assert
        exception.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public void Constructor_WithNullMessageAndInnerExceptionAndNullStatusCode_ShouldSetStatusCodeToBadRequest()
    {
        // Arrange
        var innerException = new Exception("Inner exception");

        // Act
        var exception = new DomainException(null, innerException, null);

        // Assert
        exception.StatusCode.Should().Be(400);
    }
}
