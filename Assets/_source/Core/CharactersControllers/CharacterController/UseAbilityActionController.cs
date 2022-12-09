using Game.Core.Abilities;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    [RequireComponent(typeof(Character))]
    public sealed class UseAbilityActionController : ActionControllerComponent
    {
        [SerializeField] private AbilitySo _ability;

        private Character _caster;


        private void Awake()
        {
            _caster = GetComponent<Character>();
        }


        protected override void HandleInput()
        {
            _ability.Cast(_caster);
        }
    }
}
