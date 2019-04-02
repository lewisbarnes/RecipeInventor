using RecipeInventor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor
{
    public class RecipeWriter
    {
        public RecipeWriter()
        {

        }

        public void WriteRecipeToFile(Recipe recipe)
        {
            using (StreamWriter writer = File.CreateText($"Recipes/{DateTime.UtcNow.Millisecond.ToString()}" +
                $"_{DateTime.UtcNow.Second.ToString()}" +
                $"_{DateTime.UtcNow.Minute.ToString()}" +
                $"_{DateTime.UtcNow.Hour.ToString()}.txt"))
            {
                foreach(RecipeIngredient r in recipe.Ingredients)
                {
                    writer.WriteLineAsync(r.ToString());
                }
            }
        }
    }
}
