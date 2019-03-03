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
            foreach(Ingredient i in ingredients)
            {
                Nodes.Add(new IngredientNode(i));
            }
            foreach(Ingredient i in ingredients)
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

        public Recipe GenerateRecipeFromGraph(Ingredient ingredient)
        {
            // Find the node corresponding to the given ingredient
            IngredientNode startingNode = Nodes.Where(x => x.Value.Id == ingredient.Id).First();

            IngredientNode currentNode = startingNode;
            IngredientNode previousNode = startingNode;
            List<Edge> edges = currentNode.Edges.OrderByDescending(x => x.Cost).ToList();
            
            
            Recipe returnRecipe = new Recipe();
            if (edges.Count() == 0)
            {
                return null;
            }
            Edge mostCompatible = edges.First();
            int loopCount = 0;
            while (returnRecipe.Ingredients.Count() < 7)
            {
                if(currentNode == null)
                {
                    break;
                }
                if(currentNode.Edges.Count > 0)
                {
                    
                    if (returnRecipe.Ingredients.Exists(x => x.Ingredient == currentNode.Value) && loopCount < 5)
                    {
                        loopCount++;
                        if(loopCount >= 5)
                        {
                            break;
                        }
                        currentNode = previousNode;
                        edges = currentNode.Edges.OrderByDescending(x => x.Cost).ToList();
                        if (loopCount >= edges.Count() - 1)
                        {
                            mostCompatible = edges.ElementAt(edges.Count()-1);
                        }
                        else
                        {
                            mostCompatible = edges.ElementAt(loopCount);
                        }
                        currentNode = mostCompatible.To;
                       
                    }
                    else if(loopCount >= 5)
                    {
                        break;
                    }
                    else
                    {
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
