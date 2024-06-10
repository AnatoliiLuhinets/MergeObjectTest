using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefaultSlider : MonoBehaviour
    {
        public event Action<float> SliderValueChanged;
        
        [SerializeField] protected Slider _slider;

        protected virtual void Awake()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        protected virtual void OnSliderValueChanged(float value)
        {
            SliderValueChanged?.Invoke(value);
        }

        protected virtual void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }
    }
}
