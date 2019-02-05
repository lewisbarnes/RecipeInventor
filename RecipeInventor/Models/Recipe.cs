using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class Recipe
    {
        public List<RecipeIngredient> Ingredients { get; private set; }

        public Recipe()
        {

        }
        public Recipe(List<RecipeIngredient> ingredients)
        {
            Ingredients = new List<RecipeIngredient>();
            foreach(RecipeIngredient r in ingredients)
            {
                if (!Ingredients.Exists(x => x.Ingredient == r.Ingredient))
                {
                    Ingredients.Add(r);
                }
            }
        }
        public void AddIngredient(RecipeIngredient ingredient)
        {
            // If new RecipeIngredient has existing ingredient
            if (!Ingredients.Exists(x => x.Ingredient == ingredient.Ingredient))
            {
                Ingredients.Add(ingredient);
            }
        }

    }
}
