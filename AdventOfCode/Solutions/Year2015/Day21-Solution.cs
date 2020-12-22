using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day21 : ASolution
    {
        string partOne;
        string partTwo;

        public Day21() : base(21, 2015, "")
        {
            List<GameItem> weapons = new List<GameItem>();
            List<GameItem> armors = new List<GameItem>();
            List<GameItem> ringsL = new List<GameItem>();
            List<GameItem> ringsR;

            List<GameItem> equipment;
            int lowestCost = int.MaxValue;
            int highestCost = 0;
            int cost = 0;

            weapons.AddRange(new GameItem[]{ new GameItem(8, 4, 0), new GameItem(10, 5, 0), new GameItem(25, 6, 0), new GameItem(40, 7, 0), new GameItem(74, 8, 0) });
            armors.AddRange(new GameItem[]{ new GameItem(13, 0, 1), new GameItem(31, 0, 2), new GameItem(53, 0, 3), new GameItem(75, 0, 4), new GameItem(102, 0, 5), GameItem.empty });
            ringsL.AddRange(new GameItem[] { new GameItem(25, 1, 0), new GameItem(50, 2, 0), new GameItem(100, 3, 0), new GameItem(20, 0, 1), new GameItem(40, 0, 2), new GameItem(80, 0, 3), GameItem.empty, GameItem.empty });

            foreach (GameItem weapon in weapons)
            {
                foreach (GameItem armor in armors)
                {
                    foreach (GameItem ringL in ringsL)
                    {
                        ringsR = ringsL.Clone();
                        ringsR.Remove(ringL);

                        foreach (GameItem ringR in ringsR)
                        {
                            equipment = new List<GameItem>();
                            equipment.AddRange(new GameItem[] { weapon, armor, ringL, ringR });
                            cost = weapon.cost + armor.cost + ringL.cost + ringR.cost;

                            if (PlayerWinsGame(equipment))
                            {
                                if (cost <= lowestCost) lowestCost = cost;
                            }
                            else
                            {
                                if (cost >= highestCost) highestCost = cost;
                            }
                        }
                    }
                }
            }

            partOne = lowestCost.ToString();
            partTwo = highestCost.ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public class GameItem
        {
            public int cost;
            public int damage;
            public int armor;

            public GameItem(int c, int d, int a)
            {
                cost = c;
                damage = d;
                armor = a;
            }

            public static readonly GameItem empty = new GameItem(0, 0, 0);
        }

        public static bool PlayerWinsGame(List<GameItem> equipment)
        {
            bool won = false;
            int playerHealth = 100;
            int playerArmor = 0;
            int playerDamage = 0;

            foreach (GameItem item in equipment)
            {
                playerArmor += item.armor;
                playerDamage += item.damage;
            }

            // Too lazy to parse the input...
            int bossHealth = 109;
            int bossDamage = 8;
            int bossArmor = 2;

            while (playerHealth > 0 && bossHealth > 0)
            {
                bossHealth -= Math.Max(playerDamage - bossArmor, 1);
                if (bossHealth <= 0) won = true;

                playerHealth -= Math.Max(bossDamage - playerArmor, 1);
            }

            return won;
        }

    }
}
