using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day09 : ASolution
    {
        long partOne;
        long partTwo;
        public Day09() : base(09, 2020, "")
        {
            List<long> allValues = new List<long>();
            long[] pastNumbers = new long[25];
            int index = 0;
            int count = 0;

            foreach (string line in Input.SplitByNewline())
            {
                if (count < 25)
                {
                    pastNumbers[index] = Int64.Parse(line);
                    allValues.Add(pastNumbers[index]);
                    index = (index + 1) % 25;
                    count++;
                    
                }
                else
                {
                    long next = Int64.Parse(line);
                    allValues.Add(next);
                    bool valid = false;

                    for (int i = 0; i < 24; i++)
                    {
                        for (int j = i+1; j < 25; j++)
                        {
                            if (next == pastNumbers[i] + pastNumbers[j]) valid = true;
                        }
                    }

                    if (!valid) partOne = next;

                    count++;
                    pastNumbers[index] = next;
                    index = (index + 1) % 25;

                }
                
            }

            for (int i = 0; i < allValues.Count && partTwo == 0; i++)
            {
                long sum = allValues[i];
                int offset = 1;

                long smallest = allValues[i];
                long largest = allValues[i];

                while (sum < partOne)
                {
                    if (allValues[i + offset] < smallest) smallest = allValues[i + offset];
                    if (allValues[i + offset] > largest) largest = allValues[i + offset];

                    sum += allValues[i + offset];
                    offset++;
                }

                if (sum == partOne)
                {
                    partTwo = smallest + largest;
                }
            }
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
        }
    }
}
