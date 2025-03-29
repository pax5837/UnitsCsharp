using System.Text.Json.Serialization;

namespace Units;

public readonly struct Point3D
{
    public Distance X { get; }
    public Distance Y { get; }
    public Distance Z { get; }

    [JsonConstructor]
    public Point3D(Distance x, Distance y, Distance z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point2D GetXYPoint()
    {
        return new Point2D(X, Y);
    }
}