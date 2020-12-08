using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day01 : ASolution
    {

        private static int currentFloor;
        private static int basementEntry;

        public Day01() : base(01, 2015, "")
        {
            currentFloor = 0;
            int moveCount = 1;

            foreach (char c in Input)
            {
                if ('(' == c) currentFloor++;
                else currentFloor--;

                if (0 == basementEntry && currentFloor < 0) basementEntry = moveCount;
                moveCount++;
            }
        }

        protected override string SolvePartOne()
        {
            return currentFloor.ToString();
        }

        protected override string SolvePartTwo()
        {
            return basementEntry.ToString();
        }
    }
}
