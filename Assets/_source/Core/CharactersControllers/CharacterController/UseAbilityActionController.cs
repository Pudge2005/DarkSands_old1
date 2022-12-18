using Game.Core.Abilities;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    [RequireComponent(typeof(Character))]
    public sealed class UseAbilityActionController : ActionControllerComponent
    {
        [SerializeField] private AbilitySo<Character> _ability;

        private Character _caster;
        private ILifeHandle _abilityLifeHandle;


        private void Awake()
        {
            _caster = GetComponent<Character>();
        }


        public void Cancel()
        {
            if (_abilityLifeHandle != null)
            {
                _abilityLifeHandle.Cancel();
                _abilityLifeHandle = null;
            }
        }


        protected override void HandleInput()
        {
            if (_ability.TryCast(_caster, out _abilityLifeHandle))
            {
                UnityEngine.Debug.Log($"{name} casted {_ability.name}");
            }
        }
    }
}
