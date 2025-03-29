<!-- TOC -->
* [Intro](#intro)
* [Concept](#concept)
  * [Reciprocity of operations](#reciprocity-of-operations)
  * [Comparison operators](#comparison-operators)
  * [IBaseValue\<T>](#ibasevaluet)
  * [Serialization](#serialization)
* [Examples](#examples)
  * [Basics](#basics)
  * [Headings](#headings)
  * [Clamping](#clamping)
  * [Abs (IBaseValue\<T>)](#abs-ibasevaluet)
  * [Inter- extrapolation (IBaseValue\<T>)](#inter--extrapolation-ibasevaluet)
  * [MaxVal / MinVal (IBaseValue\<T>)](#maxval--minval-ibasevaluet)
  * [AverageVal (IBaseValue\<T>)](#averageval-ibasevaluet)
  * [SumVal (IBaseValue\<T>)](#sumval-ibasevaluet)
  * [Ratios](#ratios)
  * [Points](#points)
<!-- TOC -->

# Intro

My personal version on doing units in c#

Goals:
- Increase readability of code by hiding bits and bytes of the calculations
- Provide type safety when handling physical quantities
- Prevent unit conversion and other calculation mistakes
- Make it's author happy

# Concept
The units wraps decimal in structs for different kind of "quantities", at the time of writing:

| Value name     | Comment                                                                                           | Implements IBaseValue\<T> <br/>(see below) |
|----------------|---------------------------------------------------------------------------------------------------|--------------------------------------------|
| Acceleration   |                                                                                                   | Yes                                        |
| Angle          |                                                                                                   | Yes                                        |
| Distance       |                                                                                                   | Yes                                        |
| Acceleration   |                                                                                                   | Yes                                        |
| Heading        | An angle with the particularity that it's reduced to the range -180° to 180°                      | No                                         |
| HeadingSpeed   | An angular speed related to the change of heading over time                                       | Yes                                        |
| Point2D        | Represent a position in the plane, and are composed of 2 'Distance' quantities.                   | No                                         |
| Point3D        | Represent a position in the room, and are composed of 3 'Distance' quantities.                    | No                                         |
| Product<TA,TB> | for unusual products                                                                              | No                                         |
| Ratio<TA,TB>   | for unusual ratios like speed ramps (m/s)/m which would be 1/s or Hz which usually is a frequency | No                                         |
| Speed          |                                                                                                   | Yes                                        |
| Time           |                                                                                                   | Yes                                        |

The different quantity types provide different operators and methods to do calculations with them.

## Reciprocity of operations
The implemented operators must be reciprocal, meaning:
- when a + b = c then a = c - b
- when a x b = c then a = c / b.

That's why Heading does not implement multiplication or division by a number.
assume a = 170°, and b = 175°.
- a + b =  c = 345° which gets reduced to -15°. So c - b: (-15°) - 175° = -190° which gets reduced to 170°, and b - c = a is true.
- but a x 2 = c = 340° which gets reduced to -20°. And c / 2 = -10° which is nowhere near a.

## Comparison operators
With the exception of composite types (Points) the operators <, >, <=, >= are implemented as expected.
The operators == and != are not implemented as it would require to define a tolerance, instead methods IsCloseTo and IsNotCloseTo are provided, these take a maxDelta as additional argument.

## IBaseValue\<T>

Most of the values implement IBaseValue<T> which allows to use some generic math like, Max, Min, Average, Sum, etc (see below for examples).

## Serialization

All values do serialize when using `System.Text.Json.JsonSerializer`.

# Examples
The value system allows to do things like

## Basics
```csharp
var a = 410f.MilliMetersPerSecondSquared();
Console.WriteLine($"a: {a}");
// a: 0.41 [m/s^2]

var t = 1.7f.Seconds();
Console.WriteLine($"t {t}"); t 1.7 [s]
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
```

## Headings

```csharp
var h1 = 45.Degrees();
var h2 = (MathF.PI / 2).Radians();
var h3 = Heading.Average(h1, h2);
Console.WriteLine($"Heading 1: {h1}, heading 2: {h2}, average: {h3}");
// Heading 1: 45 [°], heading 2: 90 [°], average: 67.5 [°]
```

## Clamping
```csharp
var v = 4.MetersPerSecond();
var clampedSpeed = v.ClampWith(firstBoundary: Speed.Zero, secondBoundary: 0.5f.MetersPerSecond());
Console.WriteLine($"Clamped speed: {clampedSpeed}");
// Clamped speed: 0.5 [m/s]

var clampedSpeed2 = v.ClampWith(firstBoundary: 0.5f.MetersPerSecond(), secondBoundary: Speed.Zero);
Console.WriteLine($"Clamped speed 2 (first boundary needs not been less than second one): {clampedSpeed2}");
// Clamped speed 2 (first boundary needs not been less than second one): 0.5 [m/s]
```

## Abs (IBaseValue\<T>)

```csharp
var negativeSpeed = -5.MetersPerSecond();
var absSpeed = negativeSpeed.Abs();
Console.WriteLine($"negative speed: {negativeSpeed}, abs {absSpeed}");
// negative speed: -5 [m/s], abs 5 [m/s]
```

## Inter- extrapolation (IBaseValue\<T>)

```csharp
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
```
## MaxVal / MinVal (IBaseValue\<T>)

```csharp
var d1 = 1m.Meters();
var d2 = 2000m.MilliMeters();
var d3 = 3m.Meters();

var max2 =  BaseValueMath.MaxVal(d1, d2);
Console.WriteLine(max2);  // outputs: 2 [m]

var max3 = BaseValueMath.MaxVal(d1, d2, d3); // can be used with 2 or more parameters
Console.WriteLine(max3); // outputs: 3 [m]

var min = BaseValueMath.MinVal(d1, d2, d3); // can be used with 2 or more parameters
Console.WriteLine(min);  // outputs: 1 [m]
```
Or with collections
```csharp
var d1 = 1m.Meters();
var d2 = 2000m.MilliMeters();
var d3 = 3m.Meters();

var distances = new[] {d1, d2, d3}.ToList(); // Can be any IEnumberable<IBaseValue<T>>

var max = new[] distances.MaxVal();
Console.WriteLine(max); // outputs: 3 [m]

var min = distances.MinVal();
Console.WriteLine(min);  // outputs: 1 [m]
```

## AverageVal (IBaseValue\<T>)

```csharp
var d1 = 1m.Meters();
var d2 = 22.87m.MilliMeters();
var d3 = 3.05m.Meters();
var d4 = -5m.Meters();

var average1 = BaseValueMath.AverageVal(d1, d2); // 2 or more parameters 
Console.WriteLine(average1); // Outputs:0.511435 [m]

var average2 = BaseValueMath.AverageVal(d1, d2, d3, d4); // 2 or more parameters 
Console.WriteLine(average2); // Outputs: -0.2317825 [m]
```
Or with collections
```csharp
var d1 = 1m.Meters();
var d2 = 22.87m.MilliMeters();
var d3 = 3.05m.Meters();
var d4 = -5m.Meters();

var distances = new[] {d1, d2, d3, d4}.ToList(); // Can be any IEnumberable<IBaseValue<T>>

var average2 = distances.AverageVal(d1, d2, d3, d4);
Console.WriteLine(average2); // Outputs: -0.2317825 [m]
```

## SumVal (IBaseValue\<T>)
```csharp
var d1 = 1m.Meters();
var d2 = 22.87m.MilliMeters();
var d3 = 3.05m.Meters();
var d4 = -5m.Meters();

var sum1 = BaseValueMath.SumVal(d1, d2); // 2 or more parameters 
Console.WriteLine(sum1); // Outputs: 1.02287 [m]

var sum2 = BaseValueMath.SumVal(d1, d2, d3, d4); // 2 or more parameters
Console.WriteLine(sum2); // Outputs: -0.92713 [m]
```

Or with collections
```csharp
var d1 = 1m.Meters();
var d2 = 22.87m.MilliMeters();
var d3 = 3.05m.Meters();
var d4 = -5m.Meters();

var distances = new[] {d1, d2, d3, d4}.ToList(); // Can be any IEnumberable<IBaseValue<T>>

var sum = distances.SumVal(d1, d2, d3, d4);
Console.WriteLine(sum); // Outputs: -0.92713 [m]
```

## Ratios
```csharp 
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
```

## Points
```csharp 
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
```