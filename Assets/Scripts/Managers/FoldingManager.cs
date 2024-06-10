using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Environment;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class FoldingManager : MonoBehaviour
    {
        public event Action<FoldingItemsType> ObjectsFolded;
        
        private ObjectsSpawner _spawner;
        private GridManager _gridManager;
        private ObjectsTracker _objectsTracker;
        private LevelSaver _levelSaver;

        private List<TrackedObjectData> _objects = new List<TrackedObjectData>();
        
        [Inject]
        private void Construct(ObjectsTracker tracker, GridManager gridManager, ObjectsSpawner spawner, LevelSaver levelSaver)
        {
            _objectsTracker = tracker;
            _gridManager = gridManager;
            _spawner = spawner;
            _levelSaver = levelSaver;
        }

        private void Start()
        {
            RegisterObjects();
        }

        public void FoldItems(FoldingObjectController first, FoldingObjectController second)
        {
            var cell = _gridManager.GetGridCellByController(second);
            var type = FoldingItemUpgradeData.GetTypeForUpgrade(first.ObjectData.ObjectType);
            
            DestroyImmediate(first.gameObject);
            DestroyImmediate(second.gameObject);

            if (type == FoldingItemsType.None)
            {
                _levelSaver.Save();
                
                ObjectsFolded?.Invoke(type);
                return;
            }

            var instance =  _spawner.SpawnByObjectType(type);

            _gridManager.PlaceObjectByCell(instance, cell);
            
            _levelSaver.Save();
            
            ObjectsFolded?.Invoke(type);
        }

        private void OnObjectDestroyed(BaseComponent baseComponent)
        {
            var destroyedObject = baseComponent.ComponentsKeeper.GetFirstComponentOfType<FoldingObjectController>(false);
            var tracked = _objectsTracker.GetTrackedObjectDataByController(destroyedObject);

            destroyedObject.Destroyed -= OnObjectDestroyed;
            
            _objectsTracker.UntrackItem(tracked);
            _objects.Remove(tracked);
        }
        
        private void RegisterObjects()
        {
            _objects = _objectsTracker.RetrieveAll().ToList();

            foreach (var item in _objects)
            {
                item.Controller.Destroyed += OnObjectDestroyed;
            }
        }
    }
}
