// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using TextCopy;
using Units.CodeGenerator.ConsoleApp;

var ucg = new UnitCodeGenerator(indent: "    ");
var distance = new ValueDefinition(
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

var power = new ValueDefinition(
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


var temperature = new ValueDefinition(
    ValueName: "Temperature",
    Namespace: "Units",
    DefaultUnit: new UnitDefinition("DegreesC", "°C", [new UnitEquality(1m, 1m)]),
    [
        new UnitDefinition("Kelvin", "K", [new UnitEquality(0, -273.15m), new UnitEquality(273.15m, 0m)]),
        new UnitDefinition("DegFahrenheit", "°F", [new UnitEquality(32m, 0m), new UnitEquality(50m, 10m)]),
    ],
    Operations: [],
    Options: Options.None);

IImmutableList<ValueDefinition> values =
[
    power,
];

var lines = values.SelectMany(x => ucg.GenerateCode(x).Append(string.Empty)).ToImmutableList();

foreach (var line in lines)
{
    Console.WriteLine(line);
}

var linesString = string.Join(Environment.NewLine, lines);

ClipboardService.SetText(linesString);