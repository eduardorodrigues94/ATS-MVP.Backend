using ATS.MVP.Domain.Common.Models;
using Bogus;
using FluentAssertions;

namespace ATS.MVP.Tests.Common.Models;


public class AggregateRootTests
{
    public class TestAggregateRoot : AggregateRoot<Guid>
    {
        public TestAggregateRoot(Guid id) : base(id) { }
    }

    [Fact]
    public void AggregateRoot_ShouldBeAbstract()
    {
        typeof(AggregateRoot<int>).IsAbstract.Should().BeTrue();
    }

    [Fact]
    public void Constructor_ShouldSetId()
    {
        var faker = new Faker();

        // Arrange
        var id = faker.Random.Guid();

        // Act
        var aggregateRoot = new TestAggregateRoot(id);

        // Assert
        aggregateRoot.Id.Should().Be(id);
    }

    [Fact]
    public void EntitiesWithSameId_ShouldBeEqual()
    {
        // Arrange
        var faker = new Faker();
        var id = faker.Random.Guid();
        var aggregateRoot1 = new TestAggregateRoot(id);
        var aggregateRoot2 = new TestAggregateRoot(id);

        // Act & Assert
        aggregateRoot1.Should().Be(aggregateRoot2);
        aggregateRoot1.GetHashCode().Should().Be(aggregateRoot2.GetHashCode());
        (aggregateRoot1 == aggregateRoot2).Should().BeTrue();
        (aggregateRoot1 != aggregateRoot2).Should().BeFalse();
    }

    [Fact]
    public void EntitiesWithDifferentId_ShouldNotBeEqual()
    {
        // Arrange
        var faker = new Faker();
        var aggregateRoot1 = new TestAggregateRoot(faker.Random.Guid());
        var aggregateRoot2 = new TestAggregateRoot(faker.Random.Guid());

        // Act & Assert
        aggregateRoot1.Should().NotBe(aggregateRoot2);
        aggregateRoot1.GetHashCode().Should().NotBe(aggregateRoot2.GetHashCode());
        (aggregateRoot1 == aggregateRoot2).Should().BeFalse();
        (aggregateRoot1 != aggregateRoot2).Should().BeTrue();
    }
}
