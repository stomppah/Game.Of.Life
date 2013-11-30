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
        private static Grid g_Read;

        public MainForm()
        {
            InitializeComponent();

            g_Read = new Grid();
        }

        private void Window_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(g_Read.Buffer, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            g_Read.nextGrid();
        }
    }
}
