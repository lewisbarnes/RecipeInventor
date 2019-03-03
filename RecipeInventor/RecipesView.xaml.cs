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
    /// Interaction logic for RecipesView.xaml
    /// </summary>
    public partial class RecipesView : Page
    {
        public RecipesView()
        {
            InitializeComponent();
            RecipeList.ItemsSource = DataManager.GetRecipes();
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("AddRecipePage.xaml", UriKind.Relative));
        }

        private void btnRemoveRecipe_Click(object sender, RoutedEventArgs e)
        {
            DataManager.DeleteRecipe((Recipe)RecipeList.SelectedItem);
        }

        private void RecipeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RecipeIngredientsView((Recipe)RecipeList.SelectedItem));
        }
    }
}
