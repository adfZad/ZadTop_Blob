using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class AgencyInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long AgencyTypeId { get; set; }
        public string AgencyType { get; set; }
        public string Description { get; set; }
    }
}
