using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day11 : ASolution
    {
        string partOne;
        string partTwo;

        int shortestPath = 100;
        Dictionary<string,int> minDepthForState = new Dictionary<string,int>();

        string input = 
@"The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.";

        string realInput =
@"The first floor contains a thulium generator, a thulium-compatible microchip, a plutonium generator, a elerium generator, a elerium-compatible microchip, a dilithium generator, a dilithium-compatible microchip, and a strontium generator.
The second floor contains a plutonium-compatible microchip and a strontium-compatible microchip.
The third floor contains a promethium generator, a promethium-compatible microchip, a ruthenium generator, and a ruthenium-compatible microchip.
The fourth floor contains nothing relevant.";

        public Day11() : base(11, 2016, "")
        {
            FloorLayout startingLayout = new FloorLayout(realInput);

            Queue<(FloorLayout floors, int depth)> layoutsToCheck = new Queue<(FloorLayout f, int depth)>();

            layoutsToCheck.Enqueue((startingLayout, 1));

            bool matchFound = false;

            while (!matchFound)
            {
                var layout = layoutsToCheck.Dequeue();

                foreach (FloorLayout child in layout.floors.PossibleMoves())
                {
                    string state = child.ToString();
                    int depthSeenAt = minDepthForState.GetValueOrDefault(state, int.MaxValue);

                    if (depthSeenAt > layout.depth)
                    {
                        minDepthForState[state] = layout.depth;

                        if (child.IsFinishedLayout())
                        {
                            shortestPath = layout.depth;
                            matchFound = true;
                        }
                        else
                        {
                            layoutsToCheck.Enqueue((child, layout.depth + 1));
                        }
                    }
                }
            }

            partOne = shortestPath.ToString();

        }

        private int GetShortestSolution(FloorLayout startingLayout, int currentDepth)
        {
            int depth = currentDepth + 1;

            if (depth < shortestPath)
            {
                foreach (FloorLayout f in startingLayout.PossibleMoves())
                {
                    string state = f.ToString();
                    int depthSeenAt = minDepthForState.GetValueOrDefault(state, int.MaxValue);

                    if (depthSeenAt > depth)
                    {
                        minDepthForState[state] = depth;

                        if (f.IsFinishedLayout())
                        {
                            shortestPath = depth;
                        }
                        else
                        {
                            GetShortestSolution(f, depth);
                        }
                    }
                }
            }

            return shortestPath;
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private class FloorLayout
        {
            static Dictionary<string, int> elementToId = new Dictionary<string, int>();
            static int elementCount = 0;

            int elevatorFloor = 0;
            int floorCount = 0;
            List<List<int>> floorComponents;

            public FloorLayout(string input)
            {
                floorComponents = new List<List<int>>();
                string[] floors = input.SplitByNewline();

                foreach (string floor in floors)
                {
                    List<int> components = new List<int>();

                    string cleaned = floor.Replace(" floor contains a ", "")
                                          .Replace(" and a ", ",")
                                          .Replace(",,", ",")
                                          .Replace(".", "")
                                          .Replace(" a ", "")
                                          .Replace("The first", "")
                                          .Replace("The second", "")
                                          .Replace("The third", "")
                                          .Replace("The fourth", "");

                    string[] lineParts = cleaned.Split(",");

                    foreach (string component in lineParts)
                    {
                        string[] componentParts = component.Split(new char[] { ' ', '-' });

                        if (2 == componentParts.Length)
                        {
                            components.Add(GetElementId(componentParts[0]));
                        }
                        else if (3 == componentParts.Length)
                        {
                            components.Add(100 + GetElementId(componentParts[0]));
                        }

                    }

                    floorComponents.Add(components);
                    floorCount++;
                }
            }

            private FloorLayout(FloorLayout original)
            {
                elevatorFloor = original.elevatorFloor;
                floorCount = original.floorCount;
                floorComponents = new List<List<int>>();

                foreach (List<int> floor in original.floorComponents)
                {
                    floorComponents.Add(floor.Clone());
                }
            }

            public List<FloorLayout> PossibleMoves()
            {
                List<FloorLayout> validChildren = new List<FloorLayout>();

                List<(int,int)> componentCombinations = new List<(int,int)>();

                for (int i = 0; i < floorComponents[elevatorFloor].Count; i++)
                {
                    componentCombinations.Add((floorComponents[elevatorFloor][i], -1));
                    for (int j = i + 1; j < floorComponents[elevatorFloor].Count; j++)
                    {
                        componentCombinations.Add((floorComponents[elevatorFloor][i], floorComponents[elevatorFloor][j]));
                    }
                }

                foreach ((int a, int b) combination in componentCombinations)
                {
                    FloorLayout child;

                    if (TryGetChildLayout(1, combination, out child))
                    {
                        validChildren.Add(child);
                    }

                    if (TryGetChildLayout(-1, combination, out child))
                    {
                        validChildren.Add(child);
                    }
                }

                return validChildren;
            }

            public bool TryGetChildLayout(int elevatorOffset, (int a, int b) components, out FloorLayout child)
            {
                child = new FloorLayout(this);
                if (elevatorFloor + elevatorOffset >= floorCount || elevatorFloor + elevatorOffset < 0) return false;

                child.floorComponents[child.elevatorFloor].Remove(components.a);
                child.floorComponents[child.elevatorFloor].Remove(components.b);

                child.elevatorFloor += elevatorOffset;

                if (components.a >= 0) child.floorComponents[child.elevatorFloor].Add(components.a);
                if (components.b >= 0) child.floorComponents[child.elevatorFloor].Add(components.b);

                return child.IsValidLayout();

            }

            public bool IsValidLayout()
            {
                for (int i = 0; i < floorCount; i++)
                {
                    bool unshieldedChip = false;
                    bool generator = false;

                    foreach (int component in floorComponents[i])
                    {
                        if (component >= 100 && !floorComponents[i].Contains(component - 100))
                        {
                            unshieldedChip = true;
                        }
                        else if (component < 100)
                        {
                            generator = true;
                        }
                    }

                    if (generator && unshieldedChip) return false;
                }

                return true;
            }

            public bool IsFinishedLayout()
            {
                for (int i = 0; i < floorCount - 1; i++)
                {
                    if (floorComponents[i].Count > 0) return false;
                }

                return true;
            }

            public int GetElementId(string element)
            {
                if (!elementToId.ContainsKey(element))
                {
                    elementToId[element] = elementCount;
                    elementCount++;
                }

                return elementToId[element];
            }

            public override string ToString()
            {
                StringBuilder b = new StringBuilder();
                b.Append(elevatorFloor);
                b.Append(';');

                for (int i = 0; i < floorCount; i++)
                {
                    floorComponents[i].Sort();

                    foreach (int j in floorComponents[i])
                    {
                        b.Append(j);
                        b.Append(',');
                    }

                    b.Append(';');
                }

                return b.ToString().Replace(",;",";");
            }
        }
    }
}
