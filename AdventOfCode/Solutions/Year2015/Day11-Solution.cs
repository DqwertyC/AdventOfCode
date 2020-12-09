using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day11 : ASolution
    {
        string newPassOne;
        string newPassTwo;

        public Day11() : base(11, 2015, "")
        {
            string password = Input;

            while (!IsValid(password)) password = Increment(password);
            newPassOne = password;

            password = Increment(password);
            while (!IsValid(password)) password = Increment(password);
            newPassTwo = password;
        }

        protected override string SolvePartOne()
        {
            return newPassOne;
        }

        protected override string SolvePartTwo()
        {
            return newPassTwo;
        }

        private static string Increment(string input)
        {
            StringBuilder incremented = new StringBuilder();
            bool doneWrapping = false;

            //Take that, POLA
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[input.Length - 1 - i];

                if (!doneWrapping)
                {
                    if ('z' == current)
                    {
                        current = 'a';
                    }
                    else
                    {
                        current++;
                        doneWrapping = true;
                    }
                }

                incremented.Insert(0, current);
            }

            if (!doneWrapping) incremented.Insert(0, 'a');

            return incremented.ToString();
        }

        private static bool IsValid(string s)
        {
            bool containsRamp = false;
            bool containsDoubles = false;
            bool containsInvalid = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 2)
                {
                    if (s[i + 1] == s[i] + 1 && s[i + 2] == s[i] + 2) containsRamp = true;

                    if (s[i + 1] == s[i])
                    {
                        for (int j = i + 2; j < s.Length - 1; j++)
                        {
                            if (s[j + 1] == s[j]) containsDoubles = true;
                        }
                    }
                }

                if (s[i] == 'i' || s[i] == 'o' || s[i] == 'l') containsInvalid = true;

            }

            return containsRamp && containsDoubles && !containsInvalid;
        }
    }
}
