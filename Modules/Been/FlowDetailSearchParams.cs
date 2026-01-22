using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been.Enums;

namespace ZadHolding.Been
{
    public class FlowDetailSearchParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; } 
        public long CreatedId { get; set; }
        public long FlowTemplateId { get; set; }
        public long FlowId { get; set; }
        public long UserId { get; set; }
        public long LevelId { get; set; }
        
       
       
    }
}
