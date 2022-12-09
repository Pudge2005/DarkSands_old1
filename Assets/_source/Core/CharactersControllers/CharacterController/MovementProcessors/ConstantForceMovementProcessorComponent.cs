using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public sealed class ConstantForceMovementProcessorComponent : MovementProcessorComponent
    {
        [SerializeField] private Vector3 _force = new(0, -15f, 0);


        public Vector3 Force { get => _force; set => _force = value; }


        public override Vector2 ProcessHorizontalVelocity(Vector2 horizontal)
        {
            return new Vector2(horizontal.x + _force.x, horizontal.y + _force.z);
        }

        public override float ProcessVerticalVelocity(float vertical)
        {
            return vertical + _force.y;
        }
    }
}
