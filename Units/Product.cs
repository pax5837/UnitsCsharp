using System.Text.Json.Serialization;

namespace Units;

public readonly struct Product<TA, TB>
    where TA : IBaseValue<TA>
    where TB : IBaseValue<TB>
{
    [JsonInclude]
    public TA ValueA { private get; init; }
    
    [JsonInclude]
    public TB ValueB { private get; init; }

    [JsonIgnore]
    public decimal ProductValue => ValueA.GetBaseValue() / ValueB.GetBaseValue(); 
    
    public Product(TA valueA, TB valueB)
    {
        ValueA = valueA;
        ValueB = valueB;
    }
    
    public static TB operator /(Product<TA, TB> p, TA a) => p.ValueB.FromBaseValue(p.ProductValue / a.GetBaseValue());
    public static TA operator /(Product<TA, TB> p, TB b) => p.ValueA.FromBaseValue(p.ProductValue / b.GetBaseValue());
}