using System;
using System.Collections.Generic;
using UnityEngine;

namespace Crossout.Grid.UI
{
    public class GridViewManager : MonoBehaviour
    {
        [SerializeField]
        private GridCellView _viewPrefab;
        [SerializeField]
        private Transform _viewParent;
        [SerializeField]
        private GridController _gridController;

        private GridViewFactory _viewFactory;
        private List<GridCellView> _viewInstances;

        private void Awake()
        {
            _viewFactory = new(_viewPrefab, _viewParent);
            _viewInstances = new();

            _gridController.OnGridUpdated += OnGridUpdated;
        }

        private void OnDestroy()
        {
            _gridController.OnGridUpdated -= OnGridUpdated;
        }

        private void OnGridUpdated(GridData gridData)
        {
            SetupViews(gridData);
        }

        private void SetupViews(int[,] grid)
        {
            CleanupPreviousViews();
            InstantiateViews(grid);
        }

        private void CleanupPreviousViews()
        {
            _viewInstances.ForEach(view => Destroy(view.gameObject));
            _viewInstances.Clear();
        }

        private void InstantiateViews(int[,] grid)
        {
            var width = grid.GetLength(0);
            var height = grid.GetLength(1);
            ViewContextData viewContext = CreateViewContextBase();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    viewContext.Position = new(x, y);
                    _viewInstances.Add(_viewFactory.Create(viewContext));
                }
            }
        }

        private ViewContextData CreateViewContextBase()
        {
            Action<Vector2Int> selectedDelegate = _gridController.SelectCell;
            Action<Vector2Int> deselectedDelegate = _gridController.DeselectCell;
            Func<bool> canSelectDelegate = _gridController.CanSelectCell;
            return new ViewContextData(Vector2Int.zero, selectedDelegate, deselectedDelegate, canSelectDelegate);
        }

        private struct ViewContextData
        {
            public Vector2Int Position { get; set; }
            public Action<Vector2Int> OnSelected { get; }
            public Action<Vector2Int> OnDeselected { get; }
            public Func<bool> CanSelect { get; }

            public ViewContextData(Vector2Int position, Action<Vector2Int> onSelected, Action<Vector2Int> onDeselected, Func<bool> canSelect)
            {
                Position = position;
                OnSelected = onSelected;
                OnDeselected = onDeselected;
                CanSelect = canSelect;
            }
        }

        private class GridViewFactory
        {
            private readonly GridCellView _viewPrefab;
            private readonly Transform _viewParent;

            public GridViewFactory(GridCellView prefab, Transform parent)
            {
                _viewPrefab = prefab;
                _viewParent = parent;
            }

            public GridCellView Create(ViewContextData context)
            {
                var viewInstance = Instantiate(_viewPrefab, _viewParent);
                viewInstance.SetGridPosition(context.Position);
                viewInstance.SetSelectedDelegate(context.OnSelected);
                viewInstance.SetDeselectedDelegate(context.OnDeselected);
                viewInstance.SetCanSelectDelegate(context.CanSelect);
                return viewInstance;
            }
        }
    }
}
