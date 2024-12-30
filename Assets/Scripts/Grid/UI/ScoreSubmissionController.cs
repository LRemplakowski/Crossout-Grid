using System;
using Crossout.Core;
using UnityEngine;

namespace Crossout.Grid.UI
{
    public class ScoreSubmissionController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _submitButtonGO;
        [SerializeField]
        private GridController _gridController;
        [SerializeField]
        private GameManager _gameManager;

        private void Awake()
        {
            _gridController.OnCellSelected += OnCellSelectionUpdated;
            _gridController.OnCellDeselected += OnCellSelectionUpdated;
            _gridController.OnGridUpdated += OnGridUpdated;
            _gridController.OnGridDisposed += OnGridDisposed;
        }

        private void OnDestroy()
        {
            _gridController.OnCellDeselected -= OnCellSelectionUpdated;
            _gridController.OnCellDeselected -= OnCellSelectionUpdated;
            _gridController.OnGridUpdated -= OnGridUpdated;
            _gridController.OnGridDisposed -= OnGridDisposed;
        }

        private void OnGridDisposed()
        {
            OnCellSelectionUpdated(0);
        }

        private void OnGridUpdated(GridData _)
        {
            OnCellSelectionUpdated(0);
        }

        private void OnCellSelectionUpdated(int selectedCellsCount)
        {
            _submitButtonGO.SetActive(selectedCellsCount > 0);
        }

        public void SubmitScore()
        {
            _gameManager.FinishRound();
        }
    }
}