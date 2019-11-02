using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    class DepthFirstSolver : Solver
    {
        private Stack<State> stack = new Stack<State>();

        protected override void AddState(State state)
        {
            if (!stack.Contains(state))
                stack.Push(state);
        }

        protected override bool HasElements()
        {
            return stack.Count != 0;
        }

        protected override State NextState()
        {
            return stack.Pop();
        }

        protected override void ClearOpen()
        {
            stack.Clear();
        }
    }
}
