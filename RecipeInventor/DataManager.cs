using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeInventor.Models;
using LiteDB;

namespace RecipeInventor
{
    public class DataManager
    {
        public static void InitialiseDatabase()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<Ingredient> ingredients = db.GetCollection<Ingredient>();
                ingredients.EnsureIndex(x => x.Name);
                LiteCollection<FlavourAttribute> flavourAttributes = db.GetCollection<FlavourAttribute>();
                flavourAttributes.EnsureIndex(x => x.Description);
                LiteCollection<IngredientStatistics> ingredientStatistics = db.GetCollection<IngredientStatistics>();
                ingredientStatistics.EnsureIndex(x => x.Ingredient);
            }
        }
        public static void SaveIngredient(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<Ingredient> ingredients = db.GetCollection<Ingredient>();
                ingredients.Insert(ingredient);
            }
        }

        public static void DeleteIngredient(string name)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<Ingredient> ingredients = db.GetCollection<Ingredient>();
                ingredients.Delete(x=> x.Name == name);
            }
        }

        public static void UpdateIngredient(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                bool ingredients = db.GetCollection<Ingredient>().Update(ingredient);
            }
        }

        public static List<Ingredient> GetIngredients()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                var ingredients = db.GetCollection<Ingredient>().FindAll().OrderBy(x => x.Name);
                return ingredients.ToList();
            }
        }

        public static List<Recipe> GetRecipes()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                IEnumerable<Recipe> recipes = db.GetCollection<Recipe>().FindAll();
                return recipes.ToList();
            }
        }

        public static IngredientStatistics GetIngredientStatistics(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                return db.GetCollection<IngredientStatistics>().Find(x => x.Ingredient == ingredient).FirstOrDefault();
            }
        }

        public static void InsertIngredientStatistics(IngredientStatistics ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                db.GetCollection<IngredientStatistics>().Insert(ingredient);
            }
        }

        public static List<IngredientStatistics> GetIngredientStatistics()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                return db.GetCollection<IngredientStatistics>().FindAll().ToList();
            }
        }

        public static void AddIngredientStatistics(IngredientStatistics ingredientStatistics)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<IngredientStatistics> i = db.GetCollection<IngredientStatistics>();
                i.Insert(ingredientStatistics);
            }
        }

        public static void AddFlavourAttributes(FlavourAttribute flavourAttribute)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                var flavourAttributes = db.GetCollection<FlavourAttribute>();
                flavourAttributes.Insert(flavourAttribute);
            }
        }
    }
}
