using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;
using System.Data;

namespace ZadHolding.Business
{
    public class DividendManager
    {
        public static bool Insert(DividendInfo dividendInfo)
        {
            return DividendDAL.Insert(dividendInfo);
        }

        public static bool Update(DividendInfo dividendInfo)
        {
            return DividendDAL.Update(dividendInfo);
        }

        public static bool Delete(long dividendId)
        {
            return DividendDAL.Delete(dividendId);
        }

        public static DividendInfo Get(long dividendId)
        {
            return DividendDAL.Get(dividendId);
        }

        public static DividendInfoCollection GetAll()
        {
            return DividendDAL.GetAll();
        }

        public static DividendInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DividendDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }

        public static DividendInfoCollection Search(CallSearchParams callSearchParams, out long totalRows)
        {
            return DividendDAL.Search(callSearchParams, out totalRows);
        }
    }
}
