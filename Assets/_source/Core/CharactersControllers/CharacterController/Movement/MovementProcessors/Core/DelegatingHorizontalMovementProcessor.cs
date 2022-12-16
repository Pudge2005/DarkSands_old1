using System;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public sealed class DelegatingHorizontalMovementProcessor : IHorizontalMovementProcessor
    {
        private readonly Func<Vector2, Vector2> _processHorizontalVelocityAction;


        public DelegatingHorizontalMovementProcessor(Func<Vector2, Vector2> processHorizontalVelocityAction)
        {
            _processHorizontalVelocityAction = processHorizontalVelocityAction;
        }


        public Vector2 ProcessHorizontalVelocity(Vector2 horizontal)
        {
            return _processHorizontalVelocityAction(horizontal);
        }
    }
}
