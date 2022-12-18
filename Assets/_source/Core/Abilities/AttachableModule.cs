using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AttachableModule<TContext> : MonoBehaviour
        where TContext : Component
    {
        private TContext _context;
        private IAbilityLifeHandle _handle;


        protected TContext Context => _context;
        protected IAbilityLifeHandle Handle => _handle;


        internal void InitModuleInternal(TContext context, IAbilityLifeHandle handle)
        {
            _context = context;
            _handle = handle;
            HandleInitialization();
        }


        protected virtual void HandleInitialization() { }
    }
}
