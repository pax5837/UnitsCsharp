using System.Text.Json.Serialization;

namespace Units;

public readonly struct Ratio<TA, TB> 
    where TA : IBaseValue<TA>
    where TB : IBaseValue<TB>
{
    [JsonInclude]
    public TA Numerator { private get; init; }
    
    [JsonInclude]
    public TB Denominator { private get; init; }

    [JsonIgnore]
    public decimal RatioValue => Numerator.GetBaseValue() / Denominator.GetBaseValue(); 
    
    public Ratio(TA numerator, TB denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public Ratio<TB, TA> Inverse()
    {
        return new(Denominator, Numerator);
    }

    public override string ToString()
    {
        return $"Ratio {nameof(Numerator)}: {Numerator}, {nameof(Denominator)}: {Denominator}, {nameof(RatioValue)}: {RatioValue}";
    }

    public static TA operator *(Ratio<TA, TB> r, TB b) => r.Numerator.FromBaseValue(r.RatioValue * b.GetBaseValue());
    public static TA operator *(TB b, Ratio<TA, TB> r) => r.Numerator.FromBaseValue(r.RatioValue * b.GetBaseValue());
    public static TB operator /(TA a, Ratio<TA, TB> r) => r.Denominator.FromBaseValue(a.GetBaseValue() / r.RatioValue);
}