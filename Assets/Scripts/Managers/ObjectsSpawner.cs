using Cysharp.Threading.Tasks;
using Data;
using Environment;
using Zenject;

namespace Managers
{
    public class ObjectsSpawner 
    {
        private readonly DiContainer _diContainer;

        private readonly ObjectDataLibrary _objectsDataLibrary;
        private readonly ObjectsTracker _objectsTracker;

        public ObjectsSpawner(DiContainer diContainer, ObjectsTracker objectsTracker, ObjectDataLibrary objectsDataLibrary)
        {
            _diContainer = diContainer;
            _objectsTracker = objectsTracker;
            _objectsDataLibrary = objectsDataLibrary;
        }

        public FoldingObjectController SpawnByObjectData(ObjectData objectData, bool registerInTracker = true)
        {
            var spawnedObject = _diContainer.InstantiatePrefab(objectData.Prefab);
            var spawnedController = spawnedObject.GetComponent<FoldingObjectController>();

            if (registerInTracker)
                _objectsTracker.RegisterObject(spawnedController.ObjectData.ObjectType, spawnedController);

            return spawnedController;
        }

        public FoldingObjectController SpawnByObjectType(FoldingItemsType type, bool registerInTracker = true)
        {
            var objectData = _objectsDataLibrary.GetObjectDataByType(type);
            
            var spawnResult =  SpawnByObjectData(objectData, registerInTracker);
            
            return spawnResult;
        }

        public FoldingObjectController SpawnByID(string ID, bool registerInTracker = true)
        {
            var objectData = _objectsDataLibrary.GetObjectDataByID(ID);
            
            var spawnResult =  SpawnByObjectData(objectData, registerInTracker);
            
            return spawnResult;
        }
    }
}
