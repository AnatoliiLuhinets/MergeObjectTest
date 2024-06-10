using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Environment;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int _firstObjectsCount = 12;

        private List<SceneItem> _loadItems;
        private GridManager _gridManager;
        private ObjectsSpawner _objectsSpawner;
        private ObjectDataLibrary _objectDataLibrary;
        private LevelSaver _levelSaver;
        private FoldingManager _foldingManager;
        
        [Inject]
        private void Construct(ObjectsSpawner objectsSpawner, ObjectDataLibrary objectDataLibrary, 
            GridManager gridManager, LevelSaver levelSaver, FoldingManager foldingManager)
        {
            _objectsSpawner = objectsSpawner;
            _objectDataLibrary = objectDataLibrary;
            _gridManager = gridManager;
            _levelSaver = levelSaver;
            _foldingManager = foldingManager;

            _foldingManager.ObjectsFolded += OnObjectFolded;
        }

        private void OnObjectFolded(FoldingItemsType type)
        {
            var itemsCounts = FoldingItemUpgradeData.GetUpgradeLevel(type);
            
            SpawnRandomlyItems(itemsCounts);
        }

        private void Start()
        {
            CreateObjects().Forget();
        }

        private async UniTask CreateObjects()
        {
            _loadItems = await _levelSaver.Load();

            if (_loadItems.Count != 0)
            {
                CreateLoadItems();
                return;
            }

            SpawnRandomlyItems(_firstObjectsCount);
        }

        private void CreateLoadItems()
        {
            foreach (var item in _loadItems)
            {
                var instance = _objectsSpawner.SpawnByID(item.ObjectID);
                var cell = _gridManager.GetGridCellByID(item.CellID);
                    
                _gridManager.PlaceObjectByCell(instance, cell);
            }
        }
        
        private void SpawnRandomlyItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var data = _objectDataLibrary.DataLibrary[Random.Range(0, _objectDataLibrary.DataLibrary.Capacity)];
            
                var item =  _objectsSpawner.SpawnByObjectData(data);
            
                _gridManager.PlaceObject(item);
            }
        }

        private void OnApplicationQuit()
        {
            _levelSaver.Save();
        }

        private void OnDestroy()
        {
            _foldingManager.ObjectsFolded -= OnObjectFolded;
        }
    }
}
