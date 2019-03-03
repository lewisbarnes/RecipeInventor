using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public List<RecipeIngredient> Ingredients { get; set; }
        public string Name { get; set; }
        public Recipe()
        {
            Ingredients = new List<RecipeIngredient>();
        }

        public Recipe(string name)
        {
            Name = name;
            Ingredients = new List<RecipeIngredient>();
        }
        public Recipe(List<RecipeIngredient> ingredients, string name)
        {
            Name = name;
            Ingredients = new List<RecipeIngredient>();
            foreach(RecipeIngredient r in ingredients)
            {
                if (!Ingredients.Exists(x => x.Ingredient == r.Ingredient))
                {
                    Ingredients.Add(r);
                }
            }
        }
        public bool AddIngredient(RecipeIngredient ingredient)
        {
            // If new RecipeIngredient has existing ingredient
            if (!Ingredients.Exists(x => x.Ingredient.Id == ingredient.Ingredient.Id))
            {
                Ingredients.Add(ingredient);

                // Return true if ingredient was able to be added
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
