namespace Units.Tests;

public class DistanceTests
{
    private const decimal maxDeltaMeters = 0.000_001m;
    private const decimal maxDeltaMilliMeters = 0.001m;

    [TestCase(0, 0)]
    [TestCase(0.11f, 110f)]
    [TestCase(-1.2f, -1200f)]
    public void FromMeters_ReturnsExpectedMetersAndMilliMetersValues(
        float inputMeters,
        float expectedMilliMeters)
    {
        // Arrange & Act
        var distance = ((decimal)inputMeters).Meters();
        
        // Assert
        distance.MilliMeters.ShouldBeCloseTo((decimal)expectedMilliMeters, maxDeltaMilliMeters);
        distance.Meters.ShouldBeCloseTo((decimal)inputMeters, maxDeltaMeters);
    }
    
    [TestCase(0, 0)]
    [TestCase(963f, 0.963f)]
    [TestCase(-4724f, -4.724f)]
    public void FromMilliMeters_ReturnsExpectedMetersValues(
        float input,
        float expected)
    {
        // Arrange & Act
        var distance = input.MilliMeters();
        
        // Assert
        distance.Meters.ShouldBeCloseTo((decimal)expected, maxDeltaMeters);
    }
    
    [TestCase(0, 0, 0)]
    [TestCase(1.2f, 0.284f, 1.484f)]
    [TestCase(1.2f, -1.845f, -0.645f)]
    [TestCase(425.38f, -5.38f, 420f)]
    public void Addition_ReturnsExpectedMetersValues(
        float input1,
        float input2,
        float expected)
    {
        // Arrange
        var distance1 = input1.Meters();
        var distance2 = input2.Meters();
        
        // Act
        var result = distance1 + distance2;
        
        // Assert
        result.Meters.ShouldBeCloseTo((decimal)expected, maxDeltaMeters);
    }
    
    [TestCase(0, 0, 0)]
    [TestCase(47.3f, 17.3f, 30f)]
    [TestCase(-2f, 2.24f, -4.24f)]
    [TestCase(2f, 2.24f, -0.24f)]
    public void Substraction_ReturnsExpectedMetersValues(
        float input1,
        float input2,
        float expected)
    {
        // Arrange
        var distance1 = input1.Meters();
        var distance2 = input2.Meters();
        
        // Act
        var result = distance1 - distance2;
        
        // Assert
        result.Meters.ShouldBeCloseTo((decimal)expected, maxDeltaMeters);
    }

    [Test]
    public void OperatorMinus_WithOneOperand_ReturnsNegatveValue()
    {
        // Arrange
        var distance = 4.Meters();

        // Act
        var negativeDistance = -distance;

        // Assert
        negativeDistance.Meters.ShouldBeCloseTo(-4, maxDeltaMeters);
    }
}