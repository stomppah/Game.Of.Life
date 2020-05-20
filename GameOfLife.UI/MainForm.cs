/**
 * Author: Anthony Woodward
 * Email: a.woodward6237@student.leedsmet.ac.uk
 * Date: 29/11/13
 * Description: Provides human interation mechanisms.
 * URL: https://github.com/stomppah/Conways-Game-in-.NET
 */

namespace GameOfLife.UI
{
    using GameOfLife.Library;
    using GameOfLife.Services;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private const int BitmapWidth = 1115;
        private const int BitmapHeight = 700;

        private readonly int cellPixelSize;
        private LifeGrid lifeGrid;

        private bool m_MouseDown = false;
        private SolidBrush aliveCell = new SolidBrush(Color.Black);
        private Graphics graphics;
        private Bitmap bitmap;

        private ConwaysClient client;

        public MainForm() : this(140, 223) { }

        public MainForm(int rows, int columns)
        {
            this.cellPixelSize = (BitmapHeight / rows);
            this.lifeGrid = new LifeGrid(rows, columns, CellFactory.GetCell()) ;
            this.lifeGrid.Randomise();
            this.bitmap = new Bitmap(BitmapWidth, BitmapHeight);
            this.graphics = Graphics.FromImage(bitmap);
            this.client = new ConwaysClient();

            InitializeComponent();

            ShowGrid(this.lifeGrid.CurrentGrid);
        }

        private void ShowGrid(ICell[,] currentGrid)
        {
            client.ShowGrid(currentGrid, this.cellPixelSize, this.aliveCell, ref this.graphics);
        }

        private void Window_Load(object sender, EventArgs e)
        {

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(bitmap, 0, 0);
        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled ? true : false;
            startStopBtn.Text = !timer1.Enabled ? "Run" : "Stop";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PerformIteration();
        }

        private void stepBtn_Click(object sender, EventArgs e)
        {
            PerformIteration();
        }

        private void PerformIteration()
        {
            lifeGrid.UpdateState();
            ShowGrid(lifeGrid.CurrentGrid);
            Refresh();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            //m_Grid.ClearAll();
            Refresh();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string saveFile = "";
            saveFileDialog1.Title = "Save current state of the game.";
            saveFileDialog1.FileName = "";

            saveFileDialog1.Filter = "GOL Files|*.gol";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                saveFile = saveFileDialog1.FileName;

                Stream stream = File.Open(saveFile, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();

                Console.WriteLine("Writing GOL Information");
                bformatter.Serialize(stream, lifeGrid);
                stream.Close();
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            string loadFile = "";
            openFileDialog1.Title = "Load saved game state.";
            openFileDialog1.FileName = "";

            openFileDialog1.Filter = "GOL Files|*.gol";

            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                loadFile = openFileDialog1.FileName;

                //Clear mp for further usage.
                //m_Grid = null;

                //Open the file written above and read values from it.
                Stream stream = File.Open(loadFile, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();

                Console.WriteLine("Reading GOL Information");
                this.lifeGrid = (LifeGrid)bformatter.Deserialize(stream);
                ShowGrid(this.lifeGrid.CurrentGrid);
                stream.Close();
                Refresh();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = "Conways Game of Life: MouseX: " + e.X + " - MouseY: " + e.Y;
            if (m_MouseDown)
            {
                // bloody important
                //int i = (int)e.X / m_Grid.CellSize;
                //int j = (int)e.Y / m_Grid.CellSize;
                //if ((i >= 0 && i < 192) && (j >= 0 && j < 106))
                //{
                //    m_Grid.paintCellAt(i, j, true);
                //}
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