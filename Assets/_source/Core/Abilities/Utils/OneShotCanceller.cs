using System;

namespace Game.Core.Abilities
{
    internal struct OneShotCanceller : ILifeHandle
    {
        private ILifeHandle _internalHandle;


        public OneShotCanceller(ILifeHandle handle)
        {
            _internalHandle = handle;
            OnCancelled = null;
            handle.OnCancelled += HandleInternalHandleCancelled;
        }


        public bool Canceled => _internalHandle.Canceled;


        public event Action OnCancelled;


        public void Cancel()
        {
            OnCancelled?.Invoke();

            if (_internalHandle == null)
                return;

            _internalHandle.Cancel();
            _internalHandle = null;
        }


        private void HandleInternalHandleCancelled()
        {
            if (_internalHandle != null)
            {
                _internalHandle.OnCancelled -= HandleInternalHandleCancelled;
                _internalHandle = null;
            }

            Cancel();
        }
    }
}
