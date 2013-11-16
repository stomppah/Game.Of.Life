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
    public partial class WorldDisplayForm : Form
    {

        private int rows = World.Width / Cell.Size;
        private int columns = World.Height / Cell.Size;

        private bool[,] read;
        private bool[,] write;

        private Graphics g1;
        private Bitmap bmp;
        private Pen emptyCellPen;

        public WorldDisplayForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            
            read    = new bool[rows, columns];
            write   = new bool[rows, columns];

            bmp = new Bitmap(World.Width, World.Height);
            g1 = Graphics.FromImage(bmp);

            emptyCellPen = new Pen(Color.Gray);

            evaluateGrid();
        }

        private void evaluateGrid()
        {
            g1.Clear(Color.White);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <columns; j++)
                {
                    if ((i >= 0 && i < rows) && (j >= 0 && j < columns))
                    {
                        if (read[i, j])
                        {
                            g1.FillRectangle(new SolidBrush(Color.Green), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                        }
                        else
                        {
                            g1.DrawRectangle(emptyCellPen, new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                        }
                    }
                }
            }
        }

        private void worldCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bmp, 0, 0);
        }

        private void worldCanvas_Click(object sender, EventArgs e)
        {
            int i = (int)World.XPos / Cell.Size;
            int j = (int)World.YPos / Cell.Size;
            if ((i >= 0 && i < rows) && (j >= 0 && j < columns))
            {
                read[i, j] = true;
                g1.FillRectangle(new SolidBrush(Color.Green), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                Refresh();
            }
        }

        private void worldCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            World.XPos = e.X;
            World.YPos = e.Y;
            this.Text = "MouseX: " + e.X + " - MouseY: " + e.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i > 0 && j > 0 && i < rows - 1 && j < columns -1)
                    {
                        int liveNeighbours = 0;

                        // Check row above the cell.
                        if (read[i, j - 1]) liveNeighbours++;
                        if (read[i-1, j-1]) liveNeighbours++;
                        if (read[i+1, j-1]) liveNeighbours++;

                        // Check row containing the cell.
                        if (read[i-1, j]) liveNeighbours++;
                        if (read[i+1, j]) liveNeighbours++;

                        // Check row below the cell.
                        if (read[i-1, j+1]) liveNeighbours++;
                        if (read[i+1,j+1]) liveNeighbours++;
                        if (read[i, j+1]) liveNeighbours++;

                        // Implement game of life logic.
                        if (read[i, j]) {
                            if (liveNeighbours == 2 || liveNeighbours == 3)
                                write[i, j] = true; // Survival of a cell.
                            else
                                write[i, j] = false; // Death from under/overcrowding.
                        } else {
                            if (liveNeighbours == 3) {
                                write[i, j] = true; // Birth of a live cell.
                            }
                        }
                    }
                }
            }
            bool[,] temp = new bool[rows, columns];
            read = temp;
            read = write;
            write = temp;
            
            evaluateGrid();
            Refresh();

        }

        private void WorldDisplayForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                timer1.Enabled = !timer1.Enabled ? true : false;
            }
        }
    }
}
