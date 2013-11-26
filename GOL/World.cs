/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 16/11/13
 * Time: 16:57
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace GOL
{
    class World
    {
        private const int _Width     = 960;
        private const int _Height    = 530;

        private int _rows = _Width / Cell.Size;
        private int _columns = _Height / Cell.Size;

        private static int _globalX, _globalY;

        private bool[,] _read;
        private bool[,] _write;

        private Graphics _g1;
        private Bitmap _bmp;

        private bool _mousePainting = false;
        private bool _evaluatingGrid = false;

        public int XPos { get { return _globalX; } set { _globalX = value; } }
        public int YPos { get { return _globalY; } set { _globalY = value; } }

        public int Rows { get { return _rows; } }
        public int Columns { get { return _columns; } }

        public bool[,] Read { get { return _read; } set { _read = value; } }
        public bool[,] Write { get { return _write; } set { _write = value; } }

        public Graphics G1 { get { return _g1; } set { _g1 = value; } }
        public Bitmap Bmp { get { return _bmp; } set { _bmp = value; } }

        public bool MousePainting { get { return _mousePainting; } set { _mousePainting = value; } }
        public bool EvaluatingGrid { get { return _evaluatingGrid; } set { _evaluatingGrid = value; } }

        private int portionSize;

        public World()
        {
            _read = new bool[_rows, _columns];
            _write = new bool[_rows, _columns];

            _bmp = new Bitmap(_Width, _Height);
            _g1 = Graphics.FromImage(_bmp);

            portionSize = 48;
        }

        //public void workerThread1(object portionNumber)
        //{
        //    while (true)
        //        checkForNewLife();
        //}

        public void workerThread2()
        {
            while (true)
                drawNewGrid();
        }

        public void drawNewGrid()
        {
            _g1.Clear(Color.White);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    if ((i >= 0 && i < _rows) && (j >= 0 && j < _columns))
                    {
                        if (_read[i, j])
                        {
                            _g1.FillRectangle(new SolidBrush(Color.Green), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                        }
                        else
                        {
                            _g1.DrawRectangle(new Pen(Color.Beige), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                        }
                    }
                }
            }
        }

        public void setupSliderGun()
        {
            _read[11, 7] = true;
            _read[11, 8] = true;

            _read[12, 7] = true;
            _read[12, 8] = true;

            _read[21, 7] = true;
            _read[21, 8] = true;
            _read[21, 9] = true;

            _read[22, 6] = true;
            _read[22, 10] = true;

            _read[23, 5] = true;
            _read[23, 11] = true;

            _read[24, 5] = true;
            _read[24, 11] = true;

            _read[25, 8] = true;

            _read[26, 6] = true;
            _read[26, 10] = true;

            _read[27, 7] = true;
            _read[27, 8] = true;
            _read[27, 9] = true;

            _read[28, 8] = true;

            _read[31, 5] = true;
            _read[31, 6] = true;
            _read[31, 7] = true;

            _read[32, 5] = true;
            _read[32, 6] = true;
            _read[32, 7] = true;

            _read[33, 4] = true;
            _read[33, 8] = true;

            _read[35, 3] = true;
            _read[35, 4] = true;
            _read[35, 8] = true;
            _read[35, 9] = true;

            _read[45, 5] = true;
            _read[45, 6] = true;

            _read[46, 5] = true;
            _read[46, 6] = true;

            drawNewGrid();
        }

        public void checkForNewLife(object portionNumber)
        {
            int portionNumberAsInt = (int)portionNumber;
            int baseIndex = portionNumberAsInt * portionSize;

                for (int i = baseIndex; i < baseIndex + portionSize; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        if (i > 0 && j > 0 && i < _rows - 1 && j < _columns - 1)
                        {
                            int liveNeighbours = 0;

                            // Check row above the cell.
                            if (_read[i, j - 1]) liveNeighbours++;
                            if (_read[i - 1, j - 1]) liveNeighbours++;
                            if (_read[i + 1, j - 1]) liveNeighbours++;

                            // Check row containing the cell.
                            if (_read[i - 1, j]) liveNeighbours++;
                            if (_read[i + 1, j]) liveNeighbours++;

                            // Check row below the cell.
                            if (_read[i - 1, j + 1]) liveNeighbours++;
                            if (_read[i + 1, j + 1]) liveNeighbours++;
                            if (_read[i, j + 1]) liveNeighbours++;

                            // Implement game of life logic.
                            if (_read[i, j])
                            {
                                if (liveNeighbours == 2 || liveNeighbours == 3)
                                    _write[i, j] = true; // Survival of a cell.
                                else
                                    _write[i, j] = false; // Death from under/overcrowding.
                            }
                            else
                            {
                                if (liveNeighbours == 3)
                                {
                                    _write[i, j] = true; // Birth of a live cell.
                                }
                            }
                        }
                    }
                
            }
        }

        //Cleanly swaps data sets.
        public void swapPointers()
        {
            bool[,] temp = new bool[_rows, _columns];
            _read = temp;
            _read = _write;
            _write = temp;
        }

    }
}
