using ATS.MVP.Tests.Common.Fakes;
using FluentAssertions;

namespace ATS.MVP.Tests.Common.Models;

public class EntityTests
{
    [Fact]
    public void EntitiesWithSameId_ShouldBeEqual()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(1);

        // Act & Assert
        entity1.Should().Be(entity2);
        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        (entity1 == entity2).Should().BeTrue();
        (entity1 != entity2).Should().BeFalse();
    }

    [Fact]
    public void EntitiesWithDifferentId_ShouldNotBeEqual()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(2);

        // Act & Assert
        entity1.Should().NotBe(entity2);
        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
        (entity1 == entity2).Should().BeFalse();
        (entity1 != entity2).Should().BeTrue();
    }

    [Fact]
    public void EntitiesWithSameId_ShouldHaveSameHashCode()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(1);

        // Act & Assert
        entity1.GetHashCode().Should().Be(entity2.GetHashCode());
    }

    [Fact]
    public void EntitiesWithDifferentId_ShouldHaveDifferentHashCode()
    {
        // Arrange
        var entity1 = new FakeEntity(1);
        var entity2 = new FakeEntity(2);

        // Act & Assert
        entity1.GetHashCode().Should().NotBe(entity2.GetHashCode());
    }

    [Fact]
    public void Entity_ShouldNotBeEqualToNull()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Act & Assert
        entity.Should().NotBe(null);
    }

    [Fact]
    public void Entity_ShouldNotBeEqualToDifferentType()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Act & Assert
        entity.Should().NotBe("string");
    }

    [Fact]
    public void Entity_ShouldBeEqualToItself()
    {
        // Arrange
        var entity = new FakeEntity(1);

        // Act & Assert
        entity.Should().Be(entity);
    }
}
