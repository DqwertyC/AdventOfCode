using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day07 : ASolution
    {
        private static int linksToGold;

        public Day07() : base(07, 2020, "")
        {
            foreach (string line in Input.SplitByNewline())
            {
                new Bag(line);
            }
        }

        protected override string SolvePartOne()
        {
            linksToGold = 0;

            // Go through all the bags and see which ones end up at shiny gold
            foreach (var s in Bag.allBags)
            {
                if (s.Value.HasShiny()) linksToGold++;
            }

            return linksToGold.ToString();
        }

        protected override string SolvePartTwo()
        {
            return Bag.allBags["shiny gold"].CountChildren().ToString();
        }


        private class Bag
        {
            //This is a list of all the bags in the input
            public static Dictionary<string, Bag> allBags;

            //Name of ourself, list of bags
            string name;
            Dictionary<string, int> holds;

            public Bag(string input)
            { 

                // Set up our contents
                holds = new Dictionary<string, int>();
                string[] partsA = input.Replace("bags", "bag").Split("bag contain", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                name = partsA[0];

                // Go through each bag this one contains
                string[] contents = partsA[1].Split(",", StringSplitOptions.TrimEntries);

                foreach (string s in contents)
                {
                    if (!s.StartsWith("no other"))
                    {
                        string[] partsB = s.Split(" ");

                        int count = Int32.Parse(partsB[0]);
                        string heldName = partsB[1] + " " + partsB[2];

                        holds[heldName] = count;
                    }
                }

                // Create the dictionary of bags if it doesn't exist
                if (allBags == null) allBags = new Dictionary<string, Bag>();

                // Add ourselves to the dictionary of bags
                allBags.Add(this.name, this);
            }

            // See if this bag or its children link to "shiny gold"
            public bool HasShiny()
            {
                foreach (string s in holds.Keys)
                {
                    if (s.Contains("shiny gold"))
                        return true;
                    else if (allBags[s].HasShiny())
                        return true;
                }

                return false;
            }

            // Count the children of this bag
            public int CountChildren()
            {
                int value = 0;
                if (holds.Count == 0) return 0;

                foreach (string s in holds.Keys)
                {
                    value += holds[s] + (holds[s] * allBags[s].CountChildren());
                }

                return value;
            }
        }
    }
}
