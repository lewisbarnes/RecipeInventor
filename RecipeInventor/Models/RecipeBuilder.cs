using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class RecipeBuilder
    {
        Recipe Recipe;
        public RecipeBuilder()
        {
            Recipe = new Recipe();
        }

        public RecipeBuilder WithIngredients(params RecipeIngredient[] ingredients)
        {
            foreach (RecipeIngredient i in ingredients)
            {
                Recipe.AddIngredient(i);
            }
            return this;
        }
        public Recipe Build()
        {
            return Recipe;
        }
    }
}
