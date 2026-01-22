using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class MarketParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public long CompanyId { get; set; }
    }
}
