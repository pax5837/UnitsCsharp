using System.Text.Json.Serialization;

namespace Units;

public readonly struct Speed : IBaseValue<Speed>
{
    public static readonly Speed Zero = FromMetersPerSecond(0m);

    private const decimal MillimetersPerSecondFactor = 0.001m;
    private const decimal CentimetersPerSecondFactor = 0.01m;
    private const decimal DecimetersPerSecondFactor = 0.1m;
    private const decimal KilometersPerSecondFactor = 1000m;
    private const decimal FeetPerSecondFactor = 0.3047999902464003121151900123m;
    private const decimal InchesPerSecondFactor = 0.0253999604776614967587110434m;
    private const decimal MilesPerHourFactor = 0.44704m;
    private const decimal KilometersPerHourFactor = 3.6m;

    [JsonInclude]
    public decimal MetersPerSecond { get; }

    [JsonIgnore]
    public decimal MillimetersPerSecond => MetersPerSecond / MillimetersPerSecondFactor;

    [JsonIgnore]
    public decimal CentimetersPerSecond => MetersPerSecond / CentimetersPerSecondFactor;

    [JsonIgnore]
    public decimal DecimetersPerSecond => MetersPerSecond / DecimetersPerSecondFactor;

    [JsonIgnore]
    public decimal KilometersPerSecond => MetersPerSecond / KilometersPerSecondFactor;

    [JsonIgnore]
    public decimal FeetPerSecond => MetersPerSecond / FeetPerSecondFactor;

    [JsonIgnore]
    public decimal InchesPerSecond => MetersPerSecond / InchesPerSecondFactor;

    [JsonIgnore]
    public decimal MilesPerHour => MetersPerSecond / MilesPerHourFactor;

    [JsonIgnore]
    public decimal KilometersPerHour => MetersPerSecond / KilometersPerHourFactor;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Speed(decimal metersPerSecond)
    {
        MetersPerSecond = metersPerSecond;
    }

    [JsonConstructor]
    private Speed(decimal metersPerSecond, bool _)
    {
        MetersPerSecond = metersPerSecond;
    }

    public static Speed FromMetersPerSecond(decimal value) => new Speed(value, false);
    public static Speed FromMillimetersPerSecond(decimal value) => new Speed(value * MillimetersPerSecondFactor, false);
    public static Speed FromCentimetersPerSecond(decimal value) => new Speed(value * CentimetersPerSecondFactor, false);
    public static Speed FromDecimetersPerSecond(decimal value) => new Speed(value * DecimetersPerSecondFactor, false);
    public static Speed FromKilometersPerSecond(decimal value) => new Speed(value * KilometersPerSecondFactor, false);
    public static Speed FromFeetPerSecond(decimal value) => new Speed(value * FeetPerSecondFactor, false);
    public static Speed FromInchesPerSecond(decimal value) => new Speed(value * InchesPerSecondFactor, false);
    public static Speed FromMilesPerHour(decimal value) => new Speed(value * MilesPerHourFactor, false);
    public static Speed FromKilometersPerHour(decimal value) => new Speed(value * KilometersPerHourFactor, false);

    public override string ToString() => $"{MetersPerSecond:F3} [m/s]";

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue() => MetersPerSecond;

    [Obsolete("Should only be used for combinations", error: true)]
    public Speed FromBaseValue(decimal value) => FromMetersPerSecond(value);

    public static Speed operator +(Speed a, Speed b) => FromMetersPerSecond(a.MetersPerSecond + b.MetersPerSecond);
    public static Speed operator -(Speed a, Speed b) => FromMetersPerSecond(a.MetersPerSecond - b.MetersPerSecond);
    public static Speed operator -(Speed a) => FromMetersPerSecond(-a.MetersPerSecond);
    public static Speed operator /(Speed a, decimal b) => FromMetersPerSecond(a.MetersPerSecond / b);
    public static Speed operator *(Speed a, decimal b) => FromMetersPerSecond(a.MetersPerSecond * b);
    public static Speed operator *(decimal a, Speed b) => FromMetersPerSecond(a * b.MetersPerSecond);
    public static decimal operator /(Speed d1, Speed d2) => d1.MetersPerSecond / d2.MetersPerSecond;

    public static bool operator <(Speed a, Speed b) => a.MetersPerSecond < b.MetersPerSecond;
    public static bool operator <=(Speed a, Speed b) => a.MetersPerSecond <= b.MetersPerSecond;
    public static bool operator >(Speed a, Speed b) => a.MetersPerSecond > b.MetersPerSecond;
    public static bool operator >=(Speed a, Speed b) => a.MetersPerSecond >= b.MetersPerSecond;

    public static Distance operator *(Speed a, Time b) => Distance.FromMeters(a.MetersPerSecond * b.Seconds);
    public static Acceleration operator /(Speed a, Time b) => Acceleration.FromMetersPerSecondSquared(a.MetersPerSecond / b.Seconds);
    public static Time operator /(Speed a, Acceleration b) => Time.FromSeconds(a.MetersPerSecond / b.MetersPerSecondSquared);

}