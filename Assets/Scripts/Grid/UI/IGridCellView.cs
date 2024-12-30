using System;
using UnityEngine;

namespace Crossout.Grid
{
    public interface IGridCellView
    {
        void SetGridPosition(Vector2Int position);
        void SetSelectedDelegate(Action<Vector2Int> onSelected);
        void SetDeselectedDelegate(Action<Vector2Int> onDeselected);
        void SetCanSelectDelegate(Func<bool> canSelect);
    }
}