﻿using System;
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

                BsonMapper mapper = new BsonMapper();
                mapper.Entity<Recipe>()
                    .DbRef(x => x.Ingredients, "ingredients");

                mapper.Entity<RecipeIngredient>()
                    .DbRef(x => x.Ingredient, "ingredients");

            }
        }
        public static void SaveIngredient(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<Ingredient> ingredients = db.GetCollection<Ingredient>("ingredients");
                ingredients.Insert(ingredient);
            }
        }

        public static void DeleteIngredient(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                LiteCollection<Ingredient> ingredients = db.GetCollection<Ingredient>("ingredients");
                ingredients.Delete(x=> x.Id == ingredient.Id);
            }
        }

        public static void UpdateIngredient(Ingredient ingredient)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                ingredient.RecalculateStatistics();
                bool ingredients = db.GetCollection<Ingredient>("ingredients").Update(ingredient);
            }
        }

        public static List<Ingredient> GetIngredients()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                var ingredients = db.GetCollection<Ingredient>("ingredients").FindAll().OrderBy(x => x.Name).ToList();
                return ingredients;
            }
        }

        public static Ingredient GetIngredientByID(int id)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                Ingredient ingredient = db.GetCollection<Ingredient>("ingredients").FindById(id);
                return ingredient;
            }
        }

        public static List<Recipe> GetRecipes()
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                IEnumerable<Recipe> recipes = db.GetCollection<Recipe>("recipes").FindAll();
                return recipes.ToList();
            }
        }

        public static void SaveRecipe(Recipe recipe)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {

                db.GetCollection<Recipe>("recipes").Insert(recipe);
                foreach (RecipeIngredient ri in recipe.Ingredients)
                {
                    UpdateIngredient(ri.Ingredient);
                }
            }
        }

        public static void DeleteRecipe(Recipe recipe)
        {
            using (LiteDatabase db = new LiteDatabase("IngredientData.db"))
            {
                db.GetCollection<Recipe>("recipes").Delete(x => x.Id == recipe.Id);
                foreach (RecipeIngredient ri in recipe.Ingredients)
                {
                    UpdateIngredient(ri.Ingredient);
                }
            }
        }
    }
}
