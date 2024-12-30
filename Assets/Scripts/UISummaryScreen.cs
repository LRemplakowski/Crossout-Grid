using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISummaryScreen : MonoBehaviour
{
    public class SummaryInfoDM
    {
        public bool PlayerWon;
        public int? ScoreAdjustment;
        public int? TotalScore;
    }

    [SerializeField]
    private Text winnerLabel;

    [SerializeField]
    private Text scoreAdjustmentLabel;
    [SerializeField]
    private Text totalScoreLabel;

    private UnityAction onClose;

    /// <summary>
    /// Call this method to display summary screen. Pass callback to know when the window was closed.
    /// </summary>
    /// <param name="info">Small summary of the game.</param>
    /// <param name="onCloseCallback">Called whenever window will be closed.</param>
    public void ShowSummary(SummaryInfoDM info, UnityAction onCloseCallback = null)
    {
        onClose = onCloseCallback;

        if (info != null)
        {
            winnerLabel.text = info.PlayerWon ? "You won!!!" : "You lose...";

            scoreAdjustmentLabel.gameObject.SetActive(info.ScoreAdjustment.HasValue);
            totalScoreLabel.gameObject.SetActive(info.TotalScore.HasValue);
            if (info.ScoreAdjustment.HasValue)
            {
                scoreAdjustmentLabel.text = "Points: " + info.ScoreAdjustment.Value.ToString("N0", CultureInfo.InvariantCulture);
            }
            if (info.TotalScore.HasValue)
            {
                totalScoreLabel.text = "Total Score: " + info.TotalScore.Value.ToString("N0", CultureInfo.InvariantCulture);
            }
        }
        else
        {
            Debug.LogError("No info was passed!");
            winnerLabel.text = "Missing info";
            totalScoreLabel.gameObject.SetActive(false);
        }

        gameObject.SetActive(true);
    }

    /// <summary>
    /// This method is assigned to the button, but you can call it if you want.
    /// </summary>
    public void CloseSummary()
    {
        onClose?.Invoke();
        onClose = null;
        gameObject.SetActive(false);
    }
}
