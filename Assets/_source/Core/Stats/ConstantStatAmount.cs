using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Core.Stats
{
    [System.Serializable]
    public struct ConstantStatAmount
    {
        [SerializeField] private StatSo _stat;
        [SerializeField] private int _amount;


        public ConstantStatAmount(StatSo stat, int amount)
        {
            _stat = stat;
            _amount = amount;
        }


        public StatSo Stat => _stat;
        public int Amount => _amount;
    }
}
