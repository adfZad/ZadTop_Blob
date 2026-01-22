using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been.Enums;    

namespace ZadHolding.Been
{
    public class PolicySearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public TransactionType Type { get; set; }
        public long CustomerId { get; set; }
        public long VehicleId { get; set; }
        public long PolicyId { get; set; }
        public long MakeId { get; set; }
        public long ModelId { get; set; }
        public long VariantId { get; set; }
        public long TypeId { get; set; }
        public long ModelYear { get; set; }
        public long Cylinders { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }

    }
}
