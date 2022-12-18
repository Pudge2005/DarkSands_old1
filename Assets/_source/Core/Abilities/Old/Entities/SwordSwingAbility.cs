using Game.Core.Characters;
using UnityEngine;

namespace Game.Core.Abilities
{
    //[CreateAssetMenu(menuName = "Game/Abilities/SwordSwing")]
    //public class SwordSwingAbility : AttackAbility
    //{
    //    //prototype only
    //    [SerializeField] private AbilityStageSo _castStage;

    //    [SerializeField] private float _radius;
    //    [SerializeField] private float _offset;

    //    protected override void HandleStageEnded(Character caster, AbilityStageSo stage)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    protected override void HandleCastStageStarted(Character caster)
    //    {
    //        Vector3 center = caster.transform.position + caster.RealFacingDirection * _offset;

    //        var targets = Physics.OverlapSphere(center, _radius);

    //        foreach (var item in targets)
    //        {
    //            if (item.TryGetComponent<Character>(out var ch))
    //            {
    //                if (ch == caster)
    //                    continue;

    //                DealDamage(ch);
    //            }
    //        }
    //    }

    //    protected override void HandleStageStarted(Character caster, AbilityStageSo stage)
    //    {
    //    }
    //}
}
