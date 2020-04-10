using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameOfLife.Library;
using BlazorSvgHelper.Classes.SubClasses;
using BlazorSvgHelper;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Rendering;

namespace GameOfLife.BlazorUi.Pages
{
    public class ConwaysBase : ComponentBase
    {
        private readonly int cellPixelSize = 1115 / 140;
        readonly LifeGrid lifeGrid;

        svg _Svg = null;

        SvgHelper SvgHelper1 = new SvgHelper();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            //ClockSettings.radius_2_Times = WidthAndHeight;
            //ClockSettings.radius_Origin = WidthAndHeight / 2;
            //ClockSettings.radius_90_Percent = ClockSettings.radius_Origin * 0.9;

            //Generate_Clock_SVG();


            SvgHelper1.Cmd_Render(_Svg, 0, builder);
        }

        public ConwaysBase()
        {
            this.lifeGrid = new LifeGrid(gridHeight: 140, gridWidth: 223, CellFactory.GetCell());
            this.lifeGrid.Randomise();
        }

    }
}
