using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public interface IHorizontalMovementProcessor
    {
        Vector2 ProcessHorizontalVelocity(Vector2 horizontal);
    }
}
