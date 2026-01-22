using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class VehicleManager
    {
        public static byte Insert(VehicleInfo vehicleInfo)
        {
            return VehicleDAL.Insert(vehicleInfo);
        }

        public static byte Update(VehicleInfo vehicleInfo)
        {
            return VehicleDAL.Update(vehicleInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return VehicleDAL.Delete(exchangeRateId);
        }

        public static VehicleInfo Get(long exchangeRateId)
        {
            return VehicleDAL.Get(exchangeRateId);
        }

        public static VehicleInfoCollection GetAll()
        {
            return VehicleDAL.GetAll();
        }

        public static VehicleInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return VehicleDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static VehicleInfoCollection Search(VehicleSearchParams vehicleSearchParams, out long totalRows)
        {
            return VehicleDAL.Search(vehicleSearchParams, out totalRows);
        }
    }
}
