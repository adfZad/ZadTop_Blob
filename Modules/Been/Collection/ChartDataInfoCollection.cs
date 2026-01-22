using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZadHolding.Been
{
    public class ChartDataInfoCollection: List<ChartDataInfo>
    {
        public string ChartTitle { get; set; }

        public ChartDataInfoCollection() 
        {
            ChartTitle = string.Empty;
        }
    }
}
