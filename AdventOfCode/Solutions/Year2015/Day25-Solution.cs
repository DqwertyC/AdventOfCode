using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day25 : ASolution
    {

        string partOne;
        string partTwo;

        public Day25() : base(25, 2015, "")
        {
            string[] coords = Input.Replace("To continue, please consult the code grid in the manual.  Enter the code at row ", "")
                                  .Replace(", column ", " ")
                                  .Replace(".", "")
                                  .Split(" ");

            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);

            int iterations = CoordsToIterations(row, col);

            long num = 20151125;

            for (int i = 1; i < iterations; i++)
            {
                num = (num * 252533) % 33554393;
            }

            partOne = num.ToString();
            partTwo = "Merry Christmas!";
        }

        private static int CoordsToIterations(int row, int col)
        {
            int diagStartRow = row + col - 1;
            int diagStartNum = (diagStartRow * diagStartRow - diagStartRow + 2) / 2;
            return diagStartNum + col - 1;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }
    }
}
