using System;

namespace DevourDev.Pools
{
    public readonly struct PooledItem<T> : IDisposable
        where T : class
    {
        private readonly IPool<T> _pool;
        private readonly T _item;


        public PooledItem(IPool<T> pool, T item)
        {
            _pool = pool;
            _item = item;
        }


        public void Dispose()
        {
            _pool.Return(_item);
        }
    }
}
