using System.Collections;
using System.Collections.Generic;
using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Core.Characters
{
    public sealed class DynamicStatsCollectionComponent : CharacterComponent, IEnumerable<DynamicStatData>
    {
        //todo: decompose
        [System.Serializable]
        private struct StatInitializer
        {
            public StatSo Stat;
            public float Value;
        }


        [SerializeField] private StatInitializer[] _statInitializers;


        private readonly Dictionary<StatSo, DynamicStatData> _stats = new();


        private void Awake()
        {
            foreach (var si in _statInitializers)
            {
                var statData = new DynamicStatData(si.Stat, si.Value, si.Value);
                _stats.Add(si.Stat, statData);
            }
        }


        public bool TryGetStatData(StatSo stat, out DynamicStatData statData)
        {
            return _stats.TryGetValue(stat, out statData);
        }

        public IEnumerator<DynamicStatData> GetEnumerator()
        {
            return _stats.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _stats.Values.GetEnumerator();
        }


        internal void SetStatInternal(int statIndex, float max, float cur, bool raiseEvent)
        {
            var statData = _stats[_statInitializers[statIndex].Stat];
            StatsInternalsExposer.SetDynamicStatData(statData, max, cur, raiseEvent);
        }
    }
}
