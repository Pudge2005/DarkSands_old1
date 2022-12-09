using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    [CreateAssetMenu(menuName = "Game/Abilities/SwordSwing")]
    public class SwordSwingAbility : AttackAbility
    {
        //prototype only

        [SerializeField] private float _radius;
        [SerializeField] private Vector3 _centerOffset;


        protected override void CastInherited(Character caster)
        {
            Vector3 center = caster.transform.position + caster.transform.rotation * _centerOffset;

            var targets = Physics.OverlapSphere(center, _radius);

            foreach (var item in targets)
            {
                if (item.TryGetComponent<Character>(out var ch))
                {
                    if (ch == caster)
                        continue;

                    DealDamage(ch);
                }
            }
        }
    }
}
