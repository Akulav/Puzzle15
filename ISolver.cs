using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    public interface ISolver
    {
        LinkedList<State> Solve(State initial, BackgroundWorker worker);
    }
}
