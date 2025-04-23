using System.Text.Json.Serialization;

namespace Units;

public readonly partial struct Current : IBaseValue<Current>
{
    public static readonly Current Zero = FromAmperes(0m);

    private const decimal MicroAmperesFactor = 0.0000001m;
    private const decimal MilliAmperesFactor = 0.001m;
    private const decimal KiloAmperesFactor = 1000m;
    private const decimal MegaAmperesFactor = 1000000m;

    [JsonInclude]
    public decimal Amperes { get; }

    [JsonIgnore]
    public decimal MicroAmperes => Amperes / MicroAmperesFactor;

    [JsonIgnore]
    public decimal MilliAmperes => Amperes / MilliAmperesFactor;

    [JsonIgnore]
    public decimal KiloAmperes => Amperes / KiloAmperesFactor;

    [JsonIgnore]
    public decimal MegaAmperes => Amperes / MegaAmperesFactor;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Current(decimal amperes)
    {
        Amperes = amperes;
    }

    private Current(decimal amperes, bool _)
    {
        Amperes = amperes;
    }

    public static Current FromAmperes(decimal value) => new Current(value, false);
    public static Current FromMicroAmperes(decimal value) => new Current(value * MicroAmperesFactor, false);
    public static Current FromMilliAmperes(decimal value) => new Current(value * MilliAmperesFactor, false);
    public static Current FromKiloAmperes(decimal value) => new Current(value * KiloAmperesFactor, false);
    public static Current FromMegaAmperes(decimal value) => new Current(value * MegaAmperesFactor, false);

    public override string ToString() => $"{Amperes:F3} [A]";

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue() => Amperes;

    [Obsolete("Should only be used for combinations", error: true)]
    public Current FromBaseValue(decimal value) => FromAmperes(value);

    public static Current operator +(Current a, Current b) => FromAmperes(a.Amperes + b.Amperes);
    public static Current operator -(Current a, Current b) => FromAmperes(a.Amperes - b.Amperes);
    public static Current operator -(Current a) => FromAmperes(-a.Amperes);
    public static Current operator /(Current a, decimal b) => FromAmperes(a.Amperes / b);
    public static Current operator *(Current a, decimal b) => FromAmperes(a.Amperes * b);
    public static Current operator *(decimal a, Current b) => FromAmperes(a * b.Amperes);
    public static decimal operator /(Current d1, Current d2) => d1.Amperes / d2.Amperes;

    public static bool operator <(Current a, Current b) => a.Amperes < b.Amperes;
    public static bool operator <=(Current a, Current b) => a.Amperes <= b.Amperes;
    public static bool operator >(Current a, Current b) => a.Amperes > b.Amperes;
    public static bool operator >=(Current a, Current b) => a.Amperes >= b.Amperes;

    public static Power operator *(Current a, Voltage b) => Power.FromWatts(a.Amperes * b.Volts);

}