using DevourDev.Unity.International;
using DevourDev.Unity.Utils;
using UnityEngine;

namespace Game.Fighting
{
    public sealed class SimpleHealingAndDamage : PoppingDynamicStatDeltasViewBase
    {
        private const string _rawInterpolationKey = "<raw>";
        private const string _safeInterpolationKey = "<safe>";
        [SerializeField] private PoppingText3D _healPopPrefab;
        [SerializeField] private PoppingText3D _damagePopPrefab;
        [Header("Raw Interpolation Key: " + _rawInterpolationKey + "\n"
              + "Safe Internalation Key: " + _safeInterpolationKey)]
        [SerializeField] private MultiCulturalString _healText;
        [SerializeField] private MultiCulturalString _dmgText;


        protected override void HandleDamage(float absRaw, float absSafe)
        {
            var pop = Instantiate(_damagePopPrefab);
            pop.transform.position = OriginPoint.position;
            string interpolatedText = _dmgText.Get()
                .Replace(_rawInterpolationKey, absRaw.ToString("N0"))
                .Replace(_safeInterpolationKey, absSafe.ToString("N0"));
            pop.SetText(interpolatedText);
            pop.Init(1, 1);
        }

        protected override void HandleHeal(float absRaw, float absSafe)
        {
            var pop = Instantiate(_healPopPrefab);
            pop.transform.position = OriginPoint.position;
            string interpolatedText = _healText.Get()
                .Replace(_rawInterpolationKey, absRaw.ToString("N0"))
                .Replace(_safeInterpolationKey, absSafe.ToString("N0"));
            pop.SetText(interpolatedText);
            pop.Init(1, 1);
        }
    }
}
