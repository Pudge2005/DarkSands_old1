using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevourDev.Base.Asynchronous
{
    public class AsyncEvent
    {
        private readonly List<System.Func<Task>> _funcs;


        public AsyncEvent()
        {
            _funcs = new();
            var f = _funcs[0];
        }

        public void Subscribe(System.Func<Task> func)
        {
            _funcs.Add(func);
        }

        public void Unsubscribe(System.Func<Task> func)
        {
            _funcs.Remove(func);
        }


        public async Task InvokeAsync()
        {
            var fs = _funcs;

            for (int i = 0; i < fs.Count; i++)
            {
                Func<Task> f = fs[i];
                await f();
            }
        }
    }


    public class AsyncEvent<T>
    {
        private readonly List<System.Func<T, Task>> _funcs;


        public AsyncEvent()
        {
            _funcs = new();
        }


        public void Subscribe(System.Func<T, Task> func)
        {
            _funcs.Add(func);
        }

        public void Unsubscribe(System.Func<T, Task> func)
        {
            _funcs.Remove(func);
        }


        public async Task InvokeAsync(T t)
        {
            var fs = _funcs;

            for (int i = 0; i < fs.Count; i++)
            {
                Func<T, Task> f = fs[i];
                await f(t);
            }
        }
    }


    public class AsyncEvent<T, T1>
    {
        private readonly List<System.Func<T, T1, Task>> _funcs;


        public AsyncEvent()
        {
            _funcs = new();
        }


        public void Subscribe(System.Func<T, T1, Task> func)
        {
            _funcs.Add(func);
        }

        public void Unsubscribe(System.Func<T, T1, Task> func)
        {
            _funcs.Remove(func);
        }


        public async Task InvokeAsync(T t, T1 t1)
        {
            var fs = _funcs;

            for (int i = 0; i < fs.Count; i++)
            {
                Func<T, T1, Task> f = fs[i];
                await f(t, t1);
            }
        }


    }


    public class AsyncEvent<T, T1, T2>
    {
        private readonly List<System.Func<T, T1, T2, Task>> _funcs;


        public AsyncEvent()
        {
            _funcs = new();
        }


        public void Subscribe(System.Func<T, T1, T2, Task> func)
        {
            _funcs.Add(func);
        }

        public void Unsubscribe(System.Func<T, T1, T2, Task> func)
        {
            _funcs.Remove(func);
        }


        public async Task InvokeAsync(T t, T1 t1, T2 t2)
        {
            var fs = _funcs;

            for (int i = 0; i < fs.Count; i++)
            {
                Func<T, T1, T2, Task> f = fs[i];
                await f(t, t1, t2);
            }
        }
    }
}
