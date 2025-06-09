using Accenture.MWT.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CreditMgntData
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class CreaditMgntData : Base
    {
        public int CreditMgnt_Data_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Credit_Control { get; set; }
        public string Total_Amt { get; set; }
        public string Individual_Limit { get; set; }
        public string Customer_Acc_No { get; set; }
        public string Date_Batch { get; set; }
        public string Indicator_Blocked { get; set; }
        public string Credit_Group { get; set; }
        public string Date_Batch1 { get; set; }
        public string Credit_info_number { get; set; }
        public string Date_Batch2 { get; set; }
        public string Cust_Credit_Group { get; set; }
        public string Date_Batch3 { get; set; }
        public string Customer_Group { get; set; }
        public string Reco_credit_limit { get; set; }
        public string Currency_recommend { get; set; }
        public string Date_Batch4 { get; set; }
        public int IsActive { get; set; }
    }

}