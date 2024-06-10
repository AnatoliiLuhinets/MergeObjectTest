using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DefaultButton : MonoBehaviour
    {
        public event Action ButtonClicked;
        
        [SerializeField] private Button _button;

        protected virtual void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        protected virtual void OnButtonClicked()
        {
            ButtonClicked?.Invoke();
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
    }
}
