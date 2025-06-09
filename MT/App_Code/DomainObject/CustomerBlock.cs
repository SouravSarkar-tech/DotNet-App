using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CustomerBlock
/// </summary>
/// 

namespace Accenture.MWT.DomainObject
{
    public class CustomerBlock : Base
    {
        public int Customer_Block_Id { get; set; }
        public int Master_Header_Id { get; set; }

        public string Customer_Code { get; set; }
        public string Customer_Desc { get; set; }
        public string Company_Code { get; set; }
        public string Customer_Acc_Grp { get; set; }
        public string Customer_Category { get; set; }

        public string Sales_Organisation_Id { get; set; }
        public string Distribution_Channel_Id { get; set; }
        public string Division_Id { get; set; }

        public string IsAllCompanyBlock { get; set; }
        public string IsSelectedCompanyBlock { get; set; }
        public string IsAllSalesAreaOrderBlock { get; set; }
        public string IsSelectedSalesAreaOrderBlock { get; set; }
        public string IsAllSalesAreaDeliveryBlock { get; set; }
        public string IsSelectedSalesAreaDeliveryBlock { get; set; }
        public string IsAllSalesAreaBillingBlock { get; set; }
        public string IsSelectedSalesAreaBillingBlock { get; set; }
        public string IsAllSalesAreaBlockSalesSupport { get; set; }
        public string IsSelectedSalesAreaBlockSalesSupport { get; set; }
        public string Remarks { get; set; }
        public string IsActive { get; set; }
    }
}