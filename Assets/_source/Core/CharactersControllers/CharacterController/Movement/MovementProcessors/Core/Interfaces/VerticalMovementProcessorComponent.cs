using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public abstract class VerticalMovementProcessorComponent : MonoBehaviour, IVerticalMovementProcessor
    {
        public abstract float ProcessVerticalVelocity(float vertical);
    }
}
