using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class CauseManager
    {
        public static byte Insert(CauseInfo causeInfo)
        {
            return CauseDAL.Insert(causeInfo);
        }

        public static byte Update(CauseInfo causeInfo)
        {
            return CauseDAL.Update(causeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CauseDAL.Delete(exchangeRateId);
        }

        public static CauseInfo Get(long exchangeRateId)
        {
            return CauseDAL.Get(exchangeRateId);
        }

        public static CauseInfoCollection GetAll()
        {
            return CauseDAL.GetAll();
        }

        public static CauseInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CauseDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
