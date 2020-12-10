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

            adapters.AddRange(Input.ToIntArray("\n"));
            adapters.Sort();

            int countOne = 0;
            int countThree = 0;

            // Check the offset of the first adapter
            if (adapters[0] == 1) countOne++;
            else countThree++;

            // Count how many are seperated by one and how many by three
            for (int i = 1; i < adapters.Count; i++)
            {
                if (adapters[i] - adapters[i - 1] == 1) countOne++;
                else if (adapters[i] - adapters[i - 1] == 3) countThree++;
            }

            // Add a plus one for the computer adapter
            partOne = countOne * (countThree + 1);

            int strandlength = 0;
            if (adapters[0] == 1) strandlength++;

            List<long> factors = new List<long>();

            // Each strand of ones can be traversed multiple ways, but are all
            // independent of the other strands of ones
            for (int i = 1; i < adapters.Count; i++)
            {
                if (adapters[i] == adapters[i-1] + 1)
                {
                    strandlength++;
                }
                else
                {
                    factors.Add(lengthToFactor(strandlength));
                    strandlength = 0;
                }
            }

            // Don't forget the last one!
            factors.Add(lengthToFactor(strandlength));
            long product = 1;

            // Multiply them all together
            foreach (long i in factors)
            {
                product *= i;
            }

            partTwo = product;

        }

        private long lengthToFactor(int input)
        {
            long factor = 0;

            for (int i = 0; i <= (input / 3); i++)
            {
                int spotsLeft = input - 3 * i;

                for (int j = 0; j <= (spotsLeft /2); j++)
                {
                    int blocksOfThree = i;
                    int blocksOfTwo = j;
                    int blocksOfOne = spotsLeft - 2 * j;

                    long factorOne = factorial(blocksOfOne + blocksOfTwo + blocksOfThree);
                    long factorTwo = factorial(blocksOfOne) * factorial(blocksOfTwo) * factorial(blocksOfThree);

                    factor += ((factorOne) / (factorTwo));
                }
            }
            return factor;
        }
        


        private long factorial(int input)
        {
            long output = 1;

            for (int i = 1; i <= input; i++)
            {
                output *= i;
            }

            return output;
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
