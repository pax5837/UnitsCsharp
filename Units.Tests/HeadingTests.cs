namespace Units.Tests;

public class HeadingTests
{
    private const decimal MaxDeltaDegrees = 0.000_1m;
    private const decimal MaxDeltaRadians = MaxDeltaDegrees  * 3.1416m / 180m;

    [TestCase(0, 0, 0)]
    [TestCase(3.1415f, 3.1415f, 179.994691f)]
    [TestCase(3.141592828f, -3.1415925f, -179.99999f)]
    [TestCase(4.71238898f, -1.570796327f, -90f)]
    public void FromRadians_ReturnsExpectedRadianAndDegreeValues(
        float inputRadians,
        float expectedRadians,
        float expectedDegrees)
    {
        // Arrange & Act
        var heading = inputRadians.Radians();

        // Assert
        heading.Radians.ShouldBeCloseTo((decimal)expectedRadians, MaxDeltaRadians);
        heading.Degrees.ShouldBeCloseTo((decimal)expectedDegrees, MaxDeltaDegrees);
    }

    [TestCase(0, 0, 0)]
    [TestCase(179.99995f, 3.1415925f, 179.99999f)]
    [TestCase(180.1f, -3.1398473243378056722222222221f, -179.90000000f)]
    [TestCase(270f, -1.570796327f, -90f)]
    public void FromDegrees_ReturnsExpectedRadianAndDegreeValues(
        float inputDegrees,
        float expectedRadians,
        float expectedDegrees)
    {
        // Arrange & Act
        var heading = inputDegrees.Degrees();

        // Assert
        heading.Radians.ShouldBeCloseTo((decimal)expectedRadians, MaxDeltaRadians);
        heading.Degrees.ShouldBeCloseTo((decimal)expectedDegrees, MaxDeltaDegrees);
    }

