
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialCreateExtension
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class MaterialBlock : Base
    {
        public int Material_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Number { get; set; }
        public string Material_Type { get; set; }
        public string Material_Short_Description { get; set; }
        public string Blocking_Level { get; set; }
        public string Plant_Id { get; set; }
        public string Storage_Location { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Material_Status { get; set; }
        public string Purchase_Status { get; set; }
        public string Remarks { get; set; }
        public string IsActive { get; set; }
    }
}