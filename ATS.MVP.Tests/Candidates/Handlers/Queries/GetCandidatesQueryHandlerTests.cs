using ATS.MVP.Application.Candidates.Queries.Handlers;
using ATS.MVP.Application.Candidates.Queries;
using ATS.MVP.Domain.Candidates.DTOs;
using ATS.MVP.Domain.Candidates.Repositories;
using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using NSubstitute;
using FluentAssertions;
using ATS.MVP.Tests.Candidates.Helpers;

namespace ATS.MVP.Tests.Candidates.Handlers.Queries;

public class GetCandidatesQueryHandlerTests
{
    [Fact]
    public async Task Handle_Should_Return_Candidates_List()
    {
        // Arrange
        var dtos = new CandidateDTOGenerator().Generate(5);

        var expectedCandidates = dtos
            .Select(dto =>
                new Candidate(
                    CandidateId.Create(dto.Id),
                    PersonName.Create(dto.Name),
                    Email.Create(dto.Email),
                    PhoneNumber.Create(dto.PhoneNumber))
            );

        var candidateRepository = Substitute.For<ICandidateRepository>();

        candidateRepository
            .GetAllCandidatesAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<IEnumerable<CandidateDTO>>(dtos));

        var handler = new GetCandidatesQueryHandler(candidateRepository);

        // Act
        var result = await handler.Handle(new GetCandidatesQuery(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedCandidates);
    }
}
