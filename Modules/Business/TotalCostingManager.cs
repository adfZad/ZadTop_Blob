using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class TotalCostingManager
    {
        public static TotalCostingInfoCollection GetTotalCostingData()
        {
            TotalCostingInfoCollection totalCostingCollection = TotalCostingDAL.GetTotalCostingData();
            return totalCostingCollection;
        }
    }
}
