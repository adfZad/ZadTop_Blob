using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class AllocationInfo
    {
        public long Id { get; set; }
        public long ClaimId { get; set; }
        public string Claim { get; set; }
        public long RepairId { get; set; }
        public string Repair { get; set; }
        public DateTime AllocationDate { get; set; }
        public string Description { get; set; }
    }
}
