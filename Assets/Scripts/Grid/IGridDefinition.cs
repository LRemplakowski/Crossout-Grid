using UnityEngine;

namespace Crossout.Grid
{
    public interface IGridDefinition
    {
        public const int DEFAULT_MIN_VALUE = 1;
        public const int DEFAULT_MAX_VALUE = 11;
        public const int DEFAULT_GRID_WIDTH = 4;
        public const int DEFAULT_GRID_HEIGHT = 4;

        Vector2Int GetDimensions();
        IGridRandomizationStrategy GetCellRandomizationStrategy();
    }
}