using UnityEngine;

namespace Crossout.Grid
{
    [CreateAssetMenu(fileName = "New Grid Config", menuName = "Crossout Grid/Grid Config")]
    public class GridConfig : ScriptableObject, IGridDefinition
    {
        [SerializeField]
        private int _gridWidth;
        [SerializeField]
        private int _gridHeight;
        [SerializeField]
        private int _minCellValue;
        [SerializeField]
        private int _maxCellValue;

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