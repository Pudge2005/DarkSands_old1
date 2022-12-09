using System.Runtime.CompilerServices;

namespace DevourDev.Numerics
{
    public struct FastDecimal64
    {
        private const long _div = 100;

        private readonly long _data;


        internal FastDecimal64(long data)
        {
            _data = data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator long(FastDecimal64 v)
        {
            return v._data / _div;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FastDecimal64(long v)
        {
            return new FastDecimal64(v * _div);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float(FastDecimal64 v)
        {
            return (float)v._data / _div;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator FastDecimal32(FastDecimal64 v)
        {
            return new FastDecimal32((int)v._data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FastDecimal64(FastDecimal32 v)
        {
            return new FastDecimal64((int)v * _div);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FastDecimal64(float v)
        {
            return new FastDecimal64((long)(v * _div));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FastDecimal64 operator +(FastDecimal64 a, FastDecimal64 b)
        {
            return new FastDecimal64(a._data + b._data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FastDecimal64 operator -(FastDecimal64 a, FastDecimal64 b)
        {
            return new FastDecimal64(a._data - b._data);
        }
    }
}
