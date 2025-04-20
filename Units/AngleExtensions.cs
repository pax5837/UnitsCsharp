namespace Units;

public static class AngleExtensions
{
    public static decimal Sin(this Angle angle)
    {
        return (decimal)Math.Sin((double)angle.Radians);
    }

    public static decimal Cos(this Angle angle)
    {
        return (decimal)Math.Cos((double)angle.Radians);
    }

    public static decimal Tan(this Angle angle)
    {
        return (decimal)Math.Tan((double)angle.Radians);
    }
}