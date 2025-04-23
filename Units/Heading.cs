using System.Text.Json.Serialization;

namespace Units;

public readonly partial struct Heading
{
    public static readonly Heading Zero = Heading.FromRadians(0);

    public decimal Radians { get; }

    [JsonIgnore]
    public decimal Degrees => (decimal)(Radians * 180m / ((decimal)Math.PI));

    [Obsolete("Should only be used for deserialization", error: true)]
    [JsonConstructor]
    public Heading(decimal radians)
    {
        Radians = radians;
    }

    /// <summary>
    /// Used as an internal constructor.
    /// </summary>
    public Heading(decimal radians, bool _)
    {
        Radians = radians;
    }

    public override string ToString()
    {
        var degreesString = Degrees.ToString("F1");
        return $"{degreesString} [°]";
    }

    internal static Heading FromRadians(decimal value)
    {
        return new Heading(ReduceRadians_To_MinusPiToPlusPi(value), true);
    }

    internal static Heading FromDegrees(decimal value)
    {
        var valueRadians = (value / 180m * (decimal)Math.PI);
        return FromRadians(valueRadians);
    }

    public bool IsCloseTo(Heading other, Heading maxDelta)
    {
        var delta = this - other;
        return Math.Abs(delta.Radians) <= maxDelta.Radians;
    }

    public bool IsNotCloseTo(Heading other, Heading maxDelta)
    {
        return !IsCloseTo(other, maxDelta);
    }


    public static Heading From(decimal y, decimal x)
    {
        return Heading.FromRadians((decimal)Math.Atan2((double)y, (double)x));
    }

    public static Heading From(Distance y, Distance x)
    {
        return Heading.FromRadians((decimal)Math.Atan2((double)y.Meters, (double)x.Meters));
    }


    public static Heading operator +(Heading a, Heading b) => FromRadians(a.Radians + b.Radians);
    public static Heading operator -(Heading a, Heading b) => FromRadians(a.Radians - b.Radians);
    public static Heading operator -(Heading a) => FromRadians(-a.Radians);
    public static HeadingSpeed operator /(Heading h, Time s) => HeadingSpeed.FromRadiansPerSecond(h.Radians / s.Seconds);
    public static bool operator <(Heading a, Heading b) => a.Radians < b.Radians;
    public static bool operator >(Heading a, Heading b) => a.Radians > b.Radians;
    public static bool operator <=(Heading a, Heading b) => a.Radians <= b.Radians;
    public static bool operator >=(Heading a, Heading b) => a.Radians >= b.Radians;
}