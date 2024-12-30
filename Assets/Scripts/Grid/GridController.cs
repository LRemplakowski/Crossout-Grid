using System;
using System.Collections.Generic;
using System.Linq;
using Crossout.Grid.UI;
using UnityEngine;

namespace Crossout.Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField]
        private GridConfig _gridGenerationConfig;

        private GridFactory _gridFactory;
        private GridData _cachedGridData;

        private readonly HashSet<Vector2Int> _selectedCells = new();

        public delegate void CellSelectionDelegate(int selectedCellsCount);

        public event Action OnGridDisposed;
        public event Action<GridData> OnGridUpdated;
        public event CellSelectionDelegate OnCellSelected;
        public event CellSelectionDelegate OnCellDeselected;

        private void Awake()
        {
            _gridFactory = new();
        }

        public void RebuildGrid()
        {
            CleanupGrid();
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
            OnCellSelected?.Invoke(_selectedCells.Count);
        }

        public void DeselectCell(Vector2Int position)
        {
            _selectedCells.Remove(position);
            OnCellDeselected?.Invoke(_selectedCells.Count);
        }

        public bool CanSelectCell()
        {
            return true;
        }

        public int GetSelectedCellValues()
        {
            return _selectedCells.Sum(position => _cachedGridData[position.x, position.y]);
        }

        public void CleanupGrid()
        {
            _cachedGridData = null;
            OnGridDisposed?.Invoke();
            _selectedCells.Clear();
            OnCellDeselected?.Invoke(_selectedCells.Count);
        }
    }
}
