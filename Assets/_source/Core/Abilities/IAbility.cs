using Game.Core.Characters;

namespace Game.Core.Abilities
{
    public interface IAbility
    {
        void Cast(Character caster);
    }
}
