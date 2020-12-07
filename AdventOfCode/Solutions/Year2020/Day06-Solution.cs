using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day06 : ASolution
    {
        private static int partOne = 0;
        private static int partTwo = 0;

        public Day06() : base(06, 2020, "")
        {
            string[] groups = Input.Split("\n\n");

            foreach (string g in groups)
            {
                CountYes(g, out int yesOne, out int yesTwo);
                partOne += yesOne;
                partTwo += yesTwo;
            }
        }

        protected override string SolvePartOne()
        {
            return "" + partOne;
        }

        protected override string SolvePartTwo()
        {
            return "" + partTwo;
        }

        private static void CountYes(string group, out int partOne, out int partTwo)
        {
            partOne = 0;
            partTwo = 0;

            for (char i = 'a'; i <= 'z'; i++)
            {
                if (group.Contains(i)) partOne++;

                bool valid = true;

                foreach (string person in group.Split("\n"))
                {
                    if (!person.Contains(i)) valid = false;
                }

                if (valid) partTwo++;
            }
        }
    }
}
