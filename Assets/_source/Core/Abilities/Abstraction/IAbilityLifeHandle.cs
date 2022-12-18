using System;

namespace Game.Core.Abilities
{
    public interface IAbilityLifeHandle : IReadOnlyAbilityHandle
    {
        void Cancel();
    }

    public interface IReadOnlyAbilityHandle
    {
        bool Canceled { get; }

        event Action OnCancelled;
    }

}
