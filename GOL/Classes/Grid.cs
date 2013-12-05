/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Description: The Grid class is responsible for both read 
 * and write grids, as well as drawing them to a bitmap for display.
 * URL: https://github.com/stomppah/Conways-Game-in-.NET
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace GOL.Classes
{
    [Serializable()]
    class Grid : IDisposable, ISerializable
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

        // Public properties
        public Bitmap Buffer { get { return m_Bitmap; } }

        public int CellSize { get { return m_CSize; } }

        public Grid()
        {
            m_ReadCell = new Cell[m_Rows, m_Cols];
            m_WriteCell = new Cell[m_Rows, m_Cols];
            
            initializeGrids();

            m_Bitmap = new Bitmap(m_Width, m_Height);
            m_Graphics = Graphics.FromImage(m_Bitmap);
        }

        public Grid(SerializationInfo info, StreamingContext ctxt)
        {
            m_WriteCell = (Cell[,])info.GetValue("m_WriteCell", typeof(Cell[,]));
            m_ReadCell  = (Cell[,])info.GetValue("m_ReadCell",  typeof(Cell[,]));
            m_Bitmap    = (Bitmap)info.GetValue("m_Bitmap",     typeof(Bitmap));
        }

        // Used to paint to the grid and simultaneously add to the correct grid.
        public void paintCellAt(int xCoord, int yCoord, bool alive)
        {
            try
            {
                m_ReadCell[xCoord, yCoord].IsAlive = alive;
                drawCellAt(xCoord, yCoord);
            }
            catch (IndexOutOfRangeException e)
            {  
                // User has painted past the edge of the grid, in this case 
                // there is nothing to paint to so the error can be ignored.
            }
        }

        // Checks for a live cell at the specified coordinates.
        private bool liveCellAt(int xCoord, int yCoord)
        {
            return m_ReadCell[xCoord, yCoord].IsAlive;
        }

        // Steps through the grid once, implementing the Game of Life rules.
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

        // Cleanly swaps data sets.......
        private void swapPointers()
        {
            Cell[,] l_TempCell = new Cell[m_Rows, m_Cols];
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    
                    l_TempCell [i, j] = new Cell(|);
                }
            }
        
            m_ReadCell = l_TempCell;
            m_ReadCell = m_WriteCell;
            m_WriteCell = l_TempCell;
        }
        // .......

        // Temp function for loading interesting preset!
        public void setupSliderGun()
        {
            paintCellAt(11, 7, true);
            paintCellAt(11, 8, true);

            paintCellAt(12, 7, true);
            paintCellAt(12, 8, true);

            paintCellAt(21, 7, true);
            paintCellAt(21, 8, true);
            paintCellAt(21, 9, true);

            paintCellAt(22, 6, true);
            paintCellAt(22, 10, true);

            paintCellAt(23, 5, true);
            paintCellAt(23, 11, true);

            paintCellAt(24, 5, true);
            paintCellAt(24, 11, true);

            paintCellAt(25, 8, true);

            paintCellAt(26, 6, true);
            paintCellAt(26, 10, true);

            paintCellAt(27, 7, true);
            paintCellAt(27, 8, true);
            paintCellAt(27, 9, true);

            paintCellAt(28, 8, true);

            paintCellAt(31, 5, true);
            paintCellAt(31, 6, true);
            paintCellAt(31, 7, true);

            paintCellAt(32, 5, true);
            paintCellAt(32, 6, true);
            paintCellAt(32, 7, true);

            paintCellAt(33, 4, true);
            paintCellAt(33, 8, true);

            paintCellAt(35, 3, true);
            paintCellAt(35, 4, true);
            paintCellAt(35, 8, true);
            paintCellAt(35, 9, true);

            paintCellAt(45, 5, true);
            paintCellAt(45, 6, true);

            paintCellAt(46, 5, true);
            paintCellAt(46, 6, true);
        }

        // Responsible for drawing dead or live cells.
        private void drawCellAt(int x, int y)
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

        private void GetObjectData(SerializationInfo info, StreamingContext ctx)
        {
            info.AddValue("m_WriteCell", m_WriteCell);
            info.AddValue("m_ReadCell", m_ReadCell);
            info.AddValue("m_Bitmap", m_Bitmap);
            info.AddValue("m_Graphics", m_Graphics);
        }

        private void initializeGrids()
        {
            for (int i = 0; i < m_Rows; i++)
            {
                for (int j = 0; j < m_Cols; j++)
                {
                    m_ReadCell[i, j] = new Cell();
                    m_WriteCell[i, j] = new Cell();
                }
            }
        }

        private void setCellAt(int xCoord, int yCoord, bool alive)
        {
            m_WriteCell[xCoord, yCoord].IsAlive = alive;
        }

        private void IDisposable.Dispose()
        {
            m_Bitmap.Dispose();
        }

        public void Serialise()
        {
            // @See: http://www.codeproject.com/Articles/1789/Object-Serialization-using-C
            // Open a file and serialize the object into it in binary format.
            // EmployeeInfo.osl is the file that we are creating. 
            // Note:- you can give any extension you want for your file
            // If you use custom extensions, then the user will now 
            //   that the file is associated with your program.
            Stream stream = File.Open("EmployeeInfo.osl", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();

            Console.WriteLine("Writing Employee Information");
            bformatter.Serialize(stream, mp);
            stream.Close();
        }
    } //class

} //namespace