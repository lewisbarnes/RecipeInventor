using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RecipeInventor.Models
{
    /// <summary>
    /// Base ingredient class to be used for all further ingredients created
    /// </summary>
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientType Type { get; set; }
        public List<FlavourAttribute> FlavourProfiles { get; private set; }
        public double TypicallyUsedQuantity { get; set; }
        public MeasuringUnits UnitOfMeasurement { get; set; }
        public Ingredient()
        {
        }
        public void SetAttributes(string name, IngredientType type, List<FlavourAttribute> flavourAttributes, double typicallyUsedQuantity, MeasuringUnits units)
        {
            Name = name;
            Type = type;
            FlavourProfiles = flavourAttributes;
            TypicallyUsedQuantity = typicallyUsedQuantity;
            UnitOfMeasurement = units;
        }
        public override string ToString()
        {
            return Name;
        }

        public void AddFlavourProfile(FlavourAttribute profile)
        {
            FlavourProfiles.Add(profile);
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
