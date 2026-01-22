using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class FlowHeaderManager
    {
        public static byte Insert(FlowHeaderInfo makeInfo)
        {
            return FlowHeaderDAL.Insert(makeInfo);
        }

        public static byte Update(FlowHeaderInfo makeInfo)
        {
            return FlowHeaderDAL.Update(makeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return FlowHeaderDAL.Delete(exchangeRateId);
        }

        public static FlowHeaderInfo Get(long exchangeRateId)
        {
            return FlowHeaderDAL.Get(exchangeRateId);
        }

        public static FlowHeaderInfoCollection GetAll()
        {
            return FlowHeaderDAL.GetAll();
        }

        public static FlowHeaderInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return FlowHeaderDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static FlowHeaderInfoCollection Search(FlowHeaderSearchParams makeSearchParams, out long totalRows)
        {
            return FlowHeaderDAL.Search(makeSearchParams, out totalRows);
        }
    }
}

