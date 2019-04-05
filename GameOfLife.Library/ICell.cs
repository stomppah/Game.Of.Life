using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GameOfLife.Library
{
    public interface ICell
    {
        ICell Create();
    }

    public class Neighborhood : ICell
    {
        private readonly ICell[,] grid;

        private readonly int gridHeight;
        private readonly int gridWidth;

        public Neighborhood(int gridHeight, int gridWidth, ICell prototype)
        {
            this.gridHeight = gridHeight;
            this.gridWidth = gridWidth;
            this.grid = new ICell[gridHeight, gridWidth];
            for (int row = 0; row < gridHeight; ++row)
            {
                for (int col = 0; col < gridWidth; ++col)
                {
                    grid[row, col] = prototype.Create();
                }
            }
        }

        public ICell Create()
        {
            return new Neighborhood(gridHeight, gridWidth, grid[0, 0]);
        }
    }

    public class Resident : ICell
    {
        private static Color BORDER_COLOR = Color.DarkGoldenrod;
        private static Color LIVE_COLOR     = Color.Green;
        private static Color DEAD_COLOR   = Color.LightGoldenrodYellow;
        public CellState State { get; set; }
        public int LiveNeighbors { get; set; }

        public ICell Create()
        {
            return new Resident() { State = CellState.Dead, LiveNeighbors = 0 };
        }
    }
}
