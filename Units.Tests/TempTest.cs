namespace Units.Tests;

public class TempTest
{
    [Test]
    [TestCase(32, 0)]
    [TestCase(50, 10)]
    [TestCase(80, 26.6667)]
    public void Fahrenheit(decimal valF, decimal valDegC)
    {
        // Arrange
        var t1 = valF.DegFahrenheit();

        // Act

        // Assert
        t1.DegreesC.Should().BeApproximately(valDegC, 0.001m);

        t1.DegFahrenheit.Should().BeApproximately(valF, 0.001m);
    }

    [Test]
    [TestCase(273.15, 0)]
    [TestCase(0, -273.15)]
    public void Kelvin(decimal valK, decimal valDegC)
    {
        // Arrange
        var t1 = Temperature.FromKelvin(valK);

        // Act

        // Assert
        t1.DegreesC.Should().BeApproximately(valDegC, 0.001m);

        t1.Kelvin.Should().BeApproximately(valK, 0.001m);
    }
}