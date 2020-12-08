using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day05 : ASolution
    {
        private static int niceCountOne;
        private static int niceCountTwo;

        public Day05() : base(05, 2015, "")
        {
            niceCountOne = 0;
            niceCountTwo = 0;

            foreach (string line in Input.SplitByNewline())
            {
                if (IsNiceOne(line)) niceCountOne++;
                if (IsNiceTwo(line)) niceCountTwo++;
            }
        }

        protected override string SolvePartOne()
        {
            return niceCountOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return niceCountTwo.ToString();
        }

        private static bool IsNiceOne(string s)
        {
            int vowelCount = 0;
            int doubleCount = 0;

            if (isVowel(s[0])) vowelCount++;

            if (s.Contains("ab") || s.Contains("cd") || s.Contains("pq") || s.Contains("xy")) return false;

            for (int i = 1; i < s.Length; i++)
            {
                if (isVowel(s[i])) vowelCount++;
                if (s[i - 1] == s[i]) doubleCount++;
            }

            return (vowelCount >= 3 && doubleCount >= 1);
        }

        private static bool IsNiceTwo(string s)
        {
            bool containsDuplicate = false;
            bool spacedDouble = false;

            for (int i = 0; i < s.Length - 2; i++)
            {
                string pair = s.Substring(i, 2);
                for (int j = i + 2; j < s.Length - 1; j++)
                {
                    if (s.Substring(j, 2).Equals(pair)) containsDuplicate = true;
                }
            }

            for (int i = 2; i < s.Length; i++)
            {
                if (s[i - 2] == s[i]) spacedDouble = true;
            }

            return containsDuplicate && spacedDouble;
        }

        private static bool isVowel(char c)
        {
            return ('a' == c || 'e' == c || 'i' == c || 'o' == c || 'u' == c);
        }
    }
}
