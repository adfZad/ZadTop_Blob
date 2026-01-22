using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class ExchangeRateInfo
    {
        public long Id { get; set; }
        public DateTime  CreatedDate { get; set; }
        public decimal Rate { get;set;}
        public long CurrencyId {get;set;}
        public string CurrencyCode { get; set; }
        public string Currency { get; set;}
        public string Symbol{get;set;}
        public string Description { get; set;}
    }
}
