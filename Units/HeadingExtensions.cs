namespace Units;

internal static class HeadingExtensions
{
    public static decimal Sin(this Heading heading)
    {
        return (decimal)Math.Sin((double)heading.Radians);
    }

    public static decimal Cos(this Heading heading)
    {
        return (decimal)Math.Cos((double)heading.Radians);
    }

    public static decimal Tan(this Heading heading)
    {
        return (decimal)Math.Tan((double)heading.Radians);
    }
}