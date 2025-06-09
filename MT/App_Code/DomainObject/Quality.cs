using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Quality
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class Quality : Base
    {
        public int Mat_Quality_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Unit_Issue { get; set; }
        public string Is_QM_in_Procurement { get; set; }
        public string Certificate_Type { get; set; }
        public string Ctrl_Key_QM_Procurement { get; set; }
        public string Is_Doc_Required { get; set; }
        public string Catlog_Profile { get; set; }
        public string Mat_Auth_Grp_Activities { get; set; }
        public string Interval_Nxt_Inspection { get; set; }
        public string Inspection_Type { get; set; }

        public string Min_Remaining_Shelf_Life { get; set; }
        public string Total_Shelf_Life_Days { get; set; }

        public int IsActive { get; set; }
    }

    //New Addition for HU tick Start
    //public class InspData : Base
    //{
    //    public int Mat_InspData_Id { get; set; }
    //    public int Master_Header_Id { get; set; }
    //    public string InspectionType { get; set; }
    //    public string PostInspStock { get; set; }
    //    public string InspHU { get; set; }
    //    public int IsActive { get; set; }
    //}
    //New Addition for HU tick End
}