using ATS.MVP.Api.Middlewares.Exceptions.Models;
using FluentAssertions;

namespace ATS.MVP.Tests.Errors.Middlewares;
public class ApiErrorDetailsTests
{
    [Fact]
    public void ApiErrorDetails_Message_Should_Be_Set()
    {
        // Arrange
        var message = "Test message";

        // Act
        var apiErrorDetails = new ApiErrorDetails(message);

        // Assert
        apiErrorDetails.Message.Should().Be(message);
    }
}
