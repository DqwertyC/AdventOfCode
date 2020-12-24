using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day06 : ASolution
    {
        string partOne;
        string partTwo;

        public Day06() : base(06, 2016, "")
        {
            Dictionary<char, int>[] letterFrequencies = new Dictionary<char, int>[8];

            for (int i = 0; i < 8; i++) letterFrequencies[i] = new Dictionary<char, int>();

            foreach (string line in Input.SplitByNewline())
            {
                for (int i = 0; i < 8; i++)
                {
                    letterFrequencies[i][line[i]] = letterFrequencies[i].GetValueOrDefault(line[i], 0) + 1;
                }
            }

            partOne = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                int mostCommenCount = 0;
                int leastCommenCount = int.MaxValue;
                char mostCommenChar = '\0';
                char leastCommenChar ='\0';

                foreach (char c in letterFrequencies[i].Keys)
                {
                    if (letterFrequencies[i][c] > mostCommenCount)
                    {
                        mostCommenChar = c;
                        mostCommenCount = letterFrequencies[i][c];
                    }

                    if (letterFrequencies[i][c] < leastCommenCount)
                    {
                        leastCommenChar = c;
                        leastCommenCount = letterFrequencies[i][c];
                    }
                }

                partOne += mostCommenChar;
                partTwo += leastCommenChar;
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
