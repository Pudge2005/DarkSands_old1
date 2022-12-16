using System;

namespace Game.Core.Abilities
{
    public interface IAbilityLifeHandle
    {
        event Action OnCancelled;


        void Cancel();
    }
}
