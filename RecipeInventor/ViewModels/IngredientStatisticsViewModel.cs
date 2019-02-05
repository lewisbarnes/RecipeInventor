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
        public List<IngredientStatistics> Statistics { get; private set; }
        public IngredientStatisticsViewModel()
        {
            Statistics = DataManager.GetIngredientStatistics();
        }
    }
}
