using System.Collections;
using System.Collections.Generic;
using Crossout.Grid;
using Crossout.Scoring;
using UnityEngine;

namespace Crossout.Core
{
    public class GameManager : MonoBehaviour
    {
        private const int GAME_WIN_THRESHOLD = 60;

        [SerializeField]
        private GridController _gridController;
        [SerializeField]
        private ScoreController _scoreController;
        [SerializeField]
        private UISummaryScreen _uiSummaryScreen;
        [SerializeField]
        private GameObject _uiGameWin;

        public void BeginGame()
        {
            CleanupPreviousGame();
            NextRound();
        }

        public void FinishRound()
        {
            int selectedCellsValue = _gridController.GetSelectedCellValues();
            var summaryInfo = _scoreController.EvaluateRoundScore(selectedCellsValue);
            CleanupPreviousRound();
            if (summaryInfo.TotalScore.GetValueOrDefault() >= GAME_WIN_THRESHOLD)
            {
                _uiSummaryScreen.ShowSummary(summaryInfo, GameWon);
            }
            else
            {
                _uiSummaryScreen.ShowSummary(summaryInfo, NextRound);
            }
        }

        private void NextRound()
        {
            _gridController.RebuildGrid();
        }

        private void GameWon()
        {
            _uiGameWin.SetActive(true);
        }

        private void CleanupPreviousGame()
        {
            _scoreController.ResetScore();
            CleanupPreviousRound();
        }

        private void CleanupPreviousRound()
        {
            _gridController.CleanupGrid();
        }
    }
}
