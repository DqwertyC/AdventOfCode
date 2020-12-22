/**
 * This utility class is largely based on:
 * https://github.com/jeroenheijmans/advent-of-code-2018/blob/master/AdventOfCode2018/Util.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{

    public static class Utilities
    {

        public static int[] ToIntArray(this string str, string delimiter = "")
        {
            if(delimiter == "")
            {
                var result = new List<int>();
                foreach(char c in str) if(int.TryParse(c.ToString(), out int n)) result.Add(n);
                return result.ToArray();
            }
            else
            {
                return str
                    .Split(delimiter)
                    .Where(n => int.TryParse(n, out int v))
                    .Select(n => Convert.ToInt32(n))
                    .ToArray();
            }
        }


        public static int MinOfMany(params int[] items)
        {
            var result = items[0];
            for(int i = 1; i < items.Length; i++)
            {
                result = Math.Min(result, items[i]);
            }
            return result;
        }

        public static int MaxOfMany(params int[] items)
        {
            var result = items[0];
            for(int i = 1; i < items.Length; i++)
            {
                result = Math.Max(result, items[i]);
            }
            return result;
        }

        // https://stackoverflow.com/a/3150821/419956 by @RonWarholic
        public static IEnumerable<T> Flatten<T>(this T[,] map)
        {
            for(int row = 0; row < map.GetLength(0); row++)
            {
                for(int col = 0; col < map.GetLength(1); col++)
                {
                    yield return map[row, col];
                }
            }
        }

        public static List<int> Factor(this int number)
        {
            var factors = new List<int>();
            int max = (int)Math.Sqrt(number);  // Round down

            for (int factor = 1; factor <= max; ++factor) // Test from 1 to the square root, or the int below it, inclusive.
            {
                if (number % factor == 0)
                {
                    factors.Add(factor);
                    if (factor != number / factor) // Don't add the square root twice!  Thanks Jon
                        factors.Add(number / factor);
                }
            }
            return factors;
        }

        public static string JoinAsStrings<T>(this IEnumerable<T> items)
        {
            return string.Join("", items);
        }

        public static string[] SplitByNewline(this string input, bool shouldTrim = false)
        {
            return input
                .Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => shouldTrim ? s.Trim() : s)
                .ToArray();
        }

        public static string Reverse(this string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static int ManhattanDistance((int x, int y) a, (int x, int y) b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public static double FindGCD(double a, double b)
        {
            if (a == 0 || b == 0) return Math.Max(a, b);
            return (a % b == 0) ? b : FindGCD(b, a % b);
        }
        
        public static double FindLCM(double a, double b) => a * b / FindGCD(a, b);

        public static void Repeat(this Action action, int count)
        {
            for(int i = 0; i < count; i++) action();
        }

        // https://github.com/tslater2006/AdventOfCode2019
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> values)
        {
            return (values.Count() == 1) ? new[] { values } : values.SelectMany(v => Permutations(values.Where(x => x.Equals(v) == false)), (v, p) => p.Prepend(v));
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size)
        {
            for(var i = 0; i < (float)array.Count() / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }

        // https://stackoverflow.com/questions/49190830/is-it-possible-for-string-split-to-return-tuple
        public static void Deconstruct<T>(this IList<T> list, out T first, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            rest = list.Skip(1).ToList();
        }

        public static void Deconstruct<T>(this IList<T> list, out T first, out T second, out IList<T> rest)
        {
            first = list.Count > 0 ? list[0] : default(T); // or throw
            second = list.Count > 1 ? list[1] : default(T); // or throw
            rest = list.Skip(2).ToList();
        }

        public static (int, int) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);

        public static List<T> Clone<T>(this List<T> old)
        {
            List <T> newList = new List<T>();

            foreach (T t in old)
            {
                newList.Add(t);
            }

            return newList;
        }

        public static List<K> KeyList<K,V>(this Dictionary<K,V> dictionary, bool sorted = false)
        {
            List<K> keyList = new List<K>();

            foreach (K key in dictionary.Keys)
            {
                keyList.Add(key);
            }

            if (sorted) keyList.Sort();

            return keyList;
        }

        //https://stackoverflow.com/questions/2641326/finding-all-positions-of-substring-in-a-larger-string-in-c-sharp
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", nameof(value));
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }

        public class Coordinate2D
        {
            public static readonly Coordinate2D origin = new Coordinate2D(0, 0);
            public static readonly Coordinate2D unit_x = new Coordinate2D(1, 0);
            public static readonly Coordinate2D unit_y = new Coordinate2D(0, 1);

            public int x;
            public int y;

            public Coordinate2D(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Coordinate2D((int x, int y) coord)
            {
                this.x = coord.x;
                this.y = coord.y;
            }

            public Coordinate2D RotateCW(int degrees, Coordinate2D center)
            {
                Coordinate2D offset = center - this;
                return center + offset.RotateCW(degrees);
            }
            public Coordinate2D RotateCW(int degrees)
            {
                switch ((degrees / 90) % 4)
                {
                    case 0: return this;
                    case 1: return RotateCW();
                    case 2: return -this;
                    case 3: return RotateCCW();
                    default: return this;     
                }
            }

            private Coordinate2D RotateCW()
            {
                return new Coordinate2D(y, -x);
            }

            public Coordinate2D RotateCCW(int degrees, Coordinate2D center)
            {
                Coordinate2D offset = center - this;
                return center + offset.RotateCCW(degrees);
            }
            public Coordinate2D RotateCCW(int degrees)
            {
                switch ((degrees / 90) % 4)
                {
                    case 0: return this;
                    case 1: return RotateCCW();
                    case 2: return -this;
                    case 3: return RotateCW();
                    default: return this;
                }
            }

            private Coordinate2D RotateCCW()
            {
                return new Coordinate2D(-y, x);
            }

            public static Coordinate2D operator +(Coordinate2D a) => a;
            public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b) => new Coordinate2D(a.x + b.x, a.y + b.y);
            public static Coordinate2D operator -(Coordinate2D a) => new Coordinate2D(-a.x, -a.y);
            public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b) => a + (-b);
            public static Coordinate2D operator *(int scale, Coordinate2D a) => new Coordinate2D(scale * a.x, scale * a.y);
            public static bool operator ==(Coordinate2D a, Coordinate2D b) => (a.x == b.x && a.y == b.y);
            public static bool operator !=(Coordinate2D a, Coordinate2D b) => (a.x != b.x || a.y != b.y);

            public static implicit operator Coordinate2D((int x, int y) a) => new Coordinate2D(a.x, a.y);

            public static implicit operator (int x, int y) (Coordinate2D a) => (a.x, a.y);
            public override bool Equals(object obj)
            {
                if (obj.GetType() != typeof(Coordinate2D)) return false;
                return this == (Coordinate2D)obj;
            }

            public override int GetHashCode()
            {
                return (100 * x + y).GetHashCode();
            }

            public static Coordinate2D[] GetNeighbors()
            {
                return neighbors2D;
            }

            private static Coordinate2D[] neighbors2D =
            {
                (-1,-1),(-1,+0),(-1,+1),
                (+0,-1),        (+0,+1),
                (+1,-1),(+1,+0),(+1,+1)
            };

        }

        public class Coordinate3D
        {
            int x;
            int y;
            int z;

            public Coordinate3D(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public static implicit operator Coordinate3D((int x, int y, int z) a) => new Coordinate3D(a.x, a.y, a.z);

            public static implicit operator (int x, int y, int z)(Coordinate3D a) => (a.x, a.y, a.z);
            public static Coordinate3D operator +(Coordinate3D a) => a;
            public static Coordinate3D operator +(Coordinate3D a, Coordinate3D b) => new Coordinate3D(a.x + b.x, a.y + b.y, a.z + b.z);
            public static Coordinate3D operator -(Coordinate3D a) => new Coordinate3D(-a.x, -a.y, -a.z);
            public static Coordinate3D operator -(Coordinate3D a, Coordinate3D b) => a + (-b);
            public static bool operator ==(Coordinate3D a, Coordinate3D b) => (a.x == b.x && a.y == b.y && a.z == b.z);
            public static bool operator !=(Coordinate3D a, Coordinate3D b) => (a.x != b.x || a.y != b.y || a.z != b.z);

            public override bool Equals(object obj)
            {
                if (obj.GetType() != typeof(Coordinate3D)) return false;
                return this == (Coordinate3D)obj;
            }

            public override int GetHashCode()
            {
                //Primes times coordinates for fewer collisions
                return (137*x + 149*y + 163*z);
            }

            public static Coordinate3D[] GetNeighbors()
            {
                return neighbors3D;
            }

            private static Coordinate3D[] neighbors3D =
            {
                (-1,-1,-1),(-1,-1,0),(-1,-1,1),(-1,0,-1),(-1,0,0),(-1,0,1),(-1,1,-1),(-1,1,0),(-1,1,1),
                (0,-1,-1), (0,-1,0), (0,-1,1), (0,0,-1),          (0,0,1), (0,1,-1), (0,1,0), (0,1,1),
                (1,-1,-1), (1,-1,0), (1,-1,1), (1,0,-1), (1,0,0), (1,0,1), (1,1,-1), (1,1,0), (1,1,1)
            };
        }

        public class Coordinate4D
        {
            int x;
            int y;
            int z;
            int w;

            public Coordinate4D(int x, int y, int z, int w)
            {
                this.x = x;
                this.y = y;
                this.z = z;
                this.w = w;
            }

            public static implicit operator Coordinate4D((int x, int y, int z, int w) a) => new Coordinate4D(a.x, a.y, a.z, a.w);

            public static implicit operator (int x, int y, int z, int w)(Coordinate4D a) => (a.x, a.y, a.z, a.w);
            public static Coordinate4D operator +(Coordinate4D a) => a;
            public static Coordinate4D operator +(Coordinate4D a, Coordinate4D b) => new Coordinate4D(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
            public static Coordinate4D operator -(Coordinate4D a) => new Coordinate4D(-a.x, -a.y, -a.z, -a.w);
            public static Coordinate4D operator -(Coordinate4D a, Coordinate4D b) => a + (-b);
            public static bool operator ==(Coordinate4D a, Coordinate4D b) => (a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w);
            public static bool operator !=(Coordinate4D a, Coordinate4D b) => (a.x != b.x || a.y != b.y || a.z != b.z || a.z != b.z);

            public override bool Equals(object obj)
            {
                if (obj.GetType() != typeof(Coordinate4D)) return false;
                return this == (Coordinate4D)obj;
            }

            public override int GetHashCode()
            {
                return (137 * x + 149 * y + 163 * z + 179 * w);
            }

            public static Coordinate4D[] GetNeighbors()
            {
                if (neighbors != null) return neighbors;

                List<Coordinate4D> neighborList = new List<Coordinate4D>();

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            for (int w = -1; w <= 1; w++)
                            {
                                if (!((0 == x) && (0 == y) && (0 == z) && (0 == w)))
                                {
                                    neighborList.Add((x, y, z, w));
                                }
                            }
                        }
                    }
                }

                neighbors = neighborList.ToArray();
                return neighbors;
            }

            private static Coordinate4D[] neighbors;
        }
    }
}
