using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class FlowManager
    {
        public static byte Insert(FlowInfo flowInfo)
        {
            return FlowDAL.Insert(flowInfo);
        }

        public static byte Update(FlowInfo flowInfo)
        {
            return FlowDAL.Update(flowInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return FlowDAL.Delete(exchangeRateId);
        }

        public static FlowInfo Get(long exchangeRateId)
        {
            return FlowDAL.Get(exchangeRateId);
        }

        public static FlowInfoCollection GetAll()
        {
            return FlowDAL.GetAll();
        }

        public static FlowInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return FlowDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static FlowInfoCollection Search(FlowSearchParams flowSearchParams, out long totalRows)
        {
            return FlowDAL.Search(flowSearchParams, out totalRows);
        }
    }
}
