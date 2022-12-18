using Game.Core.Characters;
using Game.Core.CharactersControllers;
using Game.Core.GameRules;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Dash")]
    public sealed class DashAbility : AttachingAbilityAction<Character, DashAbility.DashingModule>
    {
        [SerializeField] private float _distance = 2f;
        [SerializeField] private float _speed = 15f;


        public sealed class DashingModule : AttachableModule<Character>
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
                    gameObject.layer = _tmpLayer;
                    base.OnDestroy();
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


            internal void InitDashingModule(float distance, float speed)
            {
                var dash = Context.gameObject.AddComponent<DashingComponent>();
                Vector3 direction = Context.MovingDirection;

                if (direction == Vector3.zero)
                {
                    direction = Context.RealFacingDirection;
                }
                else
                {
                    direction.Normalize();
                }

                dash.InitForcedMotion(Context.MovementHandler);
                dash.InitDash(direction, distance, speed);
                ExternalHandle.OnCancelled += HandleActionCancelled;


                void HandleActionCancelled()
                {
                    Destroy(dash);
                }
            }
        }


        protected override void InitAttachedModule(DashingModule module, Character context)
        {
            module.InitDashingModule(_distance, _speed);
        }
    }
}
