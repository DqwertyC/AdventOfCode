using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day15 : ASolution
    {
        string partOne;
        string partTwo;

        public Day15() : base(15, 2016, "")
        {
            List<SlottedDisk> disks = new List<SlottedDisk>();

            foreach (string line in Input.SplitByNewline())
            {
                string[] lineParts = line.Split(" ");

                int positions = int.Parse(lineParts[3]);
                int start = int.Parse(lineParts[11].Replace(".", ""));

                disks.Add(new SlottedDisk(positions, start));
            }

            partOne = FindValidTime(disks).ToString();

            disks.Add(new SlottedDisk(11, 0));

            partTwo = FindValidTime(disks).ToString();

        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public long FindValidTime(List<SlottedDisk> diskList)
        {
            bool found = false;
            long startTime = 0;

            while (!found)
            {
                found = true;
                for (int i = 0; i < diskList.Count && found; i++)
                {
                    if (!diskList[i].ValidAtTime(startTime + i + 1))
                    {
                        found = false;
                    }
                }

                if (!found) startTime++;
            }

            return startTime;
        }

        public class SlottedDisk
        {
            int positions;
            int start;
            public SlottedDisk(int positions, int start)
            {
                this.positions = positions;
                this.start = start;
            }

            public bool ValidAtTime(long time)
            {
                return ((start + time) % positions) == 0;
            }
        }
    }
}
