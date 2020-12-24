using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day09 : ASolution
    {
        string partOne;
        string partTwo;

        

        public Day09() : base(09, 2016, "")
        {
            string compressed = Input.Replace("\n", "").Replace(" ", "");
            Decompress(compressed, out string decompressed);

            partOne = decompressed.Length.ToString();
        }

        public static bool Decompress(string compressed, out string decompressed)
        {
            StringBuilder decompressedBuilder = new StringBuilder();
            bool containsMarker = false;

            for (int i = 0; i < compressed.Length; i++)
            {
                if (compressed[i] != '(')
                {
                    decompressedBuilder.Append(compressed[i]);
                }
                else
                {
                    containsMarker = true;

                    i++;
                    StringBuilder marker = new StringBuilder();

                    while (compressed[i] != ')')
                    {
                        marker.Append(compressed[i]);
                        i++;
                    }

                    string[] markerVals = marker.ToString().Split('x');

                    int repeatLength = int.Parse(markerVals[0]);
                    int repeatCount = int.Parse(markerVals[1]);

                    i++;
                    StringBuilder stringToRepeat = new StringBuilder();
                    for (int j = 0; j < repeatLength; j++)
                    {
                        stringToRepeat.Append(compressed[i]);
                        i++;
                    }
                    i--;

                    for (int j = 0; j < repeatCount; j++)
                    {
                        decompressedBuilder.Append(stringToRepeat);
                    }
                }
            }

            decompressed = decompressedBuilder.ToString();
            return containsMarker;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }
    }
}
