using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class PolicyTypeManager
    {
        public static byte Insert(PolicyTypeInfo policyTypeInfo)
        {
            return PolicyTypeDAL.Insert(policyTypeInfo);
        }

        public static byte Update(PolicyTypeInfo policyTypeInfo)
        {
            return PolicyTypeDAL.Update(policyTypeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return PolicyTypeDAL.Delete(exchangeRateId);
        }

        public static PolicyTypeInfo Get(long exchangeRateId)
        {
            return PolicyTypeDAL.Get(exchangeRateId);
        }

        public static PolicyTypeInfoCollection GetAll()
        {
            return PolicyTypeDAL.GetAll();
        }

        public static PolicyTypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return PolicyTypeDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
