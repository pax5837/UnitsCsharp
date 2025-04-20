namespace Units;

public static class VoltageNumberExtensions
{
    public static Voltage Volts(this decimal value) => Voltage.FromVolts(value);
    public static Voltage Volts(this float value) => Voltage.FromVolts((decimal)value);
    public static Voltage Volts(this int value) => Voltage.FromVolts(value);
    public static Voltage Volts(this uint value) => Voltage.FromVolts(value);
    public static Voltage Volts(this long value) => Voltage.FromVolts(value);
    public static Voltage Volts(this ulong value) => Voltage.FromVolts(value);
    public static Voltage Volts(this short value) => Voltage.FromVolts(value);
    public static Voltage Volts(this ushort value) => Voltage.FromVolts(value);
    public static Voltage MicroVolts(this decimal value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this float value) => Voltage.FromMicroVolts((decimal)value);
    public static Voltage MicroVolts(this int value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this uint value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this long value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this ulong value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this short value) => Voltage.FromMicroVolts(value);
    public static Voltage MicroVolts(this ushort value) => Voltage.FromMicroVolts(value);
    public static Voltage MilliVolts(this decimal value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this float value) => Voltage.FromMilliVolts((decimal)value);
    public static Voltage MilliVolts(this int value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this uint value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this long value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this ulong value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this short value) => Voltage.FromMilliVolts(value);
    public static Voltage MilliVolts(this ushort value) => Voltage.FromMilliVolts(value);
    public static Voltage KiloVolts(this decimal value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this float value) => Voltage.FromKiloVolts((decimal)value);
    public static Voltage KiloVolts(this int value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this uint value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this long value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this ulong value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this short value) => Voltage.FromKiloVolts(value);
    public static Voltage KiloVolts(this ushort value) => Voltage.FromKiloVolts(value);
    public static Voltage MegaVolts(this decimal value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this float value) => Voltage.FromMegaVolts((decimal)value);
    public static Voltage MegaVolts(this int value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this uint value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this long value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this ulong value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this short value) => Voltage.FromMegaVolts(value);
    public static Voltage MegaVolts(this ushort value) => Voltage.FromMegaVolts(value);
}