using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environment
{
    public class ComponentsKeeper : MonoBehaviour
    {
        [SerializeField] private List<ComponentsKeeper> ChildKeepers = new ();

        public event Action<ComponentsKeeper> OnDestroying;
        
        private readonly List<BaseComponent> _componentLinks = new();
        

        private void Awake()
        {
            foreach (var childKeeper in ChildKeepers)
            {
                childKeeper.OnDestroying += RemoveChildKeeper;
            }
        }

        private void OnDestroy()
        {
            foreach (var childKeeper in ChildKeepers)
            {
                childKeeper.OnDestroying -= RemoveChildKeeper;
            }

            OnDestroying?.Invoke(this);
        }

        public void AddComponent(BaseComponent component)
        {
            if(_componentLinks.Contains(component))
                return;
            
            _componentLinks.Add(component);
        }

        public void DeleteComponent(BaseComponent component)
        {
            if(!_componentLinks.Remove(component))
                return;
        }
        
        public T GetFirstComponentOfType<T>(bool includeChildrenKeepers)
        {
            foreach (var component in _componentLinks)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }
            
            if(!includeChildrenKeepers)
                return default;

            foreach (var childKeeper in ChildKeepers)
            {
                if(!childKeeper)
                    continue;

                var component = childKeeper.GetFirstComponentOfType<T>(true);

                if (component != null)
                    return component;
            }
            
            return default;
        }

        public IEnumerable<T> GetAllComponentsOfType<T>(bool includeChildrenKeepers)
        {
            foreach (var component in _componentLinks)
            {
                if (component is T typedComponent)
                    yield return typedComponent;
            }

            if (!includeChildrenKeepers)
                yield break;

            foreach (var childKeeper in ChildKeepers)
            {
                if(!childKeeper)
                    continue;

                foreach (var item in childKeeper.GetAllComponentsOfType<T>(true))
                {
                    yield return item;
                }
            }
        }

        private void AddChildKeeper(ComponentsKeeper childKeeper)
        {
            if(!childKeeper.transform.IsChildOf(transform))
                return;

            if(ChildKeepers.Contains(childKeeper))
                return;
            
            ChildKeepers.Add(childKeeper);
            childKeeper.OnDestroying += RemoveChildKeeper;
        }

        private void RemoveChildKeeper(ComponentsKeeper childKeeper)
        {
            if(!ChildKeepers.Contains(childKeeper))
                return;

            ChildKeepers.Remove(childKeeper);
            childKeeper.OnDestroying -= RemoveChildKeeper;
        }

        private void OnValidate()
        {
            SetChildKeepers();
        }

        protected void SetChildKeepers()
        {
            var possibleChildKeepers = GetComponentsInChildren<ComponentsKeeper>(true)
                .Where(k => k != this)
                .ToList();

            foreach (var childKeeper in possibleChildKeepers)
                childKeeper.SetChildKeepers();

            ChildKeepers.Clear();
            foreach (var possibleChild in possibleChildKeepers)
            {
                var anyChildContainsThis =
                    possibleChildKeepers.Any(k => k != possibleChild && possibleChild.transform.IsChildOf(k.transform));
                if(anyChildContainsThis)
                    continue;
                ChildKeepers.Add(possibleChild);
            }
        }
    }
}
