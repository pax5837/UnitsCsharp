using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

internal class AdditionalUnitsGenerator
{
    private readonly string _indent;

    public AdditionalUnitsGenerator(string indent)
    {
        _indent = indent;
    }

    public IImmutableList<string> GenerateConstants(
        IImmutableList<UnitDefinition> additionalUnits)
    {
        var lines = new Lines(_indent);

        foreach (var additionalUnit in additionalUnits)
        {
            GenerateConstants(additionalUnit, lines);
        }

        if (lines.Any())
        {
            lines.AddEmptyLine();
        }

        return lines.ToIImmutableList();
    }

    public IImmutableList<string> GenerateProperties(
        IImmutableList<UnitDefinition> additionalUnits,
        string defaultUnitPropertyName)
    {
        var lines = new Lines(_indent);

        foreach (var additionalUnit in additionalUnits)
        {
            GenerateProperties(lines, additionalUnit, defaultUnitPropertyName);
        }

        return lines.ToIImmutableList();
    }

    public IImmutableList<string> GenerateStaticFactoryMethods(
        IImmutableList<UnitDefinition> additionalUnits,
        string className)
    {
        var lines = new Lines(_indent);

        foreach (var additionalUnit in additionalUnits)
        {
            GenerateStaticInstantiator(lines, additionalUnit, className);
        }

        return lines.ToIImmutableList();
    }

    private void GenerateStaticInstantiator(Lines lines, UnitDefinition unit, string className)
    {
        var conversion = unit.CalculateFactorAndOffset()
            .Match(
                whenFactorAndOffsetAreRelevant: () => $"(value * {FactorConstant(unit)}) + {OffsetConstant(unit)}",
                whenNeitherFactorNorOffsetAreRelevant: () => "value",
                whenOnlyFactorIsRelevant: () => $"value * {FactorConstant(unit)}",
                whenOnlyOffsetIsRelevant: () => $"value + {OffsetConstant(unit)}");

        lines
            .Add(1,
                $"public static {className} From{unit.PropertyName()}(decimal value) => new {className}({conversion}, false);");
    }

    private void GenerateProperties(Lines lines, UnitDefinition unit, string defaultUnitPropertyName)
    {
        var conversion = unit.CalculateFactorAndOffset()
            .Match(
                whenFactorAndOffsetAreRelevant: () => $"({defaultUnitPropertyName} - {OffsetConstant(unit)}) / {FactorConstant(unit)}",
                whenNeitherFactorNorOffsetAreRelevant: () => $"{defaultUnitPropertyName}",
                whenOnlyFactorIsRelevant: () => $"{defaultUnitPropertyName} / {FactorConstant(unit)}",
                whenOnlyOffsetIsRelevant: () => $"{defaultUnitPropertyName} - {OffsetConstant(unit)}");

        lines
            .Add(1, "[JsonIgnore]")
            .Add(1, $"public decimal {unit.PropertyName()} => {conversion};")
            .AddEmptyLine();
    }

    private void GenerateConstants(
        UnitDefinition unit,
        Lines lines)
    {
        var (factor, offset) = unit.CalculateFactorAndOffset();

        if (factor != 1m)
        {
            lines.Add(1, $"private const decimal {FactorConstant(unit)} = {factor}m;");
        }

        if (offset != 0m)
        {
            lines.Add(1, $"private const decimal {OffsetConstant(unit)} = {offset}m;");
        }
    }

    private static string FactorConstant(UnitDefinition unit)
    {
        return $"{unit.UnitName.PropertyName()}Factor";
    }

    private static string OffsetConstant(UnitDefinition unit)
    {
        return $"{unit.UnitName.PropertyName()}Offset";
    }
}