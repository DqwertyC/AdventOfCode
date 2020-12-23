using AdventOfCode.Solutions;
using System;

namespace AdventOfCode
{

    class Program
    {

        public static Config Config = Config.Get("config.json");
        static SolutionCollector Solutions = new SolutionCollector(Config.Year, Config.Days);

        static void Main(string[] args)
        {
            foreach(ASolution solution in Solutions)
            {
                solution.Solve();
            }

            Console.WriteLine($"Solved all Problems in {Solutions.totalSolveTime / 1000}.{Solutions.totalSolveTime % 1000}ms");


        }
    }
}
