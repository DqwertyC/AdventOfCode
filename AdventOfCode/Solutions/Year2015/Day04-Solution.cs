using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day04 : ASolution
    {

        public Day04() : base(04, 2015, "")
        {

        }

        protected override string SolvePartOne()
        {
            int count = 0;

            string result = CalculateHash(count);

            while (!result.StartsWith("00000"))
            {
                count++;
                result = CalculateHash(count);
            }

            return count.ToString();
        }

        protected override string SolvePartTwo()
        {
            int count = 0;

            string result = CalculateHash(count);

            while (!result.StartsWith("000000"))
            {
                count++;
                result = CalculateHash(count);
            }

            return count.ToString();
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
