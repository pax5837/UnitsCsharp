using System.Text.Json.Serialization;

namespace Units
{
    /// <summary>
    /// Represents a heading speed in radians per second.
    /// </summary>
    public readonly struct HeadingSpeed : IBaseValue<HeadingSpeed>
    {
        public static readonly HeadingSpeed Zero = HeadingSpeed.FromRadiansPerSecond(0);
        
        public decimal RadiansPerSecond { get; }

        [JsonIgnore]
        public decimal DegreesPerSecond => (decimal)(RadiansPerSecond * 180m / ((decimal)Math.PI));

        [Obsolete("Should only be used for deserialization", error: true)]
        [JsonConstructor]
        public HeadingSpeed(decimal radiansPerSecond)
        {
            RadiansPerSecond = radiansPerSecond;
        }

        /// <summary>
        /// Used as an internal constructor.
        /// </summary>
        public HeadingSpeed(decimal radiansPerSecond, bool _)
        {
            RadiansPerSecond = radiansPerSecond;
        }

        public override string ToString()
        {
            return $"{RadiansPerSecond} [RadiansPerSecond]";
        }

        public static HeadingSpeed FromRadiansPerSecond(decimal radiansPerSecond)
        {
            return new HeadingSpeed(radiansPerSecond, true);
        }

        public static HeadingSpeed FromDegreesPerSecond(decimal degreesPerSecond)
        {
            return new HeadingSpeed(degreesPerSecond / 180m * (decimal)Math.PI, true);
        }

        public decimal GetBaseValue()
        {
            return RadiansPerSecond;
        }

        public HeadingSpeed FromBaseValue(decimal value)
        {
            return new HeadingSpeed(value, true);
        }
        
        public static HeadingSpeed operator +(HeadingSpeed hs1, HeadingSpeed hs2) => FromRadiansPerSecond(hs1.RadiansPerSecond + hs2.RadiansPerSecond);
        public static HeadingSpeed operator -(HeadingSpeed hs1, HeadingSpeed hs2) => FromRadiansPerSecond(hs1.RadiansPerSecond - hs2.RadiansPerSecond);
        public static HeadingSpeed operator *(decimal val, HeadingSpeed hs) => FromRadiansPerSecond(val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(double val, HeadingSpeed hs) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(float val, HeadingSpeed hs) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(int val, HeadingSpeed hs) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(HeadingSpeed hs,decimal val) => FromRadiansPerSecond(val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(HeadingSpeed hs,double val) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(HeadingSpeed hs, float val) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static HeadingSpeed operator *(HeadingSpeed hs, int val) => FromRadiansPerSecond((decimal)val * hs.RadiansPerSecond);
        public static decimal operator /(HeadingSpeed hs1, HeadingSpeed hs2) => hs1.RadiansPerSecond / hs2.RadiansPerSecond;
        
        public static bool operator <(HeadingSpeed a, HeadingSpeed b) => a.RadiansPerSecond < b.RadiansPerSecond;
        public static bool operator >(HeadingSpeed a, HeadingSpeed b) => a.RadiansPerSecond > b.RadiansPerSecond;
        public static bool operator <=(HeadingSpeed a, HeadingSpeed b) => a.RadiansPerSecond <= b.RadiansPerSecond;
        public static bool operator >=(HeadingSpeed a, HeadingSpeed b) => a.RadiansPerSecond >= b.RadiansPerSecond;
    }
}