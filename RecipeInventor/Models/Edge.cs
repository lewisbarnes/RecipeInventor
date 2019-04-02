using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class Edge
    {
        public IngredientNode From { get; private set; }
        public IngredientNode To { get; private set; }
        public double Cost { get; private set; }
        public Edge(IngredientNode from, IngredientNode to, double cost)
        {
            From = from;
            To = to;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{From.ToString()} -> {To.ToString()} | Cost: {Cost.ToString()}";
        }
    }
}
