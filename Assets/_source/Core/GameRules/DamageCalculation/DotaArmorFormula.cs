using System;
using UnityEngine;

namespace Game.Core.GameRules
{
    [CreateAssetMenu(menuName = "Game/DamageSystem/Armor/Formula/Dota-Like")]
    public class DotaArmorFormula : ArmorCalculationMethod
    {
        [SerializeField] private float _b = 1f;
        [SerializeField] private float _f = 0.06f;


        public override float Calculate(float incomingDamage, float armorValue)
        {
            float multiplier = 1f - (_f * armorValue) / (_b + _f * Math.Abs(armorValue));
            return incomingDamage * multiplier;
        }
    }
}
