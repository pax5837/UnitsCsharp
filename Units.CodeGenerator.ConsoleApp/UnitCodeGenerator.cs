using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

internal class UnitCodeGenerator
{
    private readonly string _indent;

    private readonly AdditionalUnitsGenerator _additionalUnitsGenerator;

    public UnitCodeGenerator(string indent)
    {
        _indent = indent;
        _additionalUnitsGenerator = new AdditionalUnitsGenerator(_indent);
    }

    public IImmutableList<string> GenerateCode(ValueDefinition valueDefinition)
    {
        var className = valueDefinition.ValueName.ClassName();
        var nameSpace = valueDefinition.Namespace;
        var defaultUnit = valueDefinition.DefaultUnit;

        return GenerateStartOfClass(valueDefinition, className, nameSpace)
            .Concat(_additionalUnitsGenerator.GenerateConstants(valueDefinition.AdditionalUnits))
            .Concat(GenerateProperties(valueDefinition))
            .Concat(GenerateConstructors(valueDefinition, className))
            .Concat(GenerateStaticFactoryMethods(valueDefinition, className))
            .Concat(GenerateStringFormatting(valueDefinition.DefaultUnit))
            .Concat(GenerateBaseValueImplementation(className, defaultUnit, valueDefinition.Options))
            .Concat(GenerateStandardOperators(valueDefinition.DefaultUnit, className, valueDefinition.Options))
            .Concat(GenerateUnitOperations(valueDefinition, className))
            .Concat(["}", string.Empty])
            .Concat(GenerateStartOfExtensionClass(valueDefinition, className, nameSpace))
            .ToImmutableList();
    }

    private IImmutableList<string> GenerateUnitOperations(ValueDefinition valueDefinition, string className)
    {
        var lines = new Lines(_indent);

        foreach (var op in valueDefinition.Operations)
        {
            var opString = op.Operator switch
            {
                Operator.Multiplication => "*",
                Operator.Division => "/",
                _ => throw new InvalidOperationException($"Can not handle {op.Operator}"),
            };

            var defaultUnitPropertyName = valueDefinition.DefaultUnit.PropertyName();

            lines.Add(1, $"public static {op.OutputType} operator {opString}({className} a, {op.SecondInputType} b) => {op.OutputType}.From{op.OutputDefaultUnit}(a.{defaultUnitPropertyName} {opString} b.{op.SecondInputDefaultUnit});");
        }

        if (lines.Any())
        {
            lines.AddEmptyLine();
        }

        return lines.ToIImmutableList();
    }

    private IImmutableList<string> GenerateStartOfExtensionClass(ValueDefinition valueDefinition, string className, string nameSpace)
    {
        var units = valueDefinition.AdditionalUnits.Prepend(valueDefinition.DefaultUnit);

        var ext = units.SelectMany(u => GenerateNumberExtensions(u, className)).ToImmutableList();

        return new Lines(_indent)
            .Add($"namespace {nameSpace};")
            .AddEmptyLine()
            .Add($"public static class {className}Extensions")
            .Add("{")
            .AddRange(1, ext)
            .Add("}")
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateNumberExtensions(UnitDefinition unitDefinition, string className)
    {
        var propertyName = unitDefinition.UnitName.PropertyName();
        return new Lines(_indent)
            .Add($"public static {className} {propertyName}(this decimal value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this float value) => {className}.From{propertyName}((decimal)value);")
            .Add($"public static {className} {propertyName}(this int value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this uint value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this long value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this ulong value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this short value) => {className}.From{propertyName}(value);")
            .Add($"public static {className} {propertyName}(this ushort value) => {className}.From{propertyName}(value);")
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateStartOfClass(ValueDefinition valueDefinition, string className, string nameSpace)
    {
        var implementsBaseValueString = valueDefinition.Options.HasFlag(Options.ImplementsBaseValue)
            ? $" : IBaseValue<{className}>"
            : string.Empty;

        return new Lines(_indent)
            .Add("using System.Text.Json.Serialization;")
            .AddEmptyLine()
            .Add($"namespace {nameSpace};")
            .AddEmptyLine()
            .Add($"public readonly struct {className}{implementsBaseValueString}")
            .Add("{")
            .Add(1, $"public static readonly {className} Zero = From{valueDefinition.DefaultUnit.PropertyName()}(0m);", valueDefinition.Options.HasFlag(Options.WithZero))
            .AddEmptyLine(valueDefinition.Options.HasFlag(Options.WithZero))
            .ToIImmutableList();
    }


    private IImmutableList<string> GenerateProperties(ValueDefinition valueDefinition)
    {
        var additionalUnitProperties = _additionalUnitsGenerator.GenerateProperties(valueDefinition.AdditionalUnits, valueDefinition.DefaultUnit.PropertyName());

        return new Lines(_indent)
            .Add(1, "[JsonInclude]")
            .Add(1, $"public decimal {valueDefinition.DefaultUnit.PropertyName()} {{ get; }}")
            .AddEmptyLine()
            .AddRange(0, additionalUnitProperties)
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateConstructors(ValueDefinition valueDefinition, string className)
    {
        var defaultUnit = valueDefinition.DefaultUnit;

        return new Lines(_indent)
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
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateStaticFactoryMethods(ValueDefinition valueDefinition, string className)
    {
        var defaultFactoryMethod = new Lines(_indent)
            .Add(1, $"public static {className} From{valueDefinition.DefaultUnit.PropertyName()}(decimal value) => new {className}(value, false);")
            .ToIImmutableList();

        return defaultFactoryMethod
            .Concat(_additionalUnitsGenerator.GenerateStaticFactoryMethods(valueDefinition.AdditionalUnits, className))
            .Append(string.Empty)
            .ToImmutableList();
    }

    private IImmutableList<string> GenerateBaseValueImplementation(
        string className,
        UnitDefinition defaultUnit,
        Options options)
    {
        if (!options.HasFlag(Options.ImplementsBaseValue))
        {
            return [];
        }

        return new Lines(_indent)
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
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateStringFormatting(UnitDefinition defaultUnit)
    {
        return new Lines(_indent)
            .Add(1, "public override string ToString()")
            .Add(1, "{")
            .Add(2, $"return $\"{{{defaultUnit.PropertyName()}:F2}} [{defaultUnit.UnitShortSymbol}]\";")
            .Add(1, "}")
            .AddEmptyLine()
            .ToIImmutableList();
    }

    private IImmutableList<string> GenerateStandardOperators(
        UnitDefinition defaultUnit,
        string className,
        Options options)
    {
        var lines = new Lines(_indent);
        var defaultUnitProperty = defaultUnit.PropertyName();
        lines
            .Add(1, $"public static {className} operator +({className} a, {className} b) => From{defaultUnitProperty}(a.{defaultUnitProperty} + b.{defaultUnitProperty});", options.HasFlag(Options.WithAddition))
            .Add(1, $"public static {className} operator -({className} a, {className} b) => From{defaultUnitProperty}(a.{defaultUnitProperty} - b.{defaultUnitProperty});", options.HasFlag(Options.WithSubtraction))
            .Add(1, $"public static {className} operator -({className} a) => From{defaultUnitProperty}(-a.{defaultUnitProperty});", options.HasFlag(Options.WithSubtraction))
            .Add(1, $"public static {className} operator /({className} a, decimal b) => From{defaultUnitProperty}(a.{defaultUnitProperty} / b);", options.HasFlag(Options.WithDivisionByNumber))
            .Add(1, $"public static {className} operator *({className} a, decimal b) => From{defaultUnitProperty}(a.{defaultUnitProperty} * b);", options.HasFlag(Options.WithMultiplicationByNumber))
            .Add(1, $"public static {className} operator *(decimal a, {className} b) => From{defaultUnitProperty}(a * b.{defaultUnitProperty});", options.HasFlag(Options.WithMultiplicationByNumber))
            .Add(1, $"public static decimal operator /({className} d1, {className} d2) => d1.{defaultUnitProperty} / d2.{defaultUnitProperty};", options.HasFlag(Options.WithDivisionBySameValue))
            .AddEmptyLine(when: lines.Any())
            .Add(1, $"public static bool operator <({className} a, {className} b) => a.{defaultUnitProperty} < b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator <=({className} a, {className} b) => a.{defaultUnitProperty} <= b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator >({className} a, {className} b) => a.{defaultUnitProperty} > b.{defaultUnitProperty};")
            .Add(1, $"public static bool operator >=({className} a, {className} b) => a.{defaultUnitProperty} >= b.{defaultUnitProperty};")
            .AddEmptyLine()
            .Add(1, $"public bool IsCloseTo({className} other, {className} tolerance) => Math.Abs({defaultUnitProperty} - other.{defaultUnitProperty}) <= tolerance.{defaultUnitProperty};", when: !options.HasFlag(Options.ImplementsBaseValue))
            .AddEmptyLine(when: !options.HasFlag(Options.ImplementsBaseValue));

        return lines.ToIImmutableList();
    }
}