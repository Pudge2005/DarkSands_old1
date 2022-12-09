using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public sealed class VerticalSpeedLimiter : VerticalMovementProcessorComponent
    {
        [SerializeField] private float _maxSpeed = 50f;


        public override float ProcessVerticalVelocity(float vertical)
        {
            if (vertical < -_maxSpeed)
                return -_maxSpeed;

            if (vertical > _maxSpeed)
                return _maxSpeed;

            return vertical;
        }
    }
}
