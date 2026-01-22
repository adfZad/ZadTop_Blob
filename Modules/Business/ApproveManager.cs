using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;
using ZadHolding.Been.Collection;


namespace ZadHolding.Business
{
    public class ApproveManager
    {
        public static byte Insert(ApproveInfo approveInfo)
        {
            return ApproveDAL.Insert(approveInfo);
        }

        public static byte Update(ApproveInfo approveInfo)
        {
            return ApproveDAL.Update(approveInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return ApproveDAL.Delete(exchangeRateId);
        }

        public static ApproveInfo Get(long exchangeRateId)
        {
            return ApproveDAL.Get(exchangeRateId);
        }

        public static ApproveInfoCollection GetAll()
        {
            return ApproveDAL.GetAll();
        }

        public static ApproveInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ApproveDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static ApproveInfoCollection Search(ApproveSearchParams approveSearchParams, out long totalRows)
        {
            return ApproveDAL.Search(approveSearchParams, out totalRows);
        }
    }
}
