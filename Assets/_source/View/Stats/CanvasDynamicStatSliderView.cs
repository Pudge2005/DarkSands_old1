using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Fighting
{
    public sealed class CanvasDynamicStatSliderView : DynamicStatSliderViewBase
    {
        [SerializeField] private Gradient _fillGradient;
        [SerializeField] private Gradient _bgGradient;

        [SerializeField] private Image _iconImg;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _bgImg;
        [SerializeField] private Image _fillImg;
        [SerializeField] private TextMeshProUGUI _text;


        protected override void InitInternal(float min, float max, float value)
        {
            _slider.minValue = min;
            _slider.maxValue = max;
            SetSliderValue(value);

            _iconImg.sprite = Stat.Icon;
        }


        protected override void HandleMinBoundChanged(float min)
        {
            _slider.minValue = min;
        }

        protected override void HandleMaxBoundChanged(float max)
        {
            _slider.maxValue = max;
        }

        protected override void HandleValueChanged(float value, float rawDelta, float safeDelta)
        {
            SetSliderValue(value);
        }


        private void SetSliderValue(float value)
        {
            _slider.value = value;
            var t = _slider.normalizedValue;
            _fillImg.color = _fillGradient.Evaluate(t);
            _bgImg.color = _bgGradient.Evaluate(t);
            _text.text = $"{value:N0}/{Max:N0}";
        }


    }
}
