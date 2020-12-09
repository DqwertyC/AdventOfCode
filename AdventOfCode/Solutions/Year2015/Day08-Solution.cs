using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day08 : ASolution
    {
        int reducedCount = 0;
        int expandedCount = 0;

        public Day08() : base(08, 2015, "")
        {
            int charCount = 0;

            foreach (string line in Input.SplitByNewline())
            {
                CountCharsInLine(line, out int lineChars, out int lineReduced, out int lineExpanded);
                charCount += lineChars;
                reducedCount += lineReduced;
                expandedCount += lineExpanded;
            }
        }

        protected override string SolvePartOne()
        {
            return reducedCount.ToString();
        }

        protected override string SolvePartTwo()
        {
            return expandedCount.ToString();
        }

        private static void CountCharsInLine(string s, out int chars, out int reduced, out int expanded)
        {
            chars = s.Length;

            string shrunk = s.Replace("\\\"", "\"").Replace("\\\\", "\\")
                .Replace("\\x0", "")
                .Replace("\\x1", "")
                .Replace("\\x2", "")
                .Replace("\\x3", "")
                .Replace("\\x4", "")
                .Replace("\\x5", "")
                .Replace("\\x6", "")
                .Replace("\\x7", "")
                .Replace("\\x8", "")
                .Replace("\\x9", "")
                .Replace("\\xa", "")
                .Replace("\\xb", "")
                .Replace("\\xc", "")
                .Replace("\\xd", "")
                .Replace("\\xe", "")
                .Replace("\\xf", "");

            string grown = s.Replace("\\", "\\\\").Replace("\"", "\\\"");

            reduced = shrunk.Length - 2;
            expanded = grown.Length + 2;
        }
    }
}
