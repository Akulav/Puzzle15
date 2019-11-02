using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    class Puzzle15State : State
    {
        private int[,] square = new int[4, 4];
        private static int g;

        public int[,] Square
        {
            get
            {
                return square;
            }
            set
            {
                square = value;
            }
        }

        public Puzzle15State(int[,] square)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    this.square[i, j] = square[i, j];
        }

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    result += square[i, j].ToString() + ' ';
            result += "\r\n";

            return result;
        }

        public override bool Equals(object obj)
        {
            Puzzle15State s = (Puzzle15State)obj;

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (square[i, j] != s.square[i, j])
                        return false;

            return true;
        }

        public override LinkedList<State> GetPossibleMoves()
        {
            LinkedList<State> list = new LinkedList<State>();
            int[, ,] tempSquare = new int[4, 4, 4];
            int b = expand(tempSquare);

            for (int i = 0; i < b; i++)
            {
                int[,] ts = new int[4, 4];

                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        ts[j, k] = tempSquare[i, j, k];

                Puzzle15State state = new Puzzle15State(ts);
                
                state.Parent = this;
                state.Distance = g + ManhattanDistance(ts);
                list.AddLast(state);
            }

            g++;

            return list;
        }

        private int expand(int[, ,] tempSquare)
        {
            int b = -1, col = -1, i, j, k, row = -1;

            for (i = 0; i < 4; i++)
                for (j = 0; j < 4; j++)
                    for (k = 0; k < 4; k++)
                        tempSquare[i, j, k] = square[j, k];
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (square[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }
            if (row == 0 && col == 0)
            {
                tempSquare[0, 0, 0] = tempSquare[0, 0, 1];
                tempSquare[0, 0, 1] = 0;
                tempSquare[1, 0, 0] = tempSquare[1, 1, 0];
                tempSquare[1, 1, 0] = 0;
                b = 2;
            }
            else if (row == 0 && col == 1)
            {
                tempSquare[0, 0, 1] = tempSquare[0, 0, 0];
                tempSquare[0, 0, 0] = 0;
                tempSquare[1, 0, 1] = tempSquare[1, 1, 1];
                tempSquare[1, 1, 1] = 0;
                tempSquare[2, 0, 1] = tempSquare[2, 0, 2];
                tempSquare[2, 0, 2] = 0;
                b = 3;
            }
            else if (row == 0 && col == 2)
            {
                tempSquare[0, 0, 2] = tempSquare[0, 0, 1];
                tempSquare[0, 0, 1] = 0;
                tempSquare[1, 0, 2] = tempSquare[1, 1, 2];
                tempSquare[1, 1, 2] = 0;
                tempSquare[2, 0, 2] = tempSquare[2, 0, 3];
                tempSquare[2, 0, 3] = 0;
                b = 3;
            }
            else if (row == 0 && col == 3)
            {
                tempSquare[0, 0, 3] = tempSquare[0, 0, 2];
                tempSquare[0, 0, 2] = 0;
                tempSquare[1, 0, 3] = tempSquare[1, 1, 3];
                tempSquare[1, 1, 3] = 0;
                b = 2;
            }
            else if (row == 1 && col == 0)
            {
                tempSquare[0, 1, 0] = tempSquare[0, 0, 0];
                tempSquare[0, 0, 0] = 0;
                tempSquare[1, 1, 0] = tempSquare[1, 1, 1];
                tempSquare[1, 1, 1] = 0;
                tempSquare[2, 1, 0] = tempSquare[2, 2, 0];
                tempSquare[2, 2, 0] = 0;
                b = 3;
            }
            else if (row == 1 && col == 1)
            {
                tempSquare[0, 1, 1] = tempSquare[0, 1, 0];
                tempSquare[0, 1, 0] = 0;
                tempSquare[1, 1, 1] = tempSquare[1, 0, 1];
                tempSquare[1, 0, 1] = 0;
                tempSquare[2, 1, 1] = tempSquare[2, 1, 2];
                tempSquare[2, 1, 2] = 0;
                tempSquare[3, 1, 1] = tempSquare[3, 2, 1];
                tempSquare[3, 2, 1] = 0;
                b = 4;
            }
            else if (row == 1 && col == 2)
            {
                tempSquare[0, 1, 2] = tempSquare[0, 0, 2];
                tempSquare[0, 0, 2] = 0;
                tempSquare[1, 1, 2] = tempSquare[1, 1, 1];
                tempSquare[1, 1, 1] = 0;
                tempSquare[2, 1, 2] = tempSquare[2, 2, 2];
                tempSquare[2, 2, 2] = 0;
                tempSquare[3, 1, 2] = tempSquare[3, 1, 3];
                tempSquare[3, 1, 3] = 0;
                b = 4;
            }
            else if (row == 1 && col == 3)
            {
                tempSquare[0, 1, 3] = tempSquare[0, 0, 3];
                tempSquare[0, 0, 3] = 0;
                tempSquare[1, 1, 3] = tempSquare[1, 1, 2];
                tempSquare[1, 1, 2] = 0;
                tempSquare[2, 1, 3] = tempSquare[2, 2, 3];
                tempSquare[2, 2, 3] = 0;
                b = 4;
            }
            else if (row == 2 && col == 0)
            {
                tempSquare[0, 2, 0] = tempSquare[0, 1, 0];
                tempSquare[0, 1, 0] = 0;
                tempSquare[1, 2, 0] = tempSquare[1, 2, 1];
                tempSquare[1, 2, 1] = 0;
                tempSquare[2, 2, 0] = tempSquare[2, 3, 0];
                tempSquare[2, 3, 0] = 0;
                b = 3;
            }
            else if (row == 2 && col == 1)
            {
                tempSquare[0, 2, 1] = tempSquare[0, 2, 0];
                tempSquare[0, 2, 0] = 0;
                tempSquare[1, 2, 1] = tempSquare[1, 1, 1];
                tempSquare[1, 1, 1] = 0;
                tempSquare[2, 2, 1] = tempSquare[2, 2, 2];
                tempSquare[2, 2, 2] = 0;
                tempSquare[3, 2, 1] = tempSquare[3, 3, 1];
                tempSquare[3, 3, 1] = 0;
                b = 4;
            }
            else if (row == 2 && col == 2)
            {
                tempSquare[0, 2, 2] = tempSquare[0, 2, 1];
                tempSquare[0, 2, 1] = 0;
                tempSquare[1, 2, 2] = tempSquare[1, 1, 2];
                tempSquare[1, 1, 2] = 0;
                tempSquare[2, 2, 2] = tempSquare[2, 2, 3];
                tempSquare[2, 2, 3] = 0;
                tempSquare[3, 2, 2] = tempSquare[3, 3, 2];
                tempSquare[3, 3, 2] = 0;
                b = 4;
            }
            else if (row == 2 && col == 3)
            {
                tempSquare[0, 2, 3] = tempSquare[0, 1, 3];
                tempSquare[0, 1, 3] = 0;
                tempSquare[1, 2, 3] = tempSquare[1, 2, 2];
                tempSquare[1, 2, 2] = 0;
                tempSquare[2, 2, 3] = tempSquare[2, 3, 3];
                tempSquare[2, 3, 3] = 0;
                b = 3;
            }
            else if (row == 3 && col == 0)
            {
                tempSquare[0, 3, 0] = tempSquare[0, 2, 0];
                tempSquare[0, 2, 0] = 0;
                tempSquare[1, 3, 0] = tempSquare[1, 3, 1];
                tempSquare[1, 3, 1] = 0;
                b = 2;
            }
            else if (row == 3 && col == 1)
            {
                tempSquare[0, 3, 1] = tempSquare[0, 2, 1];
                tempSquare[0, 2, 1] = 0;
                tempSquare[1, 3, 1] = tempSquare[1, 3, 0];
                tempSquare[1, 3, 0] = 0;
                tempSquare[2, 3, 1] = tempSquare[2, 3, 2];
                tempSquare[2, 3, 2] = 0;
                b = 3;
            }
            else if (row == 3 && col == 2)
            {
                tempSquare[0, 3, 2] = tempSquare[0, 2, 2];
                tempSquare[0, 2, 2] = 0;
                tempSquare[1, 3, 2] = tempSquare[1, 3, 1];
                tempSquare[1, 3, 1] = 0;
                tempSquare[2, 3, 2] = tempSquare[2, 3, 3];
                tempSquare[2, 3, 3] = 0;
                b = 3;
            }
            else if (row == 3 && col == 3)
            {
                tempSquare[0, 3, 3] = tempSquare[0, 2, 3];
                tempSquare[0, 2, 3] = 0;
                tempSquare[1, 3, 3] = tempSquare[1, 3, 2];
                tempSquare[1, 3, 2] = 0;
                b = 2;
            }
            return b;
        }

        public override bool IsSolution()
        {
            bool found = true;

            for (int i = 0; found && i < 4; i++)
            {
                for (int j = 0; found && j < 4; j++)
                {
                    if (i == 3 && j == 3)
                        found = square[i, j] == 0;
                    else
                        found = square[i, j] == 4 * i + j + 1;
                }
            }

            return found;
        }

        public override double GetHeuristic()
        {
            return ManhattanDistance(square);
        }

        private int ManhattanDistance(int[,] square)
        {
            // city block or Manhatten distance heuristic
            int md = 0;

            // first row
            if (square[0, 0] == 1)
                md += 0;
            else if (square[0, 0] == 2)
                md += 1;
            else if (square[0, 0] == 3)
                md += 2;
            else if (square[0, 0] == 4)
                md += 3;
            else if (square[0, 0] == 5)
                md += 1;
            else if (square[0, 0] == 6)
                md += 2;
            else if (square[0, 0] == 7)
                md += 3;
            else if (square[0, 0] == 8)
                md += 4;
            else if (square[0, 0] == 9)
                md += 2;
            else if (square[0, 0] == 10)
                md += 3;
            else if (square[0, 0] == 11)
                md += 4;
            else if (square[0, 0] == 12)
                md += 5;
            else if (square[0, 0] == 13)
                md += 3;
            else if (square[0, 0] == 14)
                md += 4;
            else if (square[0, 0] == 15)
                md += 5;
            if (square[0, 1] == 1)
                md += 1;
            else if (square[0, 1] == 2)
                md += 0;
            else if (square[0, 1] == 3)
                md += 1;
            else if (square[0, 1] == 4)
                md += 2;
            else if (square[0, 1] == 5)
                md += 2;
            else if (square[0, 1] == 6)
                md += 1;
            else if (square[0, 1] == 7)
                md += 2;
            else if (square[0, 1] == 8)
                md += 3;
            else if (square[0, 1] == 9)
                md += 3;
            else if (square[0, 1] == 10)
                md += 2;
            else if (square[0, 1] == 11)
                md += 3;
            else if (square[0, 1] == 12)
                md += 4;
            else if (square[0, 1] == 13)
                md += 4;
            else if (square[0, 1] == 14)
                md += 3;
            else if (square[0, 1] == 15)
                md += 4;
            if (square[0, 2] == 1)
                md += 2;
            else if (square[0, 2] == 2)
                md += 1;
            else if (square[0, 2] == 3)
                md += 0;
            else if (square[0, 2] == 4)
                md += 1;
            else if (square[0, 2] == 5)
                md += 3;
            else if (square[0, 2] == 6)
                md += 2;
            else if (square[0, 2] == 7)
                md += 1;
            else if (square[0, 2] == 8)
                md += 2;
            else if (square[0, 2] == 9)
                md += 4;
            else if (square[0, 2] == 10)
                md += 3;
            else if (square[0, 2] == 11)
                md += 2;
            else if (square[0, 2] == 12)
                md += 3;
            else if (square[0, 2] == 13)
                md += 5;
            else if (square[0, 2] == 14)
                md += 4;
            else if (square[0, 2] == 15)
                md += 3;
            if (square[0, 3] == 1)
                md += 3;
            else if (square[0, 3] == 2)
                md += 2;
            else if (square[0, 3] == 3)
                md += 1;
            else if (square[0, 3] == 4)
                md += 0;
            else if (square[0, 3] == 5)
                md += 4;
            else if (square[0, 3] == 6)
                md += 3;
            else if (square[0, 3] == 7)
                md += 2;
            else if (square[0, 3] == 8)
                md += 1;
            else if (square[0, 3] == 9)
                md += 5;
            else if (square[0, 3] == 10)
                md += 4;
            else if (square[0, 3] == 11)
                md += 3;
            else if (square[0, 3] == 12)
                md += 2;
            else if (square[0, 3] == 13)
                md += 6;
            else if (square[0, 3] == 14)
                md += 5;
            else if (square[0, 3] == 15)
                md += 4;
            // second row
            if (square[1, 0] == 1)
                md += 1;
            else if (square[1, 0] == 2)
                md += 2;
            else if (square[1, 0] == 3)
                md += 3;
            else if (square[1, 0] == 4)
                md += 4;
            else if (square[1, 0] == 5)
                md += 0;
            else if (square[1, 0] == 6)
                md += 1;
            else if (square[1, 0] == 7)
                md += 2;
            else if (square[1, 0] == 8)
                md += 3;
            else if (square[1, 0] == 9)
                md += 1;
            else if (square[1, 0] == 10)
                md += 2;
            else if (square[1, 0] == 11)
                md += 3;
            else if (square[1, 0] == 12)
                md += 4;
            else if (square[1, 0] == 13)
                md += 2;
            else if (square[1, 0] == 14)
                md += 3;
            else if (square[1, 0] == 15)
                md += 4;
            if (square[1, 1] == 1)
                md += 2;
            else if (square[1, 1] == 2)
                md += 1;
            else if (square[1, 1] == 3)
                md += 2;
            else if (square[1, 1] == 4)
                md += 3;
            else if (square[1, 1] == 5)
                md += 1;
            else if (square[1, 1] == 6)
                md += 0;
            else if (square[1, 1] == 7)
                md += 1;
            else if (square[1, 1] == 8)
                md += 2;
            else if (square[1, 1] == 9)
                md += 2;
            else if (square[1, 1] == 10)
                md += 1;
            else if (square[1, 1] == 11)
                md += 2;
            else if (square[1, 1] == 12)
                md += 3;
            else if (square[1, 1] == 13)
                md += 3;
            else if (square[1, 1] == 14)
                md += 2;
            else if (square[1, 1] == 15)
                md += 3;
            if (square[1, 2] == 1)
                md += 3;
            else if (square[1, 2] == 2)
                md += 2;
            else if (square[1, 2] == 3)
                md += 1;
            else if (square[1, 2] == 4)
                md += 2;
            else if (square[1, 2] == 5)
                md += 2;
            else if (square[1, 2] == 6)
                md += 1;
            else if (square[1, 2] == 7)
                md += 0;
            else if (square[1, 2] == 8)
                md += 1;
            else if (square[1, 2] == 9)
                md += 3;
            else if (square[1, 2] == 10)
                md += 2;
            else if (square[1, 2] == 11)
                md += 1;
            else if (square[1, 2] == 12)
                md += 2;
            else if (square[1, 2] == 13)
                md += 4;
            else if (square[1, 2] == 14)
                md += 3;
            else if (square[1, 2] == 15)
                md += 2;
            if (square[1, 3] == 1)
                md += 4;
            else if (square[1, 3] == 2)
                md += 3;
            else if (square[1, 3] == 3)
                md += 2;
            else if (square[1, 3] == 4)
                md += 1;
            else if (square[1, 3] == 5)
                md += 3;
            else if (square[1, 3] == 6)
                md += 6;
            else if (square[1, 3] == 7)
                md += 1;
            else if (square[1, 3] == 8)
                md += 0;
            else if (square[1, 3] == 9)
                md += 4;
            else if (square[1, 3] == 10)
                md += 3;
            else if (square[1, 3] == 11)
                md += 2;
            else if (square[1, 3] == 12)
                md += 1;
            else if (square[1, 3] == 13)
                md += 5;
            else if (square[1, 3] == 14)
                md += 4;
            else if (square[1, 3] == 15)
                md += 3;
            // third row
            if (square[2, 0] == 1)
                md += 2;
            else if (square[2, 0] == 2)
                md += 3;
            else if (square[2, 0] == 3)
                md += 4;
            else if (square[2, 0] == 4)
                md += 5;
            else if (square[2, 0] == 5)
                md += 1;
            else if (square[2, 0] == 6)
                md += 2;
            else if (square[2, 0] == 7)
                md += 3;
            else if (square[2, 0] == 8)
                md += 4;
            else if (square[2, 0] == 9)
                md += 0;
            else if (square[2, 0] == 10)
                md += 1;
            else if (square[2, 0] == 11)
                md += 2;
            else if (square[2, 0] == 12)
                md += 3;
            else if (square[2, 0] == 13)
                md += 1;
            else if (square[2, 0] == 14)
                md += 2;
            else if (square[2, 0] == 15)
                md += 3;
            if (square[2, 1] == 1)
                md += 3;
            else if (square[2, 1] == 2)
                md += 2;
            else if (square[2, 1] == 3)
                md += 3;
            else if (square[2, 1] == 4)
                md += 4;
            else if (square[2, 1] == 5)
                md += 2;
            else if (square[2, 1] == 6)
                md += 1;
            else if (square[2, 1] == 7)
                md += 2;
            else if (square[2, 1] == 8)
                md += 3;
            else if (square[2, 1] == 9)
                md += 1;
            else if (square[2, 1] == 10)
                md += 0;
            else if (square[2, 1] == 11)
                md += 1;
            else if (square[2, 1] == 12)
                md += 2;
            else if (square[2, 1] == 13)
                md += 2;
            else if (square[2, 1] == 14)
                md += 1;
            else if (square[2, 1] == 15)
                md += 2;
            if (square[2, 2] == 1)
                md += 4;
            else if (square[2, 2] == 2)
                md += 3;
            else if (square[2, 2] == 3)
                md += 2;
            else if (square[2, 2] == 4)
                md += 3;
            else if (square[2, 2] == 5)
                md += 3;
            else if (square[2, 2] == 6)
                md += 2;
            else if (square[2, 2] == 7)
                md += 1;
            else if (square[2, 2] == 8)
                md += 2;
            else if (square[2, 2] == 9)
                md += 2;
            else if (square[2, 2] == 10)
                md += 1;
            else if (square[2, 2] == 11)
                md += 0;
            else if (square[2, 2] == 12)
                md += 1;
            else if (square[2, 2] == 13)
                md += 3;
            else if (square[2, 2] == 14)
                md += 2;
            else if (square[2, 2] == 15)
                md += 1;
            if (square[2, 3] == 1)
                md += 5;
            else if (square[2, 3] == 2)
                md += 4;
            else if (square[2, 3] == 3)
                md += 3;
            else if (square[2, 3] == 4)
                md += 2;
            else if (square[2, 3] == 5)
                md += 4;
            else if (square[2, 3] == 6)
                md += 3;
            else if (square[2, 3] == 7)
                md += 2;
            else if (square[2, 3] == 8)
                md += 1;
            else if (square[2, 3] == 9)
                md += 3;
            else if (square[2, 3] == 10)
                md += 2;
            else if (square[2, 3] == 11)
                md += 1;
            else if (square[2, 3] == 12)
                md += 0;
            else if (square[2, 3] == 13)
                md += 4;
            else if (square[2, 3] == 14)
                md += 3;
            else if (square[2, 3] == 15)
                md += 2;
            // fourth row
            if (square[3, 0] == 1)
                md += 3;
            else if (square[3, 0] == 2)
                md += 4;
            else if (square[3, 0] == 3)
                md += 5;
            else if (square[3, 0] == 4)
                md += 6;
            else if (square[3, 0] == 5)
                md += 2;
            else if (square[3, 0] == 6)
                md += 3;
            else if (square[3, 0] == 7)
                md += 4;
            else if (square[3, 0] == 8)
                md += 5;
            else if (square[3, 0] == 9)
                md += 1;
            else if (square[3, 0] == 10)
                md += 2;
            else if (square[3, 0] == 11)
                md += 3;
            else if (square[3, 0] == 12)
                md += 4;
            else if (square[3, 0] == 13)
                md += 0;
            else if (square[3, 0] == 14)
                md += 1;
            else if (square[3, 0] == 15)
                md += 2;
            if (square[3, 1] == 1)
                md += 4;
            else if (square[3, 1] == 2)
                md += 3;
            else if (square[3, 1] == 3)
                md += 4;
            else if (square[3, 1] == 4)
                md += 5;
            else if (square[3, 1] == 5)
                md += 3;
            else if (square[3, 1] == 6)
                md += 2;
            else if (square[3, 1] == 7)
                md += 3;
            else if (square[3, 1] == 8)
                md += 4;
            else if (square[3, 1] == 9)
                md += 2;
            else if (square[3, 1] == 10)
                md += 1;
            else if (square[3, 1] == 11)
                md += 2;
            else if (square[3, 1] == 12)
                md += 3;
            else if (square[3, 1] == 13)
                md += 1;
            else if (square[3, 1] == 14)
                md += 0;
            else if (square[3, 1] == 15)
                md += 1;
            if (square[3, 2] == 1)
                md += 5;
            else if (square[3, 2] == 2)
                md += 4;
            else if (square[3, 2] == 3)
                md += 3;
            else if (square[3, 2] == 4)
                md += 4;
            else if (square[3, 2] == 5)
                md += 4;
            else if (square[3, 2] == 6)
                md += 3;
            else if (square[3, 2] == 7)
                md += 2;
            else if (square[3, 2] == 8)
                md += 3;
            else if (square[3, 2] == 9)
                md += 3;
            else if (square[3, 2] == 10)
                md += 2;
            else if (square[3, 2] == 11)
                md += 1;
            else if (square[3, 2] == 12)
                md += 2;
            else if (square[3, 2] == 13)
                md += 2;
            else if (square[3, 2] == 14)
                md += 1;
            else if (square[3, 2] == 15)
                md += 0;
            if (square[3, 3] == 1)
                md += 6;
            else if (square[3, 3] == 2)
                md += 5;
            else if (square[3, 3] == 3)
                md += 4;
            else if (square[3, 3] == 4)
                md += 3;
            else if (square[3, 3] == 5)
                md += 5;
            else if (square[3, 3] == 6)
                md += 4;
            else if (square[3, 3] == 7)
                md += 3;
            else if (square[3, 3] == 8)
                md += 2;
            else if (square[3, 3] == 9)
                md += 4;
            else if (square[3, 3] == 10)
                md += 3;
            else if (square[3, 3] == 11)
                md += 2;
            else if (square[3, 3] == 12)
                md += 1;
            else if (square[3, 3] == 13)
                md += 3;
            else if (square[3, 3] == 14)
                md += 2;
            else if (square[3, 3] == 15)
                md += 1;
            return md;
        }
    }
}
