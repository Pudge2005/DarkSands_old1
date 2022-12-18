using System;

namespace Game.Core.Abilities
{
    internal class LifeHandle : ILifeHandle
    {
        private bool _canceled;


        public bool Canceled => _canceled;

        public event Action OnCancelled;


        public void Cancel()
        {
            if (_canceled)
                return;

            _canceled = true;
            OnCancelled?.Invoke();
        }
    }
}
