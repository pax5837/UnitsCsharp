namespace Units;

public static class SpeedExtensions
{
    public static Speed MetersPerSecond(this float value)
    {
        return Speed.FromMetersPerSecond((decimal)value);
    }    
    
    public static Speed MetersPerSecond(this decimal value)
    {
        return Speed.FromMetersPerSecond(value);
    }    

    public static Speed MetersPerSecond(this int value)
    {
        return Speed.FromMetersPerSecond(value);
    }
    
    public static Speed KiloMetersPerHour(this float value)
    {
        return Speed.FromKiloMetersPerHour((decimal)value);
    } 
    
    public static Speed KiloMetersPerHour(this decimal value)
    {
        return Speed.FromKiloMetersPerHour(value);
    }    

    public static Speed KiloMetersPerHour(this int value)
    {
        return Speed.FromKiloMetersPerHour(value);
    }
    
    public static Speed MilliMetersPerSecond(this float value)
    {
        return Speed.FromMilliMetersPerSecond((decimal)value);
    }    
    
    public static Speed MilliMetersPerSecond(this decimal value)
    {
        return Speed.FromMilliMetersPerSecond(value);
    }    

    public static Speed MilliMetersPerSecond(this int value)
    {
        return Speed.FromMilliMetersPerSecond(value);
    }

    public static Speed LimitWithAccelerationAndTime(this Speed value, Speed previousValue, Acceleration maxAcceleration, Time deltaTime)
    {
        var minSpeed = previousValue - (maxAcceleration.Abs() * deltaTime);
        var maxSpeed = previousValue + (maxAcceleration.Abs() * deltaTime);

        return value.ClampWith(firstBoundary: minSpeed, secondBoundary: maxSpeed);
    }
}