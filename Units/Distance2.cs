using System.Text.Json.Serialization;

namespace Units;

public readonly struct Distance2 : IBaseValue<Distance2>
{
    [JsonInclude]
    public decimal Meters { get; }

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Distance2(decimal meters)
    {
        Meters = meters;
    }

    [JsonConstructor]
    private Distance2(decimal meters, bool _)
    {
        Meters = meters;
    }

    internal static Distance2 FromMeters(decimal value)
    {
        return new Distance2(value, false);
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return Meters;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Distance2 FromBaseValue(decimal value)
    {
        return FromMeters(value);
    }

    public override string ToString()
    {
        return $"{Meters:F2} [m]";
    }

    public static Distance2 operator +(Distance2 a, Distance2 b) => FromMeters(a.Meters + b.Meters);
    public static Distance2 operator -(Distance2 a, Distance2 b) => FromMeters(a.Meters - b.Meters);
    public static Distance2 operator -(Distance2 a) => FromMeters(-a.Meters);
    public static Distance2 operator /(Distance2 a, decimal b) => FromMeters(a.Meters / b);
    public static Distance2 operator *(Distance2 a, decimal b) => FromMeters(a.Meters * b);
    public static Distance2 operator *(decimal a, Distance2 b) => FromMeters(a * b.Meters);
    public static decimal operator /(Distance2 d1, Distance2 d2) => d1.Meters / d2.Meters;

    public static bool operator <(Distance2 a, Distance2 b) => a.Meters < b.Meters;
    public static bool operator <=(Distance2 a, Distance2 b) => a.Meters <= b.Meters;
    public static bool operator >(Distance2 a, Distance2 b) => a.Meters > b.Meters;
    public static bool operator >=(Distance2 a, Distance2 b) => a.Meters >= b.Meters;

    public static Distance2 FromMilliMeters(decimal value)
    {
        return new Distance2(value * 0.001m, false);
    }

}

public readonly struct Temperature : IBaseValue<Temperature>
{
    [JsonInclude]
    public decimal DegreesC { get; }

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Temperature(decimal degreesC)
    {
        DegreesC = degreesC;
    }

    [JsonConstructor]
    private Temperature(decimal degreesC, bool _)
    {
        DegreesC = degreesC;
    }

    internal static Temperature FromDegreesC(decimal value)
    {
        return new Temperature(value, false);
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return DegreesC;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Temperature FromBaseValue(decimal value)
    {
        return FromDegreesC(value);
    }

    public override string ToString()
    {
        return $"{DegreesC:F2} [°C]";
    }

    public static Temperature operator +(Temperature a, Temperature b) => FromDegreesC(a.DegreesC + b.DegreesC);
    public static Temperature operator -(Temperature a, Temperature b) => FromDegreesC(a.DegreesC - b.DegreesC);
    public static Temperature operator -(Temperature a) => FromDegreesC(-a.DegreesC);
    public static Temperature operator /(Temperature a, decimal b) => FromDegreesC(a.DegreesC / b);
    public static Temperature operator *(Temperature a, decimal b) => FromDegreesC(a.DegreesC * b);
    public static Temperature operator *(decimal a, Temperature b) => FromDegreesC(a * b.DegreesC);
    public static decimal operator /(Temperature d1, Temperature d2) => d1.DegreesC / d2.DegreesC;

    public static bool operator <(Temperature a, Temperature b) => a.DegreesC < b.DegreesC;
    public static bool operator <=(Temperature a, Temperature b) => a.DegreesC <= b.DegreesC;
    public static bool operator >(Temperature a, Temperature b) => a.DegreesC > b.DegreesC;
    public static bool operator >=(Temperature a, Temperature b) => a.DegreesC >= b.DegreesC;

    public static Temperature FromKelvin(decimal value)
    {
        return new Temperature(value + -273.15m, false);
    }

    public static Temperature FromDegFahrenheit(decimal value)
    {
        return new Temperature((value * 0.5555555555555555555555555556m) + -17.777777778m, false);
    }

}