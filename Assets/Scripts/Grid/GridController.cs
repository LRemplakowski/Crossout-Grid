using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crossout.Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField]
        private GridConfig _gridGenerationConfig;

        [SerializeField, HideInInspector]
        private List<IGridCellView> _gridViews = new();

        private GridFactory _gridFactory;
        private GridData _cachedGridData;

        private HashSet<Vector2Int> _selectedCells = new();

        public event Action<GridData> OnGridUpdated;

        private void Awake()
        {
            _gridFactory = new();
        }

        private void Start()
        {
            RebuildGrid();
        }

        public void RebuildGrid()
        {
            if (_gridGenerationConfig)
            {
                _cachedGridData = _gridFactory.CreateWithConfig(_gridGenerationConfig);
            }
            else
            {
                _cachedGridData = _gridFactory.CreateDefault();
            }
            OnGridUpdated?.Invoke(_cachedGridData);
        }

        public void SelectCell(Vector2Int position)
        {
            if (CanSelectCell() is false)
                return;
            _selectedCells.Add(position);
        }

        public void DeselectCell(Vector2Int position)
        {
            _selectedCells.Remove(position);
        }

        public bool CanSelectCell()
        {
            return true;
        }
    }
}
