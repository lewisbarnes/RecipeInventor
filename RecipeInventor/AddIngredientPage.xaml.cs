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
using System.ComponentModel;
using System.Reflection;

namespace RecipeInventor
{
    /// <summary>
    /// Interaction logic for AddIngredientPage.xaml
    /// </summary>
    public partial class AddIngredientPage : Page
    {
        public AddIngredientPage()
        {
            InitializeComponent();
            foreach(IngredientType i in Enum.GetValues(typeof(IngredientType)))
            {
                IngredientTypeCombo.Items.Add(i);
            }
        }

        private void SaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            List<ErrorCodes> errors = new List<ErrorCodes>();
            StringBuilder sb = new StringBuilder();

            if(IngredientNameTextBox.Text == String.Empty) errors.Add(ErrorCodes.EMPTYNAMEFIELD);
            if(errors.Count == 0)
            {
                var tempIngredient = new Ingredient();
                tempIngredient.SetAttributes(IngredientNameTextBox.Text, (IngredientType)IngredientTypeCombo.SelectedItem);
                DataManager.SaveIngredient(tempIngredient);
                InfoBlock.Foreground = new SolidColorBrush(Colors.Green);
                InfoBlock.Text = $"SUCCESSFULLY SAVED \"{tempIngredient.Name}\"";
            }
            else
            {
                foreach (ErrorCodes ec in errors)
                {
                    sb.Append($"{ec.GetDescription()}, ");
                }
                InfoBlock.Text = sb.ToString();
            }
        }
    }

    public static class ExtensionMethods
        {
        public static string GetDescription(this Enum currentEnum)
        {
            string description = String.Empty;
            DescriptionAttribute da;

            FieldInfo fi = currentEnum.GetType().
                        GetField(currentEnum.ToString());
            da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi,
                        typeof(DescriptionAttribute));
            if (da != null)
                description = da.Description;
            else
                description = currentEnum.ToString();

            return description;
        }
    }
}
