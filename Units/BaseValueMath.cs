namespace Units;

public static class BaseValueMath
{
    /// <summary>
    /// Clamp function with values with two boundaries, which do not need to be ordered.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="firstBoundary">The minimum value.</param>
    /// <param name="secondBoundary">The maximum value</param>
    /// <returns>A new instance of T</returns>
    public static T ClampWith<T>(this T value, T firstBoundary, T secondBoundary)
        where T : IBaseValue<T>
    {
        var minValue = Math.Min(firstBoundary.GetBaseValue(), secondBoundary.GetBaseValue());
        var maxValue = Math.Max(firstBoundary.GetBaseValue(), secondBoundary.GetBaseValue());
        
        return value.FromBaseValue(
            Math.Clamp(
                value.GetBaseValue(),
                minValue,
                maxValue));
    }
    
    /// <summary>
    /// Performs a linear extrapolation.
    /// </summary>
    /// <returns>A new instance of TY</returns>
    public static TY ExtrapolateLinearly<TY, TX>(this TX value, TX x1, TY y1, TX x2, TY y2)
        where TY : IBaseValue<TY>
        where TX : IBaseValue<TX>
    {
        var ratio = new Ratio<TY, TX>(
            y2.FromBaseValue(y2.GetBaseValue() - y1.GetBaseValue()),
            x2.FromBaseValue(x2.GetBaseValue() - x1.GetBaseValue()));
        var deltaX = x1.FromBaseValue(value.GetBaseValue() - x1.GetBaseValue());
        var deltaY = y1.FromBaseValue((ratio * deltaX).GetBaseValue());
        return y1.FromBaseValue(y1.GetBaseValue() + deltaY.GetBaseValue());
    }
    
    /// <summary>
    /// Performs a linear extrapolation, and clamps the result.
    /// </summary>
    /// <returns>A new instance of TY</returns>
    public static TY ExtrapolateAndClamp<TY, TX>(this TX value, TX x1, TY y1, TX x2, TY y2)
        where TY : IBaseValue<TY>
        where TX : IBaseValue<TX>
    {
        var ratio = new Ratio<TY, TX>(
            y2.FromBaseValue(y2.GetBaseValue() - y1.GetBaseValue()),
            x2.FromBaseValue(x2.GetBaseValue() - x1.GetBaseValue()));
        var deltaX = x1.FromBaseValue(value.GetBaseValue() - x1.GetBaseValue());
        var deltaY = y1.FromBaseValue((ratio * deltaX).GetBaseValue());
        return y1.FromBaseValue(y1.GetBaseValue() + deltaY.GetBaseValue()).ClampWith(y1, y2);
    }
    
    /// <summary>
    /// Gets a copy of the quantity with the contained value set to the absolute value.
    /// </summary>
    /// <returns></returns>
    public static T Abs<T>(this T quantity)
        where T : IBaseValue<T>
    {
        return quantity.FromBaseValue(Math.Abs(quantity.GetBaseValue()));
    }    

    /// <summary>
    /// Returns the sign of the quantity.
    /// </summary>
    /// <returns>+1 when the contained value is positive,<br/>0 when the contained value is 0,<br/>-1 when the contained value is negative</returns>
    public static int Sign<T>(this T quantity)
        where T : IBaseValue<T>
    {
        return Math.Sign(quantity.GetBaseValue());
    }
    
    public static T MaxVal<T>(this IEnumerable<T> values)
        where T : IBaseValue<T>
    {
        return values.MaxBy(x => x.GetBaseValue())!;
    }
    
    public static T MaxVal<T>(T one, T other, params T[] more)
        where T : IBaseValue<T>
    {
        var list = new List<T> { one, other };
        list.AddRange(more);
        return list.MaxBy(x => x.GetBaseValue())!;
    }
    
    public static T MinVal<T>(this IEnumerable<T> values)
        where T : IBaseValue<T>
    {
        return values.MinBy(x => x.GetBaseValue())!;
    }
    
    public static T MinVal<T>(T one, T other, params T[] more)
        where T : IBaseValue<T>
    {
        var list = new List<T> { one, other };
        list.AddRange(more);
        return list.MinBy(x => x.GetBaseValue())!;
    }
    
    public static T AverageVal<T>(this IEnumerable<T> values)
        where T : IBaseValue<T>
    {
        return values.First().FromBaseValue(values.Sum(x => x.GetBaseValue()) / (decimal)values.Count());
    }
    
    public static T AverageVal<T>(T one, T other, params T[] more)
        where T : IBaseValue<T>
    {
        var list = new List<T> { one, other };
        list.AddRange(more);
        return one.FromBaseValue(list.Sum(x => x.GetBaseValue()) / (decimal)list.Count);
    }
    
    public static T SumVal<T>(this IEnumerable<T> values)
        where T : IBaseValue<T>
    {
        return values.First().FromBaseValue(values.Sum(x => x.GetBaseValue()));
    }
    
    public static T SumVal<T>(T one, T other, params T[] more)
        where T : IBaseValue<T>
    {
        var list = new List<T> { one, other };
        list.AddRange(more);
        return one.FromBaseValue(list.Sum(x => x.GetBaseValue()));
    }

    public static bool IsCloseTo<T>(this T one, T other, T maxDelta)
        where T : IBaseValue<T>
    {
        return Math.Abs(one.GetBaseValue() - other.GetBaseValue()) <= maxDelta.GetBaseValue();
    }

    public static bool IsNotCloseTo<T>(this T one, T other, T maxDelta)
        where T : IBaseValue<T>
    {
        return !IsCloseTo(one, other, maxDelta);
    }
}