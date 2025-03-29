namespace Units.Tests;

internal static class DecimalExtensions
{
    public static void ShouldBeCloseTo(this decimal actual, decimal expected, decimal maxDelta)
    {
        var absDelta = Math.Abs(actual - expected);
        absDelta
            .Should()
            .BeLessThan(
                maxDelta,
                $"Actual '{actual}' is not close to expected '{expected}'. The found delta '{absDelta}' is higher than the max allowed '{maxDelta}'");
    }
}