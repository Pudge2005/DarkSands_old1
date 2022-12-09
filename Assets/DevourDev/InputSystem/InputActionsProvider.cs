using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DevourDev.Unity.InputSystem
{
    public static class InputActionsProvider
    {
        private sealed class InputActionsWrapper : IDisposable
        {
            private readonly IInputActionCollection _inputActions;
            private int _contributorsCount;
            private bool _disposed;


            public InputActionsWrapper(IInputActionCollection inputActions)
            {
                _inputActions = inputActions;
            }


            public void Dispose()
            {
                if (_disposed)
                    throw new ObjectDisposedException(nameof(InputActionsWrapper));

                if (_inputActions is IDisposable disposable)
                    disposable.Dispose();

                _disposed = true;
            }


            public T GetCustomControls<T>() where T : IInputActionCollection
            {
                return (T)_inputActions;
            }


            public void RegisterContributor()
            {
                ++_contributorsCount;

                if (_contributorsCount == 1)
                    _inputActions.Enable();
            }

            public void UnregisterContributor()
            {
                --_contributorsCount;

                if (_contributorsCount == 0)
                    _inputActions.Disable();
            }

        }


        private static readonly Dictionary<Type, InputActionsWrapper> _customControls = new();



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            _customControls.Clear();

            UnityEngine.Application.quitting += OnApplicationQuitting;
        }


        private static void Dispose()
        {
            UnityEngine.Application.quitting -= OnApplicationQuitting;

            var values = _customControls.Values;

            foreach (var wrapper in values)
            {
                wrapper.Dispose();
            }
        }

        private static void OnApplicationQuitting()
        {
            Dispose();
        }

        public static TControls GetControls<TControls>() where TControls : class, IInputActionCollection, new()
        {
            var wrapper = GetWrapper<TControls>();
            wrapper.RegisterContributor();
            return wrapper.GetCustomControls<TControls>();
        }


        public static void ReleaseControls<TControls>(ref TControls controls) where TControls : class, IInputActionCollection, new()
        {
            controls = null;
            var wrapper = GetWrapper<TControls>();
            wrapper.UnregisterContributor();
        }


        private static InputActionsWrapper GetWrapper<TControls>() where TControls : IInputActionCollection, new()
        {
            if (!_customControls.TryGetValue(typeof(TControls), out var wrapper))
            {
                wrapper = new InputActionsWrapper(new TControls());
                _customControls.Add(typeof(TControls), wrapper);
            }

            return wrapper;
        }


    }
}
