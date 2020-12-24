using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day07 : ASolution
    {
        string partOne;
        string partTwo;

        public Day07() : base(07, 2016, "")
        {
            int supportsABBA = 0;
            int supportsABA = 0;

            foreach (string line in Input.SplitByNewline())
            {
                bool hasValidABBA = false;
                bool hasInvalidABBA = false;
                bool inHypernet = false;

                HashSet<(char a, char b)> ABAs = new HashSet<(char,char)>();
                HashSet<(char b, char a)> BABs = new HashSet<(char,char)>();

                foreach (string part in line.Split(new char[] {'[',']' }))
                {
                    bool hasABBA = ContainsABBA(part);

                    if (inHypernet)
                    {
                        BABs.UnionWith(GetABAs(part));
                        if (hasABBA) hasInvalidABBA = true;
                    }
                    else
                    {
                        ABAs.UnionWith(GetABAs(part));
                        if (hasABBA) hasValidABBA = true;
                    }

                    inHypernet = !inHypernet;
                }

                if (hasValidABBA && !hasInvalidABBA) 
                    supportsABBA++;

                bool validABA = false;

                foreach (var aba in ABAs)
                {
                    foreach (var bab in BABs)
                    {
                        if (aba.a == bab.a && aba.b == bab.b)
                        {
                            validABA = true;
                        }    
                    }
                }

                if (validABA) supportsABA++;
            }

            partOne = supportsABBA.ToString();
            partTwo = supportsABA.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static bool ContainsABBA(string input)
        {
            for (int i = 0; i <= input.Length - 4; i++)
            {
                char a = input[i];
                char b = input[i + 1];

                if (a != b && a == input[i + 3] && b == input[i + 2]) return true;
            }

            return false;
        }

        private static HashSet<(char a, char b)> GetABAs(string input)
        {
            HashSet<(char a, char b)> abas = new HashSet<(char a, char b)>();

            for (int i = 0; i <= input.Length - 3; i++)
            {
                char a = input[i];
                char b = input[i + 1];

                if (a != b && a == input[i + 2]) abas.Add((a, b));
            }

            return abas;
        }
    }
}
