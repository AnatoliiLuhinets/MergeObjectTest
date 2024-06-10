using System;
using System.Collections.Generic;
using Data;
using Environment;

namespace Managers
{
    public class ObjectsTracker : IDisposable
    {
        public event Action<TrackedObjectData> OnObjectRegistered;
        public event Action<TrackedObjectData> OnObjectUntracked;
        
        private readonly Dictionary<FoldingItemsType, List<TrackedObjectData>> _objectsByType = new();
        
        public TrackedObjectData RegisterObject(FoldingItemsType type, FoldingObjectController controller)
        {
            if (!_objectsByType.TryGetValue(type, out var list))
            {
                list = new List<TrackedObjectData>();
                _objectsByType.Add(type, list);
            }

            var tracked = new TrackedObjectData(type, controller);
            list.Add(tracked);
            
            OnObjectRegistered?.Invoke(tracked);
            return tracked;
        }
        
        public TrackedObjectData GetTrackedObjectDataByController(FoldingObjectController controller)
        {
            var trackedItem = new TrackedObjectData();
            
            foreach (var items in _objectsByType.Values)
            {
                foreach (var item in items)
                {
                    if (item.Controller == controller)
                    {
                        trackedItem = item;
                    }
                }
            }
            
            return trackedItem;
        }
        
        public bool FetchObjects(FoldingItemsType itemType, out IEnumerable<TrackedObjectData> items)
        {
            var hasItems = _objectsByType.TryGetValue(itemType, out var list);
            items = list;
            return hasItems;
        }
        
        public IEnumerable<TrackedObjectData> RetrieveAll()
        {
            foreach (var (type, items) in _objectsByType)
            {
                foreach (var item in items)
                {
                    if (!item.Controller)
                        continue;
                    
                    yield return item;
                }
            }
        }
        
        public bool UntrackItem(TrackedObjectData trackedObject)
        {
            if (_objectsByType.TryGetValue(trackedObject.ObjectType, out var list))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Equals(trackedObject))
                    {
                        list.RemoveAt(i);
                        OnObjectUntracked?.Invoke(trackedObject);
                        return true;
                    }
                }
            }
            return false;
        }
        
        private void ClearTracking()
        {
            foreach (var (_, items) in _objectsByType)
            {
                foreach (var item in items)
                {
                    OnObjectUntracked?.Invoke(item);
                }   
            }
            
            _objectsByType.Clear();
        }
        
        
        public void Dispose()
        {
            ClearTracking();
        }
    }
}
