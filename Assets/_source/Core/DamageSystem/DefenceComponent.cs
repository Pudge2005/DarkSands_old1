using System.Collections.Generic;
using Game.Core.GameRules;
using UnityEngine;

namespace Game.Core.Characters
{

    public sealed class DefenceComponent : CharacterComponent
    {
        [SerializeField] private FighterArmorSo[] _initialArmors;

        private List<FighterArmorSo> _armors;
        private ArmorSummary _summary;


        protected override void InitCharacterComponent()
        {
            _summary = new(ActiveGameRules.ArmorCalculationMethod);
            _armors = new();
            _armors.AddRange(_initialArmors);
            Recalculate();
        }


        public void AddArmor(FighterArmorSo armor)
        {
            _armors.Add(armor);
            Recalculate();
        }

        public void RemoveArmor(FighterArmorSo armor)
        {
            _armors.Remove(armor);
            Recalculate();
        }

        private void Recalculate()
        {
            _summary.Clear();

            foreach (var armor in _armors)
            {
                armor.FillOptmizer(ref _summary);
            }
        }

        public float ProcessDamage(float incomingDamage)
        {
            return _summary.ProcessDamage(incomingDamage);
        }
    }
}
