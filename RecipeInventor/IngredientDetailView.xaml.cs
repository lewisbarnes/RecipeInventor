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
    /// Interaction logic for IngredientDetailView.xaml
    /// </summary>
    public partial class IngredientDetailView : Page
    {
        public Ingredient Ingredient;
        public IngredientDetailView(IngredientDetailViewModel ingredient)
        {
            DataContext = ingredient;
            this.Ingredient = ingredient.OriginalObject;
            InitializeComponent();
        }

        public void EditIngredient_Click(object sender, EventArgs e)
        {
            EditIngredientPage p1 = new EditIngredientPage(Ingredient);
            NavigationService.GetNavigationService(this).Navigate(p1);
        }
    }
}
