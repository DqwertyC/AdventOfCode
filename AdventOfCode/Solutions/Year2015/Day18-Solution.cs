using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2015
{

    class Day18 : ASolution
    {
        string partOne;
        string partTwo;

        static int xMax = 100;
        static int yMax = 100;

        static List<Coordinate2D> corners;

        public Day18() : base(18, 2015, "")
        {
            Dictionary<Coordinate2D, bool> cellStatesPartOne = new Dictionary<Coordinate2D, bool>();
            Dictionary<Coordinate2D, bool> cellStatesPartTwo = new Dictionary<Coordinate2D, bool>();
            int y = 0;
            int x = 0;

            foreach (string line in Input.SplitByNewline())
            {
                x = 0;
                foreach (char c in line)
                {
                    if (c == '#')
                    {
                        cellStatesPartOne[(x, y)] = true;
                        cellStatesPartTwo[(x, y)] = true;
                    }
                    else
                    {
                        cellStatesPartOne[(x, y)] = false;
                        cellStatesPartTwo[(x, y)] = false;
                    }
                    x++;
                }
                y++;
            }

            corners = new List<Coordinate2D>();
            corners.Add((0, 0));
            corners.Add((x-1, 0));
            corners.Add((0, y-1));
            corners.Add((x-1, y-1));

            foreach (var coord in corners)
            {
                cellStatesPartTwo[coord] = true;
            }

            int countPartOne = 0;
            int countPartTwo = 0;

            for (int i = 0; i < 100; i++)
            {
                cellStatesPartOne = AutomataTick2D(cellStatesPartOne);
                cellStatesPartTwo = AutomataTick2D(cellStatesPartTwo);

                foreach(var coord in corners)
                {
                    cellStatesPartTwo[coord] = true;
                }
            }

            foreach (bool val in cellStatesPartOne.Values)
            {
                if (val) countPartOne++;
            }

            foreach (bool val in cellStatesPartTwo.Values)
            {
                if (val) countPartTwo++;
            }

            partOne = countPartOne.ToString();
            partTwo = countPartTwo.ToString();
        }

        public static Dictionary<Coordinate2D, bool> AutomataTick2D(Dictionary<Coordinate2D, bool> oldState)
        {
            Dictionary<Coordinate2D, bool> newState = new Dictionary<Coordinate2D, bool>();

            foreach (Coordinate2D cellCoord in oldState.Keys)
            {
                int neighborCount = 0;
                foreach (Coordinate2D offset in Coordinate2D.GetNeighbors())
                {
                    Coordinate2D checkCoords = cellCoord + offset;
                    if (oldState.GetValueOrDefault(checkCoords, false))
                        neighborCount++;
                }

                if (corners.Contains(cellCoord))
                {
                    newState[cellCoord] = true;
                }
                else if (oldState[cellCoord])
                {
                    if (neighborCount == 2 || neighborCount == 3) newState[cellCoord] = true;
                    else newState[cellCoord] = false;
                }
                else
                {
                    if (neighborCount == 3) newState[cellCoord] = true;
                    else newState[cellCoord] = false;
                }
            }

            return newState;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }
    }
}
