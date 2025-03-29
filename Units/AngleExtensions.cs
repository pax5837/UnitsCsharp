namespace Units;

public static class AngleExtensions
{
    public static Angle AngleDegrees(this float value)
    {
        return Angle.FromDegrees((decimal)value);
    }

    public static Angle AngleDegrees(this double value)
    {
        return Angle.FromDegrees((decimal)value);
    }

    public static Angle AngleDegrees(this decimal value)
    {
        return Angle.FromDegrees(value);
    }

    public static Angle AngleDegrees(this int value)
    {
        return Angle.FromDegrees(value);
    }

    public static Angle AngleRadians(this float value)
    {
        return Angle.FromRadians((decimal)value);
    }

    public static Angle AngleRadians(this double value)
    {
        return Angle.FromRadians((decimal)value);
    }

    public static Angle AngleRadians(this decimal value)
    {
        return Angle.FromRadians(value);
    }

    public static Angle AngleRadians(this int value)
    {
        return Angle.FromRadians(value);
    }

    public static Angle CalculateAverage(this IEnumerable<Angle> angles)
    {
        var localAngles = angles.ToArray();
        var sumVectorX = localAngles.Sum(angle => angle.Cos());
        var sumVectorY = localAngles.Sum(angle => angle.Sin());
        var heading = (decimal)Math.Atan2(y: (double)sumVectorY, x: (double)sumVectorX);
        return Angle.FromRadians(heading);
    }
}