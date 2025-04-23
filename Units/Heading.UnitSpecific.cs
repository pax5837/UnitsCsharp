namespace Units;

/// <summary>
/// Represents a heading with the value being limited to -PI to +PI, or -180° to +180°.
/// The heading 0° is along the x axis.
/// Turns counter clockwise.
/// </summary>
public readonly partial struct Heading
{
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

    private static decimal ReduceRadians_To_MinusPiToPlusPi(decimal inputRadians)
    {
        var turns = (int)Math.Round(inputRadians / (decimal)(Math.PI*2), 0);
        var reducedInput = inputRadians - (decimal)(turns * (Math.PI * 2));
        return reducedInput > (decimal)Math.PI
            ? reducedInput - (decimal)(Math.PI * 2)
            : reducedInput;
    }

    public decimal Cos() => (decimal)Math.Cos((double)Radians);
    public decimal Sin() => (decimal)Math.Sin((double)Radians);
    public decimal Tan() => (decimal)Math.Tan((double)Radians);
}