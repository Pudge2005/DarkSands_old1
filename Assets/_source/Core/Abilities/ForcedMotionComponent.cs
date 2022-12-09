using Game.Core.CharactersControllers;
using UnityEngine;

namespace Game.Core.Abilities
{
    public abstract class ForcedMotionComponent : MonoBehaviour
    {
        private MovementHandler _movementHandler;


        public abstract int Priority { get; }

        public void InitForcedMotion(MovementHandler movementHandler)
        {
            _movementHandler = movementHandler;
            _movementHandler.RegisterBlocker(this);
            InitForcedMotionInherited();
        }


        protected virtual void OnDestroy()
        {
            _movementHandler.UnregisterBlocker(this);
        }


        protected void Translate(Vector3 deltaPosition)
        {
            _movementHandler.MoveImmediate(deltaPosition);
        }


        protected virtual void InitForcedMotionInherited() { }
    }
}
