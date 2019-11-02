using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    public interface IState
    {
        LinkedList<State> GetPossibleMoves();
        bool IsSolution();
        double GetHeuristic();
    }
}
