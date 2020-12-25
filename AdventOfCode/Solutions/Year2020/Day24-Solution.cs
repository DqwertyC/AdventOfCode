using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day24 : ASolution
    {

        string partOne;
        string partTwo;

        public Day24() : base(24, 2020, "")
        {
            Dictionary<(int, int), bool> onTiles = new Dictionary<(int, int), bool>(); 

            foreach (string line in Input.SplitByNewline())
            {
                HexCoordinate newCoords = new HexCoordinate(0, 0);

                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'e')
                    {
                        newCoords += HexCoordinate.E;
                    }
                    else if (line[i] == 'w')
                    {
                        newCoords += HexCoordinate.W;
                    }
                    else if (line[i] == 'n')
                    {
                        i++;
                        if (line[i] == 'e')
                        {
                            newCoords += HexCoordinate.NE;
                        }
                        else if (line[i] == 'w')
                        {
                            newCoords += HexCoordinate.NW;
                        }
                    }
                    else if (line[i] == 's')
                    {
                        i++;
                        if (line[i] == 'e')
                        {
                            newCoords += HexCoordinate.SE;
                        }
                        else if (line[i] == 'w')
                        {
                            newCoords += HexCoordinate.SW;
                        }
                    }
                }

                onTiles[newCoords] = !onTiles.GetValueOrDefault(newCoords, false);
            }

            int onCount = 0;
            foreach (var tileVal in onTiles.Values)
            {
                if (tileVal) onCount++;
            }

            partOne = onCount.ToString();

            for (int i = 0; i < 100; i++)
            {
                AddNeighbors(onTiles);
                onTiles = RunAutomata(onTiles);
            }

            onCount = 0;
            foreach (var tileVal in onTiles.Values)
            {
                if (tileVal) onCount++;
            }

            partTwo = onCount.ToString();
        }

        private static Dictionary<(int,int), bool> RunAutomata(Dictionary<(int, int), bool> oldState)
        {
            // This method progresses the floor forward one tick
            Dictionary<(int, int), bool> newState = new Dictionary<(int, int), bool>();

            foreach ((int,int) tile in oldState.Keys)
            {
                int neighborCount = 0;

                foreach (HexCoordinate neighbor in HexCoordinate.neighbors)
                {
                    if (oldState.GetValueOrDefault(tile + neighbor, false)) neighborCount++;
                }

                if (oldState[tile])
                {
                    if (neighborCount == 1 || neighborCount == 2) newState[tile] = true;
                    else newState[tile] = false;
                }
                else
                {
                    if (neighborCount == 2) newState[tile] = true;
                    else newState[tile] = false;
                }
            }

            return newState;
        }

        private static void AddNeighbors(Dictionary<(int, int), bool> tileField)
        {
            // This method removes dead cells too far away to come alive

            (int, int)[] oldKeys = new (int, int)[tileField.Keys.Count];
            tileField.Keys.CopyTo(oldKeys, 0);

            foreach ((int,int) tile in oldKeys)
            {
                if (tileField[tile])
                {
                    foreach (HexCoordinate neighbor in HexCoordinate.neighbors)
                    {
                        if (!tileField.GetValueOrDefault(tile + neighbor, false))
                        {
                            tileField[tile + neighbor] = false;
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

        class HexCoordinate
        {
            int x;
            int y;

            public HexCoordinate(int xStart, int yStart)
            {
                x = xStart;
                y = yStart;
            }

            public static HexCoordinate E = new HexCoordinate(-2, 0);
            public static HexCoordinate W = new HexCoordinate(2, 0);
            public static HexCoordinate NE = new HexCoordinate(-1, 1);
            public static HexCoordinate NW = new HexCoordinate(1, 1);
            public static HexCoordinate SE = new HexCoordinate(-1, -1);
            public static HexCoordinate SW = new HexCoordinate(1, -1);

            public static HexCoordinate[] neighbors = new HexCoordinate[] { E, W, NE, NW, SE, SW };

            public static HexCoordinate operator +(HexCoordinate a) => a;
            public static HexCoordinate operator +(HexCoordinate a, HexCoordinate b) => new HexCoordinate(a.x + b.x, a.y + b.y);

            public static implicit operator HexCoordinate((int x, int y) a) => new HexCoordinate(a.x, a.y);
            public static implicit operator (int x, int y)(HexCoordinate a) => (a.x, a.y);

        }
    }
}
