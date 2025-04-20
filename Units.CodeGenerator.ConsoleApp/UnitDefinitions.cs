namespace Units.CodeGenerator.ConsoleApp;

internal static class UnitDefinitions
{
    public static ValueDefinition Distance = new ValueDefinition(
        ValueName: "Distance",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("Meters", "m", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1m)]),
        AdditionalUnits:
        [
            new UnitDefinition("Millimeters", "mm", [new UnitEquality(ValueUnit: 1000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Centimeters", "cm", [new UnitEquality(ValueUnit: 100m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Decimeters", "dm", [new UnitEquality(ValueUnit: 10m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Kilometers", "km", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000m)]),
            new UnitDefinition("Feet", "ft", [new UnitEquality(ValueUnit: 3.28084m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Inches", "inch", [new UnitEquality(ValueUnit: 39.37014m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Miles", "mi", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1609.34m)]),
        ],
        Operations:
        [
            new UnitOperation(Operator: Operator.Division, OutputType: "Speed", OutputDefaultUnit: "MetersPerSecond", SecondInputType: "Time", SecondInputDefaultUnit: "Seconds"),
            new UnitOperation(Operator: Operator.Division, OutputType: "Time", OutputDefaultUnit: "Seconds", SecondInputType: "Speed", SecondInputDefaultUnit: "MetersPerSecond"),
        ],
        Options: Options.ImplementsBaseValue |
                 Options.WithAddition |
                 Options.WithSubtraction |
                 Options.WithDivisionByNumber |
                 Options.WithMultiplicationByNumber |
                 Options.WithDivisionBySameValue |
                 Options.WithZero);

    public static ValueDefinition Speed = new ValueDefinition(
        ValueName: "Speed",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("MetersPerSecond", "m/s", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1m)]),
        AdditionalUnits:
        [
            new UnitDefinition("MillimetersPerSecond", "mm/s", [new UnitEquality(ValueUnit: 1000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("CentimetersPerSecond", "cm/s", [new UnitEquality(ValueUnit: 100m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("DecimetersPerSecond", "dm/s", [new UnitEquality(ValueUnit: 10m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("KilometersPerSecond", "km/s", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000m)]),
            new UnitDefinition("FeetPerSecond", "ft/s", [new UnitEquality(ValueUnit: 3.28084m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("InchesPerSecond", "inch/s", [new UnitEquality(ValueUnit: 39.37014m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("MilesPerHour", "mph", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 0.44704m)]),
            new UnitDefinition("KilometersPerHour", "km/h", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 3.6m)]),
        ],
        Operations:
        [
            new UnitOperation(Operator: Operator.Multiplication, OutputType: "Distance", OutputDefaultUnit: "Meters", SecondInputType: "Time", SecondInputDefaultUnit: "Seconds"),
            new UnitOperation(Operator: Operator.Division, OutputType: "Acceleration", OutputDefaultUnit: "MetersPerSecondSquared", SecondInputType: "Time", SecondInputDefaultUnit: "Seconds"),
            new UnitOperation(Operator: Operator.Division, OutputType: "Time", OutputDefaultUnit: "Seconds", SecondInputType: "Acceleration", SecondInputDefaultUnit: "MetersPerSecondSquared"),
        ],
        Options: Options.ImplementsBaseValue |
                 Options.WithAddition |
                 Options.WithSubtraction |
                 Options.WithDivisionByNumber |
                 Options.WithMultiplicationByNumber |
                 Options.WithDivisionBySameValue |
                 Options.WithZero);

    public static ValueDefinition Power = new ValueDefinition(
        ValueName: "Power",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("Watts", "W", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1m)]),
        AdditionalUnits:
        [
            new UnitDefinition("Milliwatts", "mW", [new UnitEquality(ValueUnit: 1000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("Kilowatts", "kW", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000m)]),
            new UnitDefinition("Megawatts", "kW", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000000m)]),
            new UnitDefinition("HorsepowerMetric", "PS", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 735.5m)]),
            new UnitDefinition("HorsepowerImperial", "hp", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 745.7m)]),
        ],
        Operations:
        [
        ],
        Options: Options.ImplementsBaseValue |
                 Options.WithAddition |
                 Options.WithSubtraction |
                 Options.WithDivisionByNumber |
                 Options.WithMultiplicationByNumber |
                 Options.WithDivisionBySameValue |
                 Options.WithZero);

    public static ValueDefinition Voltage = new ValueDefinition(
        ValueName: "Voltage",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("Volts", "V", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1m)]),
        AdditionalUnits:
        [
            new UnitDefinition("MicroVolts", "\u03BCV", [new UnitEquality(ValueUnit: 1000000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("MilliVolts", "mV", [new UnitEquality(ValueUnit: 1000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("KiloVolts", "kV", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000m)]),
            new UnitDefinition("MegaVolts", "MV", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000000m)]),
        ],
        Operations:
        [
            new UnitOperation(Operator: Operator.Multiplication, OutputType: "Power", OutputDefaultUnit: "Watts", SecondInputType: "Current", SecondInputDefaultUnit: "Amperes"),
        ],
        Options: Options.ImplementsBaseValue |
                 Options.WithAddition |
                 Options.WithSubtraction |
                 Options.WithDivisionByNumber |
                 Options.WithMultiplicationByNumber |
                 Options.WithDivisionBySameValue |
                 Options.WithZero);

    public static ValueDefinition Current = new ValueDefinition(
        ValueName: "Current",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("Amperes", "A", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1m)]),
        AdditionalUnits:
        [
            new UnitDefinition("MicroAmperes", "\u03BCA", [new UnitEquality(ValueUnit: 10000000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("MilliAmperes", "mA", [new UnitEquality(ValueUnit: 1000m, ValueDefaultUnit: 1m)]),
            new UnitDefinition("KiloAmperes", "kA", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000m)]),
            new UnitDefinition("MegaAmperes", "MA", [new UnitEquality(ValueUnit: 1m, ValueDefaultUnit: 1000000m)]),
        ],
        Operations:
        [
            new UnitOperation(Operator: Operator.Multiplication, OutputType: "Power", OutputDefaultUnit: "Watts", SecondInputType: "Voltage", SecondInputDefaultUnit: "Volts"),
        ],
        Options: Options.ImplementsBaseValue |
                 Options.WithAddition |
                 Options.WithSubtraction |
                 Options.WithDivisionByNumber |
                 Options.WithMultiplicationByNumber |
                 Options.WithDivisionBySameValue |
                 Options.WithZero);


    public static ValueDefinition Temperature = new ValueDefinition(
        ValueName: "Temperature",
        Namespace: "Units",
        DefaultUnit: new UnitDefinition("DegreesC", "°C", [new UnitEquality(1m, 1m)]),
        [
            new UnitDefinition("Kelvin", "K", [new UnitEquality(0, -273.15m), new UnitEquality(273.15m, 0m)]),
            new UnitDefinition("DegFahrenheit", "°F", [new UnitEquality(32m, 0m), new UnitEquality(50m, 10m)]),
        ],
        Operations: [],
        Options: Options.None);
}