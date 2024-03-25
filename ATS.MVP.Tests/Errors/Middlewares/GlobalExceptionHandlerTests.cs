using ATS.MVP.Api.Middlewares.Exceptions;
using ATS.MVP.Api.Middlewares.Exceptions.Models;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Tests.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;

namespace ATS.MVP.Tests.Errors.Middlewares;

public class GlobalExceptionHandlerTests
{

    [Fact]
    public async Task TryHandleAsync_WithDomainException_SetsStatusCodeAndWritesErrorResponse()
    {
        // Arrange
        var hostEnvironment = Substitute.For<IHostEnvironment>();
        var logger = Substitute.For<ILogger<GlobalExceptionHandler>>();
        var httpContext = new DefaultHttpContext();
        var responseBodyStream = new MemoryStream();
        httpContext.Response.Body = responseBodyStream;

        var cancellationToken = CancellationToken.None;
        var expectedStatusCode = HttpStatusCode.BadRequest;

        var domainException = new DomainException("Test error message", (int) expectedStatusCode);
        var expectedApiResponse = new ApiErrorResponse(expectedStatusCode);
        expectedApiResponse.AddError(domainException.Message);
        var expectedApiResponseSerialized = JsonConvert.SerializeObject(expectedApiResponse);

        var sut = new GlobalExceptionHandler(hostEnvironment, logger);

        // Act
        var result = await sut.TryHandleAsync(httpContext, domainException, cancellationToken);

        // Assert
        result.Should().BeTrue();
        httpContext.Response.StatusCode.Should().Be((int) expectedStatusCode);
        httpContext.Response.ContentType.Should().StartWith("application/json"); // Checking if it starts with "application/json"

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseBodyStream);
        var responseBody = await reader.ReadToEndAsync();
        logger.Received(1).LogError(domainException, domainException.Message);
        responseBody.Should().NotBeNullOrEmpty();
        responseBody.ToLower().Should().Be(expectedApiResponseSerialized.ToLower());
    }

    [Fact]
    public async Task TryHandleAsync_WithUnhandledExceptionInProduction_SetsStatusCodeAndWritesErrorResponse()
    {
        // Arrange
        var hostEnvironment = new CustomNameHostEnvironmentFake(true);
        var logger = Substitute.For<ILogger<GlobalExceptionHandler>>();
        var httpContext = new DefaultHttpContext();
        var responseBodyStream = new MemoryStream();
        httpContext.Response.Body = responseBodyStream;

        var exception = new Exception("Test unhandled exception");
        var defaultMessage = CommonErrorMessages.UnhandledException;
        var expectedApiResponse = new ApiErrorResponse(HttpStatusCode.InternalServerError);
        expectedApiResponse.AddError(defaultMessage);
        var expectedApiResponseSerialized = JsonConvert.SerializeObject(expectedApiResponse);

        var sut = new GlobalExceptionHandler(hostEnvironment, logger);

        // Act
        var result = await sut.TryHandleAsync(httpContext, exception, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        httpContext.Response.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);
        httpContext.Response.ContentType.Should().Be("application/json; charset=utf-8");

        responseBodyStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseBodyStream);
        var responseBody = await reader.ReadToEndAsync();
        logger.Received(1).LogError(exception, exception.Message);
        responseBody.ToLower().Should().Be(expectedApiResponseSerialized.ToLower());
    }

    [Fact]
    public async Task TryHandleAsync_WithUnhandledExceptionInNonProduction_SetsStatusCodeAndWritesErrorResponse()
    {
        // Arrange
        var hostEnvironment = new CustomNameHostEnvironmentFake(false);

        var logger = Substitute.For<ILogger<GlobalExceptionHandler>>();

        var httpContext = new DefaultHttpContext();
        var responseBodyStream = new MemoryStream();
        httpContext.Response.Body = responseBodyStream;

        var exception = new Exception("Test unhandled exception");
        var expectedApiResponse = new ApiErrorResponse(HttpStatusCode.InternalServerError);
        expectedApiResponse.AddError(exception.Message);
        var expectedApiResponseSerialized = JsonConvert.SerializeObject(expectedApiResponse);

        var sut = new GlobalExceptionHandler(hostEnvironment, logger);

        // Act
        var result = await sut.TryHandleAsync(httpContext, exception, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        httpContext.Response.StatusCode.Should().Be((int) HttpStatusCode.InternalServerError);

        httpContext.Response.ContentType.Should().Be("application/json; charset=utf-8");

        logger.Received(1).LogError(exception, exception.Message);
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseBodyStream);
        var responseBody = await reader.ReadToEndAsync();
        logger.Received(1).LogError(exception, exception.Message);
        responseBody.ToLower().Should().Be(expectedApiResponseSerialized.ToLower());
    }
}
