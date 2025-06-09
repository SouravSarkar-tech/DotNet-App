using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User_Master
/// </summary>


namespace Accenture.MWT.DomainObject
{
    public class User_Master : Base
    {
        #region Fields

        public int User_Id { get; set; }
        public int Profile_Id { get; set; }
        public int Country_Id { get; set; }
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Full_Name { get; set; }
        public string EmailId { get; set; }
        public string ReportingTo_Name { get; set; }
        public string ReportingTo_Email { get; set; }
        public string Location { get; set; }
        public string Cost_Centre { get; set; }
        public string ContactNo { get; set; }
        public string Plant_Id { get; set; }
        public int IsActive { get; set; }
        public string Last_Login_On { get; set; }
        public string Last_Login_IP { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedIP { get; set; }
        public string ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedIP { get; set; }


        #endregion
    }
}