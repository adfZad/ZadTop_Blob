using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class CompanySearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public long CreatedId { get; set; }
        public long UpdateId { get; set; }
        public long MakeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
