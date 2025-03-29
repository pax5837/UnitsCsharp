using System.Text.Json.Serialization;

namespace Units;

public readonly struct Acceleration: IBaseValue<Acceleration>
{
    public static readonly Acceleration Zero = FromMetersPerSecondSquared(0);

    public decimal MetersPerSecondSquared { get; }

    [JsonIgnore]
    public decimal MilliMetersPerSecondSquared => MetersPerSecondSquared * 1000m;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Acceleration(decimal metersPerSecondSquared)
    {
        MetersPerSecondSquared = metersPerSecondSquared;
    }

    private Acceleration(decimal metersPerSecondSquared, bool _)
    {
        MetersPerSecondSquared = metersPerSecondSquared;
    }

    internal static Acceleration FromMetersPerSecondSquared(decimal value)
    {
        return new Acceleration(value, true);
    }

    internal static Acceleration FromMilliMetersPerSecondSquared(decimal value)
    {
        return new Acceleration(value / 1000m, true);
    }

    public override string ToString()
    {
        return $"{MetersPerSecondSquared} [m/s^2]";
    }

    public static Acceleration operator +(Acceleration a, Acceleration b) => FromMetersPerSecondSquared(a.MetersPerSecondSquared + b.MetersPerSecondSquared);
    public static Acceleration operator -(Acceleration a, Acceleration b) => FromMetersPerSecondSquared(a.MetersPerSecondSquared - b.MetersPerSecondSquared);
    public static Acceleration operator /(Acceleration a, decimal b) => FromMetersPerSecondSquared(a.MetersPerSecondSquared / b);
    public static Acceleration operator *(Acceleration a, decimal b) => FromMetersPerSecondSquared(a.MetersPerSecondSquared * b);
    public static Acceleration operator *(decimal a, Acceleration b) => FromMetersPerSecondSquared(a * b.MetersPerSecondSquared);

    public static bool operator <(Acceleration a, Acceleration b) => a.MetersPerSecondSquared < b.MetersPerSecondSquared;
    public static bool operator >(Acceleration a, Acceleration b) => a.MetersPerSecondSquared > b.MetersPerSecondSquared;
    public static bool operator <=(Acceleration a, Acceleration b) => a.MetersPerSecondSquared <= b.MetersPerSecondSquared;
    public static bool operator >=(Acceleration a, Acceleration b) => a.MetersPerSecondSquared >= b.MetersPerSecondSquared;

    public static Speed operator *(Acceleration a, Time t) => Speed.FromMetersPerSecond(a.MetersPerSecondSquared * t.Seconds);
    public static Speed operator *(Time t, Acceleration a) => Speed.FromMetersPerSecond(a.MetersPerSecondSquared * t.Seconds);

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return MetersPerSecondSquared;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Acceleration FromBaseValue(decimal value)
    {
        return value.MetersPerSecondSquared();
    }
}