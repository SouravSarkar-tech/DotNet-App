using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceMasterCreate
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class PriceMasterCreate : Base
    {
        public int ID { get; set; }
        public int Master_Header_Id { get; set; }
        public string Material_Code { get; set; }
        public string Material_Desc { get; set; }
        public string Material_Group { get; set; }
        public string Batch { get; set; }
        public string ZMRP { get; set; }
        public string ZTRP { get; set; }
        public string ZSPL { get; set; }
        public string Unit { get; set; }
        public string Division { get; set; }
        public string IsActive { get; set; }
        public string MatORBatch { get; set; }
        public string dEffectivedate { get; set; }
        
    }
}