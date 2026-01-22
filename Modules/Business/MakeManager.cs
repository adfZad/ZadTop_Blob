using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class MakeManager
    {
        public static byte Insert(MakeInfo makeInfo)
        {
            return MakeDAL.Insert(makeInfo);
        }

        public static byte Update(MakeInfo makeInfo)
        {
            return MakeDAL.Update(makeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return MakeDAL.Delete(exchangeRateId);
        }

        public static MakeInfo Get(long exchangeRateId)
        {
            return MakeDAL.Get(exchangeRateId);
        }

        public static MakeInfoCollection GetAll()
        {
            return MakeDAL.GetAll();
        }

        public static MakeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return MakeDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static MakeInfoCollection Search(MakeSearchParams makeSearchParams, out long totalRows)
        {
            return MakeDAL.Search(makeSearchParams, out totalRows);
        }
    }
}
