namespace Units;

public static class DistanceExtensions
{
    public static Distance Meters(this float value)
    {
        return Distance.FromMeters((decimal)value);
    }
    
    public static Distance Meters(this decimal value)
    {
        return Distance.FromMeters(value);
    }
    
    public static Distance Meters(this int value)
    {
        return Distance.FromMeters(value);
    }
    
    public static Distance MilliMeters(this float value)
    {
        return Distance.FromMilliMeters((decimal)value);
    }
    
    public static Distance MilliMeters(this decimal value)
    {
        return Distance.FromMilliMeters(value);
    }
    
    public static Distance MilliMeters(this int value)
    {
        return Distance.FromMilliMeters(value);
    }
}