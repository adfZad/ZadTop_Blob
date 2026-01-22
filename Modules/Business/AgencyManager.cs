using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class AgencyManager
    {
        public static byte Insert(AgencyInfo agencyInfo)
        {
            return AgencyDAL.Insert(agencyInfo);
        }

        public static byte Update(AgencyInfo agencyInfo)
        {
            return AgencyDAL.Update(agencyInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return AgencyDAL.Delete(exchangeRateId);
        }

        public static AgencyInfo Get(long exchangeRateId)
        {
            return AgencyDAL.Get(exchangeRateId);
        }

        public static AgencyInfoCollection GetAll()
        {
            return AgencyDAL.GetAll();
        }

        public static AgencyInfoCollection GetAgencyByType(long exchangeRateId)
        {
            return AgencyDAL.GetAgencyByType(exchangeRateId);
        }

        public static AgencyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return AgencyDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
