using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DasSystemInfo
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class DasSystemInfo : Base
    {
        public DasSystemInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string ID { get; set; }
        public string Master_Header_Id { get; set; }
        public string Depot { get; set; }
        public string Division { get; set; }
        public string Territory { get; set; }
        public string Structure_Of_Firm { get; set; }
        public string NameOfProprietor { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_Addr { get; set; }
        public string Avail_Cheque { get; set; }
        public string Transporter_Name { get; set; }
        public string Add_Or_Replacement { get; set; }
        public string Years_Pharma_Distr { get; set; }
        public string Channel_Of_Distr { get; set; }
        public string Company_Party_Distr_Id { get; set; }
        public string Turnover_Three_Years { get; set; }
        public string Bank_Stmt_Submitted { get; set; }
        public string Expected_Monthly_Sales { get; set; }
        public string Justification_For_Appt { get; set; }
        public string Cur_Territory_Sales { get; set; }
        public string Dist_In_Territory { get; set; }
        public string Sales_Ratio_Per_Dist { get; set; }
        public string StockValue { get; set; }
        public string Outstanding_0_30 { get; set; }
        public string Outstanding_31_60 { get; set; }
        public string Outstanding_61_90 { get; set; }
        public string Outstanding_91_180 { get; set; }
        public string Outstanding_Age_180 { get; set; }
        public string Outstanding_Replacement { get; set; }
        public string Feeback { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string CreatedIP { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public string ModifiedIP { get; set; }
        public string RDMLoggedInUserDeptId { get; set; }
    }

    public class DasSystemInfoDetail : Base
    {
        public DasSystemInfoDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string ID { get; set; }
        public string Company_Party_Distr_Id { get; set; }
        public string Company_Name { get; set; }
        public string Monthly_Turnover { get; set; }
        
    }

}