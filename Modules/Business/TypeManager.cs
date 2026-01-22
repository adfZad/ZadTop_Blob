using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class TypeManager
    {
        public static byte Insert(TypeInfo typeInfo)
        {
            return TypeDAL.Insert(typeInfo);
        }

        public static byte Update(TypeInfo typeInfo)
        {
            return TypeDAL.Update(typeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return TypeDAL.Delete(exchangeRateId);
        }

        public static TypeInfo Get(long exchangeRateId)
        {
            return TypeDAL.Get(exchangeRateId);
        }

        public static TypeInfoCollection GetAll()
        {
            return TypeDAL.GetAll();
        }

        public static TypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return TypeDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static TypeInfoCollection Search(TypeSearchParams typeSearchParams, out long totalRows)
        {
            return TypeDAL.Search(typeSearchParams, out totalRows);
        }
    }
}
