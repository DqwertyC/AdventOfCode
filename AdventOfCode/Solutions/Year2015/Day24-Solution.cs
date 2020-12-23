using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day24 : ASolution
    {
        string partOne;
        string partTwo;

        static int smallestCountSeen = int.MaxValue;
        static long smallestEntSeen = long.MaxValue;
        static List<int> smallestSeen;

        public Day24() : base(24, 2015, "")
        {
            List<int> packageWeights = new List<int>();
            int totalWeight = 0;

            foreach (string line in Input.SplitByNewline())
            {
                int weight = int.Parse(line);
                totalWeight += weight;
                packageWeights.Add(weight);
            }

            packageWeights.Sort();
            packageWeights.Reverse();

            GetSmallestBag(new List<int>(), packageWeights, totalWeight / 3);

            partOne = GetEnt(smallestSeen).ToString();

            smallestCountSeen = int.MaxValue;
            smallestEntSeen = long.MaxValue;

            GetSmallestBag(new List<int>(), packageWeights, totalWeight / 4);

            partTwo = GetEnt(smallestSeen).ToString();
        }

        private static void GetSmallestBag(List<int> currentBag, List<int> availableWeights, int targetSize)
        {
            List<int> newAvailable = availableWeights.Clone();

            foreach (int weight in availableWeights)
            {
                List<int> newCurrent = currentBag.Clone();
                newAvailable.Remove(weight);
                newCurrent.Add(weight);

                int newWeight = GetTotalWeight(newCurrent);

                if (newWeight == targetSize)
                {
                    if (newCurrent.Count <= smallestCountSeen)
                    {
                        smallestCountSeen = newCurrent.Count;

                        long newEnt = GetEnt(newCurrent);

                        if (newEnt < smallestEntSeen)
                        {
                            smallestEntSeen = newEnt;
                            smallestSeen = newCurrent.Clone();
                        }
                    }
                }
                else if (newWeight < targetSize && newCurrent.Count < smallestCountSeen)
                {
                    GetSmallestBag(newCurrent, newAvailable, targetSize);
                }
            }
        }

        private static int GetTotalWeight(List<int> weights)
        {
            int weight = 0;

            foreach (int i in weights)
            {
                weight += i;
            }

            return weight;
        }

        private static long GetEnt(List<int> weights)
        {
            long ent = 1;

            foreach (int i in weights)
            {
                ent *= i;
            }

            return ent;
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
