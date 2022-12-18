using System;

namespace Game.Core.Abilities
{
    internal class AbilityLifeHandle : IAbilityLifeHandle
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
