namespace Units.CodeGenerator.ConsoleApp;

internal record FactorAndOffset(decimal Factor, decimal Offset)
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