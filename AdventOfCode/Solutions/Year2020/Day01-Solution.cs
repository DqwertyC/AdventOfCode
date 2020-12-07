using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {
        private static List<int> allInts;

        public Day01() : base(01, 2020, "")
        {
            string[] lines = Input.SplitByNewline();

            allInts = new List<int>();

            foreach (string line in lines)
            {
                //Parse out input here
                allInts.Add(Int32.Parse(line));
            }
        }

        protected override string SolvePartOne()
        {
            // Search through each combo of 3 ints until we hit the right one
            foreach (int i in allInts)
            {
                foreach (int j in allInts)
                {
                     if (2020 == i + j)
                     {
                         // Jump out of this loop early
                         return "" + (i * j);
                     }
                }
            }

            return "";
        }

        protected override string SolvePartTwo()
        {
            // Search through each combo of 3 ints until we hit the right one
            foreach (int i in allInts)
            {
                foreach (int j in allInts)
                {
                    foreach (int k in allInts)
                    {
                        if (2020 == i + j + k)
                        {
                            // Jump out of this loop early
                            return "" + (i * j * k);
                        }
                    }
                }
            }

            return "";
        }
    }
}
