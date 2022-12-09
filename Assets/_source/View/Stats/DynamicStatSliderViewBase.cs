using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Fighting
{
    public abstract class DynamicStatSliderViewBase : MonoBehaviour
    {
        private DynamicStatData _dynamicStatData; 

        private float _min;
        private float _max;
        private float _value;


        public float Min => _min;
        public float Max => _max;
        public float Value => _value;


        protected StatSo Stat => _dynamicStatData.Stat;


        public void Init(DynamicStatData dynamicStatData)
        {
            _dynamicStatData = dynamicStatData;
            _min = dynamicStatData.Min;
            _max = dynamicStatData.Max;
            _value = dynamicStatData.Value;

            InitInternal(_min, _max, _value);

            dynamicStatData.OnCurrentValueChanged += HandleValueChanged;
        }


        private void HandleValueChanged(DynamicStatData dynamicStatData, float raw, float safe)
        {
            _value = dynamicStatData.Value;
            HandleValueChanged(Value, raw, safe);
        }


        protected abstract void InitInternal(float min, float max, float value);
        protected abstract void HandleMinBoundChanged(float min);
        protected abstract void HandleMaxBoundChanged(float max);
        protected abstract void HandleValueChanged(float value, float rawDelta, float safeDelta);
    }
}
