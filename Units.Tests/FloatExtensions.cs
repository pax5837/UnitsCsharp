namespace Units.Tests;

internal static class FloatExtensions
{
    public static void ShouldBeCloseTo(this float actual, float expected, float maxDelta)
    {
        var absDelta = Math.Abs(actual - expected);
        absDelta
            .Should()
            .BeLessThan(
                maxDelta,
                $"Actual '{actual}' is not close to expected '{expected}'. The found delta '{absDelta}' is higher than the max allowed '{maxDelta}'");
    }
}