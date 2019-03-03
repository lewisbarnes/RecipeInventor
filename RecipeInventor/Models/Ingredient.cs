using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecipeInventor.Models
{
    /// <summary>
    /// Base ingredient class to be used for all further ingredients created
    /// </summary>
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }
        public double MeanQuantity { get; set; }
        public int NumberOfRecipes { get; set; }
        public double StandardDeviation { get; set; }
        public double RelativeFrequency { get; set; }
        public List<ComplementPair> OtherIngredientComplements { get; set; }

        public Ingredient()
        {
        }
        public void SetAttributes(string name, IngredientType type)
        {
            Name = name;
            Type = type;
        }
        public override string ToString()
        {
            return Name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void CalculateMeanQuantity()
        {
            // Find recipes that contain the specified ingredient
            List<Recipe> recipes = DataManager.GetRecipes().ToList();

            // Set the count of the recipes containing the ingredient
            
            double runningTotal = 0;
            List<Recipe> containsThis = recipes.Where(x => x.Ingredients.Where(y => y.Ingredient.Id == this.Id).Any()).ToList();
            int numberOfRecipes = containsThis.Count;
            // Find each ingredient in the recipe and add the quantity to the running total
            foreach (Recipe r in containsThis)
            {
                runningTotal += r.Ingredients.Where(x => x.Ingredient.Id == this.Id).First().Quantity;
            }

            MeanQuantity = runningTotal / numberOfRecipes;
            NumberOfRecipes = numberOfRecipes;

            DataManager.UpdateIngredient(this);
        }

        public void CalculateIngredientComplements()
        {
            List<Recipe> recipes = DataManager.GetRecipes();

            // Get all ingredients 
            List<Ingredient> ingredients = DataManager.GetIngredients();

            // Find recipes that contain this ingredient
            List<Recipe> containsThis = recipes.Where(x => x.Ingredients.Where(y => y.Ingredient.Id == this.Id).Any()).ToList();

            // Initialise the list
            OtherIngredientComplements = new List<ComplementPair>();

            // For each ingredient, if not equal to this ingredient, add a new ComplementPair to the list
            foreach (Ingredient i in ingredients)
            {
                if(this.Id != i.Id)
                {
                    // Count the recipes where both ingredients appear together
                    double count = containsThis.Where(x => x.Ingredients.Where(y => y.Ingredient.Id == i.Id).Any()).Count();
                    if (count > 0)
                    {
                        // Divide total count of recipes including ingredient by count of recipes including this ingredient
                        OtherIngredientComplements.Add(new ComplementPair(i, count / containsThis.Count));
                    }
                }
            }
            DataManager.UpdateIngredient(this);
        }

        public void CalculateStandardDeviation()
        {
            // Calculate mean quantity ensuring stats are up to date
            CalculateMeanQuantity();

            // Find recipes that contain the specified ingredient
            List<Recipe> recipes = DataManager.GetRecipes();
            double runningTotal = 0;
            List<Recipe> containsThis = recipes.Where(x => x.Ingredients.Where(y => y.Ingredient.Id == this.Id).Any()).ToList();
            // Find matching ingredient in the recipe, get the square of the distance to the mean quantity, add to running total
            foreach (Recipe r in containsThis)
            {
                runningTotal += Math.Pow(r.Ingredients.Where(x => x.Ingredient.Id == this.Id).First().Quantity - MeanQuantity,2);
            }

            // Get the square root of the differences;
            StandardDeviation = Math.Sqrt(runningTotal / NumberOfRecipes);

            DataManager.UpdateIngredient(this);

        }

        public void CalculateRelativeFrequency()
        {
            // Get all recipes
            List<Recipe> recipes = DataManager.GetRecipes();
            int totalRecipes = recipes.Count();
            // Get the count of recipes that contain this ingredient
            List<Recipe> containsThis = recipes.Where(x => x.Ingredients.Where(y => y.Ingredient.Id == this.Id).Any()).ToList();
            int totalRecipesThisIngredient = containsThis.Count();

            // Divide the count of recipes including this ingredient by the count of all recipes
            RelativeFrequency = totalRecipesThisIngredient / totalRecipes;

        }
    }

    public class ComplementPair
    {
        public int Ingredient { get; set; }
        public string IngredientName { get { return DataManager.GetIngredientByID(Ingredient).ToString(); } }
        public double Compatibility { get; set; }
        public ComplementPair()
        {

        }
        public ComplementPair(Ingredient ingredient, double compatibility)
        {
            Ingredient = ingredient.Id;
            Compatibility = compatibility;
        }
        public override string ToString()
        {
            return $"Ingredient: {DataManager.GetIngredientByID(Ingredient)}, Compatibility: { Compatibility:f2}";
        }
    }
}
