using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day03 : ASolution
    {

        private static int countOne;
        private static int countTwo;

        public Day03() : base(03, 2015, "")
        {
            HouseVisitor santa = new HouseVisitor();
            HouseVisitor halfSanta = new HouseVisitor();
            HouseVisitor roboSanta = new HouseVisitor();

            bool useRobot = false;

            foreach (char c in Input)
            {
                santa.Move(c);

                if (useRobot) roboSanta.Move(c);
                else halfSanta.Move(c);

                useRobot = !useRobot;
            }

            countOne = santa.GetHouseCount();
            countTwo = halfSanta.GetCombinedHouseCount(roboSanta);
        }

        protected override string SolvePartOne()
        {
            return countOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return countTwo.ToString();
        }

        private class HouseVisitor
        {
            private (int x, int y) coords;
            private Dictionary<(int x, int y), bool> visitedHouses;

            public HouseVisitor()
            {
                coords = (0, 0);
                visitedHouses = new Dictionary<(int x, int y), bool>();
                visitedHouses[(0, 0)] = true;
            }

            public void Move(char dir)
            {
                if ('<' == dir) coords.x--;
                if ('>' == dir) coords.x++;
                if ('^' == dir) coords.y++;
                if ('v' == dir) coords.y--;

                visitedHouses[coords] = true;
            }

            public int GetHouseCount()
            {
                return visitedHouses.Count;
            }

            public int GetCombinedHouseCount(HouseVisitor other)
            {
                Dictionary<(int x, int y), bool> combined = new Dictionary<(int x, int y), bool>();

                foreach (var entry in visitedHouses)
                {
                    if (entry.Value) combined[entry.Key] = true;
                }

                foreach (var entry in other.visitedHouses)
                {
                    if (entry.Value) combined[entry.Key] = true;
                }

                return combined.Count;
            }
        }
    }
}
