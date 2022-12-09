using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Core.Abilities
{
    [System.Serializable]
    public struct DynamicStatAmount
    {
        [SerializeField] private StatSo _stat;
        [SerializeField] private float _amount;


        public DynamicStatAmount(StatSo stat, float amount)
        {
            _stat = stat;
            _amount = amount;
        }


        public StatSo Stat => _stat;
        public float Amount => _amount;
    }
}
