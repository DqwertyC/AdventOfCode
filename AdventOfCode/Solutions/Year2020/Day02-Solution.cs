using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day02 : ASolution
    {
        private static string[] lines;

        public Day02() : base(02, 2020, "")
        {
            lines = Input.SplitByNewline();
        }

        protected override string SolvePartOne()
        {
            int validCount = 0;

            foreach (string line in lines)
            {
                if (ValidPartOne(line)) validCount++;
            }

            return "" + validCount;
        }

        protected override string SolvePartTwo()
        {
            int validCount = 0;

            foreach (string line in lines)
            {
                if (ValidPartTwo(line)) validCount++;
            }

            return "" + validCount;
        }

        private static bool ValidPartOne(string input)
        {
            int min;
            int max;
            char target;
            int occourences = 0;

            string[] parts = input.Split(new[] { '-', ' ', ':' });

            min = Int32.Parse(parts[0]);
            max = Int32.Parse(parts[1]);
            target = parts[2][0];

            foreach (char c in parts[4])
            {
                if (c == target) occourences++;
            }

            return (occourences >= min && occourences <= max);
        }

        private static bool ValidPartTwo(string input)
        {
            int pos_a;
            int pos_b;
            char target;

            string[] parts = input.Split(new[] { '-', ' ', ':' });

            pos_a = Int32.Parse(parts[0]);
            pos_b = Int32.Parse(parts[1]);
            target = parts[2][0];

            return ((parts[4][pos_a - 1] == target) ^ (parts[4][pos_b - 1] == target));
        }
    }
}
