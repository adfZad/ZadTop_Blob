using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been.Enums;    

namespace ZadHolding.Been
{
    public class InvestmentInfo
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CompanyId { get; set; }
        public string Company { get; set; }
        public long SectorId { get; set; }
        public string Sector { get; set; }
        public long CountryId { get; set; }
        public string Country { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Brokerage { get; set; }
        public long CurrencyId { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public decimal ExchangeRate { get; set; }
        public long BeneficiaryId { get; set; }
        public string Beneficiary { get; set; }
        public TransactionType Type { get; set; }
        public long BrokerId {get;set;}
        public string Broker { get; set;}
        public decimal TCQAR { get; set; }
        public decimal TCLocal { get; set; }
        public string Remarks { get; set; }
        public string ReferenceNo { get; set; }
    }
}
