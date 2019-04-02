using RecipeInventor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeInventor
{
    /// <summary>
    /// Interaction logic for GenerateRecipePage.xaml
    /// </summary>
    public partial class GenerateRecipePage : Page
    {
        Ingredient startingIngredient;
        GeneratedRecipe generatedRecipe;
        IngredientGraph graph = new IngredientGraph();
        Random rand = new Random();
        List<Ingredient> ingredients = DataManager.GetIngredients();
        RecipeWriter writer = new RecipeWriter();
        public GenerateRecipePage()
        {
            InitializeComponent();
            IngredientsComboBox.ItemsSource = ingredients;
        }

        private void IngredientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startingIngredient = (Ingredient)IngredientsComboBox.SelectedItem;
        }

        private void btnGenerateRecipe_Click(object sender, RoutedEventArgs e)
        {
            // If user does not pick an ingredient to start with, select one at random
            if(startingIngredient == null)
            {
                startingIngredient = ingredients[rand.Next(ingredients.Count())];
            }
            generatedRecipe = graph.GenerateRecipeFromGraph(startingIngredient);
            IngredientsList.ItemsSource = generatedRecipe.Ingredients;
            EdgesList.ItemsSource = generatedRecipe.Edges;
            writer.WriteRecipeToFile(generatedRecipe);
            IngredientsComboBox.SelectedItem = null;
            startingIngredient = null;
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            generatedRecipe.Name = $"{generatedRecipe.Ingredients[0].Ingredient.Name} {DateTime.UtcNow.Millisecond.ToString()}" +
                $"_{DateTime.UtcNow.Second.ToString()}" +
                $"_{DateTime.UtcNow.Minute.ToString()}" +
                $"_{DateTime.UtcNow.Hour.ToString()}";
            foreach (RecipeIngredient ri in generatedRecipe.Ingredients)
            {
                
                DataManager.UpdateIngredient(ri.Ingredient);
            }
            DataManager.SaveRecipe(generatedRecipe);
        }
    }
}
