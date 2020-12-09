using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day14 : ASolution
    {
        int partOne;
        int partTwo;

        public Day14() : base(14, 2015, "")
        {
            var speedyBois = new List<SpeedyBoi>();

            foreach (string line in Input.SplitByNewline())
            {
                speedyBois.Add(new SpeedyBoi(line));
            }

            int[] points = new int[speedyBois.Count];

            for (int i = 1; i <= 2503; i++)
            {
                int furthestDistance = 0;

                for (int j = 0; j < speedyBois.Count; j++)
                {
                    SpeedyBoi b = speedyBois[j];

                    int distance = b.DistanceMoved(i);
                    if (distance >= furthestDistance)
                    {
                        furthestDistance = distance;
                    }
                }

                for (int j = 0; j < speedyBois.Count; j++)
                {
                    if (speedyBois[j].DistanceMoved(i) == furthestDistance)
                    {
                        points[j]++;
                    }
                }

                partOne = furthestDistance;
            }

            int maxPoints = 0;

            foreach (int p in points)
            {
                if (p > maxPoints) maxPoints = p;
            }

            partTwo = maxPoints;
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
        }

        private class SpeedyBoi
        {
            int speed;
            int flyTime;
            int restTime;

            public SpeedyBoi(string descriptor)
            {
                string[] parts = descriptor.Split(' ');

                speed = Int32.Parse(parts[3]);
                flyTime = Int32.Parse(parts[6]);
                restTime = Int32.Parse(parts[13]);
            }

            public int DistanceMoved(int time)
            {
                int cycleTime = flyTime + restTime;
                int cycleDist = flyTime * speed;

                int cyclesCompleted = time / cycleTime;
                int secondsLeft = time % cycleTime;

                int distance = cycleDist * cyclesCompleted;

                if (secondsLeft <= flyTime) distance += secondsLeft * speed;
                else distance += flyTime * speed;

                return distance;
            }
        }
    }
}
