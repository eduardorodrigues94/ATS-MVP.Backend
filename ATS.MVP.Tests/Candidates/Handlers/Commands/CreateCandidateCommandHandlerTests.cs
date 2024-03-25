using ATS.MVP.Application.Candidates.Commands;
using ATS.MVP.Application.Candidates.Commands.Handlers;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using FluentAssertions;
using NSubstitute;

namespace ATS.MVP.Tests.Candidates.Handlers.Commands;

public class CreateCandidateCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Create_Candidate_If_Not_Duplicated()
    {
        // Arrange
        var request = new CreateCandidateCommand("John Doe", "john@example.com", "5551123456789");

        var expectedPhoneNumber = PhoneNumber.Create(request.PhoneNumber);
        var expectedEmail = Email.Create(request.Email);

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.IsPhoneNumberDuplicated(expectedPhoneNumber, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
        candidateRepository.IsEmailDuplicated(expectedEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));

        var handler = new CreateCandidateCommandHandler(candidateRepository);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.ToString().Should().Be(request.Name);
        result.Email.Should().Be(expectedEmail);
        result.PhoneNumber.Should().Be(expectedPhoneNumber);

        await candidateRepository.Received(1)
            .AddCandidateAsync(Arg.Any<CandidateDTO>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_If_PhoneNumber_Duplicated()
    {
        // Arrange
        var request = new CreateCandidateCommand("John Doe", "john@example.com", "5551123456789");
        var expectedPhoneNumber = PhoneNumber.Create(request.PhoneNumber);

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.IsPhoneNumberDuplicated(expectedPhoneNumber, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        var handler = new CreateCandidateCommandHandler(candidateRepository);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_If_Email_Duplicated()
    {
        // Arrange
        var request = new CreateCandidateCommand("John Doe", "john@example.com", "5551123456789");
        var expectedPhoneNumber = PhoneNumber.Create(request.PhoneNumber);
        var expectedEmail = Email.Create(request.Email);

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.IsPhoneNumberDuplicated(expectedPhoneNumber, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));
        candidateRepository.IsEmailDuplicated(expectedEmail, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        var handler = new CreateCandidateCommandHandler(candidateRepository);

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(request, CancellationToken.None));
    }
}
