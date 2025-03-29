namespace Units;

public static class TimeExtensions
{
    public static Time Seconds(this float value)
    {
        return Time.FromSeconds((decimal)value);
    }  
    
    public static Time Seconds(this decimal value)
    {
        return Time.FromSeconds(value);
    }    

    public static Time Seconds(this int value)
    {
        return Time.FromSeconds(value);
    }    
    
    public static Time MilliSeconds(this float value)
    {
        return Time.FromMilliSeconds((decimal)value);
    }  
    
    public static Time MilliSeconds(this decimal value)
    {
        return Time.FromMilliSeconds(value);
    }    
    
    public static Time MilliSeconds(this double value)
    {
        return Time.FromMilliSeconds((decimal)value);
    }  
    
    public static Time MilliSeconds(this int value)
    {
        return Time.FromMilliSeconds(value);
    }    
    
    public static Time MicroSeconds(this float value)
    {
        return Time.FromMicroSeconds((decimal)value);
    }  
    
    public static Time MicroSeconds(this decimal value)
    {
        return Time.FromMicroSeconds(value);
    }  
    
    public static Time MicroSeconds(this int value)
    {
        return Time.FromMicroSeconds(value);
    }  
    
    public static Time MicroSeconds(this ulong value)
    {
        return Time.FromMicroSeconds(value);
    }
}