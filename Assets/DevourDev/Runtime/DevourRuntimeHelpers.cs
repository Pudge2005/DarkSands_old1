using System;

namespace DevourDev.Base.Runtime
{
    public static class DevourRuntimeHelpers
    {
        public static void ThrowIfNegative(float v)
        {
            if (v < 0)
                throw new Exception($"value should not be negative ({v})");
        }

        public static void ThrowIfNegative(double v)
        {
            if (v < 0)
                throw new Exception($"value should not be negative ({v})");
        }

        public static void ThrowIfNegative(int v)
        {
            if (v < 0)
                throw new Exception($"value should not be negative ({v})");
        }

        public static void ThrowIfNegative(long v)
        {
            if (v < 0)
                throw new Exception($"value should not be negative ({v})");
        }

        public static void ThrowIfNaN(float v)
        {
            if (float.IsNaN(v))
                throw new Exception($"value should not be NaN");
        }

        public static void ThrowIfNaN(double v)
        {
            if (double.IsNaN(v))
                throw new Exception($"value should not be NaN");
        }

        public static void ThrowIfInfinityOrNaN(double v)
        {
            ThrowIfNaN(v);
            ThrowIfInfinity(v);
        }

        public static void ThrowIfInfinity(double v)
        {
            if (double.IsInfinity(v))
                throw new Exception($"value should not be infinity");
        }
    }
}
