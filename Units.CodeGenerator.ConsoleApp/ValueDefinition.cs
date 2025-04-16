using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

public record ValueDefinition(
    string ValueName,
    string Namespace,
    UnitDefinition DefaultUnit,
    IImmutableList<UnitDefinition> AdditionalUnits,
    IImmutableList<UnitOperation> Operations,
    Options Options);