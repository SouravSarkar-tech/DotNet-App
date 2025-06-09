using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accenture.MWT.LDAPHelper
{
    public class Approve
    {
        //private String _requestno;

        //public String Request_No
        //{
        //    get { return _requestno; }
        //}

        //private String _department;

        //public String Department
        //{
        //    get { return _department; }
        //}

        //private String _approveby;

        //public String ApproveBy
        //{
        //    get { return _approveby; }
        //}
        public string Request_No { get; set; }
        public string Department { get; set; }
        public string ApproveBy { get; set; }
        public string sRemarks { get; set; }
        public string stxtRemarks { get; set; }
        public string ApproveByAdmin { get; set; }
    }

    public class UpdateBOMFB
    {
        public string Request_No { get; set; }
        public string sRemarks { get; set; }
        public string stxtRemarks { get; set; }
        public string CreatedBy { get; set; }
        public string AltBOMCur { get; set; }
        public string Recipe_GroupCur { get; set; }
        public string ProdVersionNoCur { get; set; }
        public string GroupCntrCur { get; set; }
        public string SAP_BOM_NoCur { get; set; }

    }

    public class UpdatePassword
    {
        public string SAPUserID { get; set; }
        public string SAPPassword { get; set; }
        public string SAPConfirmPassword { get; set; }
        public string CreatedBy { get; set; }
    }
}
