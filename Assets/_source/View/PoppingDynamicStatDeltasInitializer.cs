using DevourDev.Unity.Stats;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Fighting
{
    public sealed class PoppingDynamicStatDeltasInitializer : MonoBehaviour
    {
        [SerializeField] private PoppingDynamicStatDeltasViewBase _popper;
        [SerializeField] private StatSo _stat;
        [SerializeField] private DynamicStatsCollectionComponent _statsCollection;
        [SerializeField] private Transform _poppingsSpawnPoint;


        private void Start()
        {
            if (_statsCollection.TryGetStatData(_stat, out var statData))
            {
                _popper.Init(statData, _poppingsSpawnPoint);
            }

            Destroy(this);
        }
    }
}
