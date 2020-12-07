using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day04 : ASolution
    {
        private static int validCountOne;
        private static int validCountTwo;

        public Day04() : base(04, 2020, "")
        {
            string[] passports = Input.Split("\n\n");

            validCountOne = 0;
            validCountTwo = 0;

            foreach (string s in passports)
            {
                if (IsValidOne(s)) validCountOne++;
                if (IsValidTwo(s)) validCountTwo++;
            }
        }

        protected override string SolvePartOne()
        {
            return "" + validCountOne;
        }

        protected override string SolvePartTwo()
        {
             return "" + validCountTwo;
        }
        public static bool IsValidOne(string passportString)
        {

            bool hasBYR = passportString.Contains("byr");
            bool hasIYR = passportString.Contains("iyr");
            bool hasEYR = passportString.Contains("eyr");
            bool hasHGT = passportString.Contains("hgt");
            bool hasHCL = passportString.Contains("hcl");
            bool hasECL = passportString.Contains("ecl");
            bool hasPID = passportString.Contains("pid");

            return hasBYR && hasIYR && hasEYR && hasHGT && hasHCL && hasECL && hasPID;

        }

        public static bool IsValidTwo(string passportString)
        {
            bool hasBYR = false;
            bool hasIYR = false;
            bool hasEYR = false;
            bool hasHGT = false;
            bool hasHCL = false;
            bool hasECL = false;
            bool hasPID = false;

            string[] splitPassport = passportString.Split(' ', '\n');

            // If there's not enough args, we know it's bad
            if (splitPassport.Length < 7)
            {
                return false;
            }

            // Check each argument, see if it's valid and set a flag;
            foreach (string s in splitPassport)
            {
                string[] p = s.Split(':');

                if (s.StartsWith("byr"))
                {
                    if (Int32.TryParse(p[1], out int val))
                    {
                        if (1920 <= val && 2002 >= val) hasBYR = true;
                    }
                }
                else if (s.StartsWith("iyr"))
                {
                    if (Int32.TryParse(p[1], out int val))
                    {
                        if (2010 <= val && 2020 >= val) hasIYR = true;
                    }
                }
                else if (s.StartsWith("eyr"))
                {
                    if (Int32.TryParse(p[1], out int val))
                    {
                        if (2020 <= val && 2030 >= val) hasEYR = true;
                    }
                }
                else if (s.StartsWith("hgt"))
                {
                    if (p[1].Contains("cm"))
                    {
                        if (Int32.TryParse(p[1].Replace("cm", ""), out int val))
                        {
                            if (150 <= val && 193 >= val) hasHGT = true;
                        }
                    }
                    else if (p[1].Contains("in"))
                    {
                        if (Int32.TryParse(p[1].Replace("in", ""), out int val))
                        {
                            if (59 <= val && 76 >= val) hasHGT = true;
                        }
                    }
                }
                else if (s.StartsWith("hcl"))
                {
                    hasHCL = true;

                    if (p[1].Length != 7)
                    {
                        hasHCL = false;
                    }
                    else
                    {
                        if (p[1][0] != '#') hasHCL = false;
                        if (!IsHex(p[1][1])) hasHCL = false;
                        if (!IsHex(p[1][2])) hasHCL = false;
                        if (!IsHex(p[1][3])) hasHCL = false;
                        if (!IsHex(p[1][4])) hasHCL = false;
                        if (!IsHex(p[1][5])) hasHCL = false;
                        if (!IsHex(p[1][6])) hasHCL = false;
                    }
                }
                else if (s.StartsWith("ecl"))
                {
                    if (p[1].Equals("amb")) hasECL = true;
                    if (p[1].Equals("blu")) hasECL = true;
                    if (p[1].Equals("brn")) hasECL = true;
                    if (p[1].Equals("gry")) hasECL = true;
                    if (p[1].Equals("grn")) hasECL = true;
                    if (p[1].Equals("hzl")) hasECL = true;
                    if (p[1].Equals("oth")) hasECL = true;
                }
                else if (s.StartsWith("pid"))
                {
                    if (Int32.TryParse(p[1], out int val))
                    {
                        if (p[1].Length == 9) hasPID = true;
                    }
                }
            }

            return hasBYR && hasIYR && hasEYR && hasHGT && hasHCL && hasECL && hasPID;
        }

        // Check if a character is valid in a hex code
        private static bool IsHex(char c)
        {
            return (c >= '0' && c <= '9' || c >= 'a' && c <= 'f');
        }
    }
}
