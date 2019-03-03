using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class IngredientNode
    {
        public Ingredient Value { get; set; }
        public List<Edge> Edges { get; set; }
        public IngredientNode(Ingredient ingredient)
        {
            Value = ingredient;
            Edges = new List<Edge>();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public void AddEdge(IngredientNode to, double cost)
        {
            Edges.Add(new Edge(this, to, cost));
        }
    }
}
