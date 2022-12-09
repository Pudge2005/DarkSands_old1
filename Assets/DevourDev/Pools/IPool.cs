namespace DevourDev.Pools
{
    public interface IPool<T> where T : class
    {
        T Rent();
        void Return(T rentedItem);
        void Clear();
    }
}
