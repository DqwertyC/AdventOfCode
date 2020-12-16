using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day15 : ASolution
    {
        private static int partOne;
        private static int partTwo;
        public Day15() : base(15, 2020, "")
        {
            int[] startVals = Input.ToIntArray(",");
            Dictionary<int, int> lastAddress = new Dictionary<int, int>();

            int nextVal;
            int lastVal = 0;
            int index = 0;

            for (int i = 0; i < startVals.Length - 1; i++)
            {
                lastAddress[startVals[i]] = i;
                index++;
            }

            nextVal = startVals[startVals.Length - 1];

            while (index < 30000000)
            {
                lastVal = nextVal;
                int lastMatch = lastAddress.GetValueOrDefault(nextVal, index);
                nextVal = index - lastMatch;
                lastAddress[lastVal] = index++;

                if (index == 2020) partOne = lastVal;
            }

            partTwo = lastVal;
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
