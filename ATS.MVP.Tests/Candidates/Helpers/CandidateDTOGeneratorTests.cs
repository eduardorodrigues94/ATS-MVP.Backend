using FluentAssertions;

namespace ATS.MVP.Tests.Candidates.Helpers;

public class CandidateDTOGeneratorTests
{
    [Fact]
    public void Generate_ShouldCreateValidCandidateDTO()
    {
        // Arrange
        var generator = new CandidateDTOGenerator();

        // Act
        var candidateDTO = generator.Generate();

        // Assert
        candidateDTO.Should().NotBeNull();
        candidateDTO.Id.Should().NotBeEmpty();
        candidateDTO.Name.Should().NotBeNullOrEmpty().And.MatchRegex(@"^[a-zA-Z]+\s[a-zA-Z]+$");
        candidateDTO.Email.Should().NotBeNullOrEmpty().And.MatchRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        candidateDTO.PhoneNumber.Should().NotBeNullOrEmpty().And.MatchRegex(@"^\+\d{2}\s\(\d{2}\)\s\d{4,5}-\d{4}$");
    }
}
