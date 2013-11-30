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
        private static Grid g_Write;

        private Thread utilWorker;

        public MainForm()
        {
            InitializeComponent();

            g_Read = new Grid();
            g_Write = new Grid();

            utilWorker = new Thread(new ThreadStart(doBGWork_Temp));
        }

        private void doBGWork_Temp()
        {
            MethodInvoker mi = delegate() { this.Refresh(); };
            Invoke(mi);
        }

        private void Window_Load(object sender, EventArgs e)
        {

        }
    }
}
