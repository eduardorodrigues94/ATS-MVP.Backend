using ATS.MVP.Application.Candidates.Queries.Handlers;
using ATS.MVP.Application.Candidates.Queries;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Errors;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Common.Errors;
using NSubstitute;
using FluentAssertions;
using Bogus;

namespace ATS.MVP.Tests.Candidates.Handlers.Queries;

public class GetCandidateByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Candidate_If_Found()
    {
        // Arrange
        var faker = new Faker();
        var candidateId = faker.Random.Guid();
        var query = new GetCandidateByIdQuery(candidateId);

        var candidateDto = new CandidateDTO
        {
            Id = candidateId,
            Name = faker.Person.FullName,
            Email = faker.Person.Email,
            PhoneNumber = faker.Phone.PhoneNumber("#############")
        };

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.GetCandidateByIdAsync(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(candidateDto));

        var handler = new GetCandidateByIdQueryHandler(candidateRepository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Value.Should().Be(candidateId);
        result.Name.ToString().Should().Be(candidateDto.Name);
        result.Email.ToString().Should().Be(candidateDto.Email);
        result.PhoneNumber.Normalized().Should().Be(candidateDto.PhoneNumber);
    }

    [Fact]
    public async Task Handle_Should_Throw_DomainException_If_Not_Found()
    {
        // Arrange
        var faker = new Faker();
        var candidateId = faker.Random.Guid();
        var query = new GetCandidateByIdQuery(candidateId);

        var candidateRepository = Substitute.For<ICandidateRepository>();
        candidateRepository.GetCandidateByIdAsync(Arg.Any<CandidateId>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<CandidateDTO>(null));

        var handler = new GetCandidateByIdQueryHandler(candidateRepository);

        // Act & Assert
        await handler
            .Invoking(async h => await h.Handle(query, CancellationToken.None))
            .Should()
            .ThrowExactlyAsync<DomainException>()
            .WithMessage(CandidatesErrorMessages.NotFound(CandidateId.Create(candidateId)));
    }
}
