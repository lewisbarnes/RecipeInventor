using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeInventor.Models;

namespace RecipeInventor.ViewModels
{

    public class IngredientDetailViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public double Quantity { get; set; }
        public Ingredient OriginalObject;

        public IngredientDetailViewModel(Ingredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Title = $"Viewing Ingredient - {ingredient.Name}";
            Quantity = ingredient.MeanQuantity;
            OriginalObject = ingredient;
        }
    }
}
