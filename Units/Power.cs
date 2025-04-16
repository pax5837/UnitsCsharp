using System.Text.Json.Serialization;

namespace Units;

public readonly struct Power : IBaseValue<Power>
{
    public static readonly Power Zero = FromWatts(0m);

    private const decimal MilliwattsFactor = 0.001m;
    private const decimal KilowattsFactor = 1000m;
    private const decimal MegawattsFactor = 1000000m;
    private const decimal HorsepowerMetricFactor = 735.5m;
    private const decimal HorsepowerImperialFactor = 745.7m;

    [JsonInclude]
    public decimal Watts { get; }

    [JsonIgnore]
    public decimal Milliwatts => Watts / MilliwattsFactor;

    [JsonIgnore]
    public decimal Kilowatts => Watts / KilowattsFactor;

    [JsonIgnore]
    public decimal Megawatts => Watts / MegawattsFactor;

    [JsonIgnore]
    public decimal HorsepowerMetric => Watts / HorsepowerMetricFactor;

    [JsonIgnore]
    public decimal HorsepowerImperial => Watts / HorsepowerImperialFactor;

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Power(decimal watts)
    {
        Watts = watts;
    }

    [JsonConstructor]
    private Power(decimal watts, bool _)
    {
        Watts = watts;
    }

    public static Power FromWatts(decimal value) => new Power(value, false);
    public static Power FromMilliwatts(decimal value) => new Power(value * MilliwattsFactor, false);
    public static Power FromKilowatts(decimal value) => new Power(value * KilowattsFactor, false);
    public static Power FromMegawatts(decimal value) => new Power(value * MegawattsFactor, false);
    public static Power FromHorsepowerMetric(decimal value) => new Power(value * HorsepowerMetricFactor, false);
    public static Power FromHorsepowerImperial(decimal value) => new Power(value * HorsepowerImperialFactor, false);

    public override string ToString()
    {
        return $"{Watts:F2} [W]";
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public decimal GetBaseValue()
    {
        return Watts;
    }

    [Obsolete("Should only be used for combinations", error: true)]
    public Power FromBaseValue(decimal value)
    {
        return FromWatts(value);
    }

    public static Power operator +(Power a, Power b) => FromWatts(a.Watts + b.Watts);
    public static Power operator -(Power a, Power b) => FromWatts(a.Watts - b.Watts);
    public static Power operator -(Power a) => FromWatts(-a.Watts);
    public static Power operator /(Power a, decimal b) => FromWatts(a.Watts / b);
    public static Power operator *(Power a, decimal b) => FromWatts(a.Watts * b);
    public static Power operator *(decimal a, Power b) => FromWatts(a * b.Watts);
    public static decimal operator /(Power d1, Power d2) => d1.Watts / d2.Watts;

    public static bool operator <(Power a, Power b) => a.Watts < b.Watts;
    public static bool operator <=(Power a, Power b) => a.Watts <= b.Watts;
    public static bool operator >(Power a, Power b) => a.Watts > b.Watts;
    public static bool operator >=(Power a, Power b) => a.Watts >= b.Watts;

}