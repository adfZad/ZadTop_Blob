using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class DocumentsStatusInfo
    {
        public long Id { get; set; }
        public long DocumentId { get; set; }
        public long StatusId { get; set; }
        public string StatusName { get; set; }
        public string Date { get; set; }
        public string Code { get; set; }
        public long AmendNo { get; set; }
        public string Name { get; set; }
        public long FlowId { get; set; }
        public string FlowName { get; set; }
        public long DivisionId { get; set; }
        public string DivisionName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public long DocumentStatusId { get; set; }
        public string DocumentStatusName { get; set; }
        public decimal Amount { get; set; }
        public decimal OtherAmount { get; set; }
        public string Currency { get; set; }
        public decimal Rate { get; set; }
        public string Primary { get; set; }
        public string One { get; set; }
        public string Two { get; set; }
        public string Three { get; set; }
        public string Four { get; set; }
        public string Five { get; set; }
        public string Six { get; set; }
        public string OneName { get; set; }
        public string TwoName { get; set; }
        public string ThreeName { get; set; }
        public string FourName { get; set; }
        public string FiveName { get; set; }
        public string SixName { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string AltDescription { get; set; }
        public bool Active { get; set; }
        public long CreatedId { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedTime { get; set; }
        public long UpdatedId { get; set; }
        public string UpdatedName { get; set; }
        public DateTime UpdatedTime { get; set; }

        public string DocumentFullName
        {
            get
            {
                return string.Format("{0}-{1}", Name, AmendNo);
            }
        }

        public string NameDescription
        {
            get
            {
                return string.Format("{0}-{1}", Name, Description);
            }
        }
        public string NameAltDescription
        {
            get
            {
                return string.Format("{0}-{1}", Name, AltDescription);
            }
        }
        
    }
}

