using ATS.MVP.Tests.Common.Fakes;
using FluentAssertions;

namespace ATS.MVP.Tests.Common.Models;

public class ValueObjectTests
{
    [Fact]
    public void Equals_WhenSameInstance_ReturnsTrue()
    {
        // Arrange
        var valueObject = new FakeValueObject("test");

        // Act
        var result = valueObject.Equals(valueObject);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WhenDifferentInstanceWithSameValue_ReturnsTrue()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test");
        var valueObject2 = new FakeValueObject("test");

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WhenDifferentInstanceWithDifferentValue_ReturnsFalse()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test1");
        var valueObject2 = new FakeValueObject("test2");

        // Act
        var result = valueObject1.Equals(valueObject2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WhenNull_ReturnsFalse()
    {
        // Arrange
        var valueObject = new FakeValueObject("test");

        // Act
        var result = valueObject.Equals(null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WhenSameInstance_ReturnsSameHashCode()
    {
        // Arrange
        var valueObject = new FakeValueObject("test");

        // Act
        var hashCode1 = valueObject.GetHashCode();
        var hashCode2 = valueObject.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_WhenEqualInstances_ReturnsSameHashCode()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test");
        var valueObject2 = new FakeValueObject("test");

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_WhenDifferentInstances_ReturnsDifferentHashCode()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test1");
        var valueObject2 = new FakeValueObject("test2");

        // Act
        var hashCode1 = valueObject1.GetHashCode();
        var hashCode2 = valueObject2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void OperatorEquals_WhenSameInstance_ReturnsTrue()
    {
        // Arrange
        var valueObject = new FakeValueObject("test");

        // Act
        var result = valueObject == valueObject;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OperatorEquals_WhenDifferentInstancesWithSameValue_ReturnsTrue()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test");
        var valueObject2 = new FakeValueObject("test");

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OperatorEquals_WhenDifferentInstancesWithDifferentValue_ReturnsFalse()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test1");
        var valueObject2 = new FakeValueObject("test2");

        // Act
        var result = valueObject1 == valueObject2;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_WhenSameInstance_ReturnsFalse()
    {
        // Arrange
        var valueObject = new FakeValueObject("test");

        // Act
        var result = valueObject != valueObject;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_WhenDifferentInstancesWithSameValue_ReturnsFalse()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test");
        var valueObject2 = new FakeValueObject("test");

        // Act
        var result = valueObject1 != valueObject2;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_WhenDifferentInstancesWithDifferentValue_ReturnsTrue()
    {
        // Arrange
        var valueObject1 = new FakeValueObject("test1");
        var valueObject2 = new FakeValueObject("test2");

        // Act
        var result = valueObject1 != valueObject2;

        // Assert
        result.Should().BeTrue();
    }
}
