using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {
        private static int maxPass;
        private static int myPass;
        private static bool[] hasPass;

        public Day05() : base(05, 2020, "")
        {
            hasPass = new bool[1024];
            maxPass = 0;

            //Get the ids of each row
            foreach (string line in Input.SplitByNewline())
            {
                int id = CalculateID(line);
                hasPass[id] = true;
                if (maxPass < id) maxPass = id;
            }

            bool firstTrue = false;

            // Find the first "false" after the first "true"
            for (int i = 0; i < 1024 && 0 == myPass; i++)
            {
                if (hasPass[i] && !firstTrue) firstTrue = true;
                if (!hasPass[i] && firstTrue) myPass = i;
            }
        }

        protected override string SolvePartOne()
        {
            return "" + maxPass;
        }

        protected override string SolvePartTwo()
        {
            return "" + myPass;
        }

        public static int CalculateID(string locationString)
        {
            int row = 0;
            int col = 0;

            // The passes are just two binary numbers...
            // Get the row
            for (int i = 0; i < 7; i++)
            {
                if ('B' == locationString[i])
                {
                    row += (Int32)(Math.Pow(2, 6 - i));
                }
            }

            // Get the col
            for (int i = 0; i < 3; i++)
            {
                if ('R' == locationString[i + 7])
                {
                    col += (Int32)(Math.Pow(2, 2 - i));
                }
            }

            return 8 * row + col;
        }
    }
}
