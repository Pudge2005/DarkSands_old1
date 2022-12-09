using Game.Core.GameRules;

namespace Game.Core.Characters
{
    public struct ArmorSummary
    {
        private readonly ArmorCalculationMethod _calcMethod;
        private float _dmgBlock;
        private float _armorValue;


        public ArmorSummary(ArmorCalculationMethod calcMethod) : this()
        {
            _calcMethod = calcMethod;
        }


        public void Clear()
        {
            _dmgBlock = 0;
            _armorValue = 0;
        }

        public void RegisterDamageBlock(float block)
        {
            if (block > _dmgBlock)
                _dmgBlock = block;
        }

        public void RegisterArmorValue(float armor)
        {
            _armorValue += armor;
        }


        public float ProcessDamage(float damage)
        {
            float d = damage - _dmgBlock;

            if (d <= 0)
                return 0;

            return _calcMethod.Calculate(damage, _armorValue);
        }
    }
}
