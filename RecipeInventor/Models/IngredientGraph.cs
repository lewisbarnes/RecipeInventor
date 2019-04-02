using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class IngredientGraph
    {
        public List<IngredientNode> Nodes = new List<IngredientNode>();
        public IngredientGraph()
        {
            List<Ingredient> ingredients = DataManager.GetIngredients();
            foreach (Ingredient i in ingredients)
            {
                Nodes.Add(new IngredientNode(i));
            }
            foreach (Ingredient i in ingredients)
            {
                if (i.OtherIngredientComplements.Count() > 0)
                {
                    IngredientNode node = Nodes.Where(x => x.Value.Id == i.Id).First();
                    foreach (ComplementPair cp in i.OtherIngredientComplements)
                    {
                        IngredientNode nodeTo = Nodes.Where(x => x.Value.Id == cp.Ingredient).First();
                        node.AddEdge(nodeTo, cp.Compatibility);
                    }
                }

            }
        }

        public void PrintGraph()
        {
            Console.Clear();
            foreach (IngredientNode n in Nodes)
            {
                foreach (Edge e in n.Edges)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public GeneratedRecipe GenerateRecipeFromGraph(Ingredient ingredient)
        {
            // Find the node corresponding to the given ingredient
            IngredientNode startingNode = Nodes.Where(x => x.Value.Id == ingredient.Id).First();

            // Set the current and previous nodes to startingNode as no other node to set the values to yet
            IngredientNode currentNode = startingNode;
            IngredientNode previousNode = startingNode;

            // Order the nodes by cost, descending
            List<Edge> edges = currentNode.Edges.OrderByDescending(x => x.Cost).ToList();


            GeneratedRecipe returnRecipe = new GeneratedRecipe();
            // If the node has no edges, return null, signifying not enough data
            if (edges.Count() == 0)
            {
                return null;
            }

            // Get the first edge in the list of edges
            Edge mostCompatible = edges.First();
           

            int loopCount = 0;

            while (returnRecipe.Ingredients.Count() < 12)
            {
                if (currentNode.Edges.Count > 0)
                {
                    // If the recipe already contains the ingredient and the loopCount is less than 5
                    if (returnRecipe.Ingredients.Exists(x => x.Ingredient == currentNode.Value) && loopCount < 5)
                    {
                        loopCount++;
                        // Break if loopCount more than or equal to 5
                        if(loopCount >= 5)
                        {
                            break;
                        }
                        // Set the current node to the previous, node
                        currentNode = previousNode;

                        // Get the edges from the current node
                        edges = currentNode.Edges.OrderByDescending(x => x.Cost).ToList();
                        previousNode = currentNode;
                        // If loopCount more than maximum index of edges
                        if (loopCount >= edges.Count() - 1)
                        {
                            // Set the mostCompatible edge to the last element
                            mostCompatible = edges.ElementAt(edges.Count() - 1);
                            currentNode = mostCompatible.To;
                        }
                        else
                        {
                            mostCompatible = edges.ElementAt(loopCount);
                            currentNode = mostCompatible.To;
                        }
                    }
                    else
                    {
                        returnRecipe.AddEdge(mostCompatible);
                        returnRecipe.AddIngredient(new RecipeIngredient(currentNode.Value, currentNode.Value.MeanQuantity));
                        currentNode = mostCompatible.To;
                        previousNode = currentNode;
                        mostCompatible = currentNode.Edges.OrderByDescending(x => x.Cost).First();
                        
                        loopCount = 0;
                    }

                }
                else
                {
                    break;
                }
            }
            return returnRecipe;
        }
    }
}
