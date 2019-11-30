using GameOfLife.Library;
using System;
using System.Drawing;

namespace GameOfLife.Services
{
    public class ConwaysService
    {
        readonly LifeGrid lifeGrid;

        public ConwaysService()
        {
            this.lifeGrid = new LifeGrid(gridHeight: 140, gridWidth: 223, CellFactory.GetCell());
            this.lifeGrid.Randomise();
        }

        public ConwaysService(LifeGrid lifeGrid)
        {
            this.lifeGrid = lifeGrid;
        }

        public void ShowGrid(int cellPixelSize, Brush alive, ref Graphics graphics)
        {
            graphics.Clear(Color.White);
            int x = 0;
            int y = 0;
            int rowLength = this.lifeGrid.CurrentGrid.GetUpperBound(1) + 1;

            foreach (Resident resident in this.lifeGrid.CurrentGrid)
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

        public void UpdateState()
        {
            this.lifeGrid.UpdateState();
        }
    }
}
