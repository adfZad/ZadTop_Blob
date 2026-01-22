using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class PolicyManager
    {
        public static byte Insert(PolicyInfo policyInfo)
        {
            return PolicyDAL.Insert(policyInfo);
        }

        public static byte Update(PolicyInfo policyInfo)
        {
            return PolicyDAL.Update(policyInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return PolicyDAL.Delete(exchangeRateId);
        }

        public static PolicyInfo Get(long exchangeRateId)
        {
            return PolicyDAL.Get(exchangeRateId);
        }

        public static PolicyInfo GetPolicyByVehicleId(long vehicleId)
        {
            return PolicyDAL.GetPolicyByVehicleId(vehicleId);
        }

        public static PolicyInfoCollection GetAll()
        {
            return PolicyDAL.GetAll();
        }

        public static PolicyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return PolicyDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static PolicyInfoCollection Search(PolicySearchParams policySearchParams, out long totalRows)
        {
            return PolicyDAL.Search(policySearchParams, out totalRows);
        }
    }
}
