using System;
using System.Drawing;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private const int NumCells = 3;
        private const int CellLength = GridLength / NumCells;

        private bool[,] grid;
        private Random rand;

        public MainForm()
        {
            InitializeComponent();

            rand = new Random();
            grid = new bool[NumCells, NumCells];

            for (int r = 0; r < NumCells; r++)
                for (int c = 0; c < NumCells; c++)
                    grid[r, c] = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void GameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGameButton_Click(sender, e);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < NumCells; r++)
                for (int c = 0; c < NumCells; c++)
                    grid[r, c] = rand.Next(2) == 1;

            this.Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    Brush brush;
                    Pen pen;

                    if (grid[r, c])
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * NumCells + GridOffset ||
            e.Y < GridOffset || e.Y > CellLength * NumCells + GridOffset)
                return;
            
            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

            for (int i = r - 1; i <= r + 1; i++)
                for (int j = c - 1; j <= c + 1; j++)
                    if (i >= 0 && i < NumCells && j >= 0 && j < NumCells)
                        grid[i, j] = !grid[i, j];

            this.Invalidate();

            if (PlayerWon())
            {
                MessageBox.Show(this, "Congratulations! You've won!", "Lights Out!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool PlayerWon()
        {
            bool won = true;
            for (int r = 0; r < NumCells; r++)
            {
                for (int c = 0; c < NumCells; c++)
                {
                    if (grid[r, c] == true)
                        won = false;
                }
            }
            return won;
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            for (int r = 0; r < NumCells; r++)
                for (int c = 0; c < NumCells; c++)
                    grid[r, c] = rand.Next(2) == 1;
            
            this.Invalidate();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }
    }
}
