using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class ClaimManager
    {
        public static byte Insert(ClaimInfo claimInfo)
        {
            return ClaimDAL.Insert(claimInfo);
        }

        public static byte UpdateClaimStatus(ClaimInfo claimInfo)
        {
            return ClaimDAL.UpdateClaimStatus(claimInfo);
        }


        public static byte Update(ClaimInfo claimInfo)
        {
            return ClaimDAL.Update(claimInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return ClaimDAL.Delete(exchangeRateId);
        }

        public static ClaimInfo Get(long exchangeRateId)
        {
            return ClaimDAL.Get(exchangeRateId);
        }

        public static ClaimInfoCollection GetClaimByStatus(long exchangeRateId)
        {
            return ClaimDAL.GetClaimByStatus(exchangeRateId);
        }

        public static ClaimInfoCollection GetAll()
        {
            return ClaimDAL.GetAll();
        }

        public static ClaimInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ClaimDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static ClaimInfoCollection Search(ClaimSearchParams claimSearchParams, out long totalRows)
        {
            return ClaimDAL.Search(claimSearchParams, out totalRows);
        }
    }
}
