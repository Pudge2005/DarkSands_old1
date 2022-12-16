using Game.Core.Characters;
using Game.Core.CharactersControllers;
using Game.Core.GameRules;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Game/Abilities/Actions/Dash")]
    public sealed class DashAction : CharacterAbilityAction
    {
        private sealed class DashingComponent : ForcedMotionComponent
        {
            private const int _dashPriority = 10;
            private int _tmpLayer;

            private Vector3 _speed;
            private float _dashDuration;
            private float _time;


            public override int Priority => _dashPriority;


            public void InitDash(Vector3 direction, float distance, float speed)
            {
                _speed = direction * speed;
                _dashDuration = distance / speed;
                _tmpLayer = gameObject.layer;
                gameObject.layer = ActiveGameRules.IgnoreCharactersLayer;
            }

            protected override void OnDestroy()
            {
                base.OnDestroy();
                gameObject.layer = _tmpLayer;
            }

            private void Update()
            {
                float deltaTime = Time.deltaTime;
                _time += deltaTime;
                bool flag = _time >= _dashDuration;

                if (flag)
                    _time = _dashDuration;

                Vector3 movement = _speed * Time.deltaTime;
                Translate(movement);

                if (flag)
                {
                    Destroy(this);
                }
            }
        }


        [SerializeField] private float _distance = 3f;
        [SerializeField] private float _speed = 20f;


        public override void Act(Character caster)
        {
            var dash = caster.gameObject.AddComponent<DashingComponent>();
            Vector3 direction = caster.MovingDirection;

            if (direction == Vector3.zero)
            {
                direction = caster.RealFacingDirection;
            }
            else
            {
                direction.Normalize();
            }

            dash.InitForcedMotion(caster.MovementHandler);
            dash.InitDash(direction, _distance, _speed);
        }
    }
}
