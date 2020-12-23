using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day03 : ASolution
    {
        string partOne;
        string partTwo;

        public Day03() : base(03, 2016, "")
        {
            int possibleOne = 0;
            int possibleTwo = 0;

            int row = 0;

            int[] a = new int[3];
            int[] b = new int[3];
            int[] c = new int[3];

            foreach (string line in Input.SplitByNewline())
            {
                string[] num = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                a[row] = int.Parse(num[0]);
                b[row] = int.Parse(num[1]);
                c[row] = int.Parse(num[2]);

                if (a[row] + b[row] > c[row] && a[row] + c[row] > b[row] && b[row] + c[row] > a[row]) possibleOne++;

                row++;

                if (row == 3)
                {
                    if (a[0] + a[1] > a[2] && a[0] + a[2] > a[1] && a[2] + a[1] > a[0]) possibleTwo++;
                    if (b[0] + b[1] > b[2] && b[0] + b[2] > b[1] && b[2] + b[1] > b[0]) possibleTwo++;
                    if (c[0] + c[1] > c[2] && c[0] + c[2] > c[1] && c[2] + c[1] > c[0]) possibleTwo++;
                    row = 0;
                }
                
                
            }

            partOne = possibleOne.ToString();
            partTwo = possibleTwo.ToString();
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
