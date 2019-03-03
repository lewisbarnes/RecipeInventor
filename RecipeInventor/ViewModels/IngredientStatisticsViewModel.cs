using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeInventor.Models;

namespace RecipeInventor.ViewModels
{
    public class IngredientStatisticsViewModel
    {
        public List<Ingredient> Statistics { get; set; }
        public IngredientStatisticsViewModel()
        {
            Statistics = DataManager.GetIngredients();
        }
    }
}
