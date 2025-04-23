using System.Collections.Immutable;

namespace Units.CodeGenerator.ConsoleApp;

public record UnitDefinition(
    string UnitName,
    string UnitShortSymbol,
    IImmutableList<UnitEquality> Equalities)
{
    public string PropertyName() => UnitName.PropertyName();

    public string ParameterName() => UnitName.ParameterName();

    public FactorAndOffset CalculateFactorAndOffset()
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

        var deltaDefaultUnit = highValue.ValueDefaultUnit - lowValue.ValueDefaultUnit;
        var deltaUnit = highValue.ValueUnit - lowValue.ValueUnit;
        var factor = deltaDefaultUnit / deltaUnit;
        var offset = highValue.ValueDefaultUnit - (highValue.ValueUnit * factor);

        return new FactorAndOffset(factor, offset);
    }
}

public record FactorAndOffset(decimal Factor, decimal Offset)
{
    public string Match(
        Func<string> whenFactorAndOffsetAreRelevant,
        Func<string> whenOnlyFactorIsRelevant,
        Func<string> whenOnlyOffsetIsRelevant,
        Func<string> whenNeitherFactorNorOffsetAreRelevant)
    {
        return Factor == 1
            ? Offset == 0
                ? whenNeitherFactorNorOffsetAreRelevant()
                : whenOnlyOffsetIsRelevant()
            : Offset == 0
                ? whenOnlyFactorIsRelevant()
                : whenFactorAndOffsetAreRelevant();

    }
}