using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RecipeInventor.Models
{
    public enum ErrorCodes
    {
        [Description("The name field is empty")]
        EMPTYNAMEFIELD,
        [Description("No flavour profiles have been added")]
        NOFLAVOURPROFILES,
        [Description("The quantity field is empty")]
        EMPTYQUANTITYFIELD,
        [Description("A measuring unit has not been selected")]
        MEASURINGUNITNOTSELECTED
    }
}
