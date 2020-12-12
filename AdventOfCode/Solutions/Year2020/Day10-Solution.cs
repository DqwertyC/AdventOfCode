using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day10 : ASolution
    {
        long partOne;
        long partTwo;

        public Day10() : base(10, 2020, "")
        {
            List<int> adapters = new List<int>();
            Dictionary<int, long> paths = new Dictionary<int, long>();
            paths[0] = 1;

            adapters.AddRange(Input.ToIntArray("\n"));
            adapters.Add(0);
            adapters.Sort();
            adapters.Add(adapters[adapters.Count - 1] + 3);

            int countOne = 0;
            int countThree = 0;

            int adapterVal;
            int oldAdapterVal = adapters[0];

            // Count how many are seperated by one and how many by three
            for (int i = 1; i < adapters.Count; i++)
            {
                adapterVal = adapters[i];

                if (adapterVal - oldAdapterVal == 1) countOne++;
                else countThree++;

                long pathCount = 0;

                for (int j = 1; j <= 3; j++)
                {
                    if (paths.ContainsKey(adapterVal - j)) pathCount += paths[adapterVal - j];
                }

                paths[adapterVal] = pathCount;
                partTwo = pathCount;

                oldAdapterVal = adapterVal;
            }

            partOne = countOne * countThree;

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
