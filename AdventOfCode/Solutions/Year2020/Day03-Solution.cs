using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        // x and y of the test slopes
        private static int[] slopesX = { 1, 3, 5, 7, 1 };
        private static int[] slopesY = { 1, 1, 1, 1, 2 };
        private static int slopeCount = slopesX.Length;

        private static int[] treesHit = new int[slopeCount];

        public Day03() : base(03, 2020, "")
        {
            int[] current = new int[slopeCount];

            for (int i = 0; i < slopeCount; i++)
            {
                current[i] = 0;
                treesHit[i] = 0;
            }

            int lineNumber = 0;

            foreach (string line in Input.SplitByNewline())
            {
                for (int i = 0; i < slopeCount; i++)
                {
                    // Some slopes only hit certain lines
                    if (0 == lineNumber % slopesY[i])
                    {
                        // Check for trees and see if we need to loop
                        if (line[current[i]] == '#') treesHit[i]++;
                        current[i] = (current[i] + slopesX[i]) % line.Length;
                    }
                }

                lineNumber++;
            }
        }

        protected override string SolvePartOne()
        {
            return "" + treesHit[1];
        }

        protected override string SolvePartTwo()
        {
            long product = 1;

            for (int i = 0; i < slopeCount; i++)
            {
                product *= treesHit[i];
            }

            return "" + product;
        }
    }
}
