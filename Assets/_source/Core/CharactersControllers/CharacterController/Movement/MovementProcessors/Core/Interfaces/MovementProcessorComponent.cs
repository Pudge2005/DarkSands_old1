using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public abstract class MovementProcessorComponent : MonoBehaviour, IVerticalMovementProcessor, IHorizontalMovementProcessor
    {
        public abstract float ProcessVerticalVelocity(float vertical);
        public abstract Vector2 ProcessHorizontalVelocity(Vector2 horizontal);
    }
}
