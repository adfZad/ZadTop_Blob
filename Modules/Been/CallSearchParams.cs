using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been.Enums;    

namespace ZadHolding.Been
{
    public class CallSearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public long CompanyId { get; set; }
        public long SectorId { get; set; }
        public long CountryId { get; set; }
        public TransactionType Type { get; set; }
        public long BeneficiaryId { get; set; }
        public long BrokerId { get; set; }
        public long CurrencyId { get; set; }
        public long CustomerId { get; set; }
        public long VehicleId { get; set; }
        public long PolicyId { get; set; }

    }
}
