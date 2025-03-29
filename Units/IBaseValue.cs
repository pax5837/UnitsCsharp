namespace Units;

public interface IBaseValue<T>
{
    internal decimal GetBaseValue();
    
    /// <summary>
    /// Gets a new instance of T with a base value. 
    /// </summary>
    internal T FromBaseValue(decimal value);
}