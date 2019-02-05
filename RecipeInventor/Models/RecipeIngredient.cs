using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class RecipeIngredient
    {
        public Ingredient Ingredient { get; private set; }
        public int Quantity { get; private set; }
        public RecipeIngredient(Ingredient i, int quantity)
        {
            Ingredient = i;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Ingredient.Name} - {Quantity} {Ingredient.UnitOfMeasurement.ToString()}";
        }
    }
}
