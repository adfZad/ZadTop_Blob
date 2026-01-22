using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;
namespace ZadHolding.Business
{
    public class AllocationManager
    {
        public static byte Insert(AllocationInfo allocationInfo)
        {
            return AllocationDAL.Insert(allocationInfo);
        }

        public static byte Update(AllocationInfo allocationInfo)
        {
            return AllocationDAL.Update(allocationInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return AllocationDAL.Delete(exchangeRateId);
        }

        public static AllocationInfo Get(long exchangeRateId)
        {
            return AllocationDAL.Get(exchangeRateId);
        }


        public static AllocationInfoCollection GetAll()
        {
            return AllocationDAL.GetAll();
        }

        public static AllocationInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return AllocationDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static AllocationInfoCollection Search(AllocationSearchParams allocationSearchParams, out long totalRows)
        {
            return AllocationDAL.Search(allocationSearchParams, out totalRows);
        }
    }
}
