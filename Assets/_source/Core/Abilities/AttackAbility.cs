using DevourDev.Unity.Utils;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Attack")]
    public sealed class AttackAbility : AttachingAbilityAction<Character, AttackAbility.AttackingModule>
    {
        //todo: мб изменить обработку инпутов атаки на подсчет времени нажатия
        //кнопки атаки (держишь 3 секунды - применяется сильная атака, отпускаешь
        //на первой секунде - применяется обычная атака. Нормализованное время может
        //использоваться как множитель (урона, оглушения, т.д.)

        //с текущими настройками (атака мгновенная так как запрос на применение абилки
        //приходит после регистрации удержания кнопок) реализация подобных атак с помощью
        //Attachable бесполезна.
        public sealed class AttackingModule : AttachableModule<Character>
        {
            private float _dmg;
            private float _radius;
            private float _distance;


            internal void InitAttackingModule(float dmg, float radius, float distance)
            {
                _dmg = dmg;
                _radius = radius;
                _distance = distance;
                DealDamage();
            }


            private void DealDamage()
            {
                Vector3 center = Context.transform.position + Context.RealFacingDirection * _distance;

                var targets = PhysicsHelpers.OverlapSphere(center, _radius).Span;

                foreach (var item in targets)
                {
                    if (item.TryGetComponent<Character>(out var ch))
                    {
                        if (ch == Context)
                            continue;

                        ch.DealDamage(_dmg);
                    }
                }

                Destroy(this);
            }
        }


        [SerializeField] private float _dmg = 30f;
        [SerializeField] private float _radius = 2f;
        [SerializeField] private float _distance = 1f;


        protected override void InitAttachedModule(AttackingModule module, Character context)
        {
            module.InitAttackingModule(_dmg, _radius, _distance);
        }
    }


}
