using DevourDev.Pools;
using UnityEngine;

namespace DevourDev.Unity.Pools
{
    public abstract class PoolableComponentsPool<T> : MonoBehaviour, IPool<T>
        where T : PoolableComponent<T>
    {
        [SerializeField] private int _poolCapacity = 2048;
        private PoolablesPool<T> _pool;


        public T Rent() => _pool.Rent();
        public void Return(T rentedItem) => _pool.Return(rentedItem);
        public void Clear() => _pool.Clear();


        protected virtual void Awake()
        {
            _pool = new PoolablesPool<T>(_poolCapacity);
            _pool.SetItemCreationMethod(CreateItemInternal);
            _pool.SetItemDisposingMethod(DisposeItem);
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            Clear();
        }

        private T CreateItemInternal()
        {
            var item = CreateItem();
            item.InitPoolable(this);
            return item;
        }


        protected abstract T CreateItem();
        protected abstract void DisposeItem(T item);
    }
}
