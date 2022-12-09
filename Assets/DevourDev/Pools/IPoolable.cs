namespace DevourDev.Pools
{
    public interface IPoolable
    {
        void HandleRenting();
        void HandleReturning();
    }
}
