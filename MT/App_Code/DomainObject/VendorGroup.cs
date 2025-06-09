using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    public class VendorGroup : Base
    {
        public long Vendor_Group_ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedIp { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public string ModifiedIp { get; set; }
    }
}