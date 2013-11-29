﻿/**
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

        private const int threadCount = 4;

        private World gameWorld = new World();

        private Thread[] newLifeChecker = new Thread[threadCount];
        private Thread doThisAllTheTime;

        private void MainForm_Load(object sender, EventArgs e)
        {
            gameWorld.setupSliderGun();
            for (int i = 0; i < threadCount; i++)
            {
                newLifeChecker[i] = new Thread(gameWorld.checkForNewLife);
                newLifeChecker[i].IsBackground = true;
            }

            doThisAllTheTime = new Thread(MainLoop);
            doThisAllTheTime.Start();
        }

        private void MainLoop()
        {
            while (true)
            {
                if (!this.IsHandleCreated && !this.IsDisposed) return;

                MethodInvoker mi = delegate() { this.Refresh(); };
                this.Invoke(mi);

                if (gameWorld.isRunning)
                {
                    for (int i = 0; i < threadCount; i++)
                    {
                        newLifeChecker[i].Start(i);
                        newLifeChecker[i].Join();
                    }
                }
            }
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
                    Graphics g = Graphics.FromImage(new Bitmap(worldCanvas.Image));
                    gameWorld.Read[i, j] = true;
                    g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(i * Cell.Size, j * Cell.Size, Cell.Size, Cell.Size));
                    Refresh();
                }
            }
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    gameWorld.isRunning = !gameWorld.isRunning ? true : false;
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