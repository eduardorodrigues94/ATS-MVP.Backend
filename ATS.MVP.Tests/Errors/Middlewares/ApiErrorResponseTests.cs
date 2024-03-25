using ATS.MVP.Api.Middlewares.Exceptions.Models;
using Bogus;
using FluentAssertions;

namespace ATS.MVP.Tests.Errors.Middlewares;

public class ApiErrorResponseTests
{
    [Fact]
    public void ApiErrorResponse_StatusCode_Should_Be_Set()
    {
        // Arrange
        var statusCode = 404;

        // Act
        var apiErrorResponse = new ApiErrorResponse(statusCode);

        // Assert
        apiErrorResponse.StatusCode.Should().Be(statusCode);
    }

    [Fact]
    public void ApiErrorResponse_StatusCode_Should_Be_Set_From_HttpStatusCode()
    {
        // Arrange
        var httpStatusCode = System.Net.HttpStatusCode.NotFound;

        // Act
        var apiErrorResponse = new ApiErrorResponse(httpStatusCode);

        // Assert
        apiErrorResponse.StatusCode.Should().Be((int) httpStatusCode);
    }

    [Fact]
    public void ApiErrorResponse_Errors_Should_Be_Empty_By_Default()
    {
        // Arrange & Act
        var apiErrorResponse = new ApiErrorResponse(500);

        // Assert
        apiErrorResponse.Errors.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void ApiErrorResponse_AddError_Should_Add_Single_Error()
    {
        // Arrange
        var apiErrorResponse = new ApiErrorResponse(400);
        var errorMessage = "Test error message";

        // Act
        apiErrorResponse.AddError(errorMessage);

        // Assert
        apiErrorResponse.Errors.Should().NotBeNullOrEmpty().And.HaveCount(1);
        apiErrorResponse.Errors.First().Message.Should().Be(errorMessage);
    }

    [Fact]
    public void ApiErrorResponse_AddError_Should_Add_Multiple_Errors()
    {
        // Arrange
        var apiErrorResponse = new ApiErrorResponse(400);
        var errorMessages = new List<string> { "Error 1", "Error 2", "Error 3" };

        // Act
        apiErrorResponse.AddError(errorMessages);

        // Assert
        apiErrorResponse.Errors.Should().NotBeNullOrEmpty().And.HaveCount(errorMessages.Count);
        apiErrorResponse.Errors.Select(e => e.Message).Should().BeEquivalentTo(errorMessages);
    }

    [Fact]
    public void ApiErrorResponse_AddError_Should_Not_Modify_Original_Error_List()
    {
        // Arrange
        var apiErrorResponse = new ApiErrorResponse(400);
        var errorMessages = new List<string> { "Error 1", "Error 2", "Error 3" };

        // Act
        apiErrorResponse.AddError(errorMessages);

        // Assert
        errorMessages.Should().NotBeEmpty();
    }

    [Fact]
    public void ApiErrorResponse_AddError_Should_Generate_Errors_Using_Bogus()
    {
        // Arrange
        var apiErrorResponse = new ApiErrorResponse(400);
        var bogus = new Faker();
        var errorMessages = bogus.Random.WordsArray(3);

        // Act
        apiErrorResponse.AddError(errorMessages);

        // Assert
        apiErrorResponse.Errors.Should().NotBeNullOrEmpty().And.HaveCount(errorMessages.Length);
        apiErrorResponse.Errors.Select(e => e.Message).Should().BeEquivalentTo(errorMessages);
    }
}
