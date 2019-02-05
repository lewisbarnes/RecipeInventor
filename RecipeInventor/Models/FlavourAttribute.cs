using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeInventor.Models
{
    public class FlavourAttribute
    {
        public string Description { get; set; }
        public int Rating { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
