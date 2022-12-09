using UnityEngine;

namespace Game.Core.GameRules
{
    public abstract class ArmorCalculationMethod : ScriptableObject
    {
        public abstract float Calculate(float incomingDamage, float armorValue);
    }
}
