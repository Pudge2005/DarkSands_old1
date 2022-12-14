using System;

namespace Game.Core.Abilities
{
    internal struct OneShotCanceller : IAbilityLifeHandle
    {
        private IAbilityLifeHandle _internalHandle;


        public OneShotCanceller(IAbilityLifeHandle handle)
        {
            _internalHandle = handle;
            OnCancelled = null;
            handle.OnCancelled += HandleInternalHandleCancelled;
        }


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



    //public sealed class AbilitiesManagerInitializer : MonoBehaviour
    //{
            //когда создавал статику, сразу создал этот монобех, но уже не помню, для чего
    //}
}
