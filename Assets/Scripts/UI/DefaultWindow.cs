using System;
using UnityEngine;

namespace UI
{
    public class DefaultWindow : MonoBehaviour
    {
        [SerializeField] protected GameObject Window;
        [SerializeField] protected DefaultButton CloseButton;

        protected virtual void Awake()
        {
            CloseButton.ButtonClicked += OnCloseButtonClicked;
        }

        private void OnCloseButtonClicked()
        {
            Close();
        }

        public virtual void Close()
        {
            Window.gameObject.SetActive(false);
        }
        
        public virtual void Open()
        {
            Window.gameObject.SetActive(true);
        }

        protected virtual void OnDestroy()
        {
            CloseButton.ButtonClicked -= OnCloseButtonClicked;
        }
    }
}
