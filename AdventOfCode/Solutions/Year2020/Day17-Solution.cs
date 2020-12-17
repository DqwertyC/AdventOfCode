using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2020
{

    class Day17 : ASolution
    {
        string partOne;
        string partTwo;        
 
        public Day17() : base(17, 2020, "")
        {
            Dictionary<Coordinate3D, bool> cellStates3D = new Dictionary<Coordinate3D, bool>();
            Dictionary<Coordinate4D, bool> cellStates4D = new Dictionary<Coordinate4D, bool>();
            int y = 0;


            foreach (string line in Input.SplitByNewline())
            {
                int x = 0;
                foreach (char c in line)
                {
                    if (c == '#')
                    {
                        cellStates3D[(x, y, 0)] = true;
                        cellStates4D[(x, y, 0, 0)] = true;
                    }
                    x++;
                }
                y++;
            }

            int activeCount3D = 0;
            int activeCount4D = 0;

            AddNeighborCells3D(cellStates3D);
            AddNeighborCells4D(cellStates4D);

            for (int i = 0; i < 6; i++)
            {
                cellStates3D = AutomataTick3D(cellStates3D);
                cellStates4D = AutomataTick4D(cellStates4D);
            }

            foreach (bool val in cellStates3D.Values)
            {
                if (val) activeCount3D++;
            }

            foreach (bool val in cellStates4D.Values)
            {
                if (val) activeCount4D++;
            }

            partOne = activeCount3D.ToString();
            partTwo = activeCount4D.ToString();
        }


        public static Dictionary<Coordinate3D, bool> AutomataTick3D(Dictionary<Coordinate3D, bool> oldState)
        {
            Dictionary<Coordinate3D, bool> newState = new Dictionary<Coordinate3D, bool>();

            foreach (Coordinate3D cellCoord in oldState.Keys)
            {
                int neighborCount = 0;
                foreach (Coordinate3D offset in Coordinate3D.GetNeighbors())
                {
                    Coordinate3D checkCoords = cellCoord + offset;
                    if (oldState.GetValueOrDefault(checkCoords, false))
                        neighborCount++;
                }

                if (oldState[cellCoord])
                {
                    if (neighborCount == 2 || neighborCount == 3) newState[cellCoord] = true;
                }
                else
                {
                    if (neighborCount == 3) newState[cellCoord] = true;
                }
            }

            AddNeighborCells3D(newState);
            return newState;
        }

        public static Dictionary<Coordinate4D, bool> AutomataTick4D(Dictionary<Coordinate4D, bool> oldState)
        {
            Dictionary<Coordinate4D, bool> newState = new Dictionary<Coordinate4D, bool>();

            foreach (Coordinate4D cellCoord in oldState.Keys)
            {
                int neighborCount = 0;
                foreach (Coordinate4D offset in Coordinate4D.GetNeighbors())
                {
                    Coordinate4D checkCoords = (Coordinate4D)cellCoord + (Coordinate4D) offset;
                    if (oldState.GetValueOrDefault(checkCoords, false))
                        neighborCount++;
                }

                if (oldState[cellCoord])
                {
                    if (neighborCount == 2 || neighborCount == 3) newState[cellCoord] = true;
                }
                else
                {
                    if (neighborCount == 3) newState[cellCoord] = true;
                }
            }

            AddNeighborCells4D(newState);
            return newState;
        }

        public static void AddNeighborCells3D(Dictionary<Coordinate3D, bool> cellStates)
        {
            Coordinate3D[] oldKeys = new Coordinate3D[cellStates.Keys.Count];
            cellStates.Keys.CopyTo(oldKeys,0);

            foreach (Coordinate3D cellCoords in oldKeys)
            {
                if (cellStates.GetValueOrDefault(cellCoords, false))
                {
                    foreach (Coordinate3D offset in Coordinate3D.GetNeighbors())
                    {
                        if (!cellStates.GetValueOrDefault((Coordinate3D) cellCoords + (Coordinate3D) offset, false))
                        {
                            cellStates[(Coordinate3D)cellCoords + (Coordinate3D)offset] = false;
                        }
                    }
                }
            }
        }

        public static void AddNeighborCells4D(Dictionary<Coordinate4D, bool> cellStates)
        {
            Coordinate4D[] oldKeys = new Coordinate4D[cellStates.Keys.Count];
            cellStates.Keys.CopyTo(oldKeys, 0);

            foreach (Coordinate4D cellCoords in oldKeys)
            {
                if (cellStates.GetValueOrDefault(cellCoords, false))
                {
                    foreach (Coordinate4D offset in Coordinate4D.GetNeighbors())
                    {
                        if (!cellStates.GetValueOrDefault((Coordinate4D)cellCoords + (Coordinate4D)offset, false))
                        {
                            cellStates[(Coordinate4D)cellCoords + (Coordinate4D) offset] = false;
                        }
                    }
                }
            }
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
