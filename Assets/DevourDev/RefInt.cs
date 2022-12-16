namespace DevourDev.Base.Numerics
{

    public sealed class RefInt
    {
        public int Value;


        public RefInt(int value)
        {
            Value = value;
        }


        public void Add(int v)
            => Value += v;


        public void Set(int v)
            => Value = v;


        public override bool Equals(object obj)
        {
            return obj is RefInt @int &&
                   Value == @int.Value;
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static implicit operator int(RefInt v)
        {
            return v.Value;
        }

        public static bool operator ==(int x, RefInt y)
        {
            return x == y.Value;
        }

        public static bool operator !=(int x, RefInt y)
        {
            return x != y.Value;
        }

        public static bool operator ==(RefInt x, int y)
        {
            return x.Value == y;
        }

        public static bool operator !=(RefInt x, int y)
        {
            return x.Value != y;
        }
    }
}
