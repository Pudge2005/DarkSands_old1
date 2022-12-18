using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilitySo<T> : ScriptableObject
    {
        [SerializeField] private MetaInfo _metaInfo;
        [SerializeField] private AbilityAction<T> _action;


        public MetaInfo MetaInfo => _metaInfo;


        public bool TryCast(T context, out ILifeHandle handle)
        {
            if (!CanCast(context))
            {
                handle = default;
                return false;
            }

            handle = Cast(context);
            return true;
        }

        public ILifeHandle Cast(T context)
        {
            HandleCast(context);
            var handle = new LifeHandle();
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
