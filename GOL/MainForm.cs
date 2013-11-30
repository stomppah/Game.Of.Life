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

        private Thread[] t_Producer;

        public MainForm()
        {
            InitializeComponent();

            g_Read = new Grid();
            g_Write = new Grid();
           
        }

        private void results_Producer()
        {
            while (true)
            {
                //you need to use Invoke because the new thread can't access the UI elements directly
                //MethodInvoker mi = delegate() { this.Text = DateTime.Now.ToString(); };
                //this.Invoke(mi);
            }
        }

        private void Window_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                t_Producer[i] = new Thread(new ThreadStart(results_Producer));
                t_Producer[i].Start(i);
            }
        }
    }
}
