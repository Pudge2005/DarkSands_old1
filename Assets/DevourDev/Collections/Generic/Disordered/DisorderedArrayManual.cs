using System.Runtime.CompilerServices;

namespace DevourDev.Base.Collections.Generic
{
    public class DisorderedArrayManual<T> where T : IDisorderedArrayItem
    {
        private T[] _items;

        private int _lastElementIndex = -1;


        public DisorderedArrayManual(int initialCapacity = 0)
        {
            if (initialCapacity == 0)
                _items = System.Array.Empty<T>();
            else
                _items = new T[initialCapacity];
        }


        public T[] Array => _items;
        public int LastIndex => _lastElementIndex;
        public bool Empty => _lastElementIndex < 0;


        //  example:Royal Punch
        //    var arr = _evaluatables.Array;
        //    var count = _evaluatables.Count;

        //        for (int i = -1; ++i<count;)
        //        {
        //            arr[i].Evaluate();
        //         }


        public void Add(T item)
        {
            int index = ++_lastElementIndex;

            if (index == _items.Length)
                System.Array.Resize<T>(ref _items, (index == 0 ? 4 : index) * 2);

            _items[index] = item;
            item.DisorderedArrayIndex = index;
        }

        public void RemoveAt(int index)
        {
            if (index == _lastElementIndex)
            {
                if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
                    _items[index] = default!;
            }
            else
            {
                _items[index] = _items[_lastElementIndex];
                _items[index].DisorderedArrayIndex = index;
            }

            --_lastElementIndex;
        }

        public void Remove(T item)
        {
            RemoveAt(item.DisorderedArrayIndex);
        }

        public void Clear()
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                int count = _lastElementIndex + 1;

                for (int i = 0; i < count; i++)
                {
                    _items[i] = default!;
                }
            }

            _lastElementIndex = -1;
        }
    }
}
