using GameOfLife.Library;
using System;
using System.Drawing;

namespace GameOfLife.Services
{
    public class ConwaysClient
    {
        public void ShowGrid(
            ICell[,] currentGrid, int cellPixelSize, Brush alive, ref Graphics graphics)
        {
            graphics.Clear(Color.White);
            int x = 0;
            int y = 0;
            int rowLength = currentGrid.GetUpperBound(1) + 1;

            foreach (Resident resident in currentGrid)
            {
                if (resident.State == CellState.Alive)
                {
                    graphics.FillRectangle(alive, x * cellPixelSize, y * cellPixelSize, cellPixelSize, cellPixelSize);
                    graphics.DrawRectangle(new Pen(Color.DarkGray), x * cellPixelSize, y * cellPixelSize, cellPixelSize, cellPixelSize);
                }

                x++;
                if (x >= rowLength)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
