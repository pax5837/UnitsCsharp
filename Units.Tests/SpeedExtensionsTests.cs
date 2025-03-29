namespace Units.Tests;

public class SpeedExtensionsTests
{
    [TestCase(1,0,0.5f,1,0.5f)]
    [TestCase(1,10,0.5f,1,9.5f)]
    [TestCase(1,10,-0.5f,1,9.5f)]
    [TestCase(-10,0,2f,1,-2f)]
    [TestCase(2,1,2f,1,2f)]
    public void LimitWithAccelerationAndTimeTest(
        float valueMetersPerSecond,
        float previousSpeedMetersPerSecond,
        float maxAccelerationMetersPerSecondSquared,
        float deltaTimeSeconds,
        float expectedValueMetersPerSecond)
    {
        // Arrange
        var value = valueMetersPerSecond.MetersPerSecond();
        var previousSpeed = previousSpeedMetersPerSecond.MetersPerSecond();
        var maxAcceleration = maxAccelerationMetersPerSecondSquared.MetersPerSecondSquared();
        var deltaTime = deltaTimeSeconds.Seconds();
        
        // Act
        var result = value.LimitWithAccelerationAndTime(previousSpeed, maxAcceleration, deltaTime);
        
        // Assert
        result.MetersPerSecond.ShouldBeCloseTo((decimal)expectedValueMetersPerSecond, 0.000_001m);
    }
}