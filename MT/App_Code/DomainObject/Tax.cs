using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tax
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class Taxes : Base
    {
        public int Mat_Tax_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Sales_Organization_Id { get; set; }
        public string Plant_Id { get; set; }
        public string Distribution_Channel_ID { get; set; }
        public string Tax_Category { get; set; }
        public string Tax_Classification_Mat { get; set; }
        public string Tax_Category1 { get; set; }
        public string Tax_Classification_Mat1 { get; set; }
        public string Tax_Category2 { get; set; }
        public string Tax_Classification_Mat2 { get; set; }
        public string Tax_Category3 { get; set; }
        public string Tax_Classification_Mat3 { get; set; }
        public string Tax_Category4 { get; set; }
        public string Tax_Classification_Mat4 { get; set; }
        public string Tax_Category5 { get; set; }
        public string Tax_Classification_Mat5 { get; set; }
        public string Tax_Category6 { get; set; }
        public string Tax_Classification_Mat6 { get; set; }
        public string Tax_Category7 { get; set; }
        public string Tax_Classification_Mat7 { get; set; }
        public string Tax_Category8 { get; set; }
        public string Tax_Classification_Mat8 { get; set; }
        public int IsActive { get; set; }

        
    }
}