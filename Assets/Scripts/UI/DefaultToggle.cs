using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefaultToggle : MonoBehaviour
    {
        public event Action<bool>  ToggleStateChanged;
        
        [SerializeField] private Toggle _toggle;

        protected virtual void Awake()
        {
            _toggle.onValueChanged.AddListener(OnToggleStateChanged);
        }

        protected virtual void OnToggleStateChanged(bool state)
        {
            ToggleStateChanged?.Invoke(state);
        }
        
        protected virtual void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleStateChanged);
        }

        public virtual void SetState(bool state)
        {
            _toggle.isOn = state;
        }
    }
}
