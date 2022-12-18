using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AttachableModule<TContext> : MonoBehaviour
        where TContext : Component
    {
        private TContext _context;
        private IReadOnlyLifeHandle _extHandle;


        protected TContext Context => _context;
        protected IReadOnlyLifeHandle ExternalHandle => _extHandle;


        internal void InitModuleInternal(TContext context, IReadOnlyLifeHandle externalLifeHandle)
        {
            _context = context;
            _extHandle = externalLifeHandle;
            HandleInitialization();
        }


        protected virtual void HandleInitialization() { }
    }
}
