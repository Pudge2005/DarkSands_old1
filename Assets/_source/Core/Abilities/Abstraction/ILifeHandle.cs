using System;

namespace Game.Core.Abilities
{
    public interface ILifeHandle : IReadOnlyLifeHandle
    {
        void Cancel();
    }

    public interface IReadOnlyLifeHandle
    {
        bool Canceled { get; }

        event Action OnCancelled;
    }

}
