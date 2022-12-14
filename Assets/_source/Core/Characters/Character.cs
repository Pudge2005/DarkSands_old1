using Game.Core.CharactersControllers;
using Game.Core.GameRules;
using Game.Core.TwoDInThreeD;
using UnityEngine;

namespace Game.Core.Characters
{

    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private DynamicStatsCollectionComponent _stats;

        [SerializeField] private ActionControllerComponent<Vector2> _directionController;
        [SerializeField] private MovementHandler _movementHandler;
        [SerializeField] private DirectionSo _defaultFacingDirection;


        private CharacterSo _reference;

        private Vector3 _movingDirection;
        private Vector3 _realFacingDirection;
        private DirectionSo _facingDirection;


        public CharacterSo Reference => _reference;
        public DynamicStatsCollectionComponent DynamicStats => _stats;

        public Vector3 MovingDirection => _movingDirection;
        public DirectionSo FacingDirection => _facingDirection;

        //мб потом нужно будет точное направление (а не ограниченное 360f/(DirectionsSos count))
        public Vector3 RealFacingDirection => _realFacingDirection;

        public bool Alive => _health.Alive;

        public MovementHandler MovementHandler => _movementHandler;


        public event System.Action<Character, DirectionSo, DirectionSo> OnFacingDirectionChanged;


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + RealFacingDirection, 1f);
        }
#endif

        public void DealDamage(float damage)
        {
            _health.DealDamage(damage);
        }


        internal void InitCharacter(CharacterSo reference)
        {
            _facingDirection = _defaultFacingDirection;
            _realFacingDirection = _facingDirection.VectorDirection;

            _reference = reference;
            var charComps = gameObject.GetComponents<CharacterComponent>();

            foreach (var comp in charComps)
            {
                comp.SetCharacter(this);
            }

            _directionController.OnInputChanged += HandleDirectionChanged;
        }

        private void HandleDirectionChanged(Vector2 inputValue)
        {
            Vector3 dir = inputValue;
            (dir.y, dir.z) = (dir.z, dir.y);

            _movingDirection = dir;

            if (inputValue == Vector2.zero)
                return;

            _realFacingDirection = dir;
            SetFacingDirection(ActiveGameRules.DirectionsComposite.GetClosestDirection(dir));
        }


        private void SetFacingDirection(DirectionSo direction)
        {
            if (_facingDirection == direction)
                return;

            var prev = _facingDirection;
            _facingDirection = direction;
            OnFacingDirectionChanged?.Invoke(this, prev, direction);
        }

    }
}
