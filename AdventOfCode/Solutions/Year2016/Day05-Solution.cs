using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day05 : ASolution
    {
        string partOne;
        string partTwo;

        public Day05() : base(05, 2016, "")
        {
            GetPasswords(Input, out partOne, out partTwo);
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public static void GetPasswords(string basePass, out string passOne, out string passTwo)
        {
            int offset = 0;

            char[] passOneChars = new char[8];
            char[] passTwoChars = new char[8];

            int passOnePlaced = 0;
            int passTwoPlaced = 0;

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes;
                byte[] hashBytes;

                // Use input string to calculate MD5 hash
                while (passTwoPlaced < 8)
                {
                    do
                    {
                        string numString = basePass + offset;

                        inputBytes = System.Text.Encoding.ASCII.GetBytes(numString);
                        hashBytes = md5.ComputeHash(inputBytes);

                        offset++;
                    }
                    while (!CheckBytes(hashBytes));

                    char firstVal = hashBytes[2].ToString("x2")[1];
                    char secondVal = hashBytes[3].ToString("x2")[0];

                    if (passOnePlaced < 8)
                    {
                        passOneChars[passOnePlaced++] = firstVal;
                    }

                    firstVal -= '0';

                    if (firstVal < 8 && passTwoChars[firstVal] == '\0')
                    {
                        passTwoChars[firstVal] = secondVal;
                        passTwoPlaced++;
                    }
                }
            }

            passOne = string.Empty;
            passTwo = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                passOne += passOneChars[i];
                passTwo += passTwoChars[i];
            }
        }

        public static bool CheckBytes(byte[] bytes)
        {
            if (bytes[0] != 0) return false;
            if (bytes[1] != 0) return false;
            if (bytes[2] >= 16) return false;
            return true;
        }
    }
}
