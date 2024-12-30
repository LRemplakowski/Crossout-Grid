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
            _gridController.OnCellSelected += UpdateButtonVisibility;
            _gridController.OnCellDeselected += UpdateButtonVisibility;
            _gridController.OnGridUpdated += ResetVisibility;
        }

        private void OnDestroy()
        {
            _gridController.OnCellDeselected -= UpdateButtonVisibility;
            _gridController.OnCellDeselected -= UpdateButtonVisibility;
            _gridController.OnGridUpdated -= ResetVisibility;
        }

        private void ResetVisibility(GridData _)
        {
            UpdateButtonVisibility(0);
        }

        private void UpdateButtonVisibility(int selectedCellsCount)
        {
            _submitButtonGO.SetActive(selectedCellsCount > 0);
        }

        public void SubmitScore()
        {
            _gameManager.FinishRound();
        }
    }
}