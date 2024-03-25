using ATS.MVP.Application.Candidates.Commands;
using ATS.MVP.Application.Candidates.Commands.Handlers;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Errors;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using Bogus;
using FluentAssertions;
using NSubstitute;

namespace ATS.MVP.Tests.Candidates.Handlers.Commands;

public class UpdateCandidateCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Update_Candidate()
    {
        // Arrange
        var faker = new Faker();

        var command = new UpdateCandidateCommand(
            faker.Random.Guid(),
            faker.Person.FullName,
            faker.Internet.Email(),
            faker.Phone.PhoneNumber("+## (##) #####-####")
        );

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.Exists(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
        candidateRepository.HasSomeOtherUserWithSameEmail(Arg.Any<Email>(), Arg.Any<CandidateId>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(false));
        candidateRepository.HasSomeOtherUserWithSamePhoneNumber(Arg.Any<PhoneNumber>(), Arg.Any<CandidateId>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(false));

        var handler = new UpdateCandidateCommandHandler(candidateRepository);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Value.Should().Be(command.Id);
        result.Name.ToString().Should().Be(command.Name);
        result.Email.ToString().Should().Be(command.Email);
        result.PhoneNumber.ToString().Should().Be(command.PhoneNumber);

        await candidateRepository.Received(1).UpdateCandidateAsync(Arg.Any<CandidateId>(), Arg.Any<CandidateDTO>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_If_Candidate_Not_Found()
    {
        // Arrange
        var faker = new Faker();
        var candidateId = CandidateId.Create(faker.Random.Guid());

        var command = new UpdateCandidateCommand(
            candidateId.Value,
            faker.Person.FullName,
            faker.Internet.Email(),
            faker.Phone.PhoneNumber("+## (##) #####-####")
        );

        var expectedException = new DomainException(CandidatesErrorMessages.NotFound(candidateId), 401);

        var candidateRepository = Substitute.For<ICandidateRepository>();

        candidateRepository
            .Exists(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));

        var handler = new UpdateCandidateCommandHandler(candidateRepository);

        // Act & Assert
        await handler
            .Invoking(async h => await h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<DomainException>()
            .WithMessage(expectedException.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_If_Email_Duplicated()
    {
        // Arrange
        var faker = new Faker();
        var candidateId = CandidateId.Create(faker.Random.Guid());
        var candidateEmail = Email.Create(faker.Internet.Email());

        var command = new UpdateCandidateCommand(
            candidateId.Value,
            faker.Person.FullName,
            candidateEmail.ToString(),
            faker.Phone.PhoneNumber("+## (##) #####-####")
        );
        var expectedException = new DomainException(CandidatesErrorMessages.AlreadyExistsByEmail(candidateEmail), 401);

        var candidateRepository = Substitute.For<ICandidateRepository>();

        candidateRepository
            .Exists(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        candidateRepository
            .HasSomeOtherUserWithSameEmail(
                Arg.Any<Email>(),
                Arg.Any<CandidateId>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.FromResult(true));


        var handler = new UpdateCandidateCommandHandler(candidateRepository);

        // Act & Assert
        await handler
           .Invoking(async h => await h.Handle(command, CancellationToken.None))
           .Should()
           .ThrowExactlyAsync<DomainException>()
           .WithMessage(expectedException.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Exception_If_PhoneNumber_Duplicated()
    {
        // Arrange
        var faker = new Faker();
        var candidateId = CandidateId.Create(faker.Random.Guid());
        var candidateEmail = Email.Create(faker.Internet.Email());
        var phoneNumber = PhoneNumber.Create(faker.Phone.PhoneNumber("+## (##) #####-####"));

        var command = new UpdateCandidateCommand(
            candidateId.Value,
            faker.Person.FullName,
            candidateEmail.ToString(),
            phoneNumber.Normalized());

        var expectedException = new DomainException(CandidatesErrorMessages.AlreadyExistsByPhoneNumber(phoneNumber), 401);


        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository
            .Exists(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        candidateRepository
            .HasSomeOtherUserWithSameEmail(
                Arg.Any<Email>(),
                Arg.Any<CandidateId>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.FromResult(false));

        candidateRepository
            .HasSomeOtherUserWithSamePhoneNumber(
                Arg.Any<PhoneNumber>(),
                Arg.Any<CandidateId>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(Task.FromResult(true));

        var handler = new UpdateCandidateCommandHandler(candidateRepository);

        // Act & Assert
        await handler
           .Invoking(async h => await h.Handle(command, CancellationToken.None))
           .Should()
           .ThrowExactlyAsync<DomainException>()
           .WithMessage(expectedException.Message);
    }
}
