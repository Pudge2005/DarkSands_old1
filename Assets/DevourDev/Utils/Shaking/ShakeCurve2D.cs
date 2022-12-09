using DevourDev.Unity.Utils;
using UnityEngine;

namespace DevourDev.Unity.Utils.Shake
{
    [System.Serializable]
    public sealed class ShakeCurve2D
    {
        [Tooltip("Максимальное смещение по X (в юнитах)")]
        [SerializeField] private AnimationCurve _amplitudeX;
        [Tooltip("Максимальное смещение по Y (в юнитах)")]
        [SerializeField] private AnimationCurve _amplitudeY;

        [Tooltip("Частота смещений (смещений в секунду)")]
        [SerializeField] private AnimationCurve _frequency;


        public ShakeCurve2D(AnimationCurve amplitudeX, AnimationCurve amplitudeY, AnimationCurve frequency)
        {
            _amplitudeX = amplitudeX;
            _amplitudeY = amplitudeY;
            _frequency = frequency;
        }


        public ShakeCurve2D(float ampXMin, float ampXMax, float ampYMin,
                            float ampYMax, float frqMin, float frqMax)
        {
            _amplitudeX = AnimationHelpers.CurveStartEnd(ampXMin, ampXMax);
            _amplitudeY = AnimationHelpers.CurveStartEnd(ampYMin, ampYMax);
            _frequency = AnimationHelpers.CurveStartEnd(frqMin, frqMax);
        }


        public void SetAmplitudeXCurve(AnimationCurve curve) => _amplitudeX = curve;
        public void SetAmplitudeYCurve(AnimationCurve curve) => _amplitudeY = curve;
        public void SetFrequencyCurve(AnimationCurve curve) => _frequency = curve;


        public void Evaluate(float t, out Vector2 ampl, out float freq)
        {
            ampl.x = _amplitudeX.Evaluate(t);
            ampl.y = _amplitudeY.Evaluate(t);

            freq = _frequency.Evaluate(t);
        }
    }

}