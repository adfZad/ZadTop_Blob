using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class CountryManager
    {
        public static byte Insert(CountryInfo countryInfo)
        {
            return CountryDAL.Insert(countryInfo);
        }

        public static byte Update(CountryInfo countryInfo)
        {
            return CountryDAL.Update(countryInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CountryDAL.Delete(exchangeRateId);
        }

        public static CountryInfo Get(long exchangeRateId)
        {
            return CountryDAL.Get(exchangeRateId);
        }

        public static CountryInfoCollection GetAll()
        {
            return CountryDAL.GetAll();
        }

        public static CountryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CountryDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static CountryInfoCollection Search(CountrySearchParams countrySearchParams, out long totalRows)
        {
            return CountryDAL.Search(countrySearchParams, out totalRows);
        }
    }
}
