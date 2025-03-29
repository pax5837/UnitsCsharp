namespace Units;

public static class AccelerationExtensions
{
    public static Acceleration MetersPerSecondSquared(this float value)
    {
        return Acceleration.FromMetersPerSecondSquared((decimal)value);
    }

    public static Acceleration MetersPerSecondSquared(this decimal value)
    {
        return Acceleration.FromMetersPerSecondSquared(value);
    }

    public static Acceleration MetersPerSecondSquared(this int value)
    {
        return Acceleration.FromMetersPerSecondSquared(value);
    }


    public static Acceleration MilliMetersPerSecondSquared(this float value)
    {
        return Acceleration.FromMilliMetersPerSecondSquared((decimal)value);
    }

    public static Acceleration MilliMetersPerSecondSquared(this decimal value)
    {
        return Acceleration.FromMilliMetersPerSecondSquared(value);
    }

    public static Acceleration MilliMetersPerSecondSquared(this int value)
    {
        return Acceleration.FromMilliMetersPerSecondSquared(value);
    }
}