/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 16/11/13
 * Time: 15:36
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GOL
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            init();
        }

        private void MainForm_Load()
        {

        }

        private void init()
        {
            //thread1 = new Thread(new ThreadStart(checkForNewLife));
            //thread1.Start();
            //thread1.Join();

            //evaluateGrid();
        }       

        private void worldCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bmp, 0, 0);
        }

        private void worldCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            World.XPos = e.X;
            World.YPos = e.Y;
            this.Text = "MouseX: " + e.X + " - MouseY: " + e.Y;

            if (mousePainting)
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
        }

        private void checkForNewLife()
        {

            if (evaluatingGrid)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i > 0 && j > 0 && i < rows - 1 && j < columns - 1)
                        {
                            int liveNeighbours = 0;

                            // Check row above the cell.
                            if (read[i, j - 1]) liveNeighbours++;
                            if (read[i - 1, j - 1]) liveNeighbours++;
                            if (read[i + 1, j - 1]) liveNeighbours++;

                            // Check row containing the cell.
                            if (read[i - 1, j]) liveNeighbours++;
                            if (read[i + 1, j]) liveNeighbours++;

                            // Check row below the cell.
                            if (read[i - 1, j + 1]) liveNeighbours++;
                            if (read[i + 1, j + 1]) liveNeighbours++;
                            if (read[i, j + 1]) liveNeighbours++;

                            // Implement game of life logic.
                            if (read[i, j])
                            {
                                if (liveNeighbours == 2 || liveNeighbours == 3)
                                    write[i, j] = true; // Survival of a cell.
                                else
                                    write[i, j] = false; // Death from under/overcrowding.
                            }
                            else
                            {
                                if (liveNeighbours == 3)
                                {
                                    write[i, j] = true; // Birth of a live cell.
                                }
                            }
                        }
                    }
                }
            }
            swapPointers();

            evaluateGrid();
        }

        //Cleanly swaps data sets.
        private void swapPointers()
        {
            bool[,] temp = new bool[rows, columns];
            read = temp;
            read = write;
            write = temp;
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    evaluatingGrid = !evaluatingGrid ? true : false;
                    break;
                case (char)Keys.Enter:
                    setupSliderGun();
                    break;
            }
        }

        private void worldCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            mousePainting = true;
        }

        private void worldCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            mousePainting = false;
        }

    }
}