namespace Units
{
    public static class HeadingSpeedExtensions
    {
        public static HeadingSpeed DegreesPerSecond(this decimal degreesPerSecond)
        {
            return HeadingSpeed.FromDegreesPerSecond(degreesPerSecond);
        }

        public static HeadingSpeed DegreesPerSecond(this float degreesPerSecond)
        {
            return HeadingSpeed.FromDegreesPerSecond((decimal)degreesPerSecond);
        }

        public static HeadingSpeed DegreesPerSecond(this double degreesPerSecond)
        {
            return HeadingSpeed.FromDegreesPerSecond((decimal)degreesPerSecond);
        }

        public static HeadingSpeed DegreesPerSecond(this int degreesPerSecond)
        {
            return HeadingSpeed.FromDegreesPerSecond((decimal)degreesPerSecond);
        }

        public static HeadingSpeed RadiansPerSecond(this decimal radiansPerSecond)
        {
            return HeadingSpeed.FromRadiansPerSecond(radiansPerSecond);
        }

        public static HeadingSpeed RadiansPerSecond(this float radiansPerSecond)
        {
            return HeadingSpeed.FromRadiansPerSecond((decimal)radiansPerSecond);
        }

        public static HeadingSpeed RadiansPerSecond(this double radiansPerSecond)
        {
            return HeadingSpeed.FromRadiansPerSecond((decimal)radiansPerSecond);
        }
    }
}