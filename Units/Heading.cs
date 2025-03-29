using System.Text.Json.Serialization;

namespace Units;

/// <summary>
/// Represents a heading with the value being limited to -PI to +PI, or -180° to +180°.
/// The heading 0° is along the x axis.
/// Turns counter clockwise.
/// </summary>
public readonly struct Heading
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

    public Heading AverageWith(Heading other)
    {
        if (Math.Sign(Radians) == 0 || Math.Sign(other.Radians) == 0 || Math.Sign(Radians) == Math.Sign(other.Radians))
        {
            return FromRadians((Radians + other.Radians) / 2m);
        }
        
        var differenceHeading = Radians - other.Radians;

        return ((double)Math.Abs(differenceHeading)) < Math.PI
            ? FromRadians(other.Radians + (differenceHeading / 2))
            : FromRadians((decimal)((decimal)Math.PI + other.Radians + differenceHeading / 2));
    }
    
    public static Heading Average(Heading a, Heading b)
    {
        return a.AverageWith(b);
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

    public static Heading GetHeadingOffset(Heading setPoint, Heading currentHeading)
    {
        return setPoint - currentHeading;
    }
    
    public static Heading GetCorrectedHeading(Heading currentHeading, Heading offset)
    {
        return currentHeading + offset;
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
    
    public bool IsCloseTo(Heading other, Heading maxDelta)
    {
        var delta = this - other;
        return Math.Abs(delta.Radians) <= maxDelta.Radians;
    }
    
    public bool IsNotCloseTo(Heading other, Heading maxDelta)
    {
        return !IsCloseTo(other, maxDelta);
    }
    
    private static decimal ReduceRadians_To_MinusPiToPlusPi(decimal inputRadians)
    {
        var turns = (int)Math.Round(inputRadians / (decimal)(Math.PI*2), 0);
        var reducedInput = inputRadians - (decimal)(turns * (Math.PI * 2));
        return reducedInput > (decimal)Math.PI
            ? reducedInput - (decimal)(Math.PI * 2)
            : reducedInput;
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