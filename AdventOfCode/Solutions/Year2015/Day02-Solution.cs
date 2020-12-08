using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day02 : ASolution
    {

        private static int totalArea;
        private static int totalLength;

        public Day02() : base(02, 2015, "")
        {
            totalArea = 0;
            totalLength = 0;

            foreach (string line in Input.SplitByNewline())
            {
                string[] dims = line.Split('x');
                int l = Int32.Parse(dims[0]);
                int w = Int32.Parse(dims[1]);
                int h = Int32.Parse(dims[2]);

                totalArea += GetArea(l,w,h);
                totalLength += GetLength(l,w,h);
            }
        }

        protected override string SolvePartOne()
        {
            return totalArea.ToString();
        }

        protected override string SolvePartTwo()
        {
            return totalLength.ToString();
        }

        private static int GetArea(int l, int w, int h)
        {
            int sideA = l * w;
            int sideB = l * h;
            int sideC = w * h;

            int smallest = Math.Min(sideA, Math.Min(sideB, sideC));

            return 2 * sideA + 2 * sideB + 2 * sideC + smallest;
        }

        private static int GetLength(int l, int w, int h)
        {
            int perimA = 2 * l + 2 * w;
            int perimB = 2 * l + 2 * h;
            int perimC = 2 * w + 2 * h;

            int smallest = Math.Min(perimA, Math.Min(perimB, perimC));

            return smallest + (l * w * h);
        }
    }
}
