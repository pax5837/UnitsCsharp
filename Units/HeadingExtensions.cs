namespace Units;

public static class HeadingExtensions
{
    public static Heading Degrees(this float value)
    {
        return Heading.FromDegrees((decimal)value);
    }

    public static Heading Degrees(this decimal value)
    {
        return Heading.FromDegrees(value);
    }

    public static Heading Degrees(this double value)
    {
        return Heading.FromDegrees((decimal)value);
    }

    public static Heading Degrees(this int value)
    {
        return Heading.FromDegrees(value);
    }

    public static Heading Radians(this float value)
    {
        return Heading.FromRadians((decimal)value);
    }

    public static Heading Radians(this double value)
    {
        return Heading.FromRadians((decimal)value);
    }

    public static Heading Radians(this decimal value)
    {
        return Heading.FromRadians(value);
    }

    public static Heading Radians(this int value)
    {
        return Heading.FromRadians(value);
    }

    public static Angle ToAngle(this Heading heading)
    {
        return Angle.FromRadians(heading.Radians);
    }

    public static Heading CalculateAverage(this IEnumerable<Heading> headings)
    {
        var localHeadings = headings.ToArray();
        var sumVectorX = localHeadings.Sum(heading => heading.Cos());
        var sumVectorY = localHeadings.Sum(heading => heading.Sin());
        var heading = (decimal)Math.Atan2(y: (double)sumVectorY, x: (double)sumVectorX);
        return Heading.FromRadians(heading);
    }
}