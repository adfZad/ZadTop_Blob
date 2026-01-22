using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class CustomerSearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public long CreatedId { get; set; }
        public long UpdateId { get; set; }
        public long CategoryId { get; set; }
        public long CountryId { get; set; }
        public string PassportNo { get; set; }
        public string QID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
