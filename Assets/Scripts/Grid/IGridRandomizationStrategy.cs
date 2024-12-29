namespace Crossout.Grid
{
    public interface IGridRandomizationStrategy
    {
        int GetCellValue();
    }

    public class UnityRandomStrategy : IGridRandomizationStrategy
    {
        private readonly int _minValue, _maxValue;

        public UnityRandomStrategy(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public int GetCellValue()
        {
            return UnityEngine.Random.Range(_minValue, _maxValue + 1);
        }
    }
}