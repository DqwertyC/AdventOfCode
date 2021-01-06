using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day16 : ASolution
    {
        string partOne;
        string partTwo;

        public Day16() : base(16, 2016, "")
        {
            string data = Input;
            while (data.Length < 272) data = Dragonify(data);
            data = data.Substring(0, 272);

            string checksum = data;
            while (checksum.Length % 2 == 0) 
                checksum = Checksum(checksum);
            partOne = checksum;

            data = Input;
            while (data.Length < 35651584) data = Dragonify(data);
            data = data.Substring(0, 35651584);

            checksum = data;
            while (checksum.Length % 2 == 0)
                checksum = Checksum(checksum);
            partTwo = checksum;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public string Dragonify(string input)
        {
            return input + "0" + input.Reverse().Replace("0","O").Replace("1","0").Replace("O","1");
        }

        public string Checksum(string input)
        {
            StringBuilder checksum = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[++i])
                {
                    checksum.Append('1');
                }
                else
                {
                    checksum.Append('0');
                }
            }

            return checksum.ToString();
        }
    }
}
