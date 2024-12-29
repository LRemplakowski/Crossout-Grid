namespace Crossout.Grid
{
    public class GridFactory
    {
        private readonly GridData.Builder _gridBuilder = new();

        public GridData CreateDefault()
        {
            _gridBuilder.Reset();
            return _gridBuilder.Build();
        }

        public GridData CreateWithConfig(IGridDefinition definition)
        {
            var gridDimensions = definition.GetDimensions();
            var gridCellStrategy = definition.GetCellRandomizationStrategy();
            return _gridBuilder.Reset()
                               .WithDimensions(gridDimensions.x, gridDimensions.y)
                               .WithStrategy(gridCellStrategy)
                               .Build();
        }
    }

}