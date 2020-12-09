using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day16 : ASolution
    {
        int partOne;
        int partTwo;

        public Day16() : base(16, 2015, "")
        {
            List<Sue> aunts = new List<Sue>();

            int id = 1;
            foreach (string line in Input.SplitByNewline())
            {
                Sue newSue = new Sue(line, id);
                if (newSue.FactsMatchOne(3, 7, 2, 3, 0, 0, 5, 3, 2, 1)) partOne = id;
                if (newSue.FactsMatchTwo(3, 7, 2, 3, 0, 0, 5, 3, 2, 1)) partTwo = id;

                aunts.Add(new Sue(line, id));
                id++;
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

        private class Sue
        {
            int id;

            int chi = 0;
            int cat = 0;
            int sam = 0;
            int pom = 0;
            int aki = 0;
            int viz = 0;
            int gol = 0;
            int tre = 0;
            int car = 0;
            int per = 0;

            bool chiKnown = false;
            bool catKnown = false;
            bool samKnown = false;
            bool pomKnown = false;
            bool akiKnown = false;
            bool vizKnown = false;
            bool golKnown = false;
            bool treKnown = false;
            bool carKnown = false;
            bool perKnown = false;


            public Sue(string descriptor, int id)
            {
                this.id = id;

                string[] facts = descriptor.Substring(6 + id.ToString().Length).Split(',',StringSplitOptions.TrimEntries);

                foreach (string fact in facts)
                {
                    string[] parts = fact.Split(": ");

                    if ("children".Equals(parts[0]))
                    {
                        chiKnown = true;
                        chi = Int32.Parse(parts[1]);
                    }
                    else if ("cats".Equals(parts[0]))
                    {
                        catKnown = true;
                        cat = Int32.Parse(parts[1]);
                    }
                    else if ("samoyeds".Equals(parts[0]))
                    {
                        samKnown = true;
                        sam = Int32.Parse(parts[1]);
                    }
                    else if ("pomeranians".Equals(parts[0]))
                    {
                        pomKnown = true;
                        pom = Int32.Parse(parts[1]);
                    }
                    else if ("akitas".Equals(parts[0]))
                    {
                        akiKnown = true;
                        aki = Int32.Parse(parts[1]);
                    }
                    else if ("vizslas".Equals(parts[0]))
                    {
                        vizKnown = true;
                        viz = Int32.Parse(parts[1]);
                    }
                    else if ("goldfish".Equals(parts[0]))
                    {
                        golKnown = true;
                        gol = Int32.Parse(parts[1]);
                    }
                    else if ("trees".Equals(parts[0]))
                    {
                        treKnown = true;
                        tre = Int32.Parse(parts[1]);
                    }
                    else if ("cars".Equals(parts[0]))
                    {
                        carKnown = true;
                        car = Int32.Parse(parts[1]);
                    }
                    else if ("perfumes".Equals(parts[0]))
                    {
                        perKnown = true;
                        per = Int32.Parse(parts[1]);
                    }
                }
            }

            public bool FactsMatchOne(int chi, int cat, int sam, int pom, int aki, int viz, int gol, int tre, int car, int per)
            {
                bool match = true;

                if (chiKnown && this.chi != chi) match = false;
                if (catKnown && this.cat != cat) match = false;
                if (samKnown && this.sam != sam) match = false;
                if (pomKnown && this.pom != pom) match = false;
                if (akiKnown && this.aki != aki) match = false;
                if (vizKnown && this.viz != viz) match = false;
                if (golKnown && this.gol != gol) match = false;
                if (treKnown && this.tre != tre) match = false;
                if (carKnown && this.car != car) match = false;
                if (perKnown && this.per != per) match = false;

                return match;
            }

            public bool FactsMatchTwo(int chi, int cat, int sam, int pom, int aki, int viz, int gol, int tre, int car, int per)
            {
                bool match = true;

                if (chiKnown && this.chi != chi) match = false;
                if (catKnown && this.cat <= cat) match = false;
                if (samKnown && this.sam != sam) match = false;
                if (pomKnown && this.pom >= pom) match = false;
                if (akiKnown && this.aki != aki) match = false;
                if (vizKnown && this.viz != viz) match = false;
                if (golKnown && this.gol >= gol) match = false;
                if (treKnown && this.tre <= tre) match = false;
                if (carKnown && this.car != car) match = false;
                if (perKnown && this.per != per) match = false;

                return match;
            }
        }
    }
}
