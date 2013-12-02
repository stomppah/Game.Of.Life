﻿/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 30/11/13
 * Time: 13:50
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
    class Grid : IDisposable
    {
        //Private Members
        private static Cell[,] m_ReadCell, m_WriteCells, m_TempCells;

        private const int m_Width = 960;
        private const int m_Height = 530;

        private int m_Rows = m_Width / 5;
        private int m_Cols = m_Height / 5;

        private Graphics m_Graphics;
        private Bitmap m_Bitmap;
        
        //Public properties
        public Grid()
        {
            m_ReadCell = new Cell[m_Rows, m_Cols];
            m_WriteCells = new Cell[m_Rows, m_Cols];
            m_TempCells = new Cell[m_Rows, m_Cols];

            initializeGrids();

            //testing!!
            setupSliderGun();

            m_Bitmap = new Bitmap(m_Width, m_Height);
            m_Graphics = Graphics.FromImage(m_Bitmap);
        }

        public Bitmap Buffer { get { return m_Bitmap; } }

        private void initializeGrids()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    m_ReadCell[i, j] = new Cell();
                    m_WriteCells [i, j] = new Cell();
                    m_TempCells [i, j] = new Cell();
                }
            }
        }
        
        private void setCellAt(int xCoord, int yCoord, bool alive)
        {
            m_WriteCells[xCoord, yCoord].IsAlive = alive;
        }

        public void loadCellAt(int xCoord, int yCoord, bool alive)
        {
            m_ReadCell[xCoord, yCoord].IsAlive = alive;
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
                        int liveNeighbours = m_Neighbours(i, j);

                        // Implement game of life logic.
                        if (liveCellAt(i, j))
                        {
                            if (liveNeighbours == 2 || liveNeighbours == 3)
                                setCellAt(i, j, true); // Survival of a cell.
                            else
                                setCellAt(i, j, false); // Death from under/overcrowding.
                        }
                        else
                        {
                            if (liveNeighbours == 3) setCellAt(i, j, true); // Birth of a live cell.
                        }
                    }
                } //end for
            } //end for
            swapPointers();
            drawNewGrid();
        }

        private int m_Neighbours(int i, int j)
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
            return count;
        }

        //Cleanly swaps data sets.
        private void swapPointers()
        {
            m_ReadCell = m_TempCells;
            m_ReadCell = m_WriteCells;
            m_WriteCells = m_TempCells;
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
            Rectangle rect = new Rectangle(x * 5, y * 5, 5, 5);
            
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