using DevourDev.Unity.Stats;
using UnityEngine;

namespace Game.Fighting
{

    public abstract class PoppingDynamicStatDeltasViewBase : MonoBehaviour
    {
        private Transform _originPoint;


        protected Transform OriginPoint => _originPoint;


        public void Init(DynamicStatData dynamicStatData, Transform poppingOriginPoint)
        {
            _originPoint = poppingOriginPoint;
            dynamicStatData.OnCurrentValueChanged += HandleDynamicStatValueChanged;
        }

        private void HandleDynamicStatValueChanged(DynamicStatData dynamicStatData, float raw, float safe)
        {
            if (raw == 0)
                return;

            if (raw < 0)
                HandleDamage(-raw, -safe);
            else
                HandleHeal(raw, safe);
        }


        protected abstract void HandleHeal(float absRaw, float absSafe);
        protected abstract void HandleDamage(float absRaw, float absSafe);
    }
}
