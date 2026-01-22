using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class CategoryManager
    {
        public static byte Insert(CategoryInfo categoryInfo)
        {
            return CategoryDAL.Insert(categoryInfo);
        }

        public static byte Update(CategoryInfo categoryInfo)
        {
            return CategoryDAL.Update(categoryInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CategoryDAL.Delete(exchangeRateId);
        }

        public static CategoryInfo Get(long exchangeRateId)
        {
            return CategoryDAL.Get(exchangeRateId);
        }

        public static CategoryInfoCollection GetAll()
        {
            return CategoryDAL.GetAll();
        }

        public static CategoryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CategoryDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static CategoryInfoCollection Search(CategorySearchParams categorySearchParams, out long totalRows)
        {
            return CategoryDAL.Search(categorySearchParams, out totalRows);
        }
    }
}
