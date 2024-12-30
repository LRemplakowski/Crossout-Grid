using UnityEngine;

namespace Crossout.Scoring
{
    public class ScoreController : MonoBehaviour
    {
        private IWinStrategy _cachedWinStrategy = new DefaultWinStrategy();
        private int _currentScore = 0;

        public void ResetScore()
        {
            _currentScore = 0;
        }

        public UISummaryScreen.SummaryInfoDM EvaluateRoundScore(int selectedCellsValue)
        {
            bool isWin = _cachedWinStrategy.EvaluateWin(in selectedCellsValue, out int netScoreChange);
            _currentScore += netScoreChange;
            return new()
            {
                PlayerWon = isWin,
                ScoreAdjustment = netScoreChange,
                TotalScore = _currentScore,
            };
        }
    }
}
