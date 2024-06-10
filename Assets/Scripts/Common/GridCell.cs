using System;
using Environment;
using UnityEngine;

namespace Common
{
    [Serializable]
    public class GridCell
    {
        [field: SerializeField] public int CellID { get; private set; }
        [field: SerializeField] public bool IsOccupied { get; private set; }
        [field: SerializeField] public FoldingObjectController Controller { get; private set; }

        public GridCell(int id)
        {
            CellID = id;
            IsOccupied = false;
        }

        public void PlaceObject(FoldingObjectController controller)
        {
            Controller = controller;
            IsOccupied = true;
        }
        
        public void RemoveObject()
        {
            Controller = null;
            IsOccupied = false;
        }
    }
}
