using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day18 : ASolution
    {
        string partOne;
        string partTwo;
        public Day18() : base(18, 2020, "")
        {
            long sumOne = 0;
            long sumTwo = 0;

            foreach (string line in Input.SplitByNewline())
            {
                sumOne += EvaluateExpression(line,true);
                sumTwo += EvaluateExpression(line,false);
            }

            partOne = sumOne.ToString();
            partTwo = sumTwo.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private static long EvaluateExpression(string input, bool isPartOne)
        {
            StringBuilder simplifiedParts = new StringBuilder();

            // This loop looks for things in parentheses, the recursively evaluates them
            int i = 0;
            while (i < input.Length)
            {
                // Keep going until we're done, or we hit an open parenthesis
                while (i < input.Length && input[i] != '(') simplifiedParts.Append(input[i++]);

                // If we're *not* done
                if (i < input.Length)
                {
                    // Don't copy that parenthesis

                    // Get everything in the inner string, including inner parentheticals
                    // Keep going until the matching closing parenthesis
                    StringBuilder innerString = new StringBuilder();
                    int depth = 0;
                    bool closeFound = false;

                    while (!closeFound)
                    {
                        if (input[i] == '(')
                        {
                            depth++;
                            innerString.Append(input[i]);
                        }
                        else if (input[i] == ')' && depth > 0)
                        {
                            depth--;
                            innerString.Append(input[i]);
                        }
                        else if (input[i] == ')' && depth == 0)
                        {
                            simplifiedParts.Append(EvaluateExpression(innerString.ToString(), isPartOne));
                            closeFound = true;
                        }
                        else
                        {
                            innerString.Append(input[i]);
                        }

                        i++;
                    }
                }
            }

            // Now that all parentheticals have been removes/evaluated,
            // call the appropriate evaluation method.
            if (isPartOne) return EvaluatePartOne(simplifiedParts.ToString()); 
            else return EvaluatePartTwo(simplifiedParts.ToString()); 
        }

        // In part one, expressions are evaluated from left to right, ignoring operator type
        static long EvaluatePartOne(string simplifiedLine)
        {
            // Start with a "plus" so the first value is added to 0
            bool sawPlusLast = true;
            long value = 0;

            // Split into the operators and operands
            string[] expressionParts = simplifiedLine.ToString().Split(' ');

            foreach (string s in expressionParts)
            {
                
                if (int.TryParse(s, out int thisValue))
                {
                    // If we're an operand, update value based on
                    // the most recently seen operator
                    if (sawPlusLast) value += thisValue;
                    else value *= thisValue;
                }
                else
                {
                    // Update the last seen operator;
                    sawPlusLast = s[0] == '+';
                }
            }

            return value;
        }

        // In part two, addition is evaluated before multiplication
        static long EvaluatePartTwo(string simplifiedLine)
        {
            // Split on '*' to get the subsets that need evaluated before multiplting
            string[] values = simplifiedLine.ToString().Split('*',StringSplitOptions.TrimEntries);
            long product = 1;

            // Evaluate each subpart and multiply them together
            foreach (string subParts in values)
            {
                // Split on '+' to get the individual numbers
                string[] numbers = subParts.Split('+', StringSplitOptions.TrimEntries);
                long sum = 0;

                // Get each value and add them together
                foreach (string number in numbers)
                {
                    sum += long.Parse(number);
                }

                product *= sum;

            }

            return product;
        }
    }
}
