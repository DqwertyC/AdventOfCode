using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day08 : ASolution
    {
        private static int partOne;
        private static int partTwo;

        private static int acc;

        public Day08() : base(08, 2020, "")
        {
            partOne = 1;
            partTwo = 2;

            acc = 0;
            int index = 0;

            string[] commands = Input.SplitByNewline();
            bool finished = false;

            bool[] run = new bool[commands.Length];

            while (!finished)
            {
                if (run[index])
                {
                    finished = true;
                }
                else
                {
                    run[index] = true;
                    index += RunCommand(commands[index]);
                }
            }

            partOne = acc;

            bool problemSolved = false;

            for (int i = 0; i < commands.Length && !problemSolved; i++)
            {
                acc = 0;
                index = 0;
                finished = false;

                run = new bool[commands.Length];

                while (!finished && index < commands.Length)
                {
                    if (run[index])
                    {
                        finished = true;
                    }
                    else
                    {
                        run[index] = true;

                        if (index == i)
                        {
                            string newCommand = commands[i];
                            if (newCommand.StartsWith("jmp")) newCommand = newCommand.Replace("jmp", "nop");
                            else if (newCommand.StartsWith("nop")) newCommand = newCommand.Replace("nop", "jmp");
                            index += RunCommand(newCommand); 
                        }

                        index += RunCommand(commands[index]);
                    }
                }

                if (index >= commands.Length)
                {
                    partTwo = acc;
                    problemSolved = true;
                }
            }


        }

        private int RunCommand(string s)
        {
            string command = s.Replace("+", "");

            if (command.StartsWith("nop")) return 1;
            if (command.StartsWith("acc"))
            {
                acc += Int32.Parse(command.Split(" ")[1]);
                return 1;
            }

            return Int32.Parse(command.Split(" ")[1]);
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
