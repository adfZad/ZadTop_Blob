using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been.Enums;

namespace ZadHolding.Been
{
    public class FlowTemplateSearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AltDescription { get; set; }
        public long FlowId { get; set; }
        public long UserId { get; set; }
        public long LevelId { get; set; }
        public long CreatedId { get; set; }
        public long UpdateId { get; set; }
        public long MakeId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
