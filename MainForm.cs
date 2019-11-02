using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Puzzle15
{
    public partial class MainForm : Form
    {
        private int algorithm = 3;
        private int number;
        private int seed;
        private string text;
        private BackgroundWorker worker;
        private LinkedList<State> moves;

        public MainForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private Puzzle15State GenerateInitialState()
        {
            int i, j, k = 0;
            int[] finish = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0 };
            int[,] square = new int[4, 4];
            Random random = new Random(seed);

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    square[i, j] = finish[k++];

            Puzzle15State initial = new Puzzle15State(square);

            for (i = 0; i < number; i++)
            {
                LinkedList<State> moves = initial.GetPossibleMoves();
                int index = random.Next(moves.Count);
                Puzzle15State next = (Puzzle15State)moves.ElementAt<State>(index);
                initial.Square = next.Square;
            }

            return initial;
        }

        private void GenerateAndSolve(object sender, DoWorkEventArgs e)
        {
            bool first = true;
            Puzzle15State state = GenerateInitialState();

            text = string.Empty;

            if (algorithm == 1)
            {
                DepthFirstSolver solver = new DepthFirstSolver();
                moves = solver.Solve(state, worker);
                text += "Depth First Solution\r\n";
            }

            else if (algorithm == 2)
            {
                BreadthFirstSolver solver = new BreadthFirstSolver();
                moves = solver.Solve(state, worker);
                text += "Breadth First Solution\r\n";
            }

            else if (algorithm == 3)
            {
                BestFirstSolver solver = new BestFirstSolver();
                moves = solver.Solve(state, worker);
                text += "Best First Solution\r\n";
            }

            if (worker.CancellationPending)
            {
                text += "Computation Cancelled\r\n";
                return;
            }

            text += "Moves = " + moves.Count + "\r\n";
            
            foreach (State s in moves)
            {
                Puzzle15State ns = (Puzzle15State)s;

                if (ns.IsSolution())
                {
                    text += "Final State: " + ns;
                    break;
                }
                else if (first)
                {
                    text += "Start State: " + ns;
                    first = false;
                }
                else
                    text += "Inter State: " + ns;
            }
        }

        private void RunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                text += "Computation Cancelled\r\n";
            else if (!(e.Error == null))
                text += e.Error.Message;

            button2.Text = "Compute";
            button3.Enabled = true;
            textBox1.Text += text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text.CompareTo("Compute") == 0)
            {
                button2.Text = "Stop";
                button3.Enabled = false;
                number = int.Parse((string)comboBox1.SelectedItem);
                seed = int.Parse((string)comboBox2.SelectedItem);
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += new DoWorkEventHandler(GenerateAndSolve);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunCompleted);
                while (worker.IsBusy) { }
                worker.RunWorkerAsync();
            }
            else
                worker.CancelAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = string.Empty;
            textBox1.Text = string.Empty;
        }

        private void bestFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 3;
        }

        private void breadthFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 2;
        }

        private void depthFirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            algorithm = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (moves != null)
            {
                DrawPuzzle15Form form = new DrawPuzzle15Form("Puzzle15 Draw Solution", moves);
                form.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}