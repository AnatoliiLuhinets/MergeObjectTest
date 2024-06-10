using Environment;
using Managers;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class InputHandler : MonoBehaviour
    {
        private InputManager _inputManager;
        private FoldingManager _foldingManager;
        
        [Inject]
        private void Construct(InputManager inputManager, FoldingManager foldingManager)
        {
            _inputManager = inputManager;
            _foldingManager = foldingManager;

            _inputManager.Dragged += HandleDrag;
            _inputManager.Dropped += HandleDrop;
        }
        
        private void HandleDrag(MovableObject movableObject, Vector3 newPosition)
        {
            movableObject.Drag(newPosition);
        }

        private void HandleDrop(MovableObject movableObject)
        {
            var foldingObject = movableObject.ComponentsKeeper.GetFirstComponentOfType<FoldingObjectController>(false);

            if (foldingObject != null && foldingObject.CheckFolding())
            {
                var foldingItems = foldingObject.GetFoldingItems();
                _foldingManager.FoldItems(foldingItems.Item1, foldingItems.Item2);
                
                return;
            }
            
            movableObject.ReturnToStart();
        }
        
        public void OnDestroy()
        {
            _inputManager.Dragged += HandleDrag;
            _inputManager.Dropped += HandleDrop;
        }
    }
}
