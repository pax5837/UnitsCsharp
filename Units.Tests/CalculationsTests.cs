namespace Units.Tests;

public class CalculationsTests
{
    [TestCase(0, 0, 0)]
    [TestCase(25, 4, 100)]
    [TestCase(-25.4f, 4, -101.6f)]
    public void CalculateDistance(float speedMetersPerSecond, float timeSeconds, float expectedDistanceMeters)
    {
        // Arrange
        var v = speedMetersPerSecond.MetersPerSecond();
        var t = timeSeconds.Seconds();
        
        // Act
        var d1 = v * t;
        var d2 = t * v;
        
        // Assert
        d1.Meters.ShouldBeCloseTo((decimal)expectedDistanceMeters, 0.000_001m);
        d2.Meters.ShouldBeCloseTo((decimal)expectedDistanceMeters, 0.000_001m);
    }
    
    [TestCase(120.8f, 4, 30.2f)]
    [TestCase(-250.88f, 3.2f, -78.4f)]
    public void CalculateSpeed(float distanceMeters, float timeSeconds, float expectedSpeedMetersMPerSecond)
    {
        // Arrange
        var d = distanceMeters.Meters();
        var t = timeSeconds.Seconds();
        
        // Act
        var v = d / t;

        // Assert
        v.MetersPerSecond.ShouldBeCloseTo((decimal)expectedSpeedMetersMPerSecond, 0.000_001m);
    }    
    
    [TestCase(120.8f, 4, 30.2f)]
    public void CalculateTime(float distanceMeters, float speedMetersPerSeconds, float expectedTimeSeconds)
    {
        // Arrange
        var d = distanceMeters.Meters();
        var v = speedMetersPerSeconds.MetersPerSecond();
        
        // Act
        var t = d / v;

        // Assert
        t.Seconds.ShouldBeCloseTo((decimal)expectedTimeSeconds, 0.000_001m);
    }
}