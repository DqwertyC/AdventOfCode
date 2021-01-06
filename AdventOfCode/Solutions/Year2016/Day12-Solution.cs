using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day12 : ASolution
    {
        string partOne;
        string partTwo;

        public Day12() : base(12, 2016, "")
        {
            var firstRun = new AssemBunny(Input, 0, 0, 0, 0);
            var secondRun = new AssemBunny(Input, 0, 0, 1, 0);

            partOne = firstRun.register['a'].ToString();
            partTwo = secondRun.register['a'].ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private class AssemBunny
        {
            public Dictionary<char, int> register = new Dictionary<char, int>();

            public AssemBunny(string input, int a, int b, int c, int d)
            {
                register['a'] = a;
                register['b'] = b;
                register['c'] = c;
                register['d'] = d;

                int pc = 0;

                string[] lines = input.SplitByNewline();


                while (pc < lines.Length)
                {
                    string[] commandPart = lines[pc].Split(' ');

                    if (commandPart[0].Equals("cpy"))
                    {
                        register[commandPart[2][0]] = GetRegisterOrLiteral(commandPart[1]);
                        pc += 1;
                    }
                    else if (commandPart[0].Equals("inc"))
                    {
                        register[commandPart[1][0]] = register[commandPart[1][0]] + 1;
                        pc += 1;
                    }
                    else if (commandPart[0].Equals("dec"))
                    {
                        register[commandPart[1][0]] = register[commandPart[1][0]] - 1;
                        pc += 1;
                    }
                    else if (commandPart[0].Equals("jnz"))
                    {
                        if (0 != GetRegisterOrLiteral(commandPart[1]))
                        {
                            pc += int.Parse(commandPart[2]);
                        }
                        else
                        {
                            pc += 1;
                        }
                    }
                }
            }

            private int GetRegisterOrLiteral(string value)
            {
                if (register.ContainsKey(value[0])) return register[value[0]];
                else return int.Parse(value);
            }
        }
    }
}
