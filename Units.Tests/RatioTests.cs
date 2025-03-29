namespace Units.Tests;

public class RatioTests
{
    private decimal maxDelta = 0.000_001m;

    [Test]
    public void RatioTest()
    {
        var ratio = new Ratio<Speed, Distance>(3.MetersPerSecond(), 1.Meters());

        var s = ratio * 2.Meters();
        s.MetersPerSecond.ShouldBeCloseTo(6, maxDelta);

        var s2 = 2.Meters() * ratio;
        s2.MetersPerSecond.ShouldBeCloseTo(6, maxDelta);

        var d = 9.MetersPerSecond() / ratio;
        d.Meters.ShouldBeCloseTo(3, maxDelta);
    }
    
    [Test]
    public void Ratio_IsSerializable()
    {
        var ratio = new Ratio<Speed, Distance>(3.MetersPerSecond(), 1.Meters());

        var serialized = System.Text.Json.JsonSerializer.Serialize(ratio);

        serialized.Should().BeEquivalentTo("{\"Numerator\":{\"MetersPerSecond\":3},\"Denominator\":{\"Meters\":1}}");
        
        var deserialized = System.Text.Json.JsonSerializer.Deserialize<Ratio<Speed, Distance>>(serialized);
        
        deserialized.RatioValue.ShouldBeCloseTo(ratio.RatioValue, maxDelta);
    }
}