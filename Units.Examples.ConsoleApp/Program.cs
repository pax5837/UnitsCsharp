using Units;

Basics();
Headings();
ClampingExamples();
Abs();
InterpolateExtrapolate();
Ratio();
Points();

static void Basics()
{
    var a = 410f.MilliMetersPerSecondSquared();
    Console.WriteLine($"a: {a}");
    // a: 0.41 [m/s^2]

    var t = 1.7f.Seconds();
    Console.WriteLine($"t {t}");
    // t 1.7 [s]

    var v = a * t;
    Console.WriteLine($"v = a * t: {v}");
    //v = a * t: 0.697 [m/s]

    var v1 = -0.4f.MetersPerSecond();
    Console.WriteLine($"v ({v}) + v1 ({v1}): {v + v1}");
    // v (0.697 [m/s]) + v1 (-0.4 [m/s]): 0.29700002 [m/s]

    var v2 = 0.5f.MetersPerSecond();
    Console.WriteLine($"Speed lower than {v2}: {v < v2}");
    // Speed lower than 0.5 [m/s]: False
}

static void Headings()
{
    var h1 = 45.Degrees();
    var h2 = (MathF.PI / 2).Radians();
    var h3 = Heading.Average(h1, h2);
    Console.WriteLine($"Heading 1: {h1}, heading 2: {h2}, average: {h3}");
    // Heading 1: 45 [°], heading 2: 90 [°], average: 67.5 [°]
}

static void ClampingExamples()
{
    var v = 4.MetersPerSecond();
    var clampedSpeed = v.ClampWith(firstBoundary: Speed.Zero, secondBoundary: 0.5f.MetersPerSecond());
    Console.WriteLine($"Clamped speed: {clampedSpeed}");
    // Clamped speed: 0.5 [m/s]

    var clampedSpeed2 = v.ClampWith(firstBoundary: 0.5f.MetersPerSecond(), secondBoundary: Speed.Zero);
    Console.WriteLine($"Clamped speed 2 (first boundary needs not been less than second one): {clampedSpeed2}");
    // Clamped speed 2 (first boundary needs not been less than second one): 0.5 [m/s]
}

static void Abs()
{
    var negativeSpeed = -5.MetersPerSecond();
    var absSpeed = negativeSpeed.Abs();
    Console.WriteLine($"negative speed: {negativeSpeed}, abs {absSpeed}");
    // negative speed: -5 [m/s], abs 5 [m/s]
}

static void InterpolateExtrapolate()
{
    Console.WriteLine("Interpolate / Extrapolate");
    var distance1 = 4.Meters();
    var extrapolatedSpeed = distance1.ExtrapolateLinearly(
        x1: 1.Meters(),
        y1: 10.MetersPerSecond(),
        x2: 2.Meters(),
        y2: 12.MetersPerSecond());
    Console.WriteLine($"Extrapolated speed: {extrapolatedSpeed} for distance: {distance1}, with 'extrapolatedSpeed = distance1.ExtrapolateLinearly(1.Meters(), 10.MetersPerSecond(), 2.Meters(), 12.MetersPerSecond());'");
    // Extrapolated speed: 16 [m/s] for distance: 4 [m], with 'extrapolatedSpeed = distance1.ExtrapolateLinearly(1.Meters(), 10.MetersPerSecond(), 2.Meters(), 12.MetersPerSecond());'


    var extrapolatedAndClampedSpeed = distance1.ExtrapolateAndClamp(
        x1: 1.Meters(),
        y1: 10.MetersPerSecond(),
        x2: 2.Meters(),
        y2: 12.MetersPerSecond());
    Console.WriteLine($"Extrapolated and clamped speed: {extrapolatedAndClampedSpeed} for distance: {distance1}, with 'extrapolatedAndClampedSpeed = distance1.ExtrapolateAndClamp(1.Meters(), 10.MetersPerSecond(), 2.Meters(), 12.MetersPerSecond())'");
    // Extrapolated and clamped speed: 12 [m/s] for distance: 4 [m], with 'extrapolatedAndClampedSpeed = distance1.ExtrapolateAndClamp(1.Meters(), 10.MetersPerSecond(), 2.Meters(), 12.MetersPerSecond())'
}

static void Ratio()
{
    var ratio = new Ratio<Speed, Distance>(
        numerator: 2.8m.MetersPerSecond(),
        denominator: 0.5m.Meters());
    Console.WriteLine($"ratio {ratio}");
    // ratio Ratio Numerator: 2.8 [m/s], Denominator: 0.5 [m], RatioValue: 5.6

    var speed = 2.3f.MetersPerSecond();
    var calculatedDistance = speed / ratio;
    Console.WriteLine($"Calculated distance {calculatedDistance}");
    // Calculated distance 0.41 [m]

    var inversedRatio = ratio.Inverse();
    Console.WriteLine($"Inversed ratio {inversedRatio}");
    // Inversed ratio, Numerator: 2.8 [m/s], Denominator: 0.5 [m], RatioValue: 0.1785714285714285714285714286

    var serializedRatio = System.Text.Json.JsonSerializer.Serialize(ratio);
    Console.WriteLine($"Serialized ratio: {serializedRatio}");
    //Serialized ratio: {"Numerator":{"MetersPerSecond":2.8},"Denominator":{"Meters":0.5}}
}

static void Points()
{
    var p1 = new Point2D(2.Meters(), 3.Meters());
    Console.WriteLine($"p1: {p1}");
    // p1: x: 2 [m] y: 3 [m]

    var p2 = new Point2D(4.Meters(), 6.Meters());
    Console.WriteLine($"p2: {p2}");
    // p2: x: 4 [m] y: 6 [m]

    var d = p1.DistanceTo(p2);
    Console.WriteLine($"Distance between p1 and p2: {d}");
    // Distance between p1 and p2: 3.6055512 [m]

    var heading = p1.CalculateHeadingToOther(p2);
    Console.WriteLine($"Heading p1 to p2: {heading}");
    // Heading p1 to p2: 56.309937 [°]

    var h1 = 0.Degrees();
    var distanceAlongH1 = p1.AbsDistanceToParallelToHeading(p2, h1);
    Console.WriteLine($"Distance p1 to p2 along heading {h1}: {distanceAlongH1}");
    // Distance p1 to p2 along heading 0 [°]: 2.0000002 [m]

    var distancePerpendicularToH1 = p1.AbsDistanceToOrthogonalToHeading(p2, h1);
    Console.WriteLine($"Distance p1 to p2 perpendicular to heading {h1}: {distancePerpendicularToH1}");
    // Distance p1 to p2 perpendicular to heading 0 [°]: 3 [m]
}