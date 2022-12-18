using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilityObject<T> : ScriptableObject
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private AbilityAction<T> _action;


        public MetaInfo MetaInfo => _metaInfo;


        public bool TryCast(T context, out IAbilityLifeHandle handle)
        {
            if (!CanCast(context))
            {
                handle = default;
                return false;
            }

            handle = Cast(context);
            return true;
        }

        public IAbilityLifeHandle Cast(T context)
        {
            HandleCast(context);
            var handle = new AbilityLifeHandle();
            _action.Act(context, handle);
            return handle;
        }


        protected virtual bool CanCast(T context) => true;

        protected virtual void HandleCast(T context)
        {
            //take mana/stamina/etc...
        }
    }
}
