/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;

namespace GOL.Classes
{
    [Serializable()]
    class Grid : IDisposable : ISerializable
    {
        //Private Members
        private static Cell[,] m_ReadCell, m_WriteCell;

        private const int m_Width = 960;
        private const int m_Height = 540;

        private const int m_CSize = 10;

        private int m_Rows = m_Width / m_CSize;
        private int m_Cols = m_Height / m_CSize;

        private Graphics m_Graphics;
        private Bitmap m_Bitmap;
        
        //Public properties
        public Grid()
        {
            m_ReadCell = new Cell[m_Rows, m_Cols];
            m_WriteCell = new Cell[m_Rows, m_Cols];
            
            initializeGrids();

            m_Bitmap = new Bitmap(m_Width, m_Height);
            m_Graphics = Graphics.FromImage(m_Bitmap);

            //testing!!
            setupSliderGun();
        }

        public Bitmap Buffer { get { return m_Bitmap; } }

        public void ClearAll()
        {
            initializeGrids();
            generateNextGeneration();
        }

        private void initializeGrids()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    m_ReadCell[i, j] = new Cell();
                    m_WriteCell [i, j] = new Cell();
                }
            }
        }
        
        private void setCellAt(int xCoord, int yCoord, bool alive)
        {
            m_WriteCell[xCoord, yCoord].IsAlive = alive;
        }

        public void loadCellAt(int xCoord, int yCoord, bool alive)
        {
            m_ReadCell[xCoord, yCoord].IsAlive = alive;
            drawCellAt(xCoord, yCoord);
        }

        private bool liveCellAt(int xCoord, int yCoord)
        {
            return m_ReadCell[xCoord, yCoord].IsAlive;
        }

        public void generateNextGeneration()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    if (i > 0 && j > 0 && i < m_Rows - 1 && j < m_Cols - 1)
                    {
                        int count = 0;

                        // Check row above the cell.
                        if (liveCellAt(i, j - 1)) count++;
                        if (liveCellAt(i - 1, j - 1)) count++;
                        if (liveCellAt(i + 1, j - 1)) count++;

                        // Check row containing the cell.
                        if (liveCellAt(i - 1, j)) count++;
                        if (liveCellAt(i + 1, j)) count++;

                        // Check row below the cell.
                        if (liveCellAt(i - 1, j + 1)) count++;
                        if (liveCellAt(i + 1, j + 1)) count++;
                        if (liveCellAt(i, j + 1)) count++;

                        // Implement game of life logic.
                        if (liveCellAt(i, j))
                        {
                            if (count == 2 || count == 3)
                                setCellAt(i, j, true); // Survival of a cell.
                            else
                                setCellAt(i, j, false); // Death from under/overcrowding.
                        }
                        else
                        {
                            if (count == 3) 
                                setCellAt(i, j, true); // Birth of a live cell.
                        }
                    }
                } //end for
            } //end for
            swapPointers();
            drawNewGrid();
        }

        //Cleanly swaps data sets.
        private void swapPointers()
        {
            Cell[,] m_TempCell = new Cell[m_Rows, m_Cols];
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    
                    m_TempCell [i, j] = new Cell();
                }
            }
        
            m_ReadCell = m_TempCell;
            m_ReadCell = m_WriteCell;
            m_WriteCell = m_TempCell;
        }

        private void setupSliderGun()
        {
            loadCellAt(11, 7, true);
            loadCellAt(11, 8, true);

            loadCellAt(12, 7, true);
            loadCellAt(12, 8, true);

            loadCellAt(21, 7, true);
            loadCellAt(21, 8, true);
            loadCellAt(21, 9, true);

            loadCellAt(22, 6, true);
            loadCellAt(22, 10, true);

            loadCellAt(23, 5, true);
            loadCellAt(23, 11, true);

            loadCellAt(24, 5, true);
            loadCellAt(24, 11, true);

            loadCellAt(25, 8, true);

            loadCellAt(26, 6, true);
            loadCellAt(26, 10, true);

            loadCellAt(27, 7, true);
            loadCellAt(27, 8, true);
            loadCellAt(27, 9, true);

            loadCellAt(28, 8, true);

            loadCellAt(31, 5, true);
            loadCellAt(31, 6, true);
            loadCellAt(31, 7, true);

            loadCellAt(32, 5, true);
            loadCellAt(32, 6, true);
            loadCellAt(32, 7, true);

            loadCellAt(33, 4, true);
            loadCellAt(33, 8, true);

            loadCellAt(35, 3, true);
            loadCellAt(35, 4, true);
            loadCellAt(35, 8, true);
            loadCellAt(35, 9, true);

            loadCellAt(45, 5, true);
            loadCellAt(45, 6, true);

            loadCellAt(46, 5, true);
            loadCellAt(46, 6, true);
        }

        public void drawCellAt(int x, int y)
        {
            Color cellColor = liveCellAt(x, y) ? Color.Green : Color.Beige;
            SolidBrush brush = new SolidBrush(cellColor);
            Pen pen = new Pen(cellColor);
            Rectangle rect = new Rectangle(x * m_CSize, y * m_CSize, m_CSize, m_CSize);
            
            if (liveCellAt(x, y))
                m_Graphics.FillRectangle(brush, rect);
            else
                m_Graphics.DrawRectangle(pen, rect);
        }

        private void drawNewGrid()
        {
            lock (m_ReadCell)
            {
                m_Graphics.Clear(Color.White);
                for (int i = 0; i < m_Rows; i++)
                {
                    for (int j = 0; j < m_Cols; j++)
                    {
                        if ((i >= 0 && i < m_Rows) && (j >= 0 && j < m_Cols))
                        {
                            drawCellAt(i, j);
                        }
                    }
                }
            }
        }

        void IDisposable.Dispose()
        {
            m_Bitmap.Dispose();
        }

    } //class

    class Display
    {

    } //class

} //namespace