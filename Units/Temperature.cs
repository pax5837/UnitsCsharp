using System.Text.Json.Serialization;

namespace Units;

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

    public static Temperature FromDegreesC(decimal value) => new Temperature(value, false);
    public static Temperature FromKelvin(decimal value) => new Temperature(value - KelvinOffset, false);
    public static Temperature FromDegFahrenheit(decimal value) => new Temperature((value * DegFahrenheitFactor) - DegFahrenheitOffset, false);

    public override string ToString() => $"{DegreesC:F3} [°C]";

    public static bool operator <(Temperature a, Temperature b) => a.DegreesC < b.DegreesC;
    public static bool operator <=(Temperature a, Temperature b) => a.DegreesC <= b.DegreesC;
    public static bool operator >(Temperature a, Temperature b) => a.DegreesC > b.DegreesC;
    public static bool operator >=(Temperature a, Temperature b) => a.DegreesC >= b.DegreesC;

    public bool IsCloseTo(Temperature other, Temperature tolerance) => Math.Abs(DegreesC - other.DegreesC) <= tolerance.DegreesC;

}