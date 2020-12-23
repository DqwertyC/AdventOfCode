using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day04 : ASolution
    {

        string partOne;
        string partTwo;

        public Day04() : base(04, 2015, "")
        {
            int count = 0;

            string result = CalculateHash(count);

            while (!result.StartsWith("00000"))
            {
                count++;
                result = CalculateHash(count);
            }

            partOne = result;

            while (!result.StartsWith("000000"))
            {
                count++;
                result = CalculateHash(count);
            }

            partTwo = result;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public static string CalculateHash(int input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                string numString = "bgvyzdsv" + input;

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(numString);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
