using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

internal class UnitCodeGenerator
{
    private readonly string _indent;

    public UnitCodeGenerator(string indent)
    {
        _indent = indent;
    }

    public IImmutableList<string> GenerateCode(ValueDefintiion valueDefinition)
    {
        var className = valueDefinition.ValueName.ClassName();

        var startOfClass = GenerateStartOfClass(valueDefinition.Namespace, valueDefinition.DefaultUnit, className);

        var operators = GenerateOperators(valueDefinition.DefaultUnit, className);

        var additionalUnits = valueDefinition.AdditionalUnits.SelectMany(u => GenerateAdditionalUnit(u, className)).ToImmutableList();

        return startOfClass
            .Concat(operators)
            .Concat(additionalUnits)
            .Concat(["}"])
            .ToImmutableList();
    }

    private IImmutableList<string> GenerateStartOfClass(string nameSpace, UnitDefinition defaultUnit, string className)
    {
        var lines = new Lines(_indent);
        lines
            .Add("using System.Text.Json.Serialization;")
            .AddEmptyLine()
            .Add($"namespace {nameSpace};")
            .AddEmptyLine()
            .Add($"public readonly struct {className} : IBaseValue<{className}>")
            .Add("{")
            .Add(1, "[JsonInclude]")
            .Add(1, $"public decimal {defaultUnit.PropertyName()} {{ get; }}")
            .AddEmptyLine()
            .Add(1, "[Obsolete(\"Should only be used for deserialization\", error: true)]")
            .Add(1, "[JsonConstructor]")
            .Add(1, $"public {className}(decimal {defaultUnit.ParameterName()})")
            .Add(1, "{")
            .Add(2, $"{defaultUnit.PropertyName()} = {defaultUnit.ParameterName()};")
            .Add(1, "}")
            .AddEmptyLine()
            .Add(1, "[JsonConstructor]")
            .Add(1, $"private {className}(decimal {defaultUnit.ParameterName()}, bool _)")
            .Add(1, "{")
            .Add(2, $"{defaultUnit.PropertyName()} = {defaultUnit.ParameterName()};")
            .Add(1, "}")
            .AddEmptyLine()
            .Add(1, $"internal static {className} From{defaultUnit.PropertyName()}(decimal value)")
            .Add(1, "{")
            .Add(2, $"return new {className}(value, false);")
            .Add(1, "}")
            .AddEmptyLine()
            .Add(1, "[Obsolete(\"Should only be used for combinations\", error: true)]")
            .Add(1, "public decimal GetBaseValue()")
            .Add(1, "{")
            .Add(2, $"return {defaultUnit.PropertyName()};")
            .Add(1, "}")
            .AddEmptyLine()
            .Add(1, "[Obsolete(\"Should only be used for combinations\", error: true)]")
            .Add(1, $"public {className} FromBaseValue(decimal value)")
            .Add(1, "{")
            .Add(2, $"return From{defaultUnit.PropertyName()}(value);")
            .Add(1, "}")
            .AddEmptyLine()
            .Add(1, "public override string ToString()")
            .Add(1, "{")
            .Add(2, $"return $\"{{{defaultUnit.PropertyName()}:F2}} [{defaultUnit.UnitShortSymbol}]\";")
            .Add(1, "}")
            .AddEmptyLine();

        return lines.ToIImmutableList();
    }

    private IImmutableList<string> GenerateOperators(UnitDefinition defaultUnit, string className)
    {
        var lines = new Lines(_indent);
        var defaultUnitProperty = defaultUnit.PropertyName();
        lines
            .Add(1, $"public static {className} operator +({className} a, {className} b) => From{defaultUnitProperty}(a.{defaultUnitProperty} + b.{defaultUnitProperty});")
            .Add(1, $"public static {className} operator -({className} a, {className} b) => From{defaultUnitProperty}(a.{defaultUnitProperty} - b.{defaultUnitProperty});")
            .Add(1, $"public static {className} operator -({className} a) => From{defaultUnitProperty}(-a.{defaultUnitProperty});")
            .Add(1, $"public static {className} operator /({className} a, decimal b) => From{defaultUnitProperty}(a.{defaultUnitProperty} / b);")
            .Add(1, $"public static {className} operator *({className} a, decimal b) => From{defaultUnitProperty}(a.{defaultUnitProperty} * b);")
            .Add(1, $"public static {className} operator *(decimal a, {className} b) => From{defaultUnitProperty}(a * b.{defaultUnitProperty});")
            .Add(1, $"public static decimal operator /({className} d1, {className} d2) => d1.{defaultUnitProperty} / d2.{defaultUnitProperty};")
            .AddEmptyLine()
            .Add(1, $"public static bool operator <({className} a, {className} b) => a.{defaultUnitProperty} < b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator <=({className} a, {className} b) => a.{defaultUnitProperty} <= b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator >({className} a, {className} b) => a.{defaultUnitProperty} > b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator >=({className} a, {className} b) => a.{defaultUnitProperty} >= b.{defaultUnitProperty};")
            .AddEmptyLine();

        return lines.ToIImmutableList();
    }

    private IImmutableList<string> GenerateAdditionalUnit(
        UnitDefinition unitDefinition,
        string className)
    {
        var factor = unitDefinition.convertionToDefaultUnit.Factor;
        var offset = unitDefinition.convertionToDefaultUnit.Offset;
        var conversion = factor != 1m
            ? offset != 0
                ? $"(value * {factor}m) + {offset}m"
                : $"value * {factor}m"
            : offset != 0
                ? $"value + {offset}m"
                : "value";

        var lines = new Lines(_indent);
        lines
            .Add(1, $"public static {className} From{unitDefinition.PropertyName()}(decimal value)")
            .Add(1, "{")
            .Add(2, $"return new {className}({conversion}, false);")
            .Add(1, "}")
            .AddEmptyLine();

        return lines.ToIImmutableList();
    }
}

public record ValueDefintiion(
    string ValueName,
    string Namespace,
    UnitDefinition DefaultUnit,
    IImmutableList<UnitDefinition> AdditionalUnits);

public record UnitDefinition(string UnitName, string UnitShortSymbol, Conversion convertionToDefaultUnit)
{
    public string PropertyName() => UnitName.PropertyName();

    public string ParameterName() => UnitName.ParameterName();
}

public record Conversion(decimal Factor, decimal Offset);

internal static class StringExtensions
{
    public static string ParameterName(this string str) => char.ToLower(str[0]) + str.Substring(1);

    public static string ClassName(this string str) => char.ToUpper(str[0]) + str.Substring(1);

    public static string PropertyName(this string str) => char.ToUpper(str[0]) + str.Substring(1);
}

internal class Lines
{
    private readonly string _indent;
    List<string> _lines = new List<string>();

    public Lines(string indent)
    {
        _indent = indent;
    }

    public Lines Add(string line)
    {
        _lines.Add(line);
        return this;
    }

    public Lines AddEmptyLine()
    {
        _lines.Add(string.Empty);
        return this;
    }

    public Lines Add(ushort indentCount, string line)
    {
        var indents = string.Join(string.Empty, Enumerable.Range(1, (int)indentCount).Select(_ => _indent));
        _lines.Add($"{indents}{line}");
        return this;
    }

    public IImmutableList<string> ToIImmutableList()
    {
        return _lines.ToImmutableList();
    }
}