using System.Text.Json.Serialization;

namespace Units;

public readonly partial struct Distance : IBaseValue<Distance>
{
    public static readonly Distance Zero = FromMeters(0m);

    private const decimal MillimetersFactor = 0.001m;
    private const decimal CentimetersFactor = 0.01m;
    private const decimal DecimetersFactor = 0.1m;
    private const decimal KilometersFactor = 1000m;
    private const decimal FeetFactor = 0.3047999902464003121151900123m;
    private const decimal InchesFactor = 0.0253999604776614967587110434m;
    private const decimal InchesOffset = 0.0000000000000000000000000018m;
    private const decimal MilesFactor = 1609.34m;

    [JsonInclude]
    public decimal Meters { get; }

    [JsonIgnore]
    public decimal Millimeters => Meters / MillimetersFactor;

    [JsonIgnore]
    public decimal Centimeters => Meters / CentimetersFactor;

    [JsonIgnore]
    public decimal Decimeters => Meters / DecimetersFactor;

    [JsonIgnore]
    public decimal Kilometers => Meters / KilometersFactor;

    [JsonIgnore]
    public decimal Feet => Meters / FeetFactor;

    [JsonIgnore]
    public decimal Inches => (Meters - InchesOffset) / InchesFactor;

    [JsonIgnore]
    public decimal Miles => Meters / MilesFactor;

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

    public static Distance FromMeters(decimal value) => new Distance(value, false);
    public static Distance FromMillimeters(decimal value) => new Distance(value * MillimetersFactor, false);
    public static Distance FromCentimeters(decimal value) => new Distance(value * CentimetersFactor, false);
    public static Distance FromDecimeters(decimal value) => new Distance(value * DecimetersFactor, false);
    public static Distance FromKilometers(decimal value) => new Distance(value * KilometersFactor, false);
    public static Distance FromFeet(decimal value) => new Distance(value * FeetFactor, false);
    public static Distance FromInches(decimal value) => new Distance((value * InchesFactor) + InchesOffset, false);
    public static Distance FromMiles(decimal value) => new Distance(value * MilesFactor, false);

    public override string ToString() => $"{Meters:F3} [m]";

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue() => Meters;

    [Obsolete("Should only be used for combinations", error: true)]
    public Distance FromBaseValue(decimal value) => FromMeters(value);

    public static Distance operator +(Distance a, Distance b) => FromMeters(a.Meters + b.Meters);
    public static Distance operator -(Distance a, Distance b) => FromMeters(a.Meters - b.Meters);
    public static Distance operator -(Distance a) => FromMeters(-a.Meters);
    public static Distance operator /(Distance a, decimal b) => FromMeters(a.Meters / b);
    public static Distance operator *(Distance a, decimal b) => FromMeters(a.Meters * b);
    public static Distance operator *(decimal a, Distance b) => FromMeters(a * b.Meters);
    public static decimal operator /(Distance d1, Distance d2) => d1.Meters / d2.Meters;

    public static bool operator <(Distance a, Distance b) => a.Meters < b.Meters;
    public static bool operator <=(Distance a, Distance b) => a.Meters <= b.Meters;
    public static bool operator >(Distance a, Distance b) => a.Meters > b.Meters;
    public static bool operator >=(Distance a, Distance b) => a.Meters >= b.Meters;

    public static Speed operator /(Distance a, Time b) => Speed.FromMetersPerSecond(a.Meters / b.Seconds);
    public static Time operator /(Distance a, Speed b) => Time.FromSeconds(a.Meters / b.MetersPerSecond);

}