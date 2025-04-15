// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using TextCopy;
using Units.CodeGenerator.ConsoleApp;

var ucg = new UnitCodeGenerator(indent: "    ");

var distance2 = new ValueDefintiion(
    ValueName: "Distance2",
    Namespace: "Units",
    DefaultUnit: new UnitDefinition("Meters", "m", new Conversion(1m, 0m)),
    [
        new UnitDefinition("MilliMeters", "mm", new Conversion(0.001m, 0m)),
        new UnitDefinition("CentiMeters", "cm", new Conversion(0.01m, 0m)),
        new UnitDefinition("DeciMeters", "dm", new Conversion(0.1m, 0m)),
        new UnitDefinition("KiloMeters", "km", new Conversion(1000m, 0m)),
    ]);
var temperature = new ValueDefintiion(
    ValueName: "Temperature",
    Namespace: "Units",
    DefaultUnit: new UnitDefinition("DegreesC", "°C", new Conversion(1m, 0m)),
    [
        new UnitDefinition("Kelvin", "K", new Conversion(1m, -273.15m)),
        new UnitDefinition("DegFahrenheit", "°F", new Conversion(5m / 9m, -17.777777778m)),
    ]);

IImmutableList<ValueDefintiion> values =
[
    distance2,
    temperature,
];

var lines = values.SelectMany(x => ucg.GenerateCode(x).Append(string.Empty)).ToImmutableList();

foreach (var line in lines)
{
    Console.WriteLine(line);
}

var linesString = string.Join(Environment.NewLine, lines);

ClipboardService.SetText(linesString);