using DevourDev.Pools;
using UnityEngine;

namespace DevourDev.Unity.Pools
{
    public abstract class PoolableComponent<T> : MonoBehaviour, IPoolable
        where T : PoolableComponent<T>
    {
        private PoolableComponentsPool<T> _pool;


        public void InitPoolable(PoolableComponentsPool<T> pool)
        {
            _pool = pool;
            DontDestroyOnLoad(gameObject);
        }


        public void HandleRenting()
        {
            HandleRentingInternal();
        }

        public void HandleReturning()
        {
            HandleReturnedInternal();
        }


        protected void ReturnToPool()
        {
            _pool.Return((T)this);
        }


        protected abstract void HandleRentingInternal();
        protected abstract void HandleReturnedInternal();


    }
}
