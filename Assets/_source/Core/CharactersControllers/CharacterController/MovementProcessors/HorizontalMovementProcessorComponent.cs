using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public abstract class HorizontalMovementProcessorComponent : MonoBehaviour, IHorizontalMovementProcessor
    {
        public abstract Vector2 ProcessHorizontalVelocity(Vector2 horizontal);
    }
}
