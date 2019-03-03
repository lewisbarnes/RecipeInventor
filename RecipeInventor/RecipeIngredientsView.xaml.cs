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
    /// Interaction logic for RecipeIngredientsView.xaml
    /// </summary>
    public partial class RecipeIngredientsView : Page
    {
        public Recipe m_recipe { get; set; }
        public RecipeIngredientsView(Recipe recipe)
        {
            InitializeComponent();
            m_recipe = recipe;
            IngredientList.ItemsSource = m_recipe.Ingredients;
            RecipeTitle.Content = "Recipe - " + m_recipe.Name;
        }
    }
}
