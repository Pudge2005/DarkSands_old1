using System.Collections.Generic;
using Game.Core.Abilities;
using UnityEngine;

namespace Game.Core.CharactersControllers
{
    public sealed class MovementHandler : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;

        private readonly List<ForcedMotionComponent> _blockers = new();
        private readonly List<IVerticalMovementProcessor> _verticalProcessors = new();
        private readonly List<IHorizontalMovementProcessor> _horizontalProcessors = new();

        private float _verticalVelocity;
        private Vector2 _horizontalVelocity;


        public Vector3 Velocity => new(_horizontalVelocity.x, _verticalVelocity, _horizontalVelocity.y);
        public float Speed => Velocity.magnitude;


        public void RegisterBlocker(ForcedMotionComponent blocker)
        {
            _blockers.Add(blocker);

            if (_blockers.Count == 1)
                enabled = false;
        }

        public void UnregisterBlocker(ForcedMotionComponent blocker)
        {
            var index = _blockers.LastIndexOf(blocker);

            if (index < 0)
                return;

            _blockers.RemoveAt(index);

            if (_blockers.Count == 0)
                enabled = true;
        }

        public void RegisterVerticalMovementProcessor(IVerticalMovementProcessor movementProcessor)
        {
            _verticalProcessors.Add(movementProcessor);
        }

        public void UnRegisterVerticalMovementProcessor(IVerticalMovementProcessor movementProcessor)
        {
            var index = _verticalProcessors.LastIndexOf(movementProcessor);

            if (index < 0)
                return;

            _verticalProcessors.RemoveAt(index);
        }


        public void RegisterHorizontalMovementProcessor(IHorizontalMovementProcessor movementProcessor)
        {
            _horizontalProcessors.Add(movementProcessor);
        }

        public void UnRegisterHorizontalMovementProcessor(IHorizontalMovementProcessor movementProcessor)
        {
            var index = _horizontalProcessors.LastIndexOf(movementProcessor);

            if (index < 0)
                return;

            _horizontalProcessors.RemoveAt(index);
        }

        internal void MoveImmediate(Vector3 move)
        {
            _controller.Move(move);
        }


        private void Update()
        {
            CollectVelocityData();
            Move();
        }


        private void CollectVelocityData()
        {
            float vertical = 0;

            foreach (var processor in _verticalProcessors)
            {
                vertical = processor.ProcessVerticalVelocity(vertical);
            }

            Vector2 horizontal = Vector2.zero;

            foreach (var processor in _horizontalProcessors)
            {
                horizontal = processor.ProcessHorizontalVelocity(horizontal);
            }

            _verticalVelocity = vertical;
            _horizontalVelocity = horizontal;
        }

        private void Move()
        {
            Vector3 movement = Velocity;

            if (movement == Vector3.zero)
                return;

            _controller.Move(movement * Time.deltaTime);
        }
    }
}
