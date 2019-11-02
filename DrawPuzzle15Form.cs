using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Puzzle15
{
    public partial class DrawPuzzle15Form : Form
    {
        int move;
        string[] positions;
        LinkedList<State> moves;

        void PaintPanel(Object obj, PaintEventArgs pea)
        {
            int W = panel1.Width - 16;
            int H = panel1.Height - 16;
            int w = W / 4 - 8, x;
            int h = H / 4 - 8, y = 16;
            Brush brush = new SolidBrush(Color.White);
            Brush fontBrush = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericMonospace, (float) (h - 64), FontStyle.Bold);
            Graphics g = pea.Graphics;
            Pen pen = new Pen(Color.Black);

            g.DrawRectangle(pen, 8, 8, W, H);

            for (int i = 0; i < 4; i++)
            {
                x = 16;

                for (int j = 0; j < 4; j++)
                {
                    g.FillRectangle(brush, x, y, w, h);

                    string p = positions[4 * i + j];

                    if (p.Length == 1)
                        g.DrawString(p.ToString(), font, fontBrush, x + 6, y + 6);

                    else
                        g.DrawString(p.ToString(), font, fontBrush, x - 36, y + 6);
 
                    x += w + 8;
                }

                y += h + 8;
            }
        }

        void ButtonOnClick(Object obj, EventArgs ea)
        {
            if (move < moves.Count)
            {
                GetMove();
                panel1.Invalidate();
                textBox3.Text = move.ToString();
            }

            else
                button1.Enabled = false;
        }

        private string FromIntToString(int b)
        {
            StringBuilder sb = new StringBuilder();

            if (b >= 0 && b <= 9)
                sb.Append((char)(b + '0'));
            else
            {
                switch (b)
                {
                    case 10:
                        sb.Append("10");
                        break;
                    case 11:
                        sb.Append("11");
                        break;
                    case 12:
                        sb.Append("12");
                        break;
                    case 13:
                        sb.Append("13");
                        break;
                    case 14:
                        sb.Append("14");
                        break;
                    case 15:
                        sb.Append("15");
                        break;
                }
            }

            return sb.ToString();
        }

        private void GetMove()
        {
            int m = 0;
            Puzzle15State next = null;

            foreach (State s in moves)
            {
                next = (Puzzle15State)s;

                if (m == move)
                    break;

                m++;
            }

            move++;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    positions[4 * i + j] = FromIntToString(next.Square[i, j]);
        }

        public DrawPuzzle15Form(string title, LinkedList<State> moves)
        {
            InitializeComponent();
            move = 0;
            positions = new string[16];
            this.moves = moves;
            GetMove();
            Text = title;
            button1.Click += new EventHandler(ButtonOnClick);
            panel1.Paint += new PaintEventHandler(PaintPanel);
            textBox2.Text = moves.Count.ToString();
            textBox3.Text = "1";
        }
    }
}