/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Description: Provides human interation mechanisms.
 * URL: https://github.com/stomppah/Conways-Game-in-.NET
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

using GOL.Classes;

namespace GOL
{
    public partial class MainForm : Form
    {
        private static Grid m_Grid;
        private bool m_MouseDown = false;

        public MainForm()
        {
            InitializeComponent();
            m_Grid = new Grid();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(m_Grid.Buffer, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_Grid.generateNextGeneration();
            Refresh();
        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled ? true : false;
            startStopBtn.Text = !timer1.Enabled ? "Run" : "Stop";
        }

        private void stepBtn_Click(object sender, EventArgs e)
        {
            m_Grid.generateNextGeneration();
            Refresh();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            m_Grid.ClearAll();
            Refresh();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            m_Grid.Serialise();
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            m_Grid.setupSliderGun();
            Refresh();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = "Conways Game of Life: MouseX: " + e.X + " - MouseY: " + e.Y;
            if (m_MouseDown)
            {
                // bloody important
                int i = (int)e.X / m_Grid.CellSize;
                int j = (int)e.Y / m_Grid.CellSize;
                if ((i >= 0 && i < 192) && (j >= 0 && j < 106))
                {
                    m_Grid.paintCellAt(i, j, true);
                }
                Refresh();
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            m_MouseDown = true;
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            m_MouseDown = false;
        }
    
    }
}