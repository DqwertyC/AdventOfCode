using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{
    class Day15 : ASolution
    {
        private static int maxScore;
        private static int maxWithCap;

        public Day15() : base(08, 2015, "")
        {
            maxScore = 0;
            maxWithCap = 0;

            for (int a = 1; a <= 97; a++)
            {
                for (int b = 1; b <= (98 - a); b++)
                {
                    for (int c = 1; c <= (99 - (a + b)); c++)
                    {
                        int d = 100 - (a + b + c);

                        int scoreA = 5 * a - b - d;
                        int scoreB = 3 * b - a - c;
                        int scoreC = 4 * c;
                        int scoreD = 2 * d;
                        int scoreE = 5 * a + b + 6 * c + 8 * d;

                        if (scoreA < 0) scoreA = 0;
                        if (scoreB < 0) scoreB = 0;
                        if (scoreC < 0) scoreC = 0;
                        if (scoreD < 0) scoreD = 0;

                        int score = scoreA * scoreB * scoreC * scoreD;

                        if (score >= maxScore) maxScore = score;

                        if (scoreE == 500 && score >= maxWithCap) maxWithCap = score;
                    }
                }
            }
        }

        protected override string SolvePartOne()
        {
            return maxScore.ToString();
        }

        protected override string SolvePartTwo()
        {
            return maxWithCap.ToString();
        }
    }
}
