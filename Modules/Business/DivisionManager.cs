using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class DivisionManager
    {
        public static byte Insert(DivisionInfo divisionInfo)
        {
            return DivisionDAL.Insert(divisionInfo);
        }

        public static byte Update(DivisionInfo divisionInfo)
        {
            return DivisionDAL.Update(divisionInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return DivisionDAL.Delete(exchangeRateId);
        }

        public static DivisionInfo Get(long exchangeRateId)
        {
            return DivisionDAL.Get(exchangeRateId);
        }

        public static DivisionInfoCollection GetAll()
        {
            return DivisionDAL.GetAll();
        }

        public static DivisionInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DivisionDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DivisionInfoCollection Search(DivisionSearchParams divisionSearchParams, out long totalRows)
        {
            return DivisionDAL.Search(divisionSearchParams, out totalRows);
        }
    }
}
