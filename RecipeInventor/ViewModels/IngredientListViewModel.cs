using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using RecipeInventor.Models;

namespace RecipeInventor.ViewModels
{
    public class IngredientListViewModel : INotifyPropertyChanged
    {
        private List<Ingredient> data;
        public List<Ingredient> Data { get { return data; } set { if (value != this.data) { this.data = value; NotifyPropertyChanged(); }  } }
        public IngredientListViewModel()
        {
            data = DataManager.GetIngredients();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
