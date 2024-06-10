using Data;
using UnityEngine;

namespace Environment
{
    public class FoldingObjectController : BaseComponent
    {
        [SerializeField] private ObjectData _objectData;
        
        private TriggerController _triggerController;
        
        public ObjectData ObjectData => _objectData;
        private FoldingObjectController _otherController;

        private void Start()
        {
            _triggerController = ComponentsKeeper.GetFirstComponentOfType<TriggerController>(false);
        }

        public bool CheckFolding()
        {
            _otherController = _triggerController.GetCachedObject();

            if (!_otherController)
            {
                return false;
            }
            
            return _otherController.ObjectData.ObjectType == _objectData.ObjectType;
        }

        public (FoldingObjectController, FoldingObjectController) GetFoldingItems()
        {
            return (this, _otherController);
        }
    }
}
