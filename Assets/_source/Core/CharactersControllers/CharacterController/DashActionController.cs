using Game.Core.Abilities;
using UnityEngine;

namespace Game.Core.CharactersControllers
{

    public sealed class DashActionController : ActionControllerComponent
    {
        [SerializeField] private DashAbility _dashAbility;


        protected override void HandleInput()
        {
            if (CanDash())
                Dash();
        }

        private bool CanDash()
        {
            return true;
        }

        private void Dash()
        {
            _dashAbility.Cast(Character);
        }


    }
}
