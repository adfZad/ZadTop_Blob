using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class ClaimInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long VehicleId { get; set; }
        public string Plate_No { get; set; }
        public long CauseId { get; set; }
        public string Cause { get; set; }
        public long HandlerId { get; set; }
        public string Handler { get; set; }
        public DateTime AccidentDate { get; set; }
        public DateTime DeclareDate { get; set; }
        public string Label { get; set; }
        public decimal EstimatedValue { get; set; }
        public long StatusId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
