using UnityEngine;

namespace Game.Core.CharactersControllers
{

    public sealed class HorizontalMovementController : ActionControllerComponent<Vector2>,
                                                       IHorizontalMovementProcessor
    {
        [SerializeField] private MovementHandler _movementContext;

        [SerializeField] private float _speed = 2f;


        public Vector2 ProcessHorizontalVelocity(Vector2 horizontal)
        {
            return horizontal + InputValue.normalized * _speed;
        }

        protected override void HandleInput(Vector2 context)
        {
        }
    }
}
