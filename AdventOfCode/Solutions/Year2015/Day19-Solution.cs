using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day19 : ASolution
    {
        string partOne;
        string partTwo;

        public Day19() : base(19, 2015, "")
        {
            string[] problemParts = Input.Split("\n\n");
            List<(string find, string replace)> replacementOptions = new List<(string, string)>();

            // Sort replacement options by longest output
            replacementOptions.Sort((s1, s2) => s1.replace.Length.CompareTo(s2.replace.Length));
            replacementOptions.Reverse();

            foreach (string operation in problemParts[0].Split("\n"))
            {
                string[] parts = operation.Split(" => ");
                replacementOptions.Add((parts[0], parts[1]));
            }

            string original = problemParts[1];

            HashSet<string> possibilities = new HashSet<string>();

            foreach (var replacement in replacementOptions)
            {
                for (int i = 0; i < original.Length; i++)
                {
                    if (original.Substring(i).StartsWith(replacement.find))
                    {
                        possibilities.Add(original.Substring(0, i) + replacement.replace + original.Substring(i + replacement.find.Length));
                    }
                }
            }

            partOne = possibilities.Count.ToString();
            partTwo = (original.Count(x => char.IsUpper(x)) - original.AllIndexesOf("Rn").Count() - original.AllIndexesOf("Ar").Count() - (2 * original.AllIndexesOf("Y").Count()) - 1).ToString();
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
