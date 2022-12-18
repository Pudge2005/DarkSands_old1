using System.Runtime.CompilerServices;

namespace DevourDev.Base.Collections.Generic
{
    /// <summary>
    /// Stil not so faster than System.Queue, but atl exposed...
    /// Not thread-safe for performance reasons
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedQueue<T>
    {
        private readonly T[] _items;
        private readonly int _capacity;
        private readonly bool _isReference;
        private int _size;
        private int _tail;
        private int _head;


        public FixedQueue(int capacity)
        {
            _isReference = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            _capacity = capacity;
            _items = new T[capacity];

            _tail = capacity;
            _head = capacity - 1;
        }


        public bool Empty => _size == 0;
        public bool Full => _size == _capacity;
        public int Count => _size;

        //DEBUG

        public T[] Array => _items;
        public int Capacity => _capacity;
        public int TailIndex => _tail;
        public int HeadIndex => _head;

        //DEBUG


        public T GetHeader()
            => _items[_head];

        public T GetTailer()
            => _items[_tail];


        public bool TryGetHeader(out T header)
        {
            bool res = !Empty;
            header = res ? _items[_head] : default!;
            return res;
        }

        public bool TryGetTailer(out T tailer)
        {
            bool res = !Empty;
            tailer = res ? _items[_tail] : default!;
            return res;
        }


        public void InsertBefore(T item, int index)
        {
            if (index == 0)
            {
                Enqueue(item);
                return;
            }

            ++_size;

            if (index > _capacity / 2)
            {
                if (++_head == _capacity)
                    _head = 0;

                T tmp = _items[index];

                for (int i = index; ;)
                {
                    if (++i == _capacity)
                        i = 0;

                    if (i == _tail)
                        break;

                    (tmp, _items[i]) = (_items[i], tmp);
                }
            }
            else
            {
                if (--_tail < 0)
                    _tail = _capacity - 1;

                int i = index - 1;

                if (i < 0)
                    i = _capacity - 1;

                T tmp = _items[i];

                for (; ; )
                {
                    if (--i < 0)
                        i = _capacity - 1;

                    if (i == _head)
                        break;

                    (tmp, _items[i]) = (_items[i], tmp);
                }
            }

            if(index >= _capacity || index < 0)
            {
                throw new System.Exception($"index: {index}, capacity: {_capacity}");
            }

            _items[index] = item;
        }

        public void InsertAfter(T item, int index)
        {
            ++_size;

            if (index == _head)
            {
                if (++_head == _capacity)
                    _head = 0;

                _items[_head] = item;
                return;
            }

            ++index;

            if (index > _capacity / 2)
            {
                if (++_head == _capacity)
                    _head = 0;

                T tmp = _items[index];

                for (int i = index; ;)
                {
                    if (++i == _capacity)
                        i = 0;

                    if (i == _tail)
                        break;

                    (tmp, _items[i]) = (_items[i], tmp);
                }
            }
            else
            {
                if (--_tail < 0)
                    _tail = _capacity - 1;

                int i = index - 1;

                if (i < 0)
                    i = _capacity - 1;

                T tmp = _items[i];

                for (; ; )
                {
                    if (--i < 0)
                        i = _capacity - 1;

                    if (i == _head)
                        break;

                    (tmp, _items[i]) = (_items[i], tmp);
                }
            }

            _items[index] = item;
        }



        public void Enqueue(T item)
        {
            ++_size;
            //MoveBack(ref _tail);

            if (--_tail < 0)
                _tail = _capacity - 1;

            _items[_tail] = item;
        }

        public T Dequeue()
        {
            --_size;
            var v = _items[_head];

            if (_isReference)
                _items[_head] = default!;

            //MoveBack(ref _head);

            if (--_head < 0)
                _head = _capacity - 1;

            return v;
        }

        public void Dequeue_void()
        {
            --_size;

            if (_isReference)
                _items[_head] = default!;

            //MoveBack(ref _head);

            if (--_head < 0)
                _head = _capacity - 1;
        }




        #region slowpokes...

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromHeadToTail(System.Action<T> action)
        //{
        //    if (Empty)
        //        throw new System.Exception("Trying to iterate through EMPTY queue");

        //    int i = _head;
        //    for (; ; )
        //    {
        //        action(_items[i]);

        //        if (i == _tail)
        //            return;

        //        if (--i < 0)
        //            i = _capacity - 1;
        //    }
        //}

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromTailToHead(System.Action<T> action)
        //{
        //    int i = _tail;
        //    for (; ; )
        //    {
        //        action(_items[i]);

        //        if (++i == _capacity)
        //            i = 0;

        //        if (i == _head)
        //        {
        //            action(_items[i]);
        //            return;
        //        }
        //    }
        //}

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromHeadToTail(System.Func<T, bool> action)
        //{
        //    int i = _head;
        //    for (; ; )
        //    {
        //        if (action(_items[i]))
        //            return;

        //        if (--i < 0)
        //            i = _capacity - 1;

        //        if (i == _tail)
        //        {
        //            action(_items[i]);
        //            return;
        //        }
        //    }
        //}

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromTailToHead(System.Func<T, bool> action)
        //{
        //    int i = _tail;
        //    for (; ; )
        //    {
        //        if (action(_items[i]))
        //            return;

        //        if (++i == _capacity)
        //            i = 0;

        //        if (i == _head)
        //        {
        //            action(_items[i]);
        //            return;
        //        }
        //    }
        //}

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromHeadToTail(System.Func<T, int, bool> action)
        //{
        //    int i = _head;
        //    for (; ; )
        //    {
        //        if (action(_items[i], i))
        //            return;

        //        if (--i < 0)
        //            i = _capacity - 1;

        //        if (i == _tail)
        //        {
        //            action(_items[i], i);
        //            return;
        //        }
        //    }
        //}

        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        //public void IterateFromTailToHead(System.Func<T, int, bool> action)
        //{
        //    int i = _tail;
        //    for (; ; )
        //    {
        //        if (action(_items[i], i))
        //            return;

        //        if (++i == _capacity)
        //            i = 0;

        //        if (i == _head)
        //        {
        //            action(_items[i], i);
        //            return;
        //        }
        //    }
        //}


        //[System.Obsolete("To much Garbage. Inline with hands =_)")]
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="action"> array element, element index,
        ///// out instruction number:
        ///// negative value for going to tail,
        ///// positive for going to head,
        ///// zero for return.
        ///// (return 3 for jump 3 indexes to head)</param>
        //public void IterateFromMiddle(System.Func<T, int, int> action)
        //{
        //    int initialIndex = _size / 2;

        //    for (; ; )
        //    {
        //        int res = action(_items[initialIndex], initialIndex);

        //        if (res == 0)
        //            return;

        //        initialIndex += res;
        //    }
        //}

        // vvv slower even with inlining
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private void MoveNext(ref int v)
        //{
        //    int tmp = v + 1;

        //    if (tmp == _capacity)
        //        tmp = 0;

        //    v = tmp;
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //private void MoveBack(ref int v)
        //{
        //    int tmp = v - 1;

        //    if (tmp < 0)
        //        tmp = _capacity - 1;

        //    v = tmp;
        //}
        #endregion
    }
}
