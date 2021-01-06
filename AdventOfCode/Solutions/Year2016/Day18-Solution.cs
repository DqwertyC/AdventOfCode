using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day18 : ASolution
    {
        string partOne;
        string partTwo;

        public Day18() : base(18, 2016, "")
        {
            List<bool> isTrapped = new List<bool>();

            long safeCount = 0;
            int rowSafeCount = 0;

            isTrapped.Add(false);
            foreach (char c in Input)
            {
                if (c == '.')
                {
                    isTrapped.Add(false);
                    safeCount++;
                }
                else
                {
                    isTrapped.Add(true);
                }
            }
            isTrapped.Add(false);

            for (int i = 0; i < 400000 - 1; i++)
            {
                if (i == 39) partOne = safeCount.ToString();

                isTrapped = GetNextRow(isTrapped, out rowSafeCount);
                safeCount += rowSafeCount;
            }

            partTwo = safeCount.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static List<bool> GetNextRow(List<bool> oldRow, out int safeCount)
        {
            safeCount = 0;
            List<bool> newRow = new List<bool>();

            newRow.Add(false);
            for (int i = 1; i < oldRow.Count - 1; i++)
            {
                if (oldRow[i - 1] && oldRow[i] && !oldRow[i + 1])
                    newRow.Add(true);
                else if (!oldRow[i - 1] && oldRow[i] && oldRow[i + 1])
                    newRow.Add(true);
                else if (oldRow[i - 1] && !oldRow[i] && !oldRow[i + 1])
                    newRow.Add(true);
                else if (!oldRow[i - 1] && !oldRow[i] && oldRow[i + 1])
                    newRow.Add(true);
                else
                {
                    newRow.Add(false);
                    safeCount++;
                }
                    
            }
            newRow.Add(false);

            return newRow;
        }
    }
}
