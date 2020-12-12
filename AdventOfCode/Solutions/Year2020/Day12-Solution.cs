using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day12 : ASolution
    {
        long partOne;
        long partTwo;

        static int facing = 0;
        static int x;
        static int y;

        static int wayX = 10;
        static int wayY = 1;
        static int shipX;
        static int shipY;

        public Day12() : base(12, 2020, "")
        {
            string[] lines = Input.SplitByNewline();

            foreach (string s in lines)
            {
                UpdateState(s);
            }

            partOne = Math.Abs(x) + Math.Abs(y);
            partTwo = Math.Abs(shipX) + Math.Abs(shipY);
            
        }

        protected override string SolvePartOne()
        {
            return partOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return partTwo.ToString();
        }

        void UpdateState(string command)
        {
            if (command.StartsWith('E'))
            {
                int distance = int.Parse(command.Replace("E", ""));
                x += distance;
                wayX += distance;
            }
            else if (command.StartsWith('W'))
            {
                int distance = int.Parse(command.Replace("W", ""));
                x -= distance;
                wayX -= distance;
            }
            else if (command.StartsWith('N'))
            {
                int distance = int.Parse(command.Replace("N", ""));
                y += distance;
                wayY += distance;
            }
            else if (command.StartsWith('S'))
            {
                int distance = int.Parse(command.Replace("S", ""));
                y -= distance;
                wayY -= distance;
            }
            else if (command.StartsWith('L'))
            {
                int distance = int.Parse(command.Replace("L", ""));
                facing = (facing + distance) % 360;

                if (distance == 90)
                {
                    int helper = wayY;
                    wayY = wayX;
                    wayX = -helper;
                }
                else if (distance == 180)
                {
                    wayX = -wayX;
                    wayY = -wayY;
                }
                else if (distance == 270)
                {
                    int helper = wayY;
                    wayY = -wayX;
                    wayX = helper;
                }
            }
            else if (command.StartsWith('R'))
            {
                int distance = int.Parse(command.Replace("R", ""));
                facing = (facing - distance + 360) % 360;

                if (distance == 90)
                {
                    int helper = wayY;
                    wayY = -wayX;
                    wayX = helper;
                }
                else if (distance == 180)
                {
                    wayX = -wayX;
                    wayY = -wayY;
                }
                else if (distance == 270)
                {
                    int helper = wayY;
                    wayY = wayX;
                    wayX = -helper;
                }
            }
            else if (command.StartsWith('F'))
            {
                int distance = int.Parse(command.Replace("F", ""));

                if (facing == 0) x += distance;
                else if (facing == 90) y += distance;
                else if (facing == 180) x -= distance;
                else if (facing == 270) y -= distance;

                shipX += distance * wayX;
                shipY += distance * wayY;
            }
        }
    }
}
