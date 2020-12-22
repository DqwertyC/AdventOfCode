using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode.Solutions.Utilities;

namespace AdventOfCode.Solutions.Year2020
{

    class Day22 : ASolution
    {
        string partOne;
        string partTwo;

        static HashSet<Coordinate2D> matchesPlayed;

        public Day22() : base(22, 2020, "")
        {
            string[] hands = Input.Split("\n\n");

            string[] rawTextA = hands[0].Split("\n");
            string[] rawTextB = hands[1].Split("\n");

            List<int> handA = new List<int>();
            List<int> handB = new List<int>();

            matchesPlayed = new HashSet<Coordinate2D>();

            for (int i = 1; i < rawTextA.Length; i++)
            {
                handA.Add(int.Parse(rawTextA[i]));
                handB.Add(int.Parse(rawTextB[i]));
            }

            List<int> backupA = handA.Clone();
            List<int> backupB = handB.Clone();

            GameResult winner = PlayGame(handA, handB);
            List<int> winningHand = (winner == GameResult.A_WIN ? handA : handB);
            partOne = CalculateScore(winningHand).ToString();

            winner = PlayGameRecursive(backupA, backupB);
            winningHand = (winner == GameResult.A_WIN ? backupA : backupB);
            partTwo = CalculateScore(winningHand).ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public static long CalculateScore(List<int> hand)
        {
            Stack<int> flipped = new Stack<int>();
            while (hand.Count > 0)
            {
                flipped.Push(hand[0]);
                hand.RemoveAt(0);
            }

            int multi = 1;
            long score = 0;

            while (flipped.Count > 0)
            {
                score += multi * flipped.Pop();
                multi++;
            }

            return score;
        }

        public static GameResult PlayGame(List<int> handA, List<int> handB)
        {
            while (handA.Count > 0 && handB.Count > 0)
            {
                int cardA = handA[0];
                int cardB = handB[0];

                handA.RemoveAt(0);
                handB.RemoveAt(0);

                if (cardA > cardB)
                {
                    handA.Add(cardA);
                    handA.Add(cardB);
                }
                else
                {
                    handB.Add(cardB);
                    handB.Add(cardA);
                }
            }

            if (handA.Count > 0) return GameResult.A_WIN;
            else return GameResult.B_WIN;
        }

        public static GameResult PlayGameRecursive(List<int> handA, List<int> handB)
        {
            GameResult result = GameResult.RUNNING;
            HashSet<string> currentSetup = new HashSet<string>();

            while (result == GameResult.RUNNING)
            {
                string currentHands = HandString(handA, handB);

                if (currentSetup.Contains(currentHands))
                {
                    return GameResult.A_WIN;
                }

                currentSetup.Add(currentHands);

                int cardA = handA[0];
                int cardB = handB[0];

                handA.RemoveAt(0);
                handB.RemoveAt(0);

                if (handA.Count >= cardA && handB.Count >= cardB)
                {
                    // Recurse!
                    List<int> copyA = new List<int>();
                    List<int> copyB = new List<int>();

                    // Get the new subhands
                    for (int i = 0; i < cardA; i++)
                    {
                        copyA.Add(handA[i]);
                    }

                    for (int i = 0; i < cardB; i++)
                    {
                        copyB.Add(handB[i]);
                    }

                    // Play the sub game
                    GameResult subGame = PlayGameRecursive(copyA, copyB);

                    // Move the appropriate cards
                    if (GameResult.A_WIN == subGame)
                    {
                        handA.Add(cardA);
                        handA.Add(cardB);
                    }
                    else
                    {
                        handB.Add(cardB);
                        handB.Add(cardA);
                    }
                }
                else
                {
                    // Otherwise, choose a winner normally
                    if (cardA > cardB)
                    {
                        handA.Add(cardA);
                        handA.Add(cardB);
                    }
                    else
                    {
                        handB.Add(cardB);
                        handB.Add(cardA);
                    }
                }

                // If we have a winner, set the result to escape the loop
                if (handA.Count == 0) result = GameResult.B_WIN;
                else if (handB.Count == 0) result = GameResult.A_WIN;
            }

            return result;
        }

        public static string HandString(List<int> handA, List<int> handB)
        {
            StringBuilder handBuilder = new StringBuilder();
            foreach (int a in handA) handBuilder.Append(a + " ");
            handBuilder.Append("\n");
            foreach (int b in handB) handBuilder.Append(b + " ");

            return handBuilder.ToString();
        }

        public enum GameResult
        {
            A_WIN,
            B_WIN,
            RUNNING,
        }
    }
}
