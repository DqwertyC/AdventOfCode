using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day17 : ASolution
    {
        string partOne;
        string partTwo;

        Dictionary<int, int> combinationsAtCount;

        public Day17() : base(17, 2015, "")
        {
            int[] containers = Input.ToIntArray("\n");
            combinationsAtCount = new Dictionary<int, int>();
            partOne = GetCombinationCount(containers, 0, 150, 1).ToString();

            int minCount = combinationsAtCount.KeyList(true)[0];
            partTwo = combinationsAtCount[minCount].ToString();
            
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private int GetCombinationCount(int[] inputs, int startIndex, int targetAmount, int containerCount)
        {
            int validCombinations = 0;

            for (int i = startIndex; i < inputs.Length; i++)
            {
                if (inputs[i] == targetAmount)
                {
                    combinationsAtCount[containerCount] = combinationsAtCount.GetValueOrDefault(containerCount, 0) + 1;
                    validCombinations++;
                }
                else if (inputs[i] < targetAmount)
                {
                    validCombinations += GetCombinationCount(inputs, i + 1, targetAmount - inputs[i], containerCount + 1);
                }
            }

            return validCombinations;
        }
    }
}
