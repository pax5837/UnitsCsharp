namespace Units;

public static class TemperatureNumberExtensions
{
    public static Temperature DegreesC(this decimal value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this float value) => Temperature.FromDegreesC((decimal)value);
    public static Temperature DegreesC(this int value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this uint value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this long value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this ulong value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this short value) => Temperature.FromDegreesC(value);
    public static Temperature DegreesC(this ushort value) => Temperature.FromDegreesC(value);
    public static Temperature Kelvin(this decimal value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this float value) => Temperature.FromKelvin((decimal)value);
    public static Temperature Kelvin(this int value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this uint value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this long value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this ulong value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this short value) => Temperature.FromKelvin(value);
    public static Temperature Kelvin(this ushort value) => Temperature.FromKelvin(value);
    public static Temperature DegFahrenheit(this decimal value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this float value) => Temperature.FromDegFahrenheit((decimal)value);
    public static Temperature DegFahrenheit(this int value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this uint value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this long value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this ulong value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this short value) => Temperature.FromDegFahrenheit(value);
    public static Temperature DegFahrenheit(this ushort value) => Temperature.FromDegFahrenheit(value);
}