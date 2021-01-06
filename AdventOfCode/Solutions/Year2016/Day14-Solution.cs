using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day14 : ASolution
    {
        string partOne;
        string partTwo;

        static string salt;

        List<string> hashes = new List<string>();
        Dictionary<char, List<int>> threeIndexes = new Dictionary<char, List<int>>();
        List<int> finalIndexes = new List<int>();

        MD5 md5 = System.Security.Cryptography.MD5.Create();

        string input = "abc";

        public Day14() : base(14, 2016, "")
        {
            salt = Input;
            int index = 0;
            
            while (finalIndexes.Count < 64)
            {
                ProcessHash(index++,0);
            }
            finalIndexes.Sort();
            partOne = finalIndexes[63].ToString();
            
            threeIndexes = new Dictionary<char, List<int>>();
            finalIndexes = new List<int>();

            index = 0;
            while (finalIndexes.Count < 64)
            {
                ProcessHash(index++, 2016);
            }
            finalIndexes.Sort();
            partTwo = finalIndexes[63].ToString();

        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public void ProcessHash(int input, int stretchCount)
        {
            byte[] inputBytes;
            byte[] hashBytes;
            string hashString = salt + input;

            int stretches = 0;

            do
            {
                inputBytes = System.Text.Encoding.ASCII.GetBytes(hashString);
                hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                hashString = sb.ToString();

                stretches++;
            }
            while (stretches <= stretchCount);

            
            bool tripleFound = false;
            for (int i = 0; i <= hashString.Length-3; i++)
            {
                char val = hashString[i];
                
                if (hashString[i+1] == val && hashString[i+2] == val)
                {
                    if (!tripleFound)
                    {
                        if (!threeIndexes.ContainsKey(val))
                        {
                            threeIndexes[val] = new List<int>();
                        }

                        threeIndexes[val].Add(input);

                        tripleFound = true;
                    }
            
                    if (i <= hashString.Length - 5 && hashString[i+3] == val && hashString[i+4] == val)
                    {
                        List<int> validated = new List<int>();

                        foreach (int index in threeIndexes[val])
                        {
                            if (index != input && index + 1000 > input)
                            {
                                validated.Add(index);
                            }
                        }

                        foreach (int newValid in validated)
                        {
                            finalIndexes.Add(newValid);
                            threeIndexes[val].Remove(newValid);
                        }
                    }
                }                 
            }
        }
    }
}
