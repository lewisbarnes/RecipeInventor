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
using RecipeInventor.Models;

namespace RecipeInventor
{
    /// <summary>
    /// Interaction logic for AddRecipePage.xaml
    /// </summary>
    public partial class AddRecipePage : Page
    {
        public AddRecipePage()
        {
            InitializeComponent();
            IngredientsComboBox.ItemsSource = DataManager.GetIngredients();
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            IngredientsList.Items.Add(new RecipeIngredient((Ingredient)IngredientsComboBox.SelectedItem, Convert.ToDouble(QuantityTextBox.Text)));
        }

        private void IngredientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ingredient selection = (Ingredient)IngredientsComboBox.SelectedItem;
        }

        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsList.Items.IsEmpty) return;
            List<RecipeIngredient> ingredients = new List<RecipeIngredient>();
            Recipe recipeToSave = new Recipe(RecipeNameTextBox.Text);
            foreach (var i in IngredientsList.Items)
            {
                if(!recipeToSave.AddIngredient((RecipeIngredient)i))
                {
                    InfoBlock.Foreground = new SolidColorBrush(Colors.Red);
                    InfoBlock.Text = $"INGREDIENT {i} ADDED MULTIPLE TIMES, PLEASE CORRECT";
                    return;
                }
            }
            DataManager.SaveRecipe(recipeToSave);
            foreach(RecipeIngredient ri in recipeToSave.Ingredients)
            {
                ri.Ingredient.CalculateMeanQuantity();
                ri.Ingredient.CalculateRelativeFrequency();
                ri.Ingredient.CalculateStandardDeviation();
                ri.Ingredient.CalculateIngredientComplements();
            }
            InfoBlock.Foreground = new SolidColorBrush(Colors.Green);
            InfoBlock.Text = $"SUCCESSFULLY SAVED RECIPE \"{recipeToSave}\"";
        }
    }
}
