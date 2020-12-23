using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2016
{

    class Day02 : ASolution
    {
        string partOne;
        string partTwo;

        public Day02() : base(02, 2016, "")
        {
            Dictionary<(int,int), char> keypadOne = new Dictionary<(int,int), char>();
            keypadOne.Add((-1, +1), '1');
            keypadOne.Add((+0, +1), '2');
            keypadOne.Add((+1, +1), '3');
            keypadOne.Add((-1, +0), '4');
            keypadOne.Add((+0, +0), '5');
            keypadOne.Add((+1, +0), '6');
            keypadOne.Add((-1, -1), '7');
            keypadOne.Add((+0, -1), '8');
            keypadOne.Add((+1, -1), '9');

            Dictionary<(int, int), char> keypadTwo = new Dictionary<(int, int), char>();
            keypadTwo.Add((+0, +2), '1');
            keypadTwo.Add((-1, +1), '2');
            keypadTwo.Add((+0, +1), '3');
            keypadTwo.Add((+1, +1), '4');
            keypadTwo.Add((-2, +0), '5');
            keypadTwo.Add((-1, +0), '6');
            keypadTwo.Add((+0, +0), '7');
            keypadTwo.Add((+1, +0), '8');
            keypadTwo.Add((+2, +0), '9');
            keypadTwo.Add((-1, -1), 'A');
            keypadTwo.Add((+0, -1), 'B');
            keypadTwo.Add((+1, -1), 'C');
            keypadTwo.Add((+0, -2), 'D');

            Dictionary<char, Coordinate2D> directions = new Dictionary<char, Coordinate2D>();
            directions.Add('U', (0, 1));
            directions.Add('D', (0, -1));
            directions.Add('L', (-1, 0));
            directions.Add('R', (1, 0));

            Coordinate2D locationOne = (0, 0);
            Coordinate2D locationTwo = (0, 0);

            partOne = string.Empty;
            partTwo = string.Empty;

            foreach (string line in Input.SplitByNewline())
            {
                foreach (char c in line)
                {
                    if (keypadOne.ContainsKey(locationOne + directions[c]))
                        locationOne += directions[c];

                    if (keypadTwo.ContainsKey(locationTwo + directions[c]))
                        locationTwo += directions[c];
                }

                partOne += keypadOne[locationOne];
                partTwo += keypadTwo[locationTwo];
            } 
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
