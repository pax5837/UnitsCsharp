namespace Units.Tests;

public class BaseValueMathTests
{
    private decimal maxDelta = 0.000_001m;

    [TestCase(1,2,6,2)]
    [TestCase(7,2,6,6)]
    [TestCase(7,6,2,6)]
    public void Clamp_WithSpeed(float s, float sMin, float sMax, float expected)
    {
        // Arrange
        var speed = s.MetersPerSecond();
        var minSpeed = sMin.MetersPerSecond();
        var maxSpeed = sMax.MetersPerSecond();
        
        // Act
        var result = speed.ClampWith(minSpeed, maxSpeed);
        
        // Assert       
        result.MetersPerSecond.ShouldBeCloseTo((decimal)expected, maxDelta);
    }

    [TestCase(3,1,1,2,2,3)]
    [TestCase(3,2,2,1,1,3)]
    [TestCase(0,1,1,2,2,0)]
    [TestCase(-2.5f,1,1,2,2,-2.5f)]
    [TestCase(0,1,1,2,1,1)]
    [TestCase(0,1,1,2,-1,3)]
    public void ExtrapolateLinearly_WithSpeedAndDistance(float x, float x1, float y1, float x2, float y2, float expected)
    {
        // Arrange
        var d = x.Meters();
        var d1 = x1.Meters();
        var s1 = y1.MetersPerSecond();
        var d2 = x2.Meters();
        var s2 = y2.MetersPerSecond();
        
        // Act
        var result = d.ExtrapolateLinearly(d1, s1, d2, s2);
        
        // Assert
        result.MetersPerSecond.ShouldBeCloseTo((decimal)expected, maxDelta);
    }    
    
    [TestCase(3,1,1,2,2,2)]
    [TestCase(3,2,2,1,1,2)]
    [TestCase(0,1,1,2,2,1)]
    [TestCase(-2.5f,1,1,2,2,1)]
    [TestCase(0,1,1,2,1,1)]
    [TestCase(0,1,1,2,-1,1)]
    public void ExtrapolateAndClamp_WithSpeedAndDistance(float x, float x1, float y1, float x2, float y2, float expected)
    {
        // Arrange
        var d = x.Meters();
        var d1 = x1.Meters();
        var s1 = y1.MetersPerSecond();
        var d2 = x2.Meters();
        var s2 = y2.MetersPerSecond();
        
        // Act
        var result = d.ExtrapolateAndClamp(d1, s1, d2, s2);
        
        // Assert
        result.MetersPerSecond.ShouldBeCloseTo((decimal)expected, maxDelta);
    }

    [TestCase]
    public void Max()
    {
        var d1 = 1m.Meters();
        var d2 = 2000m.MilliMeters();
        var d3 = 3m.Meters();
        
        var result = BaseValueMath.MaxVal(d1, d2, d3);

        result.Should().Be(d3);
    }

    [TestCase]
    public void Min()
    {
        var d1 = 1m.Meters();
        var d2 = 2000m.MilliMeters();
        var d3 = 3m.Meters();
        
        var result = BaseValueMath.MinVal(d1, d2, d3);

        result.Should().Be(d1);
    }

    [TestCase]
    public void Average()
    {
        var d1 = 1m.Meters();
        var d2 = 22.7m.MilliMeters();
        var d3 = 3m.Meters();
        var d4 = -5m.Meters();
        
        var result = BaseValueMath.AverageVal(d1, d2, d3, d4);

        result.MilliMeters.ShouldBeCloseTo(-244.325m, 0.000_0001m);
    }
}