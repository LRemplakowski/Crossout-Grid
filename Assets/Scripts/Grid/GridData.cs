using System;
using System.Collections.Generic;

namespace Crossout.Grid
{
    public class GridData
    {
        public static implicit operator int[,](GridData data) => data._gridValues.Clone() as int[,];

        private readonly int[,] _gridValues;

        public int this[int x, int y]
        {
            get
            {
                try
                {
                    return _gridValues[x, y];
                }
                catch (ArgumentOutOfRangeException)
                {
                    UnityEngine.Debug.LogError(ErrorMessageFromDimensions(x, y, _gridValues));
                    return -1;
                }
            }
        }

        private static string ErrorMessageFromDimensions(in int x, in int y, int[,] grid)
        {
            var gridX = grid.GetLength(0);
            var gridY = grid.GetLength(1);
            string result = $"Tried accessing element outside of defined grid! Grid dimensions: {gridX};{gridY}! Given: {x};{y}";
            return result;
        }

        private GridData(int[,] gridValues)
        {
            _gridValues = gridValues;
        }

        public class Builder
        {
            private int _gridWidth = IGridDefinition.DEFAULT_GRID_WIDTH;
            private int _gridHeight = IGridDefinition.DEFAULT_GRID_HEIGHT;
            private IGridRandomizationStrategy _cellRandomizationStrategy;

            public Builder Reset()
            {
                _gridWidth = IGridDefinition.DEFAULT_GRID_WIDTH;
                _gridHeight = IGridDefinition.DEFAULT_GRID_HEIGHT;
                _cellRandomizationStrategy = null;
                return this;
            }

            public Builder WithDimensions(int width, int height)
            {
                return WithWidth(width)
                        .WithHeight(height);
            }

            public Builder WithHeight(int height)
            {
                _gridHeight = height;
                return this;
            }

            public Builder WithWidth(int width)
            {
                _gridWidth = width;
                return this;
            }

            public Builder WithStrategy(IGridRandomizationStrategy cellRandomizationStrategy)
            {
                _cellRandomizationStrategy = cellRandomizationStrategy;
                return this;
            }

            public GridData Build()
            {
                if (_gridWidth < 1 || _gridHeight < 1)
                {
                    throw new InvalidOperationException($"Invalid Grid dimensions! Dimension cannot be less than 1! Width: {_gridWidth}, Height: {_gridHeight}");
                }
                int[,] cells = new int[_gridWidth, _gridHeight];
                EnsureRandomizationStrategy();
                PopulateCells(cells, _cellRandomizationStrategy);
                return new GridData(cells);
            }

            private void EnsureRandomizationStrategy()
            {
                _cellRandomizationStrategy ??= new UnityRandomStrategy(IGridDefinition.DEFAULT_MIN_VALUE, IGridDefinition.DEFAULT_MAX_VALUE);
            }

            private static void PopulateCells(int[,] cells, IGridRandomizationStrategy populationStrategy)
            {
                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    for (int y = 0; y < cells.GetLength(1); y++)
                    {
                        cells[x,y] = populationStrategy.GetCellValue();
                    }
                }
            }
        }
    }
}