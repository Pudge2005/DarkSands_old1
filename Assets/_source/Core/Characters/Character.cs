using Game.Core.CharactersControllers;
using UnityEngine;

namespace Game.Core.Characters
{

    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private DynamicStatsCollectionComponent _stats;

        [SerializeField] private ActionControllerComponent<Vector2> _directionController;
        [SerializeField] private MovementHandler _movementHandler;
        [SerializeField] private Vector3 _facingDirection = Vector3.right;


        private CharacterSo _reference;


        public CharacterSo Reference => _reference;
        public DynamicStatsCollectionComponent DynamicStats => _stats;

        public Vector3 MovingDirection
        {
            get
            {
                Vector3 dir = _directionController.InputValue;
                (dir.y, dir.z) = (dir.z, dir.y);
                return dir;
            }
        }

        public Vector3 FacingDirection => transform.rotation * _facingDirection;

        public bool Alive => _health.Alive;

        public MovementHandler MovementHandler => _movementHandler;


        internal void InitCharacter(CharacterSo reference)
        {
            _reference = reference;
            var charComps = gameObject.GetComponents<CharacterComponent>();

            foreach (var comp in charComps)
            {
                comp.SetCharacter(this);
            }
        }


        //todo: create composite for prototype purposes

        public void DealDamage(float damage)
        {
            _health.DealDamage(damage);
        }
    }
}
