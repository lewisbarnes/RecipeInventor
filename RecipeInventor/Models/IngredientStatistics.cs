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


        public double StandardDeviation { get; private set; }

        public IngredientStatistics(Ingredient ingredient)
        {
        }

        public IngredientStatistics()
        {
        }

        
    }
}
