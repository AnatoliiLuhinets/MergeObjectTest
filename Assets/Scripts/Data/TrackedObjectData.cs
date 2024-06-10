using System;
using Environment;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct TrackedObjectData
    {
        [field: SerializeField] public FoldingItemsType ObjectType { get; private set; }
        [field: SerializeField] public FoldingObjectController Controller { get; private set; }

        public TrackedObjectData(FoldingItemsType objectType, FoldingObjectController controller)
        {
            ObjectType = objectType;
            Controller = controller;
        }
    }
}
