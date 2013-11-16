using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        private int cellSize    = 5; //x 5px
        private int worldWidth  = 950;
        private int worldHeight = 520;

        private bool[,] read;
        private bool[,] write;

        private Graphics g1;
        private Bitmap bmp;
        private Pen penGreen, penRed;

        public Form1()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            read    = new bool[worldWidth / cellSize, worldHeight / cellSize];
            write   = new bool[worldWidth / cellSize, worldHeight / cellSize];

            bmp = new Bitmap(worldWidth, worldHeight);
            g1 = Graphics.FromImage(bmp);

            penGreen = new Pen(Color.Green);
            penRed = new Pen(Color.Gray);


            for (int i = 0; i < worldWidth / cellSize; i++)
            {
                for (int j = 0; j < worldHeight / cellSize; j++)
                {
                    if ((i > 0 && i < worldWidth / cellSize) && (j > 0 && j < worldHeight / cellSize))
                    {
                        if (read[i, j])
                        {
                            g1.DrawRectangle(penGreen, new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize));
                        }
                        else
                        {
                            g1.DrawRectangle(penRed, new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize));
                        }
                    }
                }
            }
            bmp.Save("Test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;

            g.DrawImage(bmp, 0, 0);

        }
    }
}
