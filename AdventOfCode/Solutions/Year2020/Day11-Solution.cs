using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day11 : ASolution
    {
        long partOne;
        long partTwo;

        public enum State
        {
            EMPTY,
            FULL,
            FLOOR
        };

        public Day11() : base(11, 2020, "")
        {
            List<string> lines = new List<string>();
            lines.AddRange(Input.SplitByNewline());

            int h = lines.Count;
            int w = lines[0].Length;

            State[,] init = new State[w, h];

            int line = 0;
            foreach (string s in lines)
            {
                int col = 0;
                foreach (char c in s)
                {
                    if (c == 'L') init[col, line] = State.EMPTY;
                    else if (c == '#') init[col, line] = State.FULL;
                    else init[col, line] = State.FLOOR;

                    col++;
                }
                line++;
            }

            State[,] oldSates = init;
            State[,] newStates;

            while (UpdateStates(oldSates, w,h,out newStates))
            {
                oldSates = newStates;
            }

            int count = 0;

            foreach (State s in newStates)
            {
                if (s == State.FULL) count++;
            } 

            partOne = count;

            //Do it again for part two!

            oldSates = init;

            while (UpdateStates(oldSates, w, h, out newStates,true))
            {
                oldSates = newStates;
            }

            count = 0;

            foreach (State s in newStates)
            {
                if (s == State.FULL) count++;
            }

            partTwo = count;
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
        }

        public bool UpdateStates(State[,] old, int w, int h, out State[,] newState, bool partTwo = false)
        {
            newState = new State[w, h];

            bool changed = false;

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if (old[i,j] == State.EMPTY)
                    {
                        int count;

                        if (partTwo) count = NeighborsFullTwo(old, i, j, w, h);
                        else count = NeighborsFull(old, i, j, w, h);

                        if (count == 0)
                        {
                            newState[i, j] = State.FULL;
                            changed = true;
                        }
                        else
                        {
                            newState[i, j] = State.EMPTY;
                        }
                    }
                    else if (old[i,j] == State.FULL)
                    {
                        int count;
                        if (partTwo) count = NeighborsFullTwo(old, i, j, w, h);
                        else count = NeighborsFull(old, i, j, w, h);

                        if (count > 4)
                        {
                            newState[i, j] = State.EMPTY;
                            changed = true;
                        }
                        else
                        {
                            newState[i, j] = State.FULL;
                        }
                    }
                    else
                    {
                        newState[i,j] = State.FLOOR;
                    }
                 
                }
            }
            return changed;
        }

        public int NeighborsFull(State[,] states, int x, int y, int w, int h)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (x + i >= 0 && x + i < w && y + j >= 0 && y + j < h)
                    {
                        if (states[x+i,y+j] == State.FULL)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }

        public int NeighborsFullTwo(State[,] states, int x, int y, int w, int h)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (!(i==0 && j==0))
                    {
                        if (SeesSomeone(states, x, y, w, h, i, j)) count++;
                    }
                }
            }

            return count;

        }

        public bool SeesSomeone(State[,] states, int x, int y, int w, int h, int dx, int dy)
        {
            int lookX = x + dx;
            int lookY = y + dy;
            bool seen = false;
            bool done = false;

            while (lookX >= 0 && lookY >= 0 && lookX < w && lookY < h && !seen && !done)
            {

                if (states[lookX, lookY] == State.FULL) seen = true;
                else if (states[lookX, lookY] == State.EMPTY) done = true;
                lookX += dx;
                lookY += dy;
            }

            return seen;
        }
    }
}
