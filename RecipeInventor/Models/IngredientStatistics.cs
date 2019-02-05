using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class IngredientStatistics
    {
        public int Id { get; set; }
        public Ingredient Ingredient { get; private set; }
        public double MeanQuantity { get; private set; }
        public int NumberOfRecipes { get; private set; }

        public double StandardDeviation { get; private set; }

        public IngredientStatistics(Ingredient ingredient)
        {
            Ingredient = ingredient;
            CalculateMeanQuantity();
            CalculateStandardDeviation();
        }

        public IngredientStatistics()
        {
            CalculateMeanQuantity();
            CalculateStandardDeviation();
        }

        public void CalculateMeanQuantity()
        {
            // Find recipes that contain the specified ingredient
            IEnumerable<Recipe> recipes = DataManager.GetRecipes().Where(x => x.Ingredients.Exists(y => this.Ingredient == y.Ingredient));

            // Set the count of the recipes containing the ingredient
            NumberOfRecipes = recipes.Count();
            int runningTotal = 0;
            

            // Find each ingredient in the recipe and add the quantity to the running total
            foreach(Recipe r in recipes)
            {
                runningTotal += r.Ingredients.Find(x => x.Ingredient == this.Ingredient).Quantity;
            }

            // Divide running total by number of recipes containing ingredient
            if(NumberOfRecipes != 0)
            {
                MeanQuantity = runningTotal / NumberOfRecipes;
            }
            else
            {
                MeanQuantity = 0;
            }
            
        }

        public void CalculateStandardDeviation()
        {
            // Calculate mean quantity ensuring stats are up to date
            CalculateMeanQuantity();

            // Find recipes that contain the specified ingredient
            IEnumerable<Recipe> recipes = DataManager.GetRecipes().Where(x => x.Ingredients.Exists(y => this.Ingredient == y.Ingredient));
            double runningTotal = 0;

            // Find matching ingredient in the recipe, get the square of the distance to the mean quantity, add to running total
            foreach (Recipe r in recipes)
            {
                runningTotal += Math.Pow(r.Ingredients.Find(x => x.Ingredient == this.Ingredient).Quantity - MeanQuantity,2);
            }

            // Get the square root of the differences;
            StandardDeviation = Math.Sqrt(runningTotal / NumberOfRecipes);
        }
    }
}
