using DevourDev.Unity.Stats;
using Game.Core.Characters;
using UnityEngine;

namespace Game.Fighting
{
    public sealed class DynamicStatSliderInitializer : MonoBehaviour
    {
        [SerializeField] private DynamicStatSliderViewBase _slider;
        [SerializeField] private StatSo _stat;
        [SerializeField] private DynamicStatsCollectionComponent _statsCollection;


        private void Start()
        {
            if(_statsCollection.TryGetStatData(_stat, out var statData))
            {
                _slider.Init(statData);
            }

            Destroy(this);
        }
    }
}
