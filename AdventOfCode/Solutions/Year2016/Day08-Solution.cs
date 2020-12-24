using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day08 : ASolution
    {
        string partOne;
        string partTwo;

        readonly char[] splitChars = new char[] { ' ', '=', 'x', 'y' };
        readonly StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public Day08() : base(08, 2016, "")
        {
            Screen screen = new Screen(50, 6);

            foreach (string line in Input.SplitByNewline())
            {
                string[] lineParts = line.Split(splitChars, splitOptions);

                if (lineParts[0].StartsWith("rect"))
                {
                    screen.Rect(int.Parse(lineParts[1]), int.Parse(lineParts[2]));
                }
                else if (lineParts[1].StartsWith("row"))
                {
                    screen.Row(int.Parse(lineParts[2]), int.Parse(lineParts[4]));
                }
                else
                {
                    screen.Col(int.Parse(lineParts[2]), int.Parse(lineParts[4]));
                }
            }

            partOne = screen.CountOn().ToString();
            partTwo = screen.ToString();


        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private class Screen
        {
            Dictionary<(int, int), bool> pixelValues;
            int maxWidth;
            int maxHeight;

            public Screen(int width, int height)
            {
                pixelValues = new Dictionary<(int, int), bool>();
                maxWidth = width;
                maxHeight = height;

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        pixelValues[(x, y)] = false;
                    }
                }
            }

            public void Rect(int width, int height)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        pixelValues[(x, y)] = true;
                    }
                }
            }

            public void Row(int row, int amount)
            {
                Dictionary<(int, int),bool> newValues = new Dictionary<(int, int),bool>();

                for (int x = 0; x < maxWidth; x++)
                {
                    int lookCol = (x + maxWidth - amount) % maxWidth;
                    newValues[(x, row)] = pixelValues[(lookCol, row)];
                    pixelValues.Remove((lookCol, row));
                }

                foreach ((int,int) key in newValues.Keys)
                {
                    pixelValues[key] = newValues[key];
                }
            }

            public void Col(int col, int amount)
            {
                Dictionary<(int, int), bool> newValues = new Dictionary<(int, int), bool>();

                for (int y = 0; y < maxHeight; y++)
                {
                    int lookRow = (y + maxHeight - amount) % maxHeight;
                    newValues[(col,y)] = pixelValues[(col, lookRow)];
                    pixelValues.Remove((col, lookRow));
                }

                foreach ((int, int) key in newValues.Keys)
                {
                    pixelValues[key] = newValues[key];
                }
            }

            public int CountOn()
            {
                int count = 0;

                foreach ((int, int) key in pixelValues.Keys)
                {
                    if (pixelValues[key]) count++;
                }

                return count;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("\n");
                for (int y = 0; y < maxHeight; y++)
                {
                    for (int x = 0; x < maxWidth; x++)
                    {
                        sb.Append(pixelValues[(x, y)] ? "█" : " ");
                    }

                    sb.Append("\n");
                }

                sb.Length = sb.Length - 1;

                return sb.ToString();
            }
        }
    }
}
