using ATS.MVP.Domain.Candidates.ValueObjects;
using FluentAssertions;

namespace ATS.MVP.Tests.Candidates.ValueObjects;

public class CandidateIdTests
{
    [Fact]
    public void CreateUnique_ShouldCreateValidUniqueId()
    {
        // Act
        var candidateId1 = CandidateId.CreateUnique();
        var candidateId2 = CandidateId.CreateUnique();

        // Assert
        candidateId1.Should().NotBeNull();
        candidateId1.Should().NotBe(candidateId2);
        candidateId1.Value.Should().NotBeEmpty();
    }

    [Fact]
    public void Create_ShouldCreateValidId()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var candidateId = CandidateId.Create(guid);

        // Assert
        candidateId.Should().NotBeNull();
        candidateId.Value.Should().Be(guid);
    }

    [Fact]
    public void ToString_ShouldReturnStringRepresentationOfGuid()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var candidateId = CandidateId.Create(guid);

        // Act
        var result = candidateId.ToString();

        // Assert
        result.Should().Be(guid.ToString());
    }

    [Fact]
    public void GetEqualityComponents_ShouldReturnGuidValue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var candidateId = CandidateId.Create(guid);

        // Act
        var result = candidateId.GetEqualityComponents();

        // Assert
        result.Should().ContainSingle()
            .Which.Should().Be(guid);
    }
}
