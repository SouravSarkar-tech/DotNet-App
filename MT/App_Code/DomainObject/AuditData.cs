using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AuditData
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class AuditData : Base
    {
        public int EAudit_Form_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Location { get; set; }
        public string RequestDate { get; set; }
        public string Priority { get; set; }
        public string Department { get; set; }
        public string Spec_Dept { get; set; }
        public string Market { get; set; }
        public string Spec_Market { get; set; }
        public string Customer_Code { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string HouseNo_Street { get; set; }
        public string Street4 { get; set; }
        public string Street5 { get; set; }
        public string PO_Box { get; set; }
        public string City { get; set; }
        public string Postal_Code { get; set; }
        public string District { get; set; }
        public string PO_Box_Postal_Code { get; set; }
        public string CountryKey { get; set; }
        public string Region { get; set; }
        //public string AnalysisPerformed { get; set; }
        //public string AnalysisMethod { get; set; }
        public string Contact_Name { get; set; }
        public string Mobile_Num { get; set; }
        public string First_Tele_No { get; set; }
        public string Email_Address { get; set; }
        public string Audit_Reason { get; set; }
        public string Spec_Audit { get; set; }
        public string Prev_App_Status { get; set; }
        public string Remarks { get; set; }
        //public string RDTrialsOver { get; set; }
        //public string RDFeasibilityTrial { get; set; }
        //public string RDSpecsFreezed { get; set; }
        //public string RDSpecReq { get; set; }
        //public string RDMaterialCategory { get; set; }
        public string RDComments { get; set; }
        public string RADMF { get; set; }
        public string RAMatType { get; set; }
        public string RACatMod { get; set; }
        public string RAMatRedefined { get; set; }
        public string RAAuditNeeded { get; set; }
        public string RAJoinAudit { get; set; }

        //Start Adding Nitish Rao 10/08/2018
        public string RAComments { get; set; }
        public string RNDDMF { get; set; }
        public string RNDMatType { get; set; }
        public string RNDCatMod { get; set; }
        public string RNDMatRedefined { get; set; }
        public string RNDAuditNeeded { get; set; }
        public string RNDJoinAudit { get; set; }
        public string MobileExt { get; set; }
        //End Adding nitish Rao 10/08/2018
        //public string QASignOff { get; set; }
        //public string Justification { get; set; }
        public int IsActive { get; set; }

        


    }
    public class AuditMaterialsData : Base
    {
        public int E_Audit_Material_Id {get; set; }
        public int EAudit_Form_Id { get; set; }
        public int SerialNo { get; set; }
        public string Material_Name { get; set; }
        public string Product_Name { get; set; }
        public string LupinLoc { get; set; }
        public string Pharmacopical_Status { get; set; }
        public string AnalysisMethod { get; set; }
        public string MaterialCategory { get; set; }
        public int IsActive { get; set; }

        //Start Adding By Nitish Rao 10/08/2018 for Manufacture Changes
        public string OtherLC { get; set; }
        public string OtherPharmaStatus { get; set; }
        public string OtherMthdAnalysis { get; set; }
        public string OtherMatCategory { get; set; }
        //End Adding By Nitish Rao 10/08/2018 for Manufacture Changes

    }
    public class AuditProductsData : Base
    {
        public int E_Audit_Product_Id {get; set; }
        public int EAudit_Form_Id { get; set; }
        public int Serial_No { get; set; }
        public string Product_Name { get; set; }
        public int IsActive { get; set; }
    }
}
