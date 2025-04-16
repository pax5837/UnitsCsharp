using System.Text.Json.Serialization;

namespace Units;

public readonly struct Distance2 : IBaseValue<Distance2>
{
    private const decimal Millimeters2Factor = 0.001m;
    private const decimal Centimeters2Factor = 0.01m;
    private const decimal Decimeters2Factor = 0.1m;
    private const decimal Kilometers2Factor = 1000m;

    [JsonInclude]
    public decimal Meters2 { get; }

    [JsonIgnore]
    public decimal Millimeters2 => Meters2 / Millimeters2Factor;

    [JsonIgnore]
    public decimal Centimeters2 => Meters2 / Centimeters2Factor;

    [JsonIgnore]
    public decimal Decimeters2 => Meters2 / Decimeters2Factor;

    [JsonIgnore]
    public decimal Kilometers2 => Meters2 / Kilometers2Factor;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Distance2(decimal meters2)
    {
        Meters2 = meters2;
    }

    [JsonConstructor]
    private Distance2(decimal meters2, bool _)
    {
        Meters2 = meters2;
    }

    public static Distance2 FromMeters2(decimal value)
    {
        return new Distance2(value, false);
    }

    public static Distance2 FromMillimeters2(decimal value)
    {
        return new Distance2(value * Millimeters2Factor, false);
    }

    public static Distance2 FromCentimeters2(decimal value)
    {
        return new Distance2(value * Centimeters2Factor, false);
    }

    public static Distance2 FromDecimeters2(decimal value)
    {
        return new Distance2(value * Decimeters2Factor, false);
    }

    public static Distance2 FromKilometers2(decimal value)
    {
        return new Distance2(value * Kilometers2Factor, false);
    }

    public override string ToString()
    {
        return $"{Meters2:F2} [m]";
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return Meters2;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Distance2 FromBaseValue(decimal value)
    {
        return FromMeters2(value);
    }

    public static Distance2 operator +(Distance2 a, Distance2 b) => FromMeters2(a.Meters2 + b.Meters2);
    public static Distance2 operator -(Distance2 a, Distance2 b) => FromMeters2(a.Meters2 - b.Meters2);
    public static Distance2 operator -(Distance2 a) => FromMeters2(-a.Meters2);
    public static Distance2 operator /(Distance2 a, decimal b) => FromMeters2(a.Meters2 / b);
    public static Distance2 operator *(Distance2 a, decimal b) => FromMeters2(a.Meters2 * b);
    public static Distance2 operator *(decimal a, Distance2 b) => FromMeters2(a * b.Meters2);
    public static decimal operator /(Distance2 d1, Distance2 d2) => d1.Meters2 / d2.Meters2;

    public static bool operator <(Distance2 a, Distance2 b) => a.Meters2 < b.Meters2;
    public static bool operator <=(Distance2 a, Distance2 b) => a.Meters2 <= b.Meters2;
    public static bool operator >(Distance2 a, Distance2 b) => a.Meters2 > b.Meters2;
    public static bool operator >=(Distance2 a, Distance2 b) => a.Meters2 >= b.Meters2;

    public static Speed operator /(Distance2 a, Time b) => Speed.FromMetersPerSecond(a.Meters2 / b.Seconds);
    public static Time operator /(Distance2 a, Speed b) => Time.FromSeconds(a.Meters2 / b.MetersPerSecond);

}

public static class Distance2Extensions
{
    public static Distance2 Meters2(this decimal value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this float value) => Distance2.FromMeters2((decimal)value);
    public static Distance2 Meters2(this int value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this uint value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this long value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this ulong value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this short value) => Distance2.FromMeters2(value);
    public static Distance2 Meters2(this ushort value) => Distance2.FromMeters2(value);
    public static Distance2 Millimeters2(this decimal value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this float value) => Distance2.FromMillimeters2((decimal)value);
    public static Distance2 Millimeters2(this int value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this uint value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this long value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this ulong value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this short value) => Distance2.FromMillimeters2(value);
    public static Distance2 Millimeters2(this ushort value) => Distance2.FromMillimeters2(value);
    public static Distance2 Centimeters2(this decimal value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this float value) => Distance2.FromCentimeters2((decimal)value);
    public static Distance2 Centimeters2(this int value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this uint value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this long value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this ulong value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this short value) => Distance2.FromCentimeters2(value);
    public static Distance2 Centimeters2(this ushort value) => Distance2.FromCentimeters2(value);
    public static Distance2 Decimeters2(this decimal value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this float value) => Distance2.FromDecimeters2((decimal)value);
    public static Distance2 Decimeters2(this int value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this uint value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this long value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this ulong value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this short value) => Distance2.FromDecimeters2(value);
    public static Distance2 Decimeters2(this ushort value) => Distance2.FromDecimeters2(value);
    public static Distance2 Kilometers2(this decimal value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this float value) => Distance2.FromKilometers2((decimal)value);
    public static Distance2 Kilometers2(this int value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this uint value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this long value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this ulong value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this short value) => Distance2.FromKilometers2(value);
    public static Distance2 Kilometers2(this ushort value) => Distance2.FromKilometers2(value);
}

public readonly struct Temperature
{
    private const decimal KelvinOffset = -273.15m;
    private const decimal DegFahrenheitFactor = 0.5555555555555555555555555556m;
    private const decimal DegFahrenheitOffset = -17.777777777777777777777777779m;

    [JsonInclude]
    public decimal DegreesC { get; }

    [JsonIgnore]
    public decimal Kelvin => DegreesC - KelvinOffset;

    [JsonIgnore]
    public decimal DegFahrenheit => (DegreesC - DegFahrenheitOffset) / DegFahrenheitFactor;

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

    public static Temperature FromDegreesC(decimal value)
    {
        return new Temperature(value, false);
    }

    public static Temperature FromKelvin(decimal value)
    {
        return new Temperature(value - KelvinOffset, false);
    }

    public static Temperature FromDegFahrenheit(decimal value)
    {
        return new Temperature((value * DegFahrenheitFactor) - DegFahrenheitOffset, false);
    }

    public override string ToString()
    {
        return $"{DegreesC:F2} [°C]";
    }

    public static bool operator <(Temperature a, Temperature b) => a.DegreesC < b.DegreesC;
    public static bool operator <=(Temperature a, Temperature b) => a.DegreesC <= b.DegreesC;
    public static bool operator >(Temperature a, Temperature b) => a.DegreesC > b.DegreesC;
    public static bool operator >=(Temperature a, Temperature b) => a.DegreesC >= b.DegreesC;

    public bool IsCloseTo(Temperature other, Temperature tolerance) => Math.Abs(DegreesC - other.DegreesC) <= tolerance.DegreesC;

}

public static class TemperatureExtensions
{
    public static Temperature DegreesC(this decimal value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this float value) => Temperature.FromDegreesC((decimal)value);
    public static Temperature DegreesC(this int value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this uint value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this long value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this ulong value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this short value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this ushort value) => Temperature.FromDegreesC(value);
    public static Temperature Kelvin(this decimal value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this float value) => Temperature.FromKelvin((decimal)value);
    public static Temperature Kelvin(this int value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this uint value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this long value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this ulong value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this short value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this ushort value) => Temperature.FromKelvin(value);
    public static Temperature DegFahrenheit(this decimal value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this float value) => Temperature.FromDegFahrenheit((decimal)value);
    public static Temperature DegFahrenheit(this int value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this uint value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this long value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this ulong value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this short value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this ushort value) => Temperature.FromDegFahrenheit(value);
}