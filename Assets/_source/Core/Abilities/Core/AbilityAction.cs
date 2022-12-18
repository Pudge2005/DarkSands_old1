using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class AbilityAction<T> : ScriptableObject
    {
        public abstract void Act(T context, IReadOnlyLifeHandle externalHandle);
    }
}
