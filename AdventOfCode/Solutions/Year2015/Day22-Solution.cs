using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day22 : ASolution
    {

        static readonly int bossHealthBase = 51;
        static readonly int bossDamageBase = 9;

        static readonly int playerHealthBase = 50;
        static readonly int playerManaBase = 500;

        string partOne;
        string partTwo;

        static int minManaCost = int.MaxValue;

        public Day22() : base(22, 2015, "")
        {
            partOne = GetCheapestWin(new List<Spell>(), false).ToString();
            minManaCost = int.MaxValue;
            partTwo = GetCheapestWin(new List<Spell>(), true).ToString();
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        public static int GetCheapestWin(List<Spell> baseList, bool hardMode)
        {
            int manaCost = GetManaCost(baseList);
            if (manaCost > minManaCost) return minManaCost;

            BattleResult result = SimulateBattle(baseList, hardMode);

            if (BattleResult.Win == result)
            {
                if (manaCost < minManaCost) minManaCost = manaCost;
                return minManaCost;
            }
            else if (BattleResult.Unfinished != result)
            {
                return int.MaxValue;
            }
            else
            {
                int minChildCost = int.MaxValue;
                foreach (Spell newSpell in spellList)
                {
                    List<Spell> newList = baseList.Clone();
                    newList.Add(newSpell);
                    int childCost = GetCheapestWin(newList, hardMode);

                    minChildCost = Math.Min(minChildCost, childCost);
                }

                return minChildCost;
            }


        }

        public static int GetManaCost(List<Spell> spellList)
        {
            int cost = 0;

            foreach (Spell spell in spellList)
            {
                switch (spell)
                {
                    case Spell.MagicMissile:
                        cost += 53;
                        break;
                    case Spell.Drain:
                        cost += 73;
                        break;
                    case Spell.Shield:
                        cost += 113;
                        break;
                    case Spell.Poison:
                        cost += 173;
                        break;
                    case Spell.Recharge:
                        cost += 229;
                        break;
                }
            }

            return cost;
        }

        public static BattleResult SimulateBattle(List<Spell> playerMoves, bool hardMode)
        {
            BattleResult stopCause = BattleResult.Unfinished;

            int playerHealth = playerHealthBase;
            int playerMana = playerManaBase;
            int bossHealth = bossHealthBase;
            int playerArmor = 0;

            int armorCounter = 0;
            int poisonCounter = 0;
            int rechargeCounter = 0;

            for (int i = 0; i < playerMoves.Count && stopCause == BattleResult.Unfinished; i++)
            {
                Spell spell = playerMoves[i];

                if (hardMode)
                {
                    playerHealth--;
                    if (playerHealth == 0)
                    {
                        stopCause = BattleResult.Death;
                    }
                }

                // Start Player Turn
                if (armorCounter-- > 0) playerArmor = 7;
                else playerArmor = 0;
                if (poisonCounter-- > 0) bossHealth -= 3;
                if (rechargeCounter-- > 0) playerMana += 101;

                armorCounter = Math.Max(armorCounter, 0);
                poisonCounter = Math.Max(poisonCounter, 0);
                rechargeCounter = Math.Max(rechargeCounter, 0);

                switch (spell)
                {
                    case Spell.MagicMissile:
                        playerMana -= 53;
                        bossHealth -= 4;
                        break;
                    case Spell.Drain:
                        playerMana -= 73;
                        bossHealth -= 2;
                        playerHealth += 2;
                        break;
                    case Spell.Shield:
                        playerMana -= 113;
                        if (armorCounter == 0) armorCounter = 6;
                        else stopCause = BattleResult.Invalid;
                        break;
                    case Spell.Poison:
                        playerMana -= 173;
                        if (poisonCounter == 0) poisonCounter = 6;
                        else stopCause = BattleResult.Invalid;
                        break;
                    case Spell.Recharge:
                        playerMana -= 229;
                        if (rechargeCounter == 0) rechargeCounter = 5;
                        else stopCause = BattleResult.Invalid;
                        break;
                }

                if (playerMana < 0 && stopCause == BattleResult.Unfinished)
                {
                    stopCause = BattleResult.OutOfMana;
                }

                // Start boss turn
                if (armorCounter-- > 0) playerArmor = 7;
                else playerArmor = 0;
                if (poisonCounter-- > 0) bossHealth -= 3;
                if (rechargeCounter-- > 0) playerMana += 101;

                armorCounter = Math.Max(armorCounter, 0);
                poisonCounter = Math.Max(poisonCounter, 0);
                rechargeCounter = Math.Max(rechargeCounter, 0);

                if (bossHealth <= 0 && stopCause == BattleResult.Unfinished)
                {
                    stopCause = BattleResult.Win;
                }

                playerHealth -= (bossDamageBase - playerArmor);

                if (playerHealth <= 0 && stopCause == BattleResult.Unfinished)
                {
                    stopCause = BattleResult.Death;
                }
            }

            return stopCause;
        }

        public enum Spell
        {
            MagicMissile,
            Drain,
            Shield,
            Poison,
            Recharge
        }

        private static Spell[] spellList = new Spell[] { Spell.MagicMissile, Spell.Drain, Spell.Shield, Spell.Poison, Spell.Recharge };

        public enum BattleResult
        {
            Win,
            Death,
            OutOfMana,
            Invalid,
            Unfinished
        }
    }
}
