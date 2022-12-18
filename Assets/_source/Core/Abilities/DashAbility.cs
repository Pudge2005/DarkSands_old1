using Game.Core.Characters;
using Game.Core.CharactersControllers;
using Game.Core.GameRules;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Dash")]
    public sealed class DashAbility : AttachingAbilityAction<Character, DashAbility.DashingModule>
    {
        public sealed class DashingModule : AttachableModule<Character>
        {
            private sealed class DashingComponent : ForcedMotionComponent
            {
                private const int _dashPriority = 10;
                private int _tmpLayer;

                private Vector3 _speed;
                private float _dashDuration;
                private float _time;
                private IAbilityLifeHandle _handle;


                public override int Priority => _dashPriority;


                {
                    _speed = direction * speed;
                    _dashDuration = distance / speed;
                    _tmpLayer = gameObject.layer;
                    _handle = abilityLifeHandle;
                    gameObject.layer = ActiveGameRules.IgnoreCharactersLayer;
                }

                protected override void OnDestroy()
                {
                base.OnDestroy();
                    gameObject.layer = _tmpLayer;

                    if (!_handle.Canceled)
                        _handle.Cancel();

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
                Handle.OnCancelled += HandleCancellation;
                Vector3 dir = Context.MovingDirection;


                dash.InitForcedMotion(Context.MovementHandler);
                dash.InitDash(dir, distance, speed, Handle);

        protected override void CastInherited(Character caster)
        {
            var dash = caster.gameObject.AddComponent<DashingComponent>();
            Vector3 direction = caster.MovingDirection;

                {
                }
            else
            {
                direction.Normalize();
            }
        }

        }
    }
}
