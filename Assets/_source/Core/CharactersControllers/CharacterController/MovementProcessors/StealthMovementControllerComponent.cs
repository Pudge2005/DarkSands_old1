using System;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public sealed class StealthMovementControllerComponent : ActionControllerComponent<bool>, IHorizontalMovementProcessor
    {
        [SerializeField] private float _slow = 0.15f;


        public Vector2 ProcessHorizontalVelocity(Vector2 horizontal)
        {
            if (!InputValue)
                return horizontal;

            return horizontal - horizontal * _slow;
        }

        protected override void HandleInput(bool context)
        {
        }
    }
}
