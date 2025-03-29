using System.Text.Json.Serialization;

namespace Units;

public readonly struct Point2D {
    
    public static readonly Point2D Zero = new Point2D(Distance.Zero, Distance.Zero);
    
    public Distance X { get; }
    public Distance Y { get; }

    [JsonConstructor]
    public Point2D(Distance x, Distance y)
    {
        X = x;
        Y = y;
    }
    
    public Point2D GetTranslatedPoint(Heading heading, Distance distance)
    {
        var x = X + distance * heading.Cos();
        var y = Y + distance * heading.Sin();
        return new Point2D(x, y);
    }

    public Distance DistanceTo(Point2D other)
    {
        var deltaXMetersSquared = Math.Pow((double)(X - other.X).Meters, 2);
        var deltaYMetersSquared = Math.Pow((double)(Y - other.Y).Meters, 2);
        
        return Distance.FromMeters((decimal)Math.Sqrt(deltaXMetersSquared + deltaYMetersSquared));
    }
    
    /// <summary>
    /// Calculates the distance to an other point in the direction of the heading.
    /// <remarks>
    /// Given the X Axis has a heading of 0°/180°.
    /// </remarks>
    /// </summary>
    /// <example>
    /// - If point 1 is (0,0), point 2 is (0,1) and heading is 0° the found distance will be 0.<br/>
    /// - If point 1 is (0,0), point 2 is (0,1) and heading is 90° the found distance will be 1. 
    /// </example>
    /// <param name="other">The other point to calculate the distance to.</param>
    /// <param name="heading">The heading.</param>
    /// <returns>A positive value representing the distance.</returns>
    public Distance AbsDistanceToParallelToHeading(Point2D other, Heading heading)
    {
        // point to line problem, where the line is 90° to the heading.
        var lineHeading = heading + Heading.FromDegrees(90);

        var yPartMeters = lineHeading.Cos() * (Y - other.Y);
        var xPartMeters = lineHeading.Sin() * (X - other.X);

        var distanceMeters = Math.Abs((yPartMeters - xPartMeters).Meters);
        
        return Distance.FromMeters(distanceMeters);
    }
    
    /// <summary>
    /// Calculates the distance to an other point orthogonally the heading.
    /// </summary>
    /// <remarks>
    /// Given the X Axis has a heading of 0°/180°.
    /// </remarks>
    /// <example>
    /// - If point 1 is (0,0), point 2 is (0,1) and heading is 0° the found distance will be 1.<br/> 
    /// - If point 1 is (0,0), point 2 is (0,1) and heading is 90° the found distance will be 0. 
    /// </example>
    /// <param name="other">The other point to calculate the distance to.</param>
    /// <param name="heading">The heading.</param>
    /// <returns>A positive value representing the distance.</returns>
    public Distance AbsDistanceToOrthogonalToHeading(Point2D other, Heading heading)
    {
        var yPartMeters = heading.Cos() * (Y - other.Y);
        var xPartMeters = heading.Sin() * (X - other.X);

        var distanceMeters = Math.Abs((yPartMeters - xPartMeters).Meters);
        
        return Distance.FromMeters(distanceMeters);
    }
    
    public Heading CalculateHeadingToOther(Point2D other)
    {
        var deltaX = other.X - X;
        var deltaY = other.Y - Y;

        return Heading.FromRadians((decimal)Math.Atan2((double)deltaY.Meters, (double)deltaX.Meters));
    }
    
    public bool OtherIsInFrontHemisphere(Point2D other, Heading currentHeading)
    {
        var headingToOther = CalculateHeadingToOther(other);
        var headingDelta = headingToOther - currentHeading;
        return headingDelta >= Heading.FromDegrees(-90) && headingDelta <= Heading.FromDegrees(90);
    }

    public override string ToString()
    {
        return $"x: {X} y: {Y}";
    }
}