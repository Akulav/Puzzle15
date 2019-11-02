﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puzzle15
{
    public abstract class State : IState
    {
        private State parent = null;
        private double distance = 0;

        public State Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }

        public double Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }

        public State() { }
        public State(State parent)
        {
            this.parent = parent;
            this.distance = parent.distance + 1;
        }

        public abstract LinkedList<State> GetPossibleMoves();
        public abstract bool IsSolution();
        public abstract double GetHeuristic();
    }
}
