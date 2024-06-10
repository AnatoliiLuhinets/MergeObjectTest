using System.Collections.Generic;
using System.Linq;
using Common;
using Environment;
using UnityEngine;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private int _rows = 5;
        [SerializeField] private int _columns = 5;
        [SerializeField] private float _cellSize = 1.0f;
        
        private List<GridCell> _gridCells = new List<GridCell>();

        public bool PlaceObject(FoldingObjectController item)
        {
            var cell = _gridCells.FirstOrDefault((c) => !c.IsOccupied);

            if (cell != null)
            {
                PlaceObjectInCell(item, cell);
                return true;
            }
            
            return false;
        }

        public bool PlaceObjectByCell(FoldingObjectController item, GridCell cell)
        {
            if (cell !=null)
            {
                PlaceObjectInCell(item, cell);
                return true;
            }
            
            return false;
        }

        private void PlaceObjectInCell(FoldingObjectController item, GridCell cell)
        {
            item.Destroyed += OnItemDestroyed;
            
            cell.PlaceObject(item);
            var cellPosition = CalculatePosition(cell.CellID);
            item.transform.SetParent(transform, true);
            item.transform.localPosition = cellPosition;
                
            item.ComponentsKeeper.GetFirstComponentOfType<MovableObject>(false).SetDefaultPosition(cellPosition);
        }
        
        public GridCell GetGridCellByController(FoldingObjectController controller)
        {
            return _gridCells.FirstOrDefault(cell => cell.Controller == controller);
        }

        public GridCell GetGridCellByID(int ID)
        {
            return _gridCells.FirstOrDefault(cell => cell.CellID == ID);
        }
        
        private void Awake()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    var id = i * _columns + j;
                    var newCell = new GridCell(id);
                    _gridCells.Add(newCell);
                }
            }
        }
        
        private void OnItemDestroyed(BaseComponent item)
        {
            var controller = item.ComponentsKeeper.GetFirstComponentOfType<FoldingObjectController>(false);

            var cell = _gridCells.FirstOrDefault((c) => c.Controller == controller);
            
            cell?.RemoveObject();
            
            item.Destroyed -= OnItemDestroyed;
        }

        private Vector3 CalculatePosition(int cellID)
        {
            var row = cellID / _columns;
            var column = cellID % _columns;
            return new Vector3(column * _cellSize, -row * _cellSize, 0);
        }
    }
}