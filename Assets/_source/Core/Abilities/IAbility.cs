using Game.Core.Characters;

namespace Game.Core.Abilities
{
    public interface IAbility
    {
        bool Cast(Character caster, out IAbilityLifeHandle handle);
    }
}
