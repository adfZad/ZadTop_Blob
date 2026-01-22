using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class TotalCostingInfo
    {
        public string Company { get; set; }
        public string Sector { get; set; }
        public string Country { get; set; }
        public long Quantity { get; set; }
        public decimal AvgCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
