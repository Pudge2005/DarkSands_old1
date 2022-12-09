using System.Runtime.CompilerServices;

namespace DevourDev.Numerics
{
    public struct FastDecimal32
    {
        private const int _div = 100;

        private readonly int _data;


        internal FastDecimal32(int data)
        {
            _data = data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(FastDecimal32 v)
        {
            return v._data / _div;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FastDecimal32(int v)
        {
            return new FastDecimal32(v * _div);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float(FastDecimal32 v)
        {
            return (float)v._data / _div;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator FastDecimal32(float v)
        {
            return new FastDecimal32((int)(v * _div));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FastDecimal32 operator +(FastDecimal32 a, FastDecimal32 b)
        {
            return new FastDecimal32(a._data + b._data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FastDecimal32 operator -(FastDecimal32 a, FastDecimal32 b)
        {
            return new FastDecimal32(a._data - b._data);
        }
    }
}
