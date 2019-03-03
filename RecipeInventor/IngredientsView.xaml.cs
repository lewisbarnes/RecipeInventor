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
using RecipeInventor.ViewModels;

namespace RecipeInventor
{
    /// <summary>
    /// Interaction logic for IngredientsView.xaml
    /// </summary>
    public partial class IngredientsView : Page
    {
        public List<Ingredient> Data;
        public IngredientsView()
        {
            InitializeComponent();
            foreach(Ingredient i in DataManager.GetIngredients())
            {
                i.CalculateIngredientComplements();
            }
            IngredientList.ItemsSource = DataManager.GetIngredients();

        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("AddIngredientPage.xaml", UriKind.Relative));
        }

        private void RemoveIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientList.SelectedItem != null)
            {
                var ingredient = IngredientList.SelectedItem.ToString();
                DataManager.DeleteIngredient(ingredient);
                IngredientList.ItemsSource = DataManager.GetIngredients();
                InfoBlock.Foreground = new SolidColorBrush(Colors.Green);
                InfoBlock.Text = $"SUCCESSFULLY REMOVED \"{ingredient}\"";
                IngredientList.SelectedItem = null;
            }
            else
            {
                InfoBlock.Foreground = new SolidColorBrush(Colors.Red);
                InfoBlock.Text = "ERROR REMOVING NULL ELEMENT";
            }

        }

        private void IngredientListItem_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientList.SelectedItem == null) return;
            
            IngredientDetailViewModel model = new IngredientDetailViewModel((Ingredient)IngredientList.SelectedItem);
            NavigationService.GetNavigationService(this).Navigate(new IngredientDetailView(model));
        }
    }
}

