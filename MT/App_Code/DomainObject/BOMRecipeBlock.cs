using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BOMRecipeBlock
/// </summary>

namespace Accenture.MWT.DomainObject
{
    public class BOMRecipeBlock :Base
    {
        public int BOMRecipe_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Number { get; set; }      
        public string Plant_Id { get; set; }
        public string Recipe_Group { get; set; }
        public string Status { get; set; }
        public string AlternativeBOM { get; set; }
        public string BOMStatus { get; set; }
        public string ProdVersionNo { get; set; }
        public string Lock { get; set; }
        public string Remarks { get; set; }
        public string IsActive { get; set; }        
    }
}