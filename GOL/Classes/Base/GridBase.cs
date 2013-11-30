/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Time: 23:38
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.Classes.Base
{
    [Serializable()]
    abstract class GridBase
    {
        private int m_Rows = 960 / 5;
        private int m_Cols  = 530 / 5;

        private Graphics m_Graphics;
        private Bitmap m_Bitmap;

        public GridBase()
        {
            m_Bitmap = new Bitmap(960, 930);
            m_Graphics = Graphics.FromImage(m_Bitmap);
        }

        //public abstract void setCellAt(int xCoord, int yCoord);
        ////{
        ////    m_Grid[xCoord, yCoord].IsAlive = true;
        ////    drawCellAt(xCoord, yCoord, getCellAt(xCoord, yCoord));
        ////}

        //public abstract Cell getCellAt(int xCoord, int yCoord);
        ////{
        ////    return m_Grid[xCoord, yCoord];
        ////}

        //public abstract void drawCellAt(int xCoord, int yCoord, Cell cell);
        ////{
        ////    Color cellColor = cell.IsAlive ? Color.Green : Color.Beige;
        ////    SolidBrush brush = new SolidBrush(cellColor);
        ////    Pen pen = new Pen(cellColor);

        ////    Rectangle rect = new Rectangle(xCoord * 5, yCoord * 5, 5, 5);

        ////    if (cell.IsAlive)
        ////    {
        ////        lock (m_Graphics)
        ////    {
        ////        m_Graphics.FillRectangle(brush, rect);
        ////        }
        ////    }
        ////    else
        ////    {
        ////        lock (m_Graphics)
        ////        {
        ////            m_Graphics.DrawRectangle(pen, rect);
        ////        }
        ////    }
        ////}

        //private void clearGrid()
        //{
        //    m_Graphics.Clear(Color.White);
        //}

        //public abstract void drawNewGrid();
        ////{
        ////    clearGrid();
        ////    for (int i = 0; i < m_Rows; i++)
        ////    {
        ////        for (int j = 0; j < m_Cols; j++)
        ////        {
        ////            if ((i >= 0 && i < m_Rows) && (j >= 0 && j < m_Cols))
        ////            {
        ////                if (m_Grid[i, j].IsAlive)
        ////                {
        ////                    drawCellAt(i, j);
        ////                }
        ////                else
        ////                {
        ////                    drawCellAt(i, j);
        ////                }
        ////            }
        ////        }
        ////    }
        ////}

    }
}
