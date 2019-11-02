using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    class PriorityQueue
    {
        List<State> stateList;

        public List<State> StateList
        {
            get
            {
                return stateList;
            }

            set
            {
                stateList = value;
            }
        }

        public PriorityQueue()
        {
            stateList = new List<State>();
        }

        public PriorityQueue(List<State> sl)
        {
            stateList = new List<State>();

            for (int i = 0; i < sl.Count; i++)
                stateList.Add(sl[i]);
        }

        int Comparison(State s1, State s2)
        {
            double f1 = s1.Distance;
            double f2 = s2.Distance;

            if (f1 < f2)
                return -1;
            else if (f1 == f2)
                return 0;
            else
                return +1;
        }

        public State ExtractMin()
        {
            stateList.Sort(Comparison);
            State result = stateList[0];
            stateList.RemoveAt(0);
            return result;
        }
    }
}
