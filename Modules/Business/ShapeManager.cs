using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class ShapeManager
    {
        public static byte Insert(ShapeInfo shapeInfo)
        {
            return ShapeDAL.Insert(shapeInfo);
        }

        public static byte Update(ShapeInfo shapeInfo)
        {
            return ShapeDAL.Update(shapeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return ShapeDAL.Delete(exchangeRateId);
        }

        public static ShapeInfo Get(long exchangeRateId)
        {
            return ShapeDAL.Get(exchangeRateId);
        }

        public static ShapeInfoCollection GetAll()
        {
            return ShapeDAL.GetAll();
        }

        public static ShapeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ShapeDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static ShapeInfoCollection Search(ShapeSearchParams shapeSearchParams, out long totalRows)
        {
            return ShapeDAL.Search(shapeSearchParams, out totalRows);
        }
    }
}
