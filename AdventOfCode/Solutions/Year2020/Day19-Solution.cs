using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day19 : ASolution
    {
        string partOne;
        string partTwo;

        string input = "0: 4 1 5\n1: 2 3 | 3 2\n2: 4 4 | 5 5\n3: 4 5 | 5 4\n4: \"a\"\n5: \"b\"\n\nababbb\nbababa\nabbbab\naaabbb\naaaabbb";
        public Day19() : base(19, 2020, "")
        {
            string[] problemParts = Input.Split("\n\n");

            foreach (string line in problemParts[0].Split("\n"))
            {
                new StringRule(line);
            }

            int countOne = 0;
            int countTwo = 0;

            // Get the sets of rules we care about
            HashSet<string> baseRule = StringRule.GetMatches(0);
            HashSet<string> frontRule = StringRule.GetMatches(42);
            HashSet<string> backRule = StringRule.GetMatches(31);

            // Check each string in the input
            foreach (string line in problemParts[1].Split("\n"))
            {
                if (baseRule.Contains(line)) countOne++;
                if (PartTwoMatches(line, frontRule, backRule, 0)) countTwo++;
            }

            partOne = countOne.ToString();
            partTwo = countTwo.ToString();
        }

        // To match part two, we're made up of a instances of the front set, and b instances of the back set.
        // With b >= 1 && a > b.
        private static bool PartTwoMatches(string line, HashSet<string> frontSet, HashSet<string> backSet, int frontCount)
        {
            foreach (string s in frontSet)
            {
                if (line.StartsWith(s) && frontCount > 0)
                {
                    if (BackMatches(line.Substring(s.Length), backSet, frontCount, 0))
                    {
                        return true;
                    }
                    else
                    {
                        return PartTwoMatches(line.Substring(s.Length), frontSet, backSet, frontCount + 1);
                    }
                }
                else if (line.StartsWith(s) && frontCount == 0)
                {
                    return PartTwoMatches(line.Substring(s.Length), frontSet, backSet, frontCount + 1);
                }
            }

            return false;
        }

        private static bool BackMatches(string input, HashSet<string> backSet, int frontCount, int backCount)
        {
            foreach (string s in backSet)
            {
                if (input.Equals(s) && frontCount > backCount)
                {
                    return true;
                }
                else if (input.StartsWith(s))
                {
                    return BackMatches(input.Substring(s.Length), backSet, frontCount, backCount + 1);
                }
            }

            return false;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }


        class StringRule
        {
            private static Dictionary<int, StringRule> ruleList;

            char ruleLiteral;
            List<int> rulesL;
            List<int> rulesR;

            HashSet<string> matches;

            public StringRule(string input)
            {
                string[] ruleParts = input.Split(new[] { ':', '|' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                int ruleNumber = int.Parse(ruleParts[0]);

                if (ruleParts[1].StartsWith("\""))
                {
                    ruleLiteral = ruleParts[1][1];
                }
                else
                {
                    rulesL = new List<int>();
                    rulesL.AddRange(ruleParts[1].ToIntArray(" "));

                    if (ruleParts.Length == 3)
                    {
                        rulesR = new List<int>();
                        rulesR.AddRange(ruleParts[2].ToIntArray(" "));
                    }
                }

                if (ruleList == null) ruleList = new Dictionary<int, StringRule>();
                ruleList.Add(ruleNumber, this);
            }

            public HashSet<string> GetMatches()
            {
                if (matches == null)
                {
                    matches = new HashSet<string>();

                    if (rulesL == null && rulesR == null)
                    {
                        matches.Add(ruleLiteral.ToString());
                    }
                    else if (rulesL != null && rulesR == null)
                    {
                        matches.UnionWith(GetMatches(rulesL));
                    }
                    else if (rulesL != null && rulesR != null)
                    {
                        matches.UnionWith(GetMatches(rulesL));
                        matches.UnionWith(GetMatches(rulesR));
                    }
                }

                return matches;
            }

            public static HashSet<string> GetMatches(int ruleNumber)
            {
                return ruleList[ruleNumber].GetMatches();
            }

            public static HashSet<string> GetMatches(List<int> rules)
            {
                HashSet<string> multiMatch;

                if (rules.Count == 1)
                {
                    multiMatch = GetMatches(rules[0]);
                }
                else
                {
                    multiMatch = new HashSet<string>();
                    HashSet<string> front = GetMatches(rules[0]);
                    rules.RemoveAt(0);
                    HashSet<string> back = GetMatches(rules);

                    foreach (string f in front)
                    {
                        foreach (string b in back)
                        {
                            multiMatch.Add(f + b);
                        }
                    }
                }

                return multiMatch;
            }
        }
    }   
}
