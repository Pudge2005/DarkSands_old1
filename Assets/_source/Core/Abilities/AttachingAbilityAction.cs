using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AttachingAbilityAction<TContext, TModule> : AbilityAction<TContext>
        where TContext : Component
        where TModule : AttachableModule<TContext>
    {
        public sealed override void Act(TContext context, IReadOnlyLifeHandle externalLifeHandle)
        {
            var module = context.gameObject.AddComponent<TModule>();
            module.InitModuleInternal(context, externalLifeHandle);
            InitAttachedModule(module, context);
        }


        protected virtual void InitAttachedModule(TModule module, TContext context) { }
    }
}
