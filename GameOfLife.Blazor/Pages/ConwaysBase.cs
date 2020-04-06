using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Blazor.Extensions.Canvas;
using Blazor.Extensions;
using System.Drawing;
using GameOfLife.Library;

namespace GameOfLife.BlazorUi.Pages
{
    public class ConwaysBase : ComponentBase
    {
        private Canvas2DContext _context;
        protected BECanvasComponent _canvasReference;

        private readonly int cellPixelSize = 1115 / 140;
        readonly LifeGrid lifeGrid;

        public ConwaysBase()
        {
            this.lifeGrid = new LifeGrid(gridHeight: 140, gridWidth: 223, CellFactory.GetCell());
            this.lifeGrid.Randomise();

            // ShowGrid();
        }

        private void ShowGrid()
        {
            // graphics.Clear(Color.White);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this._context = await this._canvasReference.CreateCanvas2DAsync();
            //await this._context.SetFillStyleAsync("green");

            //await this._context.FillRectAsync(10, 100, 100, 100);

            //await this._context.SetFontAsync("48px serif");
            //await this._context.StrokeTextAsync("Hello Blazor!!!", 10, 100);

            int x = 0;
            int y = 0;
            int rowLength = this.lifeGrid.CurrentGrid.GetUpperBound(1) + 1;

            foreach (Resident resident in this.lifeGrid.CurrentGrid)
            {
                if (resident.State == CellState.Alive)
                {
                    await this._context.SetFillStyleAsync("black");
                    await this._context.FillRectAsync(x * cellPixelSize, y * cellPixelSize, cellPixelSize, cellPixelSize);
                        //.FillRectangle(alive, x * cellPixelSize, y * cellPixelSize, cellPixelSize, cellPixelSize);
                    //graphics.DrawRectangle(new Pen(Color.DarkGray), x * cellPixelSize, y * cellPixelSize, cellPixelSize, cellPixelSize);
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
