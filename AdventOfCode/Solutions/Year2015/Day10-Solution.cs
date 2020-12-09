using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day10 : ASolution
    {
        int lengthOne = 0;
        int lengthTwo = 0;

        public Day10() : base(10, 2015, "")
        {
            string lookSayString = Input;

            for (int i = 0; i < 50; i++)
            {
                if (i == 40) lengthOne = lookSayString.Length;
                lookSayString = LookSay(lookSayString);
            }

            lengthTwo = lookSayString.Length;
        }

        protected override string SolvePartOne()
        {
            return lengthOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return lengthTwo.ToString();
        }

        private static string LookSay(string input)
        {
            char current;
            int count;
            StringBuilder output = new StringBuilder();

            current = input[0];
            count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != current)
                {
                    output.Append(count);
                    output.Append(current);
                    current = input[i];
                    count = 0;
                }

                count++;
            }

            output.Append(count);
            output.Append(current);

            return output.ToString();
        }
    }
}
