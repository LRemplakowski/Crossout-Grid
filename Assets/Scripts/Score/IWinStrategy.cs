namespace Crossout.Scoring
{
    public interface IWinStrategy
    {
        public bool EvaluateWin(in int selectedCellScore, out int netScoreChange);
    }

    public class DefaultWinStrategy : IWinStrategy
    {
        private const int MAX_SCORE = 21;

        public bool EvaluateWin(in int selectedCellScore, out int netScoreChange)
        {
            netScoreChange = 0;
            if (selectedCellScore > MAX_SCORE)
            {
                netScoreChange -= selectedCellScore / 2;
                return false;
            }
            else
            {
                netScoreChange = selectedCellScore;
                return true;
            }
        }
    }
}
