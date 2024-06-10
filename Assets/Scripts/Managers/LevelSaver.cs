using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Interfaces;
using Zenject;

namespace Managers
{
    public class LevelSaver : ISavable<SceneItem>
    {
        private ObjectsTracker _objectsTracker;
        private SaveService _saveService;
        private GridManager _gridManager;

        [Inject]
        private void Construct(ObjectsTracker objectsTracker, SaveService saveService, GridManager gridManager)
        {
            _objectsTracker = objectsTracker;
            _saveService = saveService;
            _gridManager = gridManager;
        }
    
        public void Save()
        {
            var sceneItems = new List<SceneItem>();
            
            var savedObjects = _objectsTracker.RetrieveAll().ToList();

            foreach (var item in savedObjects)
            {
                var cell = _gridManager.GetGridCellByController(item.Controller);
                
                if (!item.Controller)
                {
                    continue;
                }
                
                if (cell == null)
                {
                    continue;
                }
                
                var sceneItem = new SceneItem(item.Controller.ObjectData.ID, cell.CellID);
                
                sceneItems.Add(sceneItem);
            }

            _saveService.SaveLevelProgress(sceneItems).Forget();
        }

        public async UniTask<List<SceneItem>> Load()
        {
            return await _saveService.LoadLevelProgress();
        }
    }
}
