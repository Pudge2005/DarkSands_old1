using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public abstract class ActionControllerComponent : CharacterComponent, IActionControllerComponent
    {
        public void Trigger()
        {
            HandleInput();
        }


        protected abstract void HandleInput();
    }

    public abstract class ActionControllerComponent<TContext> : CharacterComponent, IActionControllerComponent<TContext>
        where TContext : struct
    {
        private TContext _inputValue;


        public TContext InputValue
        {
            get => _inputValue;

            set
            {
                _inputValue = value;
                HandleInput(value);
            }
        }


        protected abstract void HandleInput(TContext context);
    }
}
