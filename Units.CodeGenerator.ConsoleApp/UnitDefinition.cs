using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

public record UnitDefinition(
    string UnitName,
    string UnitShortSymbol,
    IImmutableList<UnitEquality> Equalities)
{
    public string PropertyName() => UnitName.PropertyName();

    public string ParameterName() => UnitName.ParameterName();

    public FactorOffset FactorOffset()
    {
        if (Equalities.Count == 0 || Equalities.Count > 2)
        {
            throw new InvalidOperationException($"Need exactly one or 2 equalities defined");
        }

        List<UnitEquality> updatedEqualities = Equalities.ToList();
        if (updatedEqualities.Count == 1)
        {
            updatedEqualities.Add(new UnitEquality(0, 0));
        }

        var sortedEqualities = updatedEqualities.OrderBy(eq => eq.ValueUnit).ToImmutableList();
        var highValue = sortedEqualities[1];
        var lowValue = sortedEqualities[0];

        var factor = (highValue.ValueDefaultUnit - lowValue.ValueDefaultUnit) / (highValue.ValueUnit - lowValue.ValueUnit);
        var offset = (- lowValue.ValueUnit) * factor + lowValue.ValueDefaultUnit;

        return new FactorOffset(factor, offset);
    }
}

public record FactorOffset(decimal Factor, decimal Offset);