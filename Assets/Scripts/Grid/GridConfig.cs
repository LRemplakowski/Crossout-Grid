using UnityEngine;

namespace Crossout.Grid
{
    [CreateAssetMenu(fileName = "New Grid Config", menuName = "Crossout Grid/Grid Config")]
    public class GridConfig : ScriptableObject, IGridDefinition
    {
        [SerializeField]
        private int _gridWidth = IGridDefinition.DEFAULT_GRID_WIDTH;
        [SerializeField]
        private int _gridHeight = IGridDefinition.DEFAULT_GRID_HEIGHT;
        [SerializeField]
        private int _minCellValue = IGridDefinition.DEFAULT_MIN_VALUE;
        [SerializeField]
        private int _maxCellValue = IGridDefinition.DEFAULT_MAX_VALUE;

        private IGridRandomizationStrategy _cachedStrategy;

        public IGridRandomizationStrategy GetCellRandomizationStrategy()
        {
            _cachedStrategy ??= new UnityRandomStrategy(_minCellValue, _maxCellValue);
            return _cachedStrategy;
        }

        public Vector2Int GetDimensions()
        {
            return new Vector2Int(_gridWidth, _gridHeight);
        }
    }
}