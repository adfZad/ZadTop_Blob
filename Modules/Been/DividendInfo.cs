using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class DividendInfo
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CompanyId { get; set; }
        public string Company { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountQR { get; set; }
        public long BeneficiaryId { get; set; }
        public string Beneficiary { get; set; }
        public long BrokerId { get; set; }
        public string Broker { get; set; }
        public long CurrencyId { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
