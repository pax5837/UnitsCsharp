namespace Units.Tests;

public class TempTest
{
    [Test]
    [TestCase(32, 0)]
    [TestCase(50, 10)]
    [TestCase(80, 26.6667)]
    public void Bla(decimal valF, decimal valDegC)
    {
        // Arrange
        var t1 = Temperature.FromDegFahrenheit(valF);

        // Act

        // Assert
        t1.DegreesC.Should().BeApproximately(valDegC, 0.001m);
    }
}