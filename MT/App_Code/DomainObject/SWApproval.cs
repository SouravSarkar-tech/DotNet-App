using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SWApproval
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class SWApproval : Base
    {
        public int SWApproval_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string UserName { get; set; }
        public string Dept { get; set; }
        public string InstallLocation { get; set; }
        public string OtherLoc { get; set; }
        public string InstallDept { get; set; }
        public string OtherDept { get; set; }
        public string SWName { get; set; }
        public string Manufacturer { get; set; }
        public string MnfrWebsite { get; set; }
        public string MnfrCntctName { get; set; }
        public string MnfrEmail { get; set; }
        public string MnfrCntctNo { get; set; }
        public string SWCost { get; set; }
        public string SWUse { get; set; }
        public string BusinessJustification { get; set; }
        public string InstalledServer { get; set; }
        public string ServerQty { get; set; }
        public string PCLapReq { get; set; }
        public string PCLapQty { get; set; }
        public string ExpectedUsers { get; set; }
        public string ApproxSize { get; set; }
        public string NoOfPagesPD { get; set; }
        public string Requirements { get; set; }
        public string SecurityIssues { get; set; }
        public string ITReqCost { get; set; }
        public string TotalCost { get; set; }
        public string ITRemarks { get; set; }
        public int IsActive { get; set; }

    }

    public class MISCData
    {
        public int SWApp_Org_Data_Id { get; set; }
        public int Master_Header_Id { get; set; }
        public string Panel1MISC { get; set; }
        public string Panel2MISC { get; set; }
        public string Panel3MISC { get; set; }
        public int IsActive { get; set; }
    }
}
