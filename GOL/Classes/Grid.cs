/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 30/11/13
 * Time: 13:50
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.Classes
{
    [Serializable()]
    class Grid
    {
        private Cell[,] m_ReadCells, m_WriteCells;

        private const int m_Width = 960;
        private const int m_Height = 530;

        private int m_Rows = m_Width / 5;
        private int m_Cols = m_Height / 5;

        private Graphics m_Graphics;
        private Bitmap m_Bitmap;

        public Grid()
        {
            m_ReadCells = new Cell[m_Rows, m_Cols];
            m_WriteCells = new Cell[m_Rows, m_Cols];

            setupSliderGun();

            m_Bitmap = new Bitmap(m_Width, m_Height);
            m_Graphics = Graphics.FromImage(m_Bitmap);
        }

        private void setCellAt(int xCoord, int yCoord, bool alive)
        {
            m_WriteCells[xCoord, yCoord].IsAlive = alive;
        }

        public bool getCellAt(int xCoord, int yCoord)
        {
            return m_ReadCells[xCoord, yCoord].IsAlive;
        }

        public void nextGrid()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    if (i > 0 && j > 0 && i < m_Rows - 1 && j < m_Cols - 1)
                    {
                        int liveNeighbours = m_Neighbours(i, j);

                        // Implement game of life logic.
                        if (getCellAt(i, j))
                        {
                            if (liveNeighbours == 2 || liveNeighbours == 3)
                            {
                                setCellAt(i, j, true); // Survival of a cell.
                            }
                            else
                            {
                                setCellAt(i, j, false); // Death from under/overcrowding.
                            }
                        }
                        else
                        {
                            if (liveNeighbours == 3)
                            {
                                setCellAt(i, j, true); // Birth of a live cell.
                            }
                        }
                    }
                } //end for
            } //end for
        }

        private int m_Neighbours(int i, int j)
        {
            int liveNeighbours = 0;

            // Check row above the cell.
            if (getCellAt(i, j - 1)) liveNeighbours++;
            if (m_ReadCells[i - 1, j - 1].IsAlive) liveNeighbours++;
            if (m_ReadCells[i + 1, j - 1].IsAlive) liveNeighbours++;

            // Check row containing the cell.
            if (m_ReadCells[i - 1, j].IsAlive) liveNeighbours++;
            if (m_ReadCells[i + 1, j].IsAlive) liveNeighbours++;

            // Check row below the cell.
            if (m_ReadCells[i - 1, j + 1].IsAlive) liveNeighbours++;
            if (m_ReadCells[i + 1, j + 1].IsAlive) liveNeighbours++;
            if (m_ReadCells[i, j + 1].IsAlive) liveNeighbours++;
            return liveNeighbours;
        }

        //Cleanly swaps data sets.
        public void swapPointers()
        {
            Cell[,] m_TempCells = new Cell[m_Rows, m_Cols];
            m_ReadCells = m_TempCells;
            m_ReadCells = m_WriteCells;
            m_WriteCells = m_TempCells;
        }

        private void setupSliderGun()
        {
            setCellAt(11, 7, true);
            setCellAt(11, 8, true);

            setCellAt(12, 7, true);
            setCellAt(12, 8, true);

            setCellAt(21, 7, true);
            setCellAt(21, 8, true);
            setCellAt(21, 9, true);

            setCellAt(22, 6, true);
            setCellAt(22, 10, true);

            setCellAt(23, 5, true);
            setCellAt(23, 11, true);

            setCellAt(24, 5, true);
            setCellAt(24, 11, true);

            setCellAt(25, 8, true);

            setCellAt(26, 6, true);
            setCellAt(26, 10, true);

            setCellAt(27, 7, true);
            setCellAt(27, 8, true);
            setCellAt(27, 9, true);

            setCellAt(28, 8, true);

            setCellAt(31, 5, true);
            setCellAt(31, 6, true);
            setCellAt(31, 7, true);

            setCellAt(32, 5, true);
            setCellAt(32, 6, true);
            setCellAt(32, 7, true);

            setCellAt(33, 4, true);
            setCellAt(33, 8, true);

            setCellAt(35, 3, true);
            setCellAt(35, 4, true);
            setCellAt(35, 8, true);
            setCellAt(35, 9, true);

            setCellAt(45, 5, true);
            setCellAt(45, 6, true);

            setCellAt(46, 5, true);
            setCellAt(46, 6, true);
        }

        private void drawCell(int x, int y)
        {
            Color cellColor = getCellAt(x, y) ? Color.Green : Color.Beige;
            SolidBrush brush = new SolidBrush(cellColor);
            Pen pen = new Pen(cellColor);
            Rectangle rect = new Rectangle(x * 5, y * 5, 5, 5);
            
            if (getCellAt(x, y))
                m_Graphics.FillRectangle(brush, rect);
            else
                m_Graphics.DrawRectangle(pen, rect);
            
        }

        private void drawNewGrid()
        {
            lock (m_Graphics)
            {
                m_Graphics.Clear(Color.White);
                for (int i = 0; i < m_Rows; i++)
                {
                    for (int j = 0; j < m_Cols; j++)
                    {
                        if ((i >= 0 && i < m_Rows) && (j >= 0 && j < m_Cols))
                        {
                            drawCell(i, j);
                        }
                    }
                }
            }
        }

    }
}
