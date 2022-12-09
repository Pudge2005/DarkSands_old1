using UnityEngine;

namespace Game.Core.Characters
{
    [CreateAssetMenu(menuName = "Fighting/Armor/Armor")]
    public sealed class FighterArmorSo : ScriptableObject
    {
        [SerializeField, Min(0f)] private float _damageBlock = 0;
        [SerializeField] private float _armorValue = 0.24f;


        public void FillOptmizer(ref ArmorSummary summary)
        {
            summary.RegisterDamageBlock(_damageBlock);
            summary.RegisterArmorValue(_armorValue);
        }
    }
}
