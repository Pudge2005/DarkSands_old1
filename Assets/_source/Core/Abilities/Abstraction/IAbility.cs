using Game.Core.Characters;

namespace Game.Core.Abilities
{
    public interface IAbility<TContext>
    {
        bool CanCast(TContext context);
        bool TryCast(TContext context, out IAbilityLifeHandle handle);
    }
}
