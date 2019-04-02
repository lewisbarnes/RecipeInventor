using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using RecipeInventor.ViewModels;
using System.Windows.Shell;

namespace RecipeInventor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataManager.InitialiseDatabase();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }

        private void MinimiseButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
            }
            
        }

        private void IngredientsLink_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new Uri("IngredientsView.xaml", UriKind.Relative));
        }

        private void RecipesLink_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new Uri("RecipesView.xaml", UriKind.Relative));
        }

        private void FlavoursLink_Click(object sender, RoutedEventArgs e)
        {
        }

        private void StatsLink_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new Uri("IngredientStatisticsView.xaml", UriKind.Relative));
        }

        private void GenLink_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new Uri("GenerateRecipePage.xaml", UriKind.Relative));
        }
    }
}
