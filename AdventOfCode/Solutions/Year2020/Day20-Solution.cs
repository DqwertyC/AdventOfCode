using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2020
{

    class Day20 : ASolution
    {
        string partOne;
        string partTwo;

        static Coordinate2D[] serpentOffsets = { (0, 18), (1, 0), (1, 5), (1, 6), (1, 11), (1, 12), (1, 17), (1, 18), (1, 19), (2, 1), (2, 4), (2, 7), (2, 10), (2, 13), (2, 16) };

        public Day20() : base(20, 2020, "")
        {
            string[] tiles = Input.Split("\n\n");

            Dictionary<int,int> allEdges = new Dictionary<int,int>();
            List<CameraTile> allTiles = new List<CameraTile>();
            var edgeToTiles = new Dictionary<int, List<CameraTile>>();

            foreach (string tile in tiles)
            {
                CameraTile newTile = new CameraTile(tile);
                allTiles.Add(newTile);

                foreach (int edge in newTile.AllPossibleNeighbors())
                {
                    allEdges[edge] = allEdges.GetValueOrDefault(edge, 0) + 1;

                    if (edgeToTiles.TryGetValue(edge, out List<CameraTile> tilesWithEdge))
                    {
                        tilesWithEdge.Add(newTile);
                    }
                    else
                    {
                        edgeToTiles[edge] = new List<CameraTile>();
                        edgeToTiles[edge].Add(newTile);
                    }
                }
            }

            List<CameraTile> Corner = new List<CameraTile>();
            List<CameraTile> Edge = new List<CameraTile>();
            List<CameraTile> Center = new List<CameraTile>();

            foreach (CameraTile t in allTiles)
            {
                int edgeCount = 0;

                if (allEdges[t.N] == 1 && allEdges[flipped(t.N)] == 1)
                {
                    t.blockedN = true;
                    edgeCount++;
                }
                if (allEdges[t.E] == 1 && allEdges[flipped(t.E)] == 1)
                {
                    t.blockedE = true;
                    edgeCount++;
                }
                if (allEdges[t.S] == 1 && allEdges[flipped(t.S)] == 1)
                {
                    t.blockedS = true;
                    edgeCount++;
                }
                if (allEdges[t.W] == 1 && allEdges[flipped(t.W)] == 1)
                {
                    t.blockedW = true;
                    edgeCount++;
                }

                if (edgeCount == 2) Corner.Add(t);
                else if (edgeCount == 1) Edge.Add(t);
                else Center.Add(t);
            }

            long product = 1;

            foreach (CameraTile t in Corner)
            {
                product *= t.tileId;
            }

            partOne = product.ToString();

////////////////////////////////////////////////////////////////////////////////////////////////////

            Dictionary<Coordinate2D, CameraTile> board = new Dictionary<Coordinate2D, CameraTile>();

            CameraTile lastTile = Corner[0];
            board[(0, 0)] = lastTile;
            Corner.Remove(lastTile);
            bool nextFound = false;

            // Align the top left tile
            while (lastTile.blockedE || lastTile.blockedS) lastTile.RotateL();

            // Fill in the top row
            for (int i = 1; i < 11; i++)
            {
                nextFound = false;
                CameraTile next = null;

                for (int j = 0; j < Edge.Count && !nextFound; j++)
                {
                    CameraTile t = Edge[j];
                    while (!t.blockedN) t.RotateL();

                    if (lastTile.E == t.W)
                    {
                        nextFound = true;
                        next = t;
                    }
                    else if (lastTile.E == t.E)
                    {
                        t.FlipHorizontal();
                        nextFound = true;
                        next = t;
                    }
                }

                board[(0, i)] = next;
                lastTile = next;
                Edge.Remove(lastTile);
            }

            board[(0, 0)].GetActualMap();

            // Find and place the next corner
            nextFound = false;
            for (int j = 0; j < Corner.Count && !nextFound; j++)
            {
                CameraTile t = Corner[j];
                while (t.blockedW || t.blockedS) t.RotateL();

                if (lastTile.E == t.W)
                {
                    nextFound = true;
                    lastTile = t;
                    board[(0,11)] = t;
                }
                else
                {
                    t.FlipHorizontal();
                    while (t.blockedW || t.blockedS) t.RotateL();

                    if (lastTile.E == t.W)
                    {
                        nextFound = true;
                        lastTile = t;
                        board[(0, 11)] = t;
                    }
                }
            }

            board[(0, 0)].GetActualMap();
            Corner.Remove(board[(0, 11)]);

            // Fill in the right side of the board
            for (int i = 1; i < 11; i++)
            {
                nextFound = false;
                CameraTile next = null;

                for (int j = 0; j < Edge.Count && !nextFound; j++)
                {
                    CameraTile t = Edge[j];
                    while (!t.blockedE) t.RotateL();

                    if (lastTile.S == t.N)
                    {
                        nextFound = true;
                        next = t;
                    }
                    else if (lastTile.S == t.S)
                    {
                        t.FlipVertical();
                        nextFound = true;
                        next = t;
                    }
                }

                board[(i, 11)] = next;
                lastTile = next;
                Edge.Remove(lastTile);
            }

            // Find the next (lower-right) corner
            nextFound = false;
            for (int j = 0; j < Corner.Count && !nextFound; j++)
            {
                CameraTile t = Corner[j];
                while (t.blockedN || t.blockedW) t.RotateL();

                if (lastTile.S == t.N)
                {
                    nextFound = true;
                    lastTile = t;
                    board[(11, 11)] = t;
                }
                else
                {
                    t.FlipHorizontal();
                    while (t.blockedN || t.blockedW) t.RotateL();

                    if (lastTile.S == t.N)
                    {
                        nextFound = true;
                        lastTile = t;
                        board[(11, 11)] = t;
                    }
                }
            }

            Corner.Remove(board[(11, 11)]);

            // Fill in the bottom side of the board
            for (int i = 10; i > 0; i--)
            {
                nextFound = false;
                CameraTile next = null;

                for (int j = 0; j < Edge.Count && !nextFound; j++)
                {
                    CameraTile t = Edge[j];
                    while (!t.blockedS) t.RotateL();

                    if (lastTile.W == t.E)
                    {
                        nextFound = true;
                        next = t;
                    }
                    else if (lastTile.W == t.W)
                    {
                        t.FlipHorizontal();
                        nextFound = true;
                        next = t;
                    }
                }

                board[(11, i)] = next;
                lastTile = next;
                Edge.Remove(lastTile);
            }

            // Find the next (lower-left) corner
            nextFound = false;
            for (int j = 0; j < Corner.Count && !nextFound; j++)
            {
                CameraTile t = Corner[j];
                while (t.blockedE || t.blockedN) t.RotateL();

                if (lastTile.W == t.E)
                {
                    nextFound = true;
                    lastTile = t;
                    board[(11, 0)] = t;
                }
                else
                {
                    t.FlipHorizontal();
                    while (t.blockedE || t.blockedN) t.RotateL();

                    if (lastTile.W == t.E)
                    {
                        nextFound = true;
                        lastTile = t;
                        board[(11, 0)] = t;
                    }
                }
            }

            Corner.Remove(board[(11, 0)]);

            // Fill in the left side of the board
            for (int i = 10; i > 0; i--)
            {
                nextFound = false;
                CameraTile next = null;

                for (int j = 0; j < Edge.Count && !nextFound; j++)
                {
                    CameraTile t = Edge[j];
                    while (!t.blockedW) t.RotateL();

                    if (lastTile.N == t.S)
                    {
                        nextFound = true;
                        next = t;
                    }
                    else if (lastTile.N == t.N)
                    {
                        t.FlipVertical();
                        nextFound = true;
                        next = t;
                    }
                }

                board[(i, 0)] = next;
                lastTile = next;
                Edge.Remove(lastTile);
            }

            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    nextFound = false;
                    for (int k = 0; k < Center.Count && !nextFound; k++)
                    {
                        CameraTile t = Center[k];
                        int borderW = board[(i, j - 1)].E;
                        int borderN = board[(i - 1, j)].S;

                        if (t.AllPossibleNeighbors().Contains(borderN)
                            && t.AllPossibleNeighbors().Contains(borderW))
                        {
                            while (t.N != borderN && t.S != borderN) t.RotateL();
                            if (t.S == borderN && t.N != borderN) t.FlipVertical();

                            if (t.W == borderW)
                            {
                                nextFound = true;

                                if (board.ContainsKey((i, j + 1)))
                                {
                                    if (t.E != board[(i, j + 1)].W) nextFound = false;
                                }

                                if (board.ContainsKey((i + 1, j)))
                                {
                                    if (t.S != board[(i + 1, j)].N) nextFound = false;
                                }

                                if (nextFound) lastTile = t;
                            }
                        }
                    }

                    board[(i, j)] = lastTile;
                }
            }

////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////

            bool[,] massiveMap = new bool[12 * 8, 12 * 8];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    bool[,] subMap = board[(i, j)].GetActualMap();

                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            massiveMap[8 * i + k, 8 * j + l] = subMap[k, l];
                        }
                    }
                }
            }

            bool[,] serpentBoard;

            bool found = false;

            found = TryGetSerpents(massiveMap, 96, out serpentBoard);
            if (!found)
            {
                massiveMap = RotateArrayL(massiveMap, 96);
                found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                if (!found)
                {
                    massiveMap = RotateArrayL(massiveMap, 96);
                    found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                    if (!found)
                    {
                        massiveMap = RotateArrayL(massiveMap, 96);
                        found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                        if (!found)
                        {
                            massiveMap = FlipArrayHorizontal(massiveMap, 96);
                            found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                            if (!found)
                            {
                                massiveMap = RotateArrayL(massiveMap, 96);
                                found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                                if (!found)
                                {
                                    massiveMap = RotateArrayL(massiveMap, 96);
                                    found = TryGetSerpents(massiveMap, 96, out serpentBoard);

                                    if (!found)
                                    {
                                        massiveMap = RotateArrayL(massiveMap, 96);
                                        TryGetSerpents(massiveMap, 96, out serpentBoard);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int tileCount = 0;

            for (int i = 0; i < 96; i++)
            {
                for (int j = 0; j < 96; j++)
                {
                    if (massiveMap[i, j] && !serpentBoard[i, j])
                    {
                        tileCount++;
                    }
                }
            }

            partTwo = tileCount.ToString();

        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public static bool TryGetSerpents(bool[,] board, int length, out bool[,] serpents)
        {
            int count = 0;
            serpents = new bool[length, length];

            for (int i = 0; i < length - 2; i++)
            {
                for (int j = 0; j < length - 19; j++)
                {
                    bool isSerpent = true;

                    foreach (Coordinate2D offset in serpentOffsets)
                    {
                        if (!board[i + offset.x, j + offset.y]) isSerpent = false;
                    }

                    if (isSerpent)
                    {
                        count++;
                        foreach (Coordinate2D offset in serpentOffsets)
                        {
                            serpents[i + offset.x, j + offset.y] = true;
                        }
                    }
                }
            }

            return count > 0;
        }

        private class CameraTile
        {
            int hashCode;

            public readonly int tileId;
            private bool[,] tilePixels;

            public int N, E, S, W;

            public bool blockedN, blockedE, blockedS, blockedW;

            public CameraTile (string input)
            {
                tilePixels = new bool[10, 10];

                string[] lines = input.Split("\n");

                tileId = int.Parse(lines[0].Split(new char[] { ':', ' ' })[1]);

                hashCode = 0;

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (lines[i+1][j] == '#')
                        {
                            tilePixels[i, j] = lines[i + 1][j] == '#';
                            hashCode++;
                        }
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    N = N << 1;
                    E = E << 1;
                    S = S << 1;
                    W = W << 1;

                    if (tilePixels[0, i]) N++;
                    if (tilePixels[i, 9]) E++;
                    if (tilePixels[9, i]) S++;
                    if (tilePixels[i, 0]) W++;
                }
            }

            public HashSet<int> AllPossibleNeighbors()
            {
                HashSet<int> neighbors = new HashSet<int>();

                neighbors.Add(N);
                neighbors.Add(E);
                neighbors.Add(S);
                neighbors.Add(W);
                neighbors.Add(flipped(N));
                neighbors.Add(flipped(E));
                neighbors.Add(flipped(S));
                neighbors.Add(flipped(W));

                return neighbors;
            }

            public bool[,] GetActualMap()
            {
                bool[,] map = new bool[8, 8];

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        map[i, j] = tilePixels[i + 1, j + 1];
                    }
                }
                return map;
            }

            public void FlipVertical()
            {
                tilePixels = FlipArrayVertical(tilePixels, 10);

                int helper = N;
                N = S;
                S = helper;
                E = flipped(E);
                W = flipped(W);

                bool blockedHelper = blockedN;
                blockedN = blockedS;
                blockedS = blockedHelper;
            }

            public void FlipHorizontal()
            {
                tilePixels = FlipArrayHorizontal(tilePixels, 10);

                int helper = E;
                E = W;
                W = helper;
                N = flipped(N);
                S = flipped(S);

                bool blockedHelper = blockedE;
                blockedE = blockedW;
                blockedW = blockedHelper;
            }

            public void RotateL()
            {
                tilePixels = RotateArrayL(tilePixels, 10);

                int helper = N;
                N = E;
                E = flipped(S);
                S = W;
                W = flipped(helper);

                bool blockedHelper = blockedN;
                blockedN = blockedE;
                blockedE = blockedS;
                blockedS = blockedW;
                blockedW = blockedHelper;
            }

            public override int GetHashCode()
            {
                return tileId;
            }
        }

        private static T[,] RotateArrayL<T>(T[,] input, int length)
        {
            T[,] newGrid = new T[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    newGrid[i, j] = input[j, length - i - 1];
                }
            }

            return newGrid;
        }

        private static T[,] FlipArrayHorizontal<T>(T[,] input, int length)
        {
            T[,] newGrid = new T[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    newGrid[i, j] = input[i, length - j - 1];
                }
            }

            return newGrid;
        }

        private static T[,] FlipArrayVertical<T>(T[,] input, int length)
        {
            T[,] newGrid = new T[length, length];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    newGrid[i, j] = input[length - i - 1, j];
                }
            }

            return newGrid;
        }

        static int flipped(int val)
        {
            int oldVal = val;
            int newVal = 0;

            for (int i = 0; i < 10; i++)
            {
                newVal = newVal << 1;
                if (oldVal % 2 == 1)
                {
                    newVal++;
                }
                oldVal = oldVal >> 1;
            }

            return newVal;
        }
    }
}
