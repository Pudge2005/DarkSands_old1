using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DevourDev.Base.Collections.Generic
{
    public class DisorderedArray<T> : IEnumerable, IEnumerable<T>
    {
        public sealed class Bucket
        {
            private readonly DisorderedArray<T> _parent;

            private bool _inArray;

            internal T _item;
            internal int _disorderedArrayIndex;


            internal Bucket(T item, int disorderedArrayIndex, DisorderedArray<T> parent)
            {
                _item = item;
                _disorderedArrayIndex = disorderedArrayIndex;
                _parent = parent;
                _inArray = true;

            }

            internal Bucket(T item, DisorderedArray<T> parent)
            {
                _item = item;
                _parent = parent;
                _inArray = false;
            }


            public T Item => _item;
            public bool InArray => _inArray;


            public void RemoveFromArray()
            {
                if (!_inArray)
                    return;

                _parent.RemoveBucketInternal(this);
                _inArray = false;
            }

            public void ReturnToArray()
            {
                if (_inArray)
                    return;

                _parent.ReturnBucketInternal(this);
                _inArray = true;
            }
        }


        private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private readonly Bucket[] _arr;
            private readonly int _size;
            private int _index;


            internal Enumerator(Bucket[] arr, int size)
            {
                _arr = arr;
                _size = size;
                _index = -1;
            }


            public T Current => _arr[_index]._item;

            object IEnumerator.Current => _arr[_index]._item;


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                ++_index;
                return _index < _size;
            }

            public void Reset()
            {
                _index = -1;
            }

        }



        private Bucket[] _buckets;
        private int _lastIndex = -1;
        private int _capacity;


        public DisorderedArray(int initialCapacity = 0)
        {
            if (initialCapacity > 0)
            {
                InitBuffer(initialCapacity);
            }
            else
            {
                _capacity = -1;
            }
        }


        public bool Empty => _lastIndex < 0;
        public int Count => _lastIndex + 1;


        /// <returns>wrapper-optimizer</returns>
        public Bucket Add(T item)
        {
            var index = ++_lastIndex;

            if (_capacity < 0)
            {
                InitBuffer(8);
            }
            else
            {
                EnsureCapacity(index);
            }

            var b = new Bucket(item, index, this);
            _buckets[index] = b;
            return b;
        }

        /// <summary>
        /// wraps item in bucket but not adding it
        /// </summary>
        public Bucket WrapItem(T item)
        {
            return new Bucket(item, this);
        }

        /// <summary>
        /// non efficient,
        /// recomended to use Bucket.RemoveFromArray()
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            var arr = _buckets;
            var c = _lastIndex + 1;
            for (int i = -1; ++i < c;)
            {
                Bucket b = arr[i];
                if (b._item.Equals(item))
                {
                    b.RemoveFromArray();
                    return;
                }
            }
        }


        internal void RemoveBucketInternal(Bucket b)
        {
            int index = b._disorderedArrayIndex;
            int lastIndex = _lastIndex;

            if (index != lastIndex)
            {
                _buckets[index] = null;
            }
            else
            {
                var x = _buckets[lastIndex];
                _buckets[index] = x;
                x._disorderedArrayIndex = index;
                _buckets[lastIndex] = null;
            }

            --_lastIndex;
        }


        internal void ReturnBucketInternal(Bucket b)
        {
            var index = ++_lastIndex;
            EnsureCapacity(index);
            _buckets[index] = b;
            b._disorderedArrayIndex = index;
        }

        private void InitBuffer(int initialCapacity)
        {
            _buckets = new Bucket[initialCapacity];
            _capacity = initialCapacity;
        }

        private void EnsureCapacity(int min)
        {
            if (min >= _capacity)
            {
                _capacity = min * 2;
                System.Array.Resize(ref _buckets, _capacity);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(_buckets, _lastIndex + 1);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(_buckets, _lastIndex + 1);
        }
    }
}
