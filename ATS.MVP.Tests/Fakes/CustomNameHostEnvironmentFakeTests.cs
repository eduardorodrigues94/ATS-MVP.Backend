namespace ATS.MVP.Tests.Fakes;

// Importante testar se nossos objetos 'fakes' também estão corretos
public class CustomNameHostEnvironmentFakeTests
{
    [Fact]
    public void EnvironmentName_Should_Return_Production_When_IsProduction_Is_True()
    {
        // Arrange
        var isProduction = true;
        var environment = new CustomNameHostEnvironmentFake(isProduction);

        // Act
        var result = environment.EnvironmentName;

        // Assert
        Assert.Equal("Production", result);
    }

    [Fact]
    public void EnvironmentName_Should_Return_Development_When_IsProduction_Is_False()
    {
        // Arrange
        var isProduction = false;
        var environment = new CustomNameHostEnvironmentFake(isProduction);

        // Act
        var result = environment.EnvironmentName;

        // Assert
        Assert.Equal("Development", result);
    }
}
