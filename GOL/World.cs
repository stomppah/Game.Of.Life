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

        private static int _globalX, _globalY;

        private bool[,] _read;
        private bool[,] _write;

        private Graphics _g1;
        private Bitmap _bmp;

        public int XPos { get { return _globalX; } set { _globalX = value; } }
        public int YPos { get { return _globalY; } set { _globalY = value; } }

        public bool[,] Read { get { return _read; } set { _read = value; } }
        public bool[,] Write { get { return _write; } set { _write = value; } }

        public Graphics G1 { get { return _g1; } set { _g1 = value; } }
        public Bitmap bmp { get { return _bmp; } set { _bmp = value; } }

        private int rows = _Width / Cell.Size;
        private int columns = _Height / Cell.Size;

        private bool mousePainting, evaluatingGrid = false;

        public World()
        {
            _read = new bool[rows, columns];
            _write = new bool[rows, columns];

            _bmp = new Bitmap(_Width, _Height);
            _g1 = Graphics.FromImage(_bmp);
        }

        private void evaluateGrid()
        {
            _g1.Clear(Color.White);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if ((i >= 0 && i < rows) && (j >= 0 && j < columns))
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

            //if (InvokeRequired)
            //{
            //    Invoke(new Action(() => Refresh()));
            //}

        }

        private void setupSliderGun()
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

            evaluateGrid();
        }

    }
}
