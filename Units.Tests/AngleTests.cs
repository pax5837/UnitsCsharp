namespace Units.Tests;

public class AngleTests
{
    private const decimal MaxDeltaDegrees = 0.000_1m;
    private const decimal MaxDeltaRadians = MaxDeltaDegrees  * 3.1416m / 180m;

    [TestCase(179.5, -179.5, 180)]
    [TestCase(100, 150, 125)]
    [TestCase(179, -91, -136)]
    [TestCase(10, -10, 0)]
    [TestCase(-10, 10, 0)]
    public void CalculateAverage_With2Angles(
        double value1Degrees,
        double value2Degrees,
        double  expectedAverageDegrees)
    {
        // Arrange
        var angle1 = value1Degrees.AngleDegrees();
        var angle2 = value2Degrees.AngleDegrees();
        var angles = new[] { angle1, angle2 };

        // Act
        var actual = angles.CalculateAverage();

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedAverageDegrees, MaxDeltaDegrees);
    }

    [TestCase(10, 20, 30, 20)]
    [TestCase(10, 20, -10, 6.705)]
    [TestCase(370, 20, -10, 6.705)]
    [TestCase(10, 20, -135, -8.9367)]
    [TestCase(-35, 75, -135, -40.3217)]
    [TestCase(-395, 435, -135, -40.3217)]
    [TestCase(-150, 75, -42, -56.2186)]
    public void CalculateAverage_With3Angles(
        double value1Degrees,
        double value2Degrees,
        double value3Degrees,
        double expectedAverageDegrees)
    {
        // Arrange
        var angle1 = value1Degrees.AngleDegrees();
        var angle2 = value2Degrees.AngleDegrees();
        var angle3 = value3Degrees.AngleDegrees();
        var angles = new[] { angle1, angle2, angle3 };

        // Act
        var actual = angles.CalculateAverage();

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedAverageDegrees, MaxDeltaDegrees);
    }
}