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
        public string Quantity { get; set; }
        public Ingredient OriginalObject;

        public IngredientDetailViewModel(Ingredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Title = $"Viewing Ingredient - {ingredient.Name}";
            this.Quantity = $"{ingredient.TypicallyUsedQuantity} {ingredient.UnitOfMeasurement.ToString()}";
            OriginalObject = ingredient;
        }
    }
}
