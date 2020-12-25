using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day25 : ASolution
    {
        string partOne;
        public Day25() : base(25, 2020, "")
        {
            string[] values = Input.SplitByNewline();

            long doorPublic = long.Parse(values[0]);
            long cardPublic = long.Parse(values[1]);

            long doorLoops = GetLoopSize(doorPublic);

            long encryptionKey = 1;

            for (long i = 0; i < doorLoops; i++ )
            {
                encryptionKey = TransformValue(encryptionKey, cardPublic);
            }

            partOne = encryptionKey.ToString();

        }

        public long GetLoopSize(long publicKey)
        {
            int loopCount = 0;
            long value = 1;

            do
            {
                loopCount++;
                value = TransformValue(value, 7);
            }
            while (value != publicKey);

            return loopCount;
        }

        public long TransformValue(long start, long subject)
        {
            return (start * subject) % 20201227;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return "Merry Christmas!";
        }
    }
}
