using System.Text.Json.Serialization;

namespace Units;

public readonly partial struct Voltage : IBaseValue<Voltage>
{
    public static readonly Voltage Zero = FromVolts(0m);

    private const decimal MicroVoltsFactor = 0.000001m;
    private const decimal MilliVoltsFactor = 0.001m;
    private const decimal KiloVoltsFactor = 1000m;
    private const decimal MegaVoltsFactor = 1000000m;

    [JsonInclude]
    public decimal Volts { get; }

    [JsonIgnore]
    public decimal MicroVolts => Volts / MicroVoltsFactor;

    [JsonIgnore]
    public decimal MilliVolts => Volts / MilliVoltsFactor;

    [JsonIgnore]
    public decimal KiloVolts => Volts / KiloVoltsFactor;

    [JsonIgnore]
    public decimal MegaVolts => Volts / MegaVoltsFactor;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Voltage(decimal volts)
    {
        Volts = volts;
    }

    private Voltage(decimal volts, bool _)
    {
        Volts = volts;
    }

    public static Voltage FromVolts(decimal value) => new Voltage(value, false);
    public static Voltage FromMicroVolts(decimal value) => new Voltage(value * MicroVoltsFactor, false);
    public static Voltage FromMilliVolts(decimal value) => new Voltage(value * MilliVoltsFactor, false);
    public static Voltage FromKiloVolts(decimal value) => new Voltage(value * KiloVoltsFactor, false);
    public static Voltage FromMegaVolts(decimal value) => new Voltage(value * MegaVoltsFactor, false);

    public override string ToString() => $"{Volts:F3} [V]";

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue() => Volts;

    [Obsolete("Should only be used for combinations", error: true)]
    public Voltage FromBaseValue(decimal value) => FromVolts(value);

    public static Voltage operator +(Voltage a, Voltage b) => FromVolts(a.Volts + b.Volts);
    public static Voltage operator -(Voltage a, Voltage b) => FromVolts(a.Volts - b.Volts);
    public static Voltage operator -(Voltage a) => FromVolts(-a.Volts);
    public static Voltage operator /(Voltage a, decimal b) => FromVolts(a.Volts / b);
    public static Voltage operator *(Voltage a, decimal b) => FromVolts(a.Volts * b);
    public static Voltage operator *(decimal a, Voltage b) => FromVolts(a * b.Volts);
    public static decimal operator /(Voltage d1, Voltage d2) => d1.Volts / d2.Volts;

    public static bool operator <(Voltage a, Voltage b) => a.Volts < b.Volts;
    public static bool operator <=(Voltage a, Voltage b) => a.Volts <= b.Volts;
    public static bool operator >(Voltage a, Voltage b) => a.Volts > b.Volts;
    public static bool operator >=(Voltage a, Voltage b) => a.Volts >= b.Volts;

    public static Power operator *(Voltage a, Current b) => Power.FromWatts(a.Volts * b.Amperes);

}