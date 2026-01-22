using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class FlowDetailManager
    {
        public static byte Insert(FlowDetailInfo makeInfo)
        {
            return FlowDetailDAL.Insert(makeInfo);
        }

        public static byte Update(FlowDetailInfo makeInfo)
        {
            return FlowDetailDAL.Update(makeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return FlowDetailDAL.Delete(exchangeRateId);
        }

        public static FlowDetailInfo Get(long exchangeRateId)
        {
            return FlowDetailDAL.Get(exchangeRateId);
        }

        public static FlowDetailInfoCollection GetAll()
        {
            return FlowDetailDAL.GetAll();
        }

        public static FlowDetailInfoCollection Search(FlowDetailSearchParams calldetailSearchParams, out long totalRows)
        {
            return FlowDetailDAL.Search(calldetailSearchParams, out totalRows);
        }
    }
}
