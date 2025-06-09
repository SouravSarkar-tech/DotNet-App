using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkScheduling
/// </summary>
namespace Accenture.MWT.DomainObject
{
    public class WorkScheduling : Base
    {
        public int Mat_Work_Scheduling_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Plant_Id { get; set; }
        public string Unit_Of_Issue { get; set; }
        public string Production_Unit { get; set; }
        public string Production_Supervisor { get; set; }
        public string Prod_Sched_Profile { get; set; }
        public string Underdelivered_Tolerance_Lmt { get; set; }
        public string Overdelivered_Tolerance_Lmt { get; set; }
        public string Unlimited { get; set; }
        public string Serial_No_Profile { get; set; }
        public string Repetitive_Mfg_Profile { get; set; }

        public int IsActive { get; set; }
    }
}