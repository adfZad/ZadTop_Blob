using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class MarketPriceInfo
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CompanyCode { get; set; }
        public long CompanyId { get; set; }
        public decimal ClosingPrice { get; set; }
        public long Volume { get; set;}
        public string Description { get; set; }
    }
}
