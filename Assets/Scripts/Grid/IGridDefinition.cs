using UnityEngine;

namespace Crossout.Grid
{
    public interface IGridDefinition
    {
        Vector2Int GetDimensions();
        IGridRandomizationStrategy GetCellRandomizationStrategy();
    }
}