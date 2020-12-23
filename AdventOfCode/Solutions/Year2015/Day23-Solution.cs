using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day23 : ASolution
    {
        string partOne;
        string partTwo;

        public Day23() : base(23, 2015, "")
        {
            List<string> instructions = new List<string>();

            foreach (string line in Input.SplitByNewline())
            {
                instructions.Add(line);
            }

            partOne = RunProgram(instructions, 0).ToString();
            partTwo = RunProgram(instructions, 1).ToString();
        }

        static long RunProgram(List<string> instructions, int startA)
        {
            long a = startA;
            long b = 0;
            int pc = 0;

            while (pc < instructions.Count)
            {
                string[] instructionParts = instructions[pc].Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                int offset = 1;

                if (instructionParts[0].Equals("hlf"))
                {
                    if (instructionParts[1][0] == 'a')
                    {
                        a = a / 2;
                    }
                    else
                    {
                        b = b / 2;
                    }
                }
                else if (instructionParts[0].Equals("tpl"))
                {
                    if (instructionParts[1][0] == 'a')
                    {
                        a = a * 3;
                    }
                    else
                    {
                        b = b * 3;
                    }
                }
                else if (instructionParts[0].Equals("inc"))
                {
                    if (instructionParts[1][0] == 'a')
                    {
                        a = a + 1;
                    }
                    else
                    {
                        b = b + 1;
                    }
                }
                else if (instructionParts[0].Equals("jmp"))
                {
                    offset = int.Parse(instructionParts[1]);
                }
                else if (instructionParts[0].Equals("jie"))
                {
                    if (instructionParts[1][0] == 'a' && a % 2 == 0)
                    {
                        offset = int.Parse(instructionParts[2]);
                    }
                    else if (instructionParts[1][0] == 'b' && b % 2 == 0)
                    {
                        offset = int.Parse(instructionParts[2]);
                    }
                }
                else if (instructionParts[0].Equals("jio"))
                {
                    if (instructionParts[1][0] == 'a' && a == 1)
                    {
                        offset = int.Parse(instructionParts[2]);
                    }
                    else if (instructionParts[1][0] == 'b' && b == 1)
                    {
                        offset = int.Parse(instructionParts[2]);
                    }
                }

                pc += offset;
            }

            return b;
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
