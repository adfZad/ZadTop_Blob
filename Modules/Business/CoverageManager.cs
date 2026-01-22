using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class CoverageManager
    {
        public static byte Insert(CoverageInfo coverageInfo)
        {
            return CoverageDAL.Insert(coverageInfo);
        }

        public static byte Update(CoverageInfo coverageInfo)
        {
            return CoverageDAL.Update(coverageInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CoverageDAL.Delete(exchangeRateId);
        }

        public static CoverageInfo Get(long exchangeRateId)
        {
            return CoverageDAL.Get(exchangeRateId);
        }

        public static CoverageInfoCollection GetAll()
        {
            return CoverageDAL.GetAll();
        }

        public static CoverageInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CoverageDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
