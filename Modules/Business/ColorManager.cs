using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class ColorManager
    {
        public static byte Insert(ColorInfo colorInfo)
        {
            return ColorDAL.Insert(colorInfo);
        }

        public static byte Update(ColorInfo colorInfo)
        {
            return ColorDAL.Update(colorInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return ColorDAL.Delete(exchangeRateId);
        }

        public static ColorInfo Get(long exchangeRateId)
        {
            return ColorDAL.Get(exchangeRateId);
        }

        public static ColorInfoCollection GetAll()
        {
            return ColorDAL.GetAll();
        }

        public static ColorInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ColorDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static ColorInfoCollection Search(ColorSearchParams colorSearchParams, out long totalRows)
        {
            return ColorDAL.Search(colorSearchParams, out totalRows);
        }
    }
}
