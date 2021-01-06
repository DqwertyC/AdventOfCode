using System.Collections.Generic;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2016
{

    class Day17 : ASolution
    {
        string partOne;
        string partTwo;

        private static string passcode;

        public Day17() : base(17, 2016, "")
        {
            passcode = Input;

            bool shortestFound = false;
            Queue<string> pathsToSearch = new Queue<string>();
            pathsToSearch.Enqueue("");
            int longestFound = 0;

            while (pathsToSearch.Count > 0)
            {
                string nextPath = pathsToSearch.Dequeue();

                if ((3,3) == GetPosition(nextPath))
                {
                    if (!shortestFound)
                    {
                        shortestFound = true;
                        partOne = nextPath;
                    }
                    else
                    {
                        longestFound = nextPath.Length;
                    }
                }
                else
                {
                    foreach (string newPath in GetPossibleSteps(nextPath))
                    {
                        pathsToSearch.Enqueue(newPath);
                    }
                }
            }

            partTwo = longestFound.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static List<string> GetPossibleSteps(string path)
        {
            List<string> validPaths = new List<string>();
            string directions = GetHashMD5(passcode + path).Substring(0,4);
            (int x, int y) pos = GetPosition(path);

            if (directions[0] >= 'b' && pos.y > 0) validPaths.Add(path + "U");
            if (directions[1] >= 'b' && pos.y < 3) validPaths.Add(path + "D");
            if (directions[2] >= 'b' && pos.x > 0) validPaths.Add(path + "L");
            if (directions[3] >= 'b' && pos.x < 3) validPaths.Add(path + "R");

            return validPaths;
        }

        private static (int,int) GetPosition(string path)
        {
            (int x, int y) pos = (0,0);

            foreach (char c in path)
            {
                switch (c)
                {
                    case 'U': pos.y--; break;
                    case 'D': pos.y++; break;
                    case 'L': pos.x--; break;
                    case 'R': pos.x++; break;
                }
            }

            return pos;
        }
    }
}
