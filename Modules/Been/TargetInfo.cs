using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class TargetInfo
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long TargetQuantity { get; set; }
        public decimal Limit { get; set; }
        public string Description { get; set; }
    }
}
