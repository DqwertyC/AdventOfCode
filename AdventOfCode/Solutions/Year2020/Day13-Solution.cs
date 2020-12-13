using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day13 : ASolution
    {
        long partOne;
        long partTwo;

        public Day13() : base(13, 2020, "")
        {
            string[] lines = Input.SplitByNewline();

            int startTime = int.Parse(lines[0]);
            string[] busIDs = lines[1].Split(",");
            List<(int busID, int offset)> busses = new List<(int val, int offset)>();

            // Read in our values;
            int index = 0;
            for (int i = 0; i < busIDs.Length; i++)
            {
                if (busIDs[i][0] != 'x')
                {
                    busses.Add((int.Parse(busIDs[i]), index));
                }

                index++;   
            }

            // Loop through for Part One
            long searchTime = startTime;
            bool found = false;

            while (!found)
            {
                foreach (var (busID, offset) in busses)
                {
                    if (0 == searchTime % busID)
                    {
                        found = true;
                        partOne = (searchTime - startTime) * busID;
                    }
                }

                searchTime++;
            }

            int currentTargetMatches = 0;
            long currentSearchDif = 1;
            bool firstMatchFound = false;
            long firstMatchIndex = 0;

            found = false;
            while (!found)
            {
                // Check each bus to see if it works with the current searchTime
                for (int i = 0; i <= currentTargetMatches; i++)
                {
                    // Check to see if this bus works with this start time
                    if ((searchTime + busses[i].offset) % busses[i].busID == 0)
                    {
                        // If it's the first time we've gotten this many busses
                        // to work, store the current start time.
                        if (i == currentTargetMatches && !firstMatchFound)
                        {
                            firstMatchFound = true;
                            firstMatchIndex = searchTime;

                            if (currentTargetMatches == busses.Count - 1)
                            {
                                found = true;
                                partTwo = searchTime;
                            }
                        }
                        // If it's the *second* time we've gotten this many busses
                        // to work, update the delta to be the difference between
                        // the current time and the first time that worked
                        else if (i == currentTargetMatches && firstMatchFound)
                        {
                            currentTargetMatches++;
                            currentSearchDif = searchTime - firstMatchIndex;
                            firstMatchFound = false;
                        }
                    }
                }

                // Update the search time
                searchTime += currentSearchDif;
            }
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
        }
    }
}
