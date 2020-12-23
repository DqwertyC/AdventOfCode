using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2016
{

    class Day01 : ASolution
    {
        string partOne;
        string partTwo;
        
        public Day01() : base(01, 2016, "")
        {
            Coordinate2D facing = (0, 1);
            Coordinate2D location = (0, 0);

            HashSet<(int,int)> visited = new HashSet<(int,int)>();

            foreach (string instruction in Input.Split(", "))
            {
                if (instruction[0] == 'R') facing = facing.RotateCW();
                else facing = facing.RotateCCW();

                int distance = int.Parse(instruction.Substring(1));
                Coordinate2D offset = distance * facing;

                for (int i = 0; i < distance; i++)
                {
                    Coordinate2D visitedLocation = location + (i * facing);

                    if (visited.Contains(visitedLocation) && partTwo == null)
                    {
                        partTwo = (Math.Abs(visitedLocation.x) + Math.Abs(visitedLocation.y)).ToString();
                    }

                    visited.Add(visitedLocation);
                }

                location += offset;
            }

            partOne = (Math.Abs(location.x) + Math.Abs(location.y)).ToString();
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
