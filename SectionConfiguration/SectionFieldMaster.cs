using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SectionConfiguration
{
    public class SectionFieldMaster
    {
        public int ID { get; set; }
        public int SectionID { get; set; }
        public string FieldName { get; set; }
        public string FieldDescription { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string IPAddress { get; set; }
        public Nullable<int> FieldStatus { get; set; }
    }
}
