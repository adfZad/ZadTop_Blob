using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class PlateManager
    {
        public static byte Insert(PlateInfo plateInfo)
        {
            return PlateDAL.Insert(plateInfo);
        }

        public static byte Update(PlateInfo plateInfo)
        {
            return PlateDAL.Update(plateInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return PlateDAL.Delete(exchangeRateId);
        }

        public static PlateInfo Get(long exchangeRateId)
        {
            return PlateDAL.Get(exchangeRateId);
        }

        public static PlateInfoCollection GetAll()
        {
            return PlateDAL.GetAll();
        }

        public static PlateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return PlateDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static PlateInfoCollection Search(PlateSearchParams plateSearchParams, out long totalRows)
        {
            return PlateDAL.Search(plateSearchParams, out totalRows);
        }
    }
}
