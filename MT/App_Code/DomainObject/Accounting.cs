using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Accenture.MWT.DomainObject;

/// <summary>
/// Summary description for Accounting1
/// </summary>
/// 
namespace Accenture.MWT.DomainObject
{
    public class Accounting1 : Base
    {
        public int Mat_Accounting1_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Valuation_Type { get; set; }
        public string Valuation_Category { get; set; }
        public string Valuation_Class { get; set; }
        public string Price_Ctrl_Indicator { get; set; }
        public string Moving_Avg_Price { get; set; }
        public string Standard_Price { get; set; }
        public string Price_Unit { get; set; }
        public int IsActive { get; set; }
    }

    public class Accounting2 : Base
    {
        public int Mat_Accounting2_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Plant_Id { get; set; }

        public string Tax_Price1 { get; set; }
        public string Tax_Price2 { get; set; }
        public string Tax_Price3 { get; set; }

        public string Commercial_Price1 { get; set; }
        public string Commercial_Price2 { get; set; }
        public string Commercial_Price3 { get; set; }

        public int Relevant { get; set; }
        public string Pool_No_LIFO_Valuation { get; set; }
        public int IsActive { get; set; }
    }
}