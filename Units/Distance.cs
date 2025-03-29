using System.Text.Json.Serialization;

namespace Units;

public readonly struct Distance : IBaseValue<Distance>
{
    public static readonly Distance Zero = Distance.FromMeters(0);

    public decimal Meters { get; }

    [JsonIgnore]
    public decimal MilliMeters => Meters * 1000m;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Distance(decimal meters)
    {
        Meters = meters;
    }

    private Distance(decimal meters, bool _)
    {
        Meters = meters;
    }

    internal static Distance FromMeters(decimal value)
    {
        return new Distance(value, true);
    }
    
    internal static Distance FromMilliMeters(decimal value)
    {
        return new Distance(value / 1000m, true);
    }

    public override string ToString()
    {
        var metersString = Meters.ToString("F2");
        return $"{metersString} [m]";
    }

    public static Distance operator +(Distance a, Distance b) => FromMeters(a.Meters + b.Meters);
    public static Distance operator -(Distance a, Distance b) => FromMeters(a.Meters - b.Meters);
    public static Distance operator -(Distance a) => FromMeters(-a.Meters);
    public static Distance operator /(Distance a, decimal b) => FromMeters(a.Meters / b);
    public static Distance operator *(Distance a, decimal b) => FromMeters(a.Meters * b);
    public static Distance operator *(decimal a, Distance b) => FromMeters(a * b.Meters);
    
    public static bool operator <(Distance a, Distance b) => a.Meters < b.Meters;
    public static bool operator >(Distance a, Distance b) => a.Meters > b.Meters;
    public static bool operator <=(Distance a, Distance b) => a.Meters <= b.Meters;
    public static bool operator >=(Distance a, Distance b) => a.Meters >= b.Meters;

    public static Speed operator /(Distance d, Time t) => Speed.FromMetersPerSecond(d.Meters / t.Seconds);
    public static Time operator /(Distance d, Speed s) => Time.FromSeconds(d.Meters / s.MetersPerSecond);
    
    public static decimal operator /(Distance d1, Distance d2) => d1.Meters / d2.Meters;
   
    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return Meters;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Distance FromBaseValue(decimal value)
    {
        return value.Meters();
    }
}