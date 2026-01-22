using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Data;
using ZadHolding.Been.Collection;

namespace ZadHolding.Business
{
    public class CurrencyManager
    {
        public static byte Insert(CurrencyInfo currencyInfo)
        {
            return CurrencyDAL.Insert(currencyInfo);
        }

        public static byte Update(CurrencyInfo currencyInfo)
        {
            return CurrencyDAL.Update(currencyInfo);
        }

        public static byte Delete(long currencyId)
        {
            return CurrencyDAL.Delete(currencyId);
        }

        public static CurrencyInfo Get(long currencyId)
        {
            return CurrencyDAL.Get(currencyId);
        }

        public static CurrencyInfoCollection GetAll()
        {
            return CurrencyDAL.GetAll();
        }

        public static CurrencyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CurrencyDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
