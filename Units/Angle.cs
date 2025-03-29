using System.Text.Json.Serialization;

namespace Units;

public struct Angle : IBaseValue<Angle>
{
    public decimal Radians { get; }

    [JsonIgnore]
    public decimal Degrees => (decimal)(Radians * 180m / ((decimal)Math.PI));

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Angle(decimal radians)
    {
        Radians = radians;
    }

    /// <summary>
    /// Used as an internal constructor.
    /// </summary>
    public Angle(decimal radians, bool _)
    {
        Radians = radians;
    }

    public override string ToString()
    {
        var degreesString = Degrees.ToString("F1");
        return $"{degreesString} [°]";
    }

    internal static Angle FromRadians(decimal value)
    {
        return new Angle(value, true);
    }

    internal static Angle FromDegrees(decimal value)
    {
        var valueRadians = (value / 180m * (decimal)Math.PI);
        return FromRadians(valueRadians);
    }
    public decimal Sin()
    {
        return (decimal)Math.Sin((double)Radians);
    }

    public decimal Cos()
    {
        return (decimal)Math.Cos((double)Radians);
    }

    public decimal Tan()
    {
        return (decimal)Math.Tan((double)Radians);
    }

    public static Angle From(decimal y, decimal x)
    {
        return Angle.FromRadians((decimal)Math.Atan2((double)y, (double)x));
    }

    public static Angle From(Distance y, Distance x)
    {
        return Angle.FromRadians((decimal)Math.Atan2((double)y.Meters, (double)x.Meters));
    }

    public static Angle operator +(Angle a, Angle b) => FromRadians(a.Radians + b.Radians);
    public static Angle operator -(Angle a, Angle b) => FromRadians(a.Radians - b.Radians);
    public static Angle operator -(Angle a) => FromRadians(-a.Radians);
    public static Angle operator *(Angle a, decimal b) => FromRadians(b * a.Radians);
    public static Angle operator *(decimal b, Angle a) => FromRadians(b * a.Radians);
    public static Angle operator /(Angle a, decimal b) => FromRadians(a.Radians/b);

    public decimal GetBaseValue()
    {
        return Radians;
    }

    public Angle FromBaseValue(decimal value)
    {
        return FromRadians(value);
    }
}