using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day19 : ASolution
    {
        string partOne;
        string partTwo;

        public Day19() : base(19, 2016, "")
        {
            int maxVal = int.Parse(Input);
            partOne = GetSafePositionOne(maxVal).ToString();
            partTwo = GetSafePositionTwo(maxVal).ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static int GetSafePositionOne(int n)
        {
            int baseOffset = n - (int)Math.Pow(2, (int)Math.Log2(n));
            return 2 * baseOffset + 1;
        }

        private static int GetSafePositionTwo(int n)
        {
            int pow = (int)Math.Log(n - 1, 3);

            int baseValue = (int)Math.Pow(3, pow);
            int baseOffset = n - baseValue;

            if (baseOffset <= baseValue) return baseOffset;
            else return baseValue + 2 * (baseOffset - baseValue);

        }
    }
}
