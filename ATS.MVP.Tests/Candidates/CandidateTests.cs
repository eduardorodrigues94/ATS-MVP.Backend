using ATS.MVP.Domain.Candidates.ValueObjects;
using ATS.MVP.Domain.Candidates;
using ATS.MVP.Domain.Common.Models.ValueObjects;
using FluentAssertions;
using Bogus;

namespace ATS.MVP.Tests.Candidates;

public class CandidateTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        // Arrange
        var faker = new Faker();

        var id = faker.Random.Guid();
        var candidateId = CandidateId.Create(id);
        var name = PersonName.Create($"{faker.Person.FirstName} {faker.Person.LastName}");
        var email = Email.Create(faker.Internet.Email());
        var phoneNumber = PhoneNumber.Create(faker.Phone.PhoneNumber("+## (##) #####-####"));

        // Act
        var candidate = new Candidate(candidateId, name, email, phoneNumber);

        // Assert
        candidate.Id.Should().Be(candidateId);
        candidate.Name.Should().Be(name);
        candidate.Email.Should().Be(email);
        candidate.PhoneNumber.Should().Be(phoneNumber);
    }

    [Fact]
    public void Candidates_With_DifferentIds_Should_Not_Be_Equal()
    {
        // Arrange
        var faker = new Faker();
        var id = faker.Random.Guid();
        var candidateId = CandidateId.Create(id);
        var name = PersonName.Create($"{faker.Person.FirstName} {faker.Person.LastName}");
        var email = Email.Create(faker.Internet.Email());
        var phoneNumber = PhoneNumber.Create(faker.Phone.PhoneNumber("+## (##) #####-####"));


        var id2 = faker.Random.Guid();
        var candidateId2 = CandidateId.Create(id2);

        // Act
        var candidate = new Candidate(candidateId, name, email, phoneNumber);
        var candidate2 = new Candidate(candidateId2, name, email, phoneNumber);

        // Act & Assert
        candidate.Should().NotBe(candidate2);
        candidate.GetHashCode().Should().NotBe(candidate2.GetHashCode());
        (candidate == candidate2).Should().BeFalse();
        (candidate != candidate2).Should().BeTrue();
    }
}
