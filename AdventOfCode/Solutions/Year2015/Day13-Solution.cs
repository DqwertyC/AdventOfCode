using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day13 : ASolution
    {
        private static Dictionary<string, int> nums;
        private static int[,] happinessMods;
        private static int personCount;

        int partOne;
        int partTwo;

        public Day13() : base(13, 2015, "")
        {
            personCount = 0;
            nums = new Dictionary<string, int>();
            happinessMods = new int[9, 9];

            foreach (string line in Input.SplitByNewline())
            {
                string[] lineParts = line.Split(' ');

                string personA = lineParts[0];
                string personB = lineParts[10].Replace(".", "");

                if (!nums.ContainsKey(personA)) nums[personA] = personCount++;
                if (!nums.ContainsKey(personB)) nums[personB] = personCount++;

                int modifier = Int32.Parse(lineParts[3]);
                if (lineParts[2].StartsWith("lose")) modifier *= -1;

                happinessMods[nums[personA], nums[personB]] += modifier;
                happinessMods[nums[personB], nums[personA]] += modifier;
            }

            int maxHappinessLooped = 0;
            int maxHappinessUnlooped = 0;
            var possiblePaths = CreatePermutations(personCount - 1);

            foreach (List<int> path in possiblePaths)
            {
                int happiness = 0;

                for (int i = 0; i < path.Count - 1; i++)
                {
                    happiness += happinessMods[path[i], path[i + 1]];
                }

                if (happiness > maxHappinessUnlooped) maxHappinessUnlooped = happiness;

                // Finish off the circle
                happiness += happinessMods[path[0], path[path.Count - 1]];

                if (happiness > maxHappinessLooped) maxHappinessLooped = happiness;
            }

            partOne = maxHappinessLooped;
            partTwo = maxHappinessUnlooped;
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
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
