using System.Text.Json.Serialization;

namespace Units;

public readonly struct Time : IBaseValue<Time>
{
    public static readonly Time Zero = Time.FromSeconds(0);

    public decimal Seconds { get; }
    
    [JsonIgnore]
    public decimal MilliSeconds => Seconds * 1000m;
    
    [JsonIgnore]
    public decimal MicroSeconds => Seconds * 1_000_000m;
    
    [JsonIgnore]
    public decimal NanoSeconds => Seconds * 1_000_000_000m;
    
    [JsonIgnore]
    public decimal Minutes => Seconds / 1000m;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Time(decimal seconds)
    {
        Seconds = seconds;
    }

    private Time(decimal seconds, bool _)
    {
        Seconds = seconds;
    }

    internal static Time FromSeconds(decimal value)
    {
        return new Time(value, true);
    }
    
    internal static Time FromMilliSeconds(decimal value)
    {
        return new Time(value / 1_000m, true);
    }
    
    internal static Time FromMicroSeconds(decimal value)
    {
        return new Time(value / 1_000_000m, true);
    }
    
    public static Time SignedTimeFromTo(DateTime from, DateTime to)
    {
        return FromMilliSeconds((decimal)(to - from).TotalMilliseconds);
    }

    public static Time GetAbsoluteTimeDelta(DateTime a, DateTime b)
    {
        return Time.FromMilliSeconds(Math.Abs((decimal)(a - b).TotalMilliseconds));
    }

    public override string ToString()
    {
        return $"{Seconds} [s]";
    }
    
    public static Time operator +(Time a, Time b) => FromSeconds(a.Seconds + b.Seconds);
    public static Time operator -(Time a, Time b) => FromSeconds(a.Seconds - b.Seconds);
    public static Time operator -(Time a) => FromSeconds(-a.Seconds);
    public static Time operator /(Time a, decimal b) => FromSeconds(a.Seconds / b);
    public static Time operator *(Time a, decimal b) => FromSeconds(a.Seconds * b);
    public static Time operator *(decimal a, Time b) => FromSeconds(a * b.Seconds);
    
    public static bool operator <(Time a, Time b) => a.Seconds < b.Seconds;
    public static bool operator >(Time a, Time b) => a.Seconds > b.Seconds;
    public static bool operator <=(Time a, Time b) => a.Seconds <= b.Seconds;
    public static bool operator >=(Time a, Time b) => a.Seconds >= b.Seconds;
    
    public static Distance operator *(Time t, Speed v) => Distance.FromMeters(v.MetersPerSecond * t.Seconds);
    public static Speed operator *(Time t, Acceleration a) => Speed.FromMetersPerSecond(a.MetersPerSecondSquared * t.Seconds);
    
    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return Seconds;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Time FromBaseValue(decimal value)
    {
        return value.Seconds();
    }
}