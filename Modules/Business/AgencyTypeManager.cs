using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class AgencyTypeManager
    {
        public static byte Insert(AgencyTypeInfo agencyTypeInfo)
        {
            return AgencyTypeDAL.Insert(agencyTypeInfo);
        }

        public static byte Update(AgencyTypeInfo agencyTypeInfo)
        {
            return AgencyTypeDAL.Update(agencyTypeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return AgencyTypeDAL.Delete(exchangeRateId);
        }

        public static AgencyTypeInfo Get(long exchangeRateId)
        {
            return AgencyTypeDAL.Get(exchangeRateId);
        }

        public static AgencyTypeInfoCollection GetAll()
        {
            return AgencyTypeDAL.GetAll();
        }

        public static AgencyTypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return AgencyTypeDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
