// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;
using TextCopy;
using Units.CodeGenerator.ConsoleApp;

var ucg = new UnitCodeGenerator(indent: "    ");

var currentDirectory = Directory.GetCurrentDirectory();
var targetDir = Path.Combine(currentDirectory, "..//..//..//..//Units");
Console.WriteLine($"Target directory: {targetDir} {Directory.Exists(targetDir)}");

Console.WriteLine(currentDirectory);

Console.WriteLine(ucg);

IImmutableList<ValueDefinition> values =
[
    UnitDefinitions.Speed,
    UnitDefinitions.Power,
    UnitDefinitions.Voltage,
    UnitDefinitions.Current,
    UnitDefinitions.Distance,
    UnitDefinitions.Temperature,

];

var generatedTypes = values.SelectMany(x => ucg.GenerateCode(x)).ToImmutableList();

foreach (var generatedType in generatedTypes)
{
    foreach (var line in generatedType.Lines)
    {
        Console.WriteLine(line);
    }

    var targetFile = Path.Combine(targetDir, $"{generatedType.Name}.cs");

    File.WriteAllText(targetFile, string.Join(Environment.NewLine, generatedType.Lines));

    Console.WriteLine(string.Empty);
}

var linesString = string.Join(Environment.NewLine, generatedTypes.SelectMany(gt => gt.Lines));

ClipboardService.SetText(linesString);