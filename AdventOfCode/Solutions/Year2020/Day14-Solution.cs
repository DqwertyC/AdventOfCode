using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Day14 : ASolution
    {
        private long partOne;
        private long partTwo;

        Dictionary<long, long> memoryBanksOne;
        Dictionary<long, long> memoryBanksTwo;

        static long alwaysOnMask;
        static long alwaysOffMask;
        static List<(long on, long off)> floatingMasks;

        static char[] splitChars = new char[] { '=', '[', ']' };
        static StringSplitOptions splitOpts = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

        public Day14() : base(14, 2020, "")
        {
            memoryBanksOne = new Dictionary<long, long>();
            memoryBanksTwo = new Dictionary<long, long>();

            foreach (string line in Input.SplitByNewline())
            {
                string[] usefulLineParts = line.Split(splitChars, splitOpts);

                if (usefulLineParts[0].StartsWith("mask"))
                {
                    UpdateMask(usefulLineParts[1]);
                }
                else
                {
                    long mem = long.Parse(usefulLineParts[1]);
                    long val = long.Parse(usefulLineParts[2]);
                    memoryBanksOne[mem] = MaskLong(val);

                    foreach (long possibleMem in GetPossibleValues(mem))
                    {
                        memoryBanksTwo[possibleMem] = val;
                    }
                }
            }

            foreach (long val in memoryBanksOne.Values)
            {
                partOne += val;
            }

            foreach (long val in memoryBanksTwo.Values)
            {
                partTwo += val;
            }
        }

        private static void UpdateMask(string s)
        {
            alwaysOnMask = 0;
            alwaysOffMask = 0;
            floatingMasks = new List<(long,long)>();

            floatingMasks.Add((0, 0));

            foreach (char c in s)
            {
                if (c == '1') alwaysOnMask++;
                else if (c == '0') alwaysOffMask++;
                else if (c == 'X') IncrementFloating();

                alwaysOnMask = alwaysOnMask << 1;
                alwaysOffMask = alwaysOffMask << 1;
                ShiftFloatingLeft();
            }

            alwaysOnMask = alwaysOnMask >> 1;
            alwaysOffMask = alwaysOffMask >> 1;
            ShiftFloatingRight();
        }

        private static List<long> GetPossibleValues(long input)
        {
            List<long> output = new List<long>();
            long outputBase = input;
            outputBase = outputBase | alwaysOnMask;

            foreach (var mask in floatingMasks)
            {
                long possibleOutput = outputBase;
                possibleOutput = possibleOutput | mask.on;
                possibleOutput = possibleOutput & (~mask.off);
                output.Add(possibleOutput);
            }

            return output;
        }

        private static long MaskLong(long input)
        {
            long output = input;
            output = output | alwaysOnMask;
            output = output & (~alwaysOffMask);
            return output;
        }

        private static void IncrementFloating()
        {
            List<(long,long)> newFloating = new List<(long,long)>();

            foreach(var mask in floatingMasks)
            {
                (long on, long off) newOn = mask;
                (long on, long off) newOff = mask;

                newOn.on++;
                newOff.off++;

                newFloating.Add(newOn);
                newFloating.Add(newOff);
            }

            floatingMasks = newFloating;

        }

        private static void ShiftFloatingLeft()
        {
            for (int i = 0; i < floatingMasks.Count; i++)
            {
                var mask = floatingMasks[i];
                mask.on = mask.on << 1;
                mask.off = mask.off << 1;
                floatingMasks[i] = mask;
            }
        }

        private static void ShiftFloatingRight()
        {
            for (int i = 0; i < floatingMasks.Count; i++)
            {
                var mask = floatingMasks[i];
                mask.on = mask.on >> 1;
                mask.off = mask.off >> 1;
                floatingMasks[i] = mask;
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
    }
}
