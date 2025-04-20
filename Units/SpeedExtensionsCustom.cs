namespace Units;

public static class SpeedExtensionsCustom
{
    public static Speed LimitWithAccelerationAndTime(this Speed value, Speed previousValue, Acceleration maxAcceleration, Time deltaTime)
    {
        var minSpeed = previousValue - (maxAcceleration.Abs() * deltaTime);
        var maxSpeed = previousValue + (maxAcceleration.Abs() * deltaTime);

        return value.ClampWith(firstBoundary: minSpeed, secondBoundary: maxSpeed);
    }
}