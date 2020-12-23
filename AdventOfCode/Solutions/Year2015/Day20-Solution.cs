using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day20 : ASolution
    {

        string partOne;
        string partTwo;

        public Day20() : base(20, 2015, "")
        {
            uint searchTarget = uint.Parse(Input);

            bool found = false;
            uint search = searchTarget / 50;

            while (!found)
            {
                int houseGifts = CalculateGiftsPartOne(search);

                if (houseGifts >= searchTarget) found = true;
                else search++;
            }

            partOne = search.ToString();

            found = false;
            search = searchTarget / 50;

            while (!found)
            {
                int houseGifts = CalculateGiftsPartTwo(search);

                if (houseGifts >= searchTarget) found = true;
                else search++;
            }

            partTwo = search.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static int CalculateGiftsPartOne(uint houseNumber)
        {
            int gifts = 0;

            foreach (int n in houseNumber.Factor())
            {
                gifts += n;
            }

            return 10 * gifts;
        }

        private static int CalculateGiftsPartTwo(uint houseNumber)
        {
            int gifts = 0;

            foreach (int n in houseNumber.Factor())
            {
                if (houseNumber / n <= 50)
                {
                    gifts += n;
                }
            }

            return 11 * gifts;
        }
    }
}
