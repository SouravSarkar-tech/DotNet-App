using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accenture.MWT.DomainObject
{
    public class Base
    {
        public string UserId { get; set; }
        public string TodayDate { get; set; }
        public string IPAddress { get; set; }
        public string Mode { get; set; }
        public string SectionCode { get; set; }
        public string PlantId { get; set; }
        public string PlantName { get; set; }
        public string ModulePlantGroupCode { get; set; }
    }
}