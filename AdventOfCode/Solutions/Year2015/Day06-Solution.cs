using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day06 : ASolution
    {
        private static bool[,] lights = new bool[1000, 1000];
        private static int[,] dimmableLights = new int[1000, 1000];

        private static int brightness = 0;
        private static int onCount = 0;

        public Day06() : base(06, 2015, "")
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    dimmableLights[i, j] = 0;
                }
            }

            foreach (string line in Input.SplitByNewline())
            {
                LightCommand thisCommand;

                if (line.StartsWith("toggle")) thisCommand = LightCommand.TOGGLE;
                else if (line.StartsWith("turn on")) thisCommand = LightCommand.TURN_ON;
                else thisCommand = LightCommand.TURN_OFF;

                string[] numbers = line.Replace("turn on ", "")
                                       .Replace("turn off ", "")
                                       .Replace("toggle ", "")
                                       .Replace(" through ", ",")
                                       .Split(',');

                int startX = Int32.Parse(numbers[0]);
                int startY = Int32.Parse(numbers[1]);
                int stopX = Int32.Parse(numbers[2]);
                int stopY = Int32.Parse(numbers[3]);

                for (int i = startX; i <= stopX; i++)
                {
                    for (int j = startY; j <= stopY; j++)
                    {
                        switch (thisCommand)
                        {
                            case LightCommand.TURN_ON:
                                {
                                    lights[i, j] = true;
                                    dimmableLights[i, j] += 1;
                                }
                            break;
                            case LightCommand.TURN_OFF:
                                {
                                    lights[i, j] = false;
                                    dimmableLights[i, j] -= 1;
                                    if (dimmableLights[i, j] < 0) dimmableLights[i, j] = 0;
                                }
                            break;
                            case LightCommand.TOGGLE:
                                {
                                    lights[i, j] = !lights[i, j];
                                    dimmableLights[i, j] += 2;
                                }
                            break;
                        }
                    }
                }
            }

            brightness = 0;
            onCount = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    brightness += dimmableLights[i, j];

                    if (lights[i, j]) onCount++;
                }
            }
        }

        protected override string SolvePartOne()
        {
            return onCount.ToString();
        }

        protected override string SolvePartTwo()
        {
            return brightness.ToString();
        }

        private enum LightCommand
        {
            TURN_ON,
            TURN_OFF,
            TOGGLE
        };

    }
}
