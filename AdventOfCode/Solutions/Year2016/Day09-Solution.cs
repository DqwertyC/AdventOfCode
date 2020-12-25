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
            var compressedTokens = TokenizeString(Input);
            partOne = Decompress(compressedTokens, false).ToString();
            partTwo = Decompress(compressedTokens, true).ToString();
        }

        public static List<CompressionToken> TokenizeString(string input)
        {
            List<CompressionToken> tokens = new List<CompressionToken>();
            int tokenLength = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '(')
                {
                    tokenLength++;
                }
                else
                {
                    if (tokenLength > 0)
                    {
                        tokens.Add(new CompressionToken(tokenLength));
                    }
                    
                    tokenLength = 2;
                    i++;

                    StringBuilder marker = new StringBuilder();

                    while (input[i] != ')')
                    {
                        marker.Append(input[i]);
                        tokenLength++;
                        i++;
                    }

                    string[] markerVals = marker.ToString().Split('x');

                    int repeatLength = int.Parse(markerVals[0]);
                    int repeatCount = int.Parse(markerVals[1]);

                    tokens.Add(new CompressionToken(tokenLength,repeatLength,repeatCount));
                    tokenLength = 0;
                }
            }

            tokens.Add(new CompressionToken(tokenLength));
            return tokens;
        }

        public long Decompress(List<CompressionToken> compressed, bool recurse = false)
        {
            long length = 0;
            int nextLiteralLength = 0;

            for (int i = 0; i < compressed.Count; i++)
            {
                if (!compressed[i].isMarker)
                {
                    length += compressed[i].maxLength;
                }
                else
                {
                    List<CompressionToken> sublist = new List<CompressionToken>();

                    int countLeft = compressed[i].copyLength;
                    int copyTimes = compressed[i].copyAmount;

                    while (countLeft > 0)
                    {
                        i++;
                        if (compressed[i].maxLength <= countLeft)
                        {
                            sublist.Add(compressed[i]);
                            countLeft -= compressed[i].maxLength;
                        }
                        else
                        {
                            sublist.Add(new CompressionToken(countLeft));
                            nextLiteralLength = compressed[i].maxLength - countLeft;
                            countLeft = 0;
                        }
                    }

                    if (recurse)
                    {
                        length += copyTimes * Decompress(sublist, true);
                    }
                    else
                    {
                        length += copyTimes * TokenLength(sublist);
                    }

                    length += nextLiteralLength;
                    nextLiteralLength = 0;

                }
            }

            return length;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public class CompressionToken
        {
            public bool isMarker;
            public int maxLength;
            public int copyLength;
            public int copyAmount;

            public CompressionToken(int length)
            {
                isMarker = false;
                maxLength = length;
            }

            public CompressionToken(int length, int copyLen, int copyTimes)
            {
                isMarker = true;
                maxLength = length;
                copyAmount = copyTimes;
                copyLength = copyLen;
            }
        }

        public static long TokenLength(List<CompressionToken> tokens)
        {
            long length = 0;
            foreach (CompressionToken t in tokens)
            {
                length += t.maxLength;
            }

            return length;
        }
    }
}
