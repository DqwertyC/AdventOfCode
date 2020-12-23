using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day23 : ASolution
    {
        string partOne;
        string partTwo;

        public Day23() : base(23, 2020, "")
        public Day23() : base(23, 2020, "")
        {
            Dictionary<int, LoopedListNode> nodeDictionaryOne = new Dictionary<int, LoopedListNode>();
            Dictionary<int, LoopedListNode> nodeDictionaryTwo = new Dictionary<int, LoopedListNode>();

            LoopedListNode firstNodeOne = null;
            LoopedListNode firstNodeTwo = null;
            LoopedListNode lastNodeOne = null;
            LoopedListNode lastNodeTwo = null;

            StringToNodes(Input, nodeDictionaryOne, out firstNodeOne, out lastNodeOne);
            StringToNodes(Input, nodeDictionaryTwo, out firstNodeTwo, out lastNodeTwo);
            RangeToNodes(10, 1000000, nodeDictionaryTwo, lastNodeTwo, out lastNodeTwo);

            lastNodeOne.next = firstNodeOne;
            lastNodeTwo.next = firstNodeTwo;

            RunCupGame(nodeDictionaryOne, firstNodeOne, 9, 100);
            RunCupGame(nodeDictionaryTwo, firstNodeTwo, 1000000, 10000000);

            partOne = "";
            LoopedListNode scanner = nodeDictionaryOne[1];
            scanner = scanner.next;

            while (scanner.value != 1)
            {
                partOne = partOne + scanner.value;
                scanner = scanner.next;
            }

            scanner = nodeDictionaryTwo[1];
            partTwo = ((long)scanner.next.value * (long)scanner.next.next.value).ToString();
        }

        private static void StringToNodes(string input, Dictionary<int, LoopedListNode> nodeDictionary, out LoopedListNode first, out LoopedListNode last)
        {
            first = null;
            last = null;

            foreach (char c in input)
            {
                LoopedListNode newNode = new LoopedListNode(c - '0');

                if (null == first) first = newNode;
                if (null != last) last.next = newNode;

                last = newNode;
                nodeDictionary[c - '0'] = newNode;
            }
        }

        public static void RangeToNodes(int start, int end, Dictionary<int,LoopedListNode> nodeDictionary, LoopedListNode last, out LoopedListNode tail)
        {
            for (int i = start; i <= end; i++)
            {
                LoopedListNode newNode = new LoopedListNode(i);

                last.next = newNode;

                last = newNode;
                nodeDictionary[i] = newNode;
            }

            tail = last;
        }

        private static void RunCupGame(Dictionary<int, LoopedListNode> nodeDictionary, LoopedListNode start, int max, int iterations)
        {
            LoopedListNode targetNode;
            LoopedListNode current = start;
            LoopedListNode removed;

            for (int i = 0; i < iterations; i++)
            {
                removed = current.next;
                current.next = current.next.next.next.next;
                int targetValue = current.value;

                do
                {
                    targetValue--;
                    if (targetValue == 0) targetValue = max;
                } 
                while (removed.value == targetValue || removed.next.value == targetValue || removed.next.next.value == targetValue);

                targetNode = nodeDictionary[targetValue];

                removed.next.next.next = targetNode.next;
                targetNode.next = removed;

                current = current.next;
            }
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }
        
        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public class LoopedListNode
        {
            public LoopedListNode next;
            public int value;
            public LoopedListNode(int value)
            {
                this.value = value;
            }
        }
    }
}
