using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day04 : ASolution
    {
        string partOne;
        string partTwo;

        public Day04() : base(04, 2016, "")
        {
            int validSum = 0;

            foreach (string line in Input.SplitByNewline())
            {
                Dictionary<char, int> charCounts = new Dictionary<char, int>();
                string[] lineParts = line.Split(new char[] { '-', '[', ']' });
                int i = 0;
                int roomId;

                while (!int.TryParse(lineParts[i], out roomId))
                {
                    foreach (char c in lineParts[i])
                    {
                        charCounts[c] = charCounts.GetValueOrDefault(c, 0) + 1;
                    }
                    i++;
                }

                if (ConstructChecksum(charCounts).Equals(lineParts[lineParts.Length - 2]))
                {
                    validSum += roomId;

                    StringBuilder lineString = new StringBuilder();

                    for (int j = 0; j < i; j++)
                    {
                        foreach (char c in lineParts[j])
                        {
                            char d = (char)(c + (roomId % 26));
                            if (d > 'z') d -= (char)26;
                            lineString.Append(d);
                        }
                        lineString.Append('-');
                    }

                    String translatedLine = lineString.ToString();

                    if (translatedLine.StartsWith("north"))
                    {
                        partTwo = roomId.ToString();
                    }
                }
            }

            partOne = validSum.ToString();
        }

        public static string ConstructChecksum(Dictionary<char,int> input)
        {
            StringBuilder checksum = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                int mostCommonCount = 0;
                char mostCommonChar = (char)('a'-1) ;

                foreach (char c in input.Keys)
                {
                    if (input[c] > mostCommonCount)
                    {
                        mostCommonCount = input[c];
                        mostCommonChar = c;
                    }
                    else if (input[c] == mostCommonCount)
                    {
                        mostCommonChar = c > mostCommonChar ? mostCommonChar : c;
                    }
                }

                input.Remove(mostCommonChar);
                checksum.Append(mostCommonChar);
            }

            return checksum.ToString();
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
