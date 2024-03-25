using ATS.MVP.Application.Candidates.Commands.Handlers;
using ATS.MVP.Application.Candidates.Commands;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using NSubstitute;
using Bogus;

namespace ATS.MVP.Tests.Candidates.Handlers.Commands;

public class DeleteCandidateByIdCommandHandlerTests
{
    [Fact]
    public async Task Handle_Should_Delete_Candidate()
    {
        // Arrange
        var faker = new Faker();

        var command = new DeleteCandidateByIdCommand(faker.Random.Guid());

        var candidateRepository = Substitute.For<ICandidateRepository>();
        var handler = new DeleteCandidateByIdCommandHandler(candidateRepository);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        await candidateRepository.Received(1).DeleteCandidateAsync(Arg.Is<CandidateId>(id => id.Value == command.Id), Arg.Any<CancellationToken>());
    }
}