    [TestCase(0, 0, 0)]
    [TestCase(5, -10, -5)]
    [TestCase(45, 10, 55)]
    [TestCase(45, -10, 35)]
    [TestCase(90, 180, -90)]
    [TestCase(90, -180, -90)]
    [TestCase(-90, 180, 90)]
    [TestCase(-90, -180, 90)]
    [TestCase(-179, -2, 179)]
    [TestCase(179, 2, -179)]
    [TestCase(91, 91, -178)]
    [TestCase(-91, -91, 178)]
    [TestCase(-179, -179, 2)]
    [TestCase(-179, -91, 90)]
    public void Addition(float valueADegrees, float valueBDegrees, float expectedResultDegrees)
    {
        // Arrange
        var a = valueADegrees.Degrees();
        var b = valueBDegrees.Degrees();

        // Act
        var actual = a + b;

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedResultDegrees, MaxDeltaDegrees);
    }

    [TestCase(0, 0, 0)]
    [TestCase(5, 10, -5)]
    [TestCase(45, 10, 35)]
    [TestCase(45, -10, 55)]
    [TestCase(90, 180, -90)]
    [TestCase(90, -180, -90)]
    [TestCase(-90, 180, 90)]
    [TestCase(-90, -180, 90)]
    [TestCase(-179, 2, 179)]
    [TestCase(179, -2, -179)]
    [TestCase(179, -179, -2)]
    public void Substraction(float valueADegrees, float valueBDegrees, float expectedResultDegrees)
    {
        // Arrange
        var a = valueADegrees.Degrees();
        var b = valueBDegrees.Degrees();

        // Act
        var actual = a - b;

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedResultDegrees, MaxDeltaDegrees);
    }

    [TestCase(0, 0)]
    [TestCase(5, 10)]
    [TestCase(45, 10)]
    [TestCase(45, -10)]
    [TestCase(90, 180)]
    [TestCase(90, -180)]
    [TestCase(-90, 180)]
    [TestCase(-90, -180)]
    [TestCase(-179, 2)]
    [TestCase(179, -2)]
    [TestCase(179, -179)]
    [TestCase(-179, 179)]
    [TestCase(90, -90)]
    [TestCase(-90, 90)]
    public void SubstractionThenAddition(float valueADegrees, float valueBDegrees)
    {
        // Arrange
        var a = valueADegrees.Degrees();
        var b = valueBDegrees.Degrees();

        // Act
        var c = a - b;
        var actual = b + c;

        // assert
        actual.Degrees.ShouldBeCloseTo(a.Degrees, MaxDeltaDegrees);
    }

    [TestCase(2.45f)]
    [TestCase(3.1415f)]
    [TestCase(-3.1415f)]
    public void Serializable(float valueRadians)
    {
        // Arrange
        var heading = valueRadians.Radians();

        // Act
        var serialized = System.Text.Json.JsonSerializer.Serialize(heading);
        var deserialized = System.Text.Json.JsonSerializer.Deserialize<Heading>(serialized);

        // Assert
        serialized.Should().Be($"{{\"Radians\":{valueRadians}}}");
        deserialized.Radians.ShouldBeCloseTo(heading.Radians, MaxDeltaRadians);
    }

    [TestCase(0, 10, 5)]
    [TestCase(0, -10, -5)]
    [TestCase(5, 15, 10)]
    [TestCase(-5, 15, 5)]
    [TestCase(60, 120, 90)]
    [TestCase(-60, -120, -90)]
    [TestCase(-177, 179, -179)]
    [TestCase(-91, 92, -179.5f)]
    [TestCase(-89, 89, 0)]
    public void AverageWith(float valueADegrees, float valueBDegrees, float expectedResultDegrees)
    {
        // Arrange
        var a = valueADegrees.Degrees();
        var b = valueBDegrees.Degrees();

        // Act
        var actual1 = a.AverageWith(b);
        var actual2 = b.AverageWith(a);

        // assert
        actual1.Degrees.ShouldBeCloseTo((decimal)expectedResultDegrees, MaxDeltaDegrees);
        actual2.Degrees.ShouldBeCloseTo((decimal)expectedResultDegrees, MaxDeltaDegrees);
    }

    [TestCase(0f, 1f, 1.1f, true)]
    [TestCase(0f, -1f, 1.1f, true)]
    [TestCase(0f, 1f, 0.9f, false)]
    [TestCase(179.5f, -179.5f, 1.1f, true)]
    [TestCase(-179.5f, 179.5f, 1.1f, true)]
    [TestCase(179.5f, -179.5f, 0.9f, false)]
    public void IsCloseTo(float value1Degrees, float value2Degrees, float maxDeltaDegrees, bool expectedResult)
    {
        // Arrange
        var heading1 = value1Degrees.Degrees();
        var heading2 = value2Degrees.Degrees();
        var maxDelta = maxDeltaDegrees.Degrees();

        // Act
        var actual = heading1.IsCloseTo(heading2, maxDelta);

        // assert
        actual.Should().Be(expectedResult);
    }

    // [TestCase(179.5, -179.5, 180)]
    // [TestCase(100, 150, 125)]
    // [TestCase(179, -91, -136)]
    // [TestCase(10, -10, 0)]
    // [TestCase(-10, 10, 0)]
    [TestCase(-90, 90, 0)]
    public void CalculateAverage_With2Headings(
        double value1Degrees,
        double value2Degrees,
        double  expectedAverageDegrees)
    {
        // Arrange
        var heading1 = value1Degrees.Degrees();
        var heading2 = value2Degrees.Degrees();
        var headings = new[] { heading1, heading2 };

        // Act
        var actual = headings.CalculateAverage();

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedAverageDegrees, MaxDeltaDegrees);
    }

    [TestCase(10, 20, 30, 20)]
    [TestCase(10, 20, -10, 6.705)]
    [TestCase(10, 20, -135, -8.9367)]
    [TestCase(-35, 75, -135, -40.3217)]
    [TestCase(-150, 75, -42, -56.2186)]
    public void CalculateAverage_With3Headings(
        double value1Degrees,
        double value2Degrees,
        double value3Degrees,
        double expectedAverageDegrees)
    {
        // Arrange
        var heading1 = value1Degrees.Degrees();
        var heading2 = value2Degrees.Degrees();
        var heading3 = value3Degrees.Degrees();
        var headings = new[] { heading1, heading2, heading3 };

        // Act
        var actual = headings.CalculateAverage();

        // assert
        actual.Degrees.ShouldBeCloseTo((decimal)expectedAverageDegrees, MaxDeltaDegrees);
    }
}