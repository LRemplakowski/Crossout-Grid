using System;
using UnityEngine;
using UnityEngine.UI;

namespace Crossout.Grid.UI
{
    public class GridCellView : MonoBehaviour, IGridCellView
    {
        public GameObject Self => gameObject;

        [SerializeField]
        private Button _deselectedButton;
        [SerializeField]
        private Button _selectedButton;

        [SerializeField]
        private Vector2Int _gridPosition;
        private Action<Vector2Int> _onSelected;
        private Action<Vector2Int> _onDeselected;
        private Func<bool> _canSelect;

        private void Awake()
        {
            UpdateButtonVisibility(false);
        }

        public void OnSelect()
        {
            if (_canSelect?.Invoke() is false)
                return;
            UpdateButtonVisibility(true);
            _onSelected?.Invoke(_gridPosition);
        }

        public void OnDeselect()
        {
            UpdateButtonVisibility(false);
            _onDeselected?.Invoke(_gridPosition);
        }

        public void SetGridPosition(Vector2Int position)
        {
            _gridPosition = position;
        }

        public void SetSelectedDelegate(Action<Vector2Int> onSelected)
        {
            _onSelected = onSelected;
        }

        public void SetDeselectedDelegate(Action<Vector2Int> onDeselected)
        {
            _onDeselected = onDeselected;
        }

        public void SetCanSelectDelegate(Func<bool> canSelect)
        {
            _canSelect = canSelect;
        }

        private void UpdateButtonVisibility(bool selected)
        {
            _deselectedButton.gameObject.SetActive(!selected);
            _deselectedButton.interactable = !selected;
            _selectedButton.gameObject.SetActive(selected);
            _selectedButton.interactable = selected;
        }
    }
}
