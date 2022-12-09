using System;

namespace DevourDev.Pools
{
    public class PoolablesPool<T> : IPool<T>
        where T : class, IPoolable
    {
        private readonly T[] _items;
        private readonly int _capacity;
        private int _size;

        private System.Func<T> _createItemMethod;
        private System.Action<T> _disposeItemMethod;


        public PoolablesPool(int poolCapacity)
        {
            _items = new T[poolCapacity];
            _capacity = poolCapacity;
        }


        public void SetItemCreationMethod(Func<T> method) => _createItemMethod = method;
        public void SetItemDisposingMethod(Action<T> method) => _disposeItemMethod = method;


        public T Rent()
        {
            T item;

            if (_size > 0)
            {
                item = _items[--_size];
            }
            else
            {
                item = _createItemMethod();
            }

            if(item == null)
            {
                int x = 10;
            }

            item.HandleRenting();
            return item;
        }

        public void Return(T rentedItem)
        {
            if (_size < _capacity)
            {
                rentedItem.HandleReturning();
                _items[_size++] = rentedItem;
            }
            else
            {
                UnityEngine.Debug.Log($"size: {_size}, capacity: {_capacity}, BUT DISPOSED!!");
                _disposeItemMethod(rentedItem);
            }
        }

        public void Clear()
        {
            var arr = _items;
            var c = _size;
            for (int i = 0; i < c; i++)
            {
                _disposeItemMethod(arr[c]);
            }

            Array.Clear(arr, 0, c);
            _size = 0;
        }
    }
}
