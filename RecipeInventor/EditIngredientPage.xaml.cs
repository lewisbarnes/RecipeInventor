﻿using System;
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
    /// Interaction logic for EditIngredientPage.xaml
    /// </summary>
    public partial class EditIngredientPage : Page
    {
        public Ingredient ingredientToEdit { get; set; }
        public EditIngredientPage(Ingredient ingredient)
        {
            
            ingredientToEdit = ingredient;
            InitializeComponent();
            foreach (MeasuringUnits u in Enum.GetValues(typeof(MeasuringUnits)))
            {
                MeasuringUnitCombo.Items.Add(u);
            }
            foreach (IngredientType i in Enum.GetValues(typeof(IngredientType)))
            {
                IngredientTypeCombo.Items.Add(i);
                
            }
            DataContext = ingredientToEdit;
            TypicalQuantityTextBox.Text = ingredientToEdit.TypicallyUsedQuantity.ToString();

        }

        private void SaveIngredient_Click(object sender, RoutedEventArgs e)
        {
            List<ErrorCodes> errors = new List<ErrorCodes>();
            StringBuilder sb = new StringBuilder();

            if (MeasuringUnitCombo.SelectedItem == null) errors.Add(ErrorCodes.MEASURINGUNITNOTSELECTED);
            if (TypicalQuantityTextBox.Text == String.Empty) errors.Add(ErrorCodes.EMPTYQUANTITYFIELD);
            if (IngredientNameTextBox.Text == String.Empty) errors.Add(ErrorCodes.EMPTYNAMEFIELD);
            if (errors.Count == 0)
            {
                ingredientToEdit.SetAttributes(IngredientNameTextBox.Text, (IngredientType)IngredientTypeCombo.SelectedItem, null,
                    Convert.ToDouble(TypicalQuantityTextBox.Text), (MeasuringUnits)MeasuringUnitCombo.SelectedItem);
                DataManager.UpdateIngredient(ingredientToEdit);
                InfoBlock.Foreground = new SolidColorBrush(Colors.Green);
                InfoBlock.Text = $"SUCCESSFULLY EDITED \"{ingredientToEdit.Name}\"";
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
}
