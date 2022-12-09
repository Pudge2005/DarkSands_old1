using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Core.Characters
{

    public sealed class HealthComponent : CharacterComponent
    {
        [SerializeField] private StatSo _vitalStat;
        [SerializeField] private DynamicStatsCollectionComponent _dynamicStatsProvider;
        [SerializeField] private DefenceComponent _defence;

        private DynamicStatData _healthStatData;


        public bool Alive { get; private set; }


        /// <summary>
        /// character, statData, rawDelta, safeDelta
        /// </summary>
        public event System.Action<Character, DynamicStatData, float, float> OnHealthChanged;

        /// <summary>
        /// character, statData, rawDelta, safeDelta
        /// </summary>
        public event System.Action<Character, DynamicStatData, float, float> OnDeath;


        public void GetHealthValues(out Character character, out DynamicStatData healthStatData)
        {
            character = Character;
            healthStatData = _healthStatData;
        }

        public void DealDamage(float incomingDamage)
        {
            TakeDamage(incomingDamage);
        }

        private void TakeDamage(float incomingDamage)
        {
            if (!Alive)
                return;

            float finalDmg = _defence.ProcessDamage(incomingDamage);
            _healthStatData.RemovePossible(finalDmg);
        }
        private void Awake()
        {
            Alive = true;
            _ = _dynamicStatsProvider.TryGetStatData(_vitalStat, out var vitalStatData);
            _healthStatData = vitalStatData;
            vitalStatData.OnCurrentValueChanged += HandleHealthChanged;
            vitalStatData.OnMinReached += HandleMinHealthReached;
        }


        private void HandleMinHealthReached(DynamicStatData sender, float raw, float safe)
        {
            Alive = false;

            OnDeath?.Invoke(Character, sender, raw, safe);
        }

        private void HandleHealthChanged(DynamicStatData sender, float raw, float safe)
        {
            OnHealthChanged?.Invoke(Character, sender, raw, safe);
        }
    }
}
