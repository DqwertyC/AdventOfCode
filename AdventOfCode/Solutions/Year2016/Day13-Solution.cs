using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2016
{

    class Day13 : ASolution
    {
        string partOne;
        string partTwo;

        Dictionary<(int,int), bool> floorLocations = new Dictionary<(int,int), bool>();

        public Day13() : base(13, 2016, "")
        {
            int favoriteNumber = int.Parse(Input);

            floorLocations[(1, 1)] = true;

            Queue<(Coordinate2D coords, int pathLength)> tilesToCheck = new Queue<(Coordinate2D coords, int pathLength)>();
            tilesToCheck.Enqueue(((1, 1), 0));
            bool pathFound = false;
            int shortestLength = 0;
            int nearbyCount = 0;

            while (!pathFound)
            {
                var current = tilesToCheck.Dequeue();

                if (current.pathLength <= 50) nearbyCount++;

                if (current.coords.x == 31 && current.coords.y == 39)
                {
                    pathFound = true;
                    shortestLength = current.pathLength;
                }
                else
                {
                    foreach (Coordinate2D neighbor in Coordinate2D.gridNeighbors)
                    {
                        var next = current.coords + neighbor;

                        if (!floorLocations.ContainsKey(next))
                        {
                            floorLocations[next] = IsFloor(next, favoriteNumber);

                            if (floorLocations[next])
                            {
                                tilesToCheck.Enqueue((next, current.pathLength + 1));
                            }
                        }
                    }
                }    
            }

            partOne = shortestLength.ToString();
            partTwo = nearbyCount.ToString();
        }

        public static bool IsFloor(Coordinate2D coord, int number)
        {
            if (coord.x < 0 || coord.y < 0) return false;

            int value = (coord.x * coord.x + 3 * coord.x + 2 * coord.x * coord.y + coord.y + coord.y * coord.y);
            value += number;

            int bitCount = 0;
            while (value > 0)
            {
                if (value % 2 != 0) bitCount++;
                value = value >> 1;
            }

            if (bitCount % 2 != 0) return false;
            return true;

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
