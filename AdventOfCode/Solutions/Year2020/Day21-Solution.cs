using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day21 : ASolution
    {
        string partOne;
        string partTwo;
        public Day21() : base(21, 2020, "")
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            HashSet<string> ingredients = new HashSet<string>();
            HashSet<string> allergens = new HashSet<string>();
            List<FoodProduct> allProducts = new List<FoodProduct>();
            HashSet<string> safeIngredients = new HashSet<string>();

            Dictionary<string, HashSet<string>> potentialIngredients = new Dictionary<string, HashSet<string>>();
            Dictionary<string, string> finalizedIngredients = new Dictionary<string, string>();

            foreach (string line in Input.SplitByNewline())
            {
                HashSet<string> thisIngredients = new HashSet<string>();
                HashSet<string> thisAllergens = new HashSet<string>();
                HashSet<string> newAllergens = new HashSet<string>();

                string[] lineParts = line.Split("(contains", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                foreach (string allergen in lineParts[1].Split(new char[] { ',', ')' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    thisAllergens.Add(allergen);

                    if (!allergens.Contains(allergen))
                    {
                        newAllergens.Add(allergen);
                        potentialIngredients[allergen] = new HashSet<string>();
                    }
                }

                foreach (string ingredient in lineParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    thisIngredients.Add(ingredient);
                }

                foreach (string allergen in thisAllergens)
                {
                    if (newAllergens.Contains(allergen))
                    {
                        foreach (string ingredient in thisIngredients)
                        {
                            potentialIngredients[allergen].Add(ingredient);
                        }
                    }
                }

                ingredients.UnionWith(thisIngredients);
                allergens.UnionWith(thisAllergens);
                allProducts.Add(new FoodProduct(thisIngredients, thisAllergens));
            }

            bool done = false;

            while (!done)
            {
                foreach (FoodProduct product in allProducts)
                {
                    foreach (string allergen in product.allergens)
                    {
                        HashSet<string> toRemove = new HashSet<string>();

                        foreach (string ingredient in potentialIngredients[allergen])
                        {
                            if (!product.ingredients.Contains(ingredient))
                            {
                                toRemove.Add(ingredient);
                            }
                        }

                        foreach (string removeable in toRemove)
                        {
                            potentialIngredients[allergen].Remove(removeable);
                        }

                        if (potentialIngredients[allergen].Count == 1)
                        {
                            string matchingIngredient = "";
                            foreach (string ingredient in potentialIngredients[allergen])
                            {
                                matchingIngredient = ingredient;
                            }

                            foreach (var ingredientLists in potentialIngredients.Values)
                            {
                                if (ingredientLists.Count > 1)
                                {
                                    ingredientLists.Remove(matchingIngredient);
                                }
                            }
                        }
                    }
                }

                done = true;

                foreach (var ingredientLists in potentialIngredients.Values)
                {
                    if (ingredientLists.Count > 1) done = false;
                }
            }

            foreach (string allergen in allergens)
            {
                string cause = "";

                foreach(string ingredient in potentialIngredients[allergen])
                {
                    cause = ingredient;
                }

                finalizedIngredients[allergen] = cause;
            }

            foreach (string ingredient in ingredients)
            {
                if (!finalizedIngredients.ContainsValue(ingredient))
                {
                    safeIngredients.Add(ingredient);
                }
            }

            int count = 0;

            foreach (FoodProduct product in allProducts)
            {
                foreach (string ingredient in product.ingredients)
                {
                    if (safeIngredients.Contains(ingredient)) count++;
                }
            }

            partOne = count.ToString();

            List<string> alphabeticAllergens = new List<string>();
            
            foreach (string s in allergens)
            {
                alphabeticAllergens.Add(s);
            }

            alphabeticAllergens.Sort();

            StringBuilder answer = new StringBuilder();

            foreach (string allergen in alphabeticAllergens)
            {
                answer.Append(finalizedIngredients[allergen] + ",");
            }

            answer.Length = answer.Length - 1;

            partTwo = answer.ToString();

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        protected override string SolvePartOne()
        {
            return partOne;
        }

        protected override string SolvePartTwo()
        {
            return partTwo;
        }

        private class FoodProduct
        {
            public HashSet<string> ingredients;
            public HashSet<string> allergens;

            public FoodProduct(HashSet<string> ingredient, HashSet<string> allergen)
            {
                ingredients = ingredient;
                allergens = allergen;
            }
        }
    }
}
