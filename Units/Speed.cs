using System.Text.Json.Serialization;

namespace Units;

public readonly struct Speed : IBaseValue<Speed>
{
    public static readonly Speed Zero = Speed.FromMetersPerSecond(0);

    public decimal MetersPerSecond { get; }
    
    [JsonIgnore]
    public decimal MilliMetersPerSecond => MetersPerSecond * 1000m;
    
    [JsonIgnore]
    public decimal KiloMetersPerHour => MetersPerSecond * 3.6m;
    
    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Speed(decimal metersPerSecond)
    {
        MetersPerSecond = metersPerSecond;
    }
    
    private Speed(decimal metersPerSecond, bool _)
    {
        MetersPerSecond = metersPerSecond;
    }

    internal static Speed FromMetersPerSecond(decimal value)
    {
        return new Speed(value, true);
    }
    
    internal static Speed FromMilliMetersPerSecond(decimal value)
    {
        return new Speed(value / 1000m, true);
    }
    
    internal static Speed FromKiloMetersPerHour(decimal value)
    {
        return new Speed(value / 3.6m, true);
    }

    public override string ToString()
    {
        var speedString = MetersPerSecond.ToString("F2");
        return $"{speedString} [m/s]";
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return MetersPerSecond;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Speed FromBaseValue(decimal value)
    {
        return value.MetersPerSecond();
    }

    public static Speed operator +(Speed a, Speed b) => FromMetersPerSecond(a.MetersPerSecond + b.MetersPerSecond);
    public static Speed operator -(Speed a, Speed b) => FromMetersPerSecond(a.MetersPerSecond - b.MetersPerSecond);
    public static Speed operator -(Speed a) => FromMetersPerSecond(-a.MetersPerSecond);
    public static Speed operator /(Speed a, decimal b) => FromMetersPerSecond(a.MetersPerSecond / b);
    public static Speed operator *(Speed a, decimal b) => FromMetersPerSecond(a.MetersPerSecond * b);
    public static Speed operator *(decimal a, Speed b) => FromMetersPerSecond(a * b.MetersPerSecond);
    
    public static bool operator <(Speed a, Speed b) => a.MetersPerSecond < b.MetersPerSecond;
    public static bool operator >(Speed a, Speed b) => a.MetersPerSecond > b.MetersPerSecond;
    public static bool operator <=(Speed a, Speed b) => a.MetersPerSecond <= b.MetersPerSecond;
    public static bool operator >=(Speed a, Speed b) => a.MetersPerSecond >= b.MetersPerSecond;
    
    public static Distance operator *(Speed v, Time t) => Distance.FromMeters(v.MetersPerSecond * t.Seconds);
    public static Acceleration operator /(Speed v, Time t) => Acceleration.FromMetersPerSecondSquared(v.MetersPerSecond / t.Seconds);
    public static Time operator /(Speed v, Acceleration a) => Time.FromSeconds(v.MetersPerSecond / a.MetersPerSecondSquared);
    public static decimal operator /(Speed v1, Speed v2) => v1.MetersPerSecond / v2.MetersPerSecond;
}