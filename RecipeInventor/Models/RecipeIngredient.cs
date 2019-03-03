using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class RecipeIngredient
    {
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }
        public RecipeIngredient()
        {

        }
        public RecipeIngredient(Ingredient i, double quantity)
        {
            Ingredient = i;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Ingredient.Name} - {Quantity} g";
        }
    }
}
