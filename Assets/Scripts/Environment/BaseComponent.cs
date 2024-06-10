using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Environment
{
    public class BaseComponent : MonoBehaviour
    {
        public event Action<BaseComponent> Destroyed; 
        
        [SerializeField] private ComponentsKeeper _componentsKeeper;
        
        private bool _registeredInKeeper = false;
        
        public ComponentsKeeper ComponentsKeeper
        {
            get
            {
                RegisterInKeeper();
                return _componentsKeeper;
            }
            protected set
            {
                _componentsKeeper = value;
                RegisterInKeeper();
            }
        }
        
        protected virtual void OnValidate()
        {
            _componentsKeeper = GetComponentInParent<ComponentsKeeper>(true);
        }

        protected virtual void Awake()
        {
            if(_componentsKeeper == null)
                _componentsKeeper = GetComponentInParent<ComponentsKeeper>(true);
            
            RegisterInKeeper();
        }

        private void RegisterInKeeper()
        {
            if(_registeredInKeeper)
                return;
            
            if (!_componentsKeeper)
                return;
            
            _componentsKeeper.AddComponent(this);
            
            _registeredInKeeper = true;
        }

        protected virtual void OnDestroy()
        {
            Destroyed?.Invoke(this);
            
            if(!_componentsKeeper)
                return;
            
            _componentsKeeper.DeleteComponent(this);
        }
    }
}
