using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day09 : ASolution
    {

        private static Dictionary<string, int> nums;
        private static int[,] distances;
        private static int cityCount;

        int minDistance = Int32.MaxValue;
        int maxDistance = 0;

        public Day09() : base(09, 2015, "")
        {
            cityCount = 0;
            nums = new Dictionary<string, int>();
            distances = new int[8, 8];

            foreach (string line in Input.SplitByNewline())
            {
                string[] lineParts = line.Split(' ');

                if (!nums.ContainsKey(lineParts[0])) nums[lineParts[0]] = cityCount++;
                if (!nums.ContainsKey(lineParts[2])) nums[lineParts[2]] = cityCount++;

                distances[nums[lineParts[0]], nums[lineParts[2]]] = Int32.Parse(lineParts[4]);
                distances[nums[lineParts[2]], nums[lineParts[0]]] = Int32.Parse(lineParts[4]);
            }

            var possiblePaths = CreatePermutations(cityCount - 1);

            foreach (List<int> path in possiblePaths)
            {
                int distance = 0;

                for (int i = 0; i < path.Count - 1; i++)
                {
                    distance += distances[path[i], path[i + 1]];
                }

                if (distance < minDistance) minDistance = distance;
                if (distance > maxDistance) maxDistance = distance;
            }
        }

        protected override string SolvePartOne()
        {
            return minDistance.ToString();
        }

        protected override string SolvePartTwo()
        {
            return maxDistance.ToString();
        }

        private static List<List<int>> CreatePermutations(int maxValue)
        {
            if (0 == maxValue)
            {
                List<List<int>> outerList = new List<List<int>>();
                List<int> innerList = new List<int>();
                innerList.Add(0);
                outerList.Add(innerList);
                return outerList;
            }
            else
            {
                List<List<int>> newOuter = new List<List<int>>();
                List<List<int>> oldOuter = CreatePermutations(maxValue - 1);

                foreach (List<int> l in oldOuter)
                {
                    for (int i = 0; i <= l.Count; i++)
                    {
                        List<int> newInner = Clone(l);
                        newInner.Insert(i, maxValue);
                        newOuter.Add(newInner);
                    }
                }

                return newOuter;
            }
        }

        public static List<int> Clone(List<int> listToClone)
        {
            List<int> cloned = new List<int>();
            foreach (int i in listToClone) cloned.Add(i);
            return cloned;
        }
    }
}
