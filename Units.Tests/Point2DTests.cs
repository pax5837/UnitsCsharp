namespace Units.Tests;

public class Point2DTests
{
    private decimal maxDelta = 0.000_001m;

    [TestCase(1,1,-1,-1,45, 2.828427125f)]
    [TestCase(1,1,-1,-1,-135, 2.828427125f)]
    [TestCase(1,1,-1,-1,-45, 0)]
    [TestCase(1,1,-1,-1,135, 0)]
    [TestCase(1,1,-1,-1,0, 2)]
    [TestCase(1,1,-1,-1,90, 2)]
    [TestCase(1,1,-1,-1,-90, 2)]
    [TestCase(2,1,3,1,45, 0.707106781f)]
    [TestCase(0,0,0,1,0, 0)]
    [TestCase(0,0,0,1,90, 1)]
    public void DistanceToParallelToHeading(
        float x1Meters,
        float y1Meters,
        float x2Meters,
        float y2Meters,
        float headingDegrees,
        float expectedResult)
    {
        // Arrange
        var point1 = new Point2D(x1Meters.Meters(), y1Meters.Meters());
        var point2 = new Point2D(x2Meters.Meters(), y2Meters.Meters());
        var heading = headingDegrees.Degrees();
        
        // Act
        var result1 = point1.AbsDistanceToParallelToHeading(point2, heading);
        var result2 = point2.AbsDistanceToParallelToHeading(point1, heading);
        
        // Assert
        result1.Meters.ShouldBeCloseTo((decimal)expectedResult, maxDelta);
        result2.Meters.ShouldBeCloseTo((decimal)expectedResult, maxDelta);
    }
    
    [TestCase(1,1,-1,-1,45, 0)]
    [TestCase(1,1,-1,-1,-135, 0)]
    [TestCase(1,1,-1,-1,-45, 2.828427125f)]
    [TestCase(1,1,-1,-1,135, 2.828427125f)]
    [TestCase(1,1,-1,-1,0, 2)]
    [TestCase(1,1,-1,-1,90, 2)]
    [TestCase(1,1,-1,-1,-90, 2)]
    [TestCase(2,1,3,1,45, 0.707106781f)]
    [TestCase(0,0,0,1,0, 1)]
    [TestCase(0,0,0,1,90, 0)]
    public void DistanceToOrthogonalToHeading(
        float x1Meters,
        float y1Meters,
        float x2Meters,
        float y2Meters,
        float headingDegrees,
        float expectedResult)
    {
        // Arrange
        var point1 = new Point2D(x1Meters.Meters(), y1Meters.Meters());
        var point2 = new Point2D(x2Meters.Meters(), y2Meters.Meters());
        var heading = headingDegrees.Degrees();
        
        // Act
        var result1 = point1.AbsDistanceToOrthogonalToHeading(point2, heading);
        var result2 = point2.AbsDistanceToOrthogonalToHeading(point1, heading);
        
        // Assert
        result1.Meters.ShouldBeCloseTo((decimal)expectedResult, maxDelta);
        result2.Meters.ShouldBeCloseTo((decimal)expectedResult, maxDelta);
    }
}