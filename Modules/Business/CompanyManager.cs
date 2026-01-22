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
    public class CompanyManager
    {
        public static byte Insert(CompanyInfo companyInfo)
        {
            return CompanyDAL.Insert(companyInfo);
        }

        public static byte Update(CompanyInfo companyInfo)
        {
            return CompanyDAL.Update(companyInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CompanyDAL.Delete(exchangeRateId);
        }

        public static CompanyInfo Get(long exchangeRateId)
        {
            return CompanyDAL.Get(exchangeRateId);
        }

        public static CompanyInfoCollection GetAll()
        {
            return CompanyDAL.GetAll();
        }

        public static CompanyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CompanyDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static CompanyInfoCollection Search(CompanySearchParams companySearchParams, out long totalRows)
        {
            return CompanyDAL.Search(companySearchParams, out totalRows);
        }
    }
}
