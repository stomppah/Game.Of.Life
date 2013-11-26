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
        }

        private static World gameWorld = new World();

        private Thread WorldThread;// = new Thread(new ThreadStart(gameWorld.workerThread));
        private int threadCount = 4;

        private void MainForm_Load(object sender, EventArgs e)
        {
            WorldThread = new Thread(new ThreadStart(gameWorld.workerThread1));
            WorldThread.Start();
        }


        private void worldCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(gameWorld.Bmp, 0, 0);
        }

        private void worldCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            gameWorld.XPos = e.X;
            gameWorld.YPos = e.Y;
            this.Text = "MouseX: " + e.X + " - MouseY: " + e.Y;

            if (gameWorld.MousePainting)
            {
                int i = (int)gameWorld.XPos / Cell.Size;
                int j = (int)gameWorld.YPos / Cell.Size;
                if ((i >= 0 && i < gameWorld.Rows) && (j >= 0 && j < gameWorld.Columns))
                {
                    gameWorld.Read[i, j] = true;
                    gameWorld.G1.FillRectangle(new SolidBrush(Color.Green), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                    Refresh();
                }
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    gameWorld.EvaluatingGrid = !gameWorld.EvaluatingGrid ? true : false;
                    break;
                case (char)Keys.Enter:
                    gameWorld.setupSliderGun();
                    break;
            }
        }

        private void worldCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            gameWorld.MousePainting = true;
        }

        private void worldCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            gameWorld.MousePainting = false;
        }

    }
}