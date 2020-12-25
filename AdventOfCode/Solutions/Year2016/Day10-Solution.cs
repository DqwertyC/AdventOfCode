using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2016
{

    class Day10 : ASolution
    {
        string partOne;
        string partTwo;

        public Day10() : base(10, 2016, "")
        {
            List<(int bot, int value)> initialValues = new List<(int, int)>();
            foreach (string line in Input.SplitByNewline())
            {
                string[] lineParts = line.Split(' ');

                if (lineParts[0].Equals("value"))
                {
                    initialValues.Add((int.Parse(lineParts[5]), int.Parse(lineParts[1])));
                }
                else
                {
                    int botId = int.Parse(lineParts[1]);
                    int lowTarget = int.Parse(lineParts[6]);
                    int highTarget = int.Parse(lineParts[11]);

                    bool lowIsBot = lineParts[5].Equals("bot");
                    bool highIsBot = lineParts[10].Equals("bot");

                    new Bot(botId, lowIsBot, lowTarget, highIsBot, highTarget);
                }
            }

            foreach (var initial in initialValues)
            {
                Bot.bots[initial.bot].AddValue(initial.value);
            }

            foreach (Bot bot in Bot.bots.Values)
            {
                if (bot.CheckValues(17, 61) || bot.CheckValues(61, 17))
                {
                    partOne = bot.botId.ToString();
                }
            }

            partTwo = (Bot.outputs[0] * Bot.outputs[1] * Bot.outputs[2]).ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private class Bot
        {
            public static Dictionary<int, Bot> bots = new Dictionary<int, Bot>();
            public static Dictionary<int, int> outputs = new Dictionary<int, int>();

            public int botId;

            public int valueOne;
            public int valueTwo;

            bool hasValueOne;

            bool lowIsBot;
            bool highIsBot;

            int lowTarget;
            int highTarget;

            public Bot(int botId, bool lowIsBot, int lowTarget, bool highIsBot, int highTarget)
            {
                this.botId = botId;
                this.lowIsBot = lowIsBot;
                this.lowTarget = lowTarget;
                this.highIsBot = highIsBot;
                this.highTarget = highTarget;

                bots.Add(botId, this);
            }

            public bool CheckValues(int valueOne, int valueTwo)
            {
                return (this.valueOne == valueOne && this.valueTwo == valueTwo);
            }

            public void AddValue(int value)
            {
                if (hasValueOne)
                {
                    valueTwo = value;

                    int lowValue = Math.Min(valueOne, valueTwo);
                    int highValue = Math.Max(valueOne, valueTwo);

                    if (lowIsBot) bots[lowTarget].AddValue(lowValue);
                    else outputs[lowTarget] = lowValue;

                    if (highIsBot) bots[highTarget].AddValue(highValue);
                    else outputs[highTarget] = highValue;
                }
                else
                {
                    valueOne = value;
                    hasValueOne = true;
                }
            }
        }
    }
}
