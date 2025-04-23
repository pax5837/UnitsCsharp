namespace Units;

public static class HeadingNumberExtensions
{
    public static Heading Degrees(this float value) => Heading.FromDegrees((decimal)value);
    public static Heading Degrees(this decimal value) => Heading.FromDegrees(value);
    public static Heading Degrees(this double value) => Heading.FromDegrees((decimal)value);
    public static Heading Degrees(this int value) => Heading.FromDegrees(value);
    public static Heading Radians(this float value) => Heading.FromRadians((decimal)value);
    public static Heading Radians(this double value) => Heading.FromRadians((decimal)value);
    public static Heading Radians(this decimal value) => Heading.FromRadians(value);
    public static Heading Radians(this int value) => Heading.FromRadians(value);

    public static Angle ToAngle(this Heading heading) => Angle.FromRadians(heading.Radians);

    public static Heading CalculateAverage(this IEnumerable<Heading> headings)
    {
        var localHeadings = headings.ToArray();
        var sumVectorX = localHeadings.Sum(heading => heading.Cos());
        var sumVectorY = localHeadings.Sum(heading => heading.Sin());
        var heading = (decimal)Math.Atan2(y: (double)sumVectorY, x: (double)sumVectorX);
        return Heading.FromRadians(heading);
    }
}