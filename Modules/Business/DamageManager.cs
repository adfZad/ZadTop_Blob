using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class DamageManager
    {
        public static byte Insert(DamageInfo damageInfo)
        {
            return DamageDAL.Insert(damageInfo);
        }

        public static byte Update(DamageInfo damageInfo)
        {
            return DamageDAL.Update(damageInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return DamageDAL.Delete(exchangeRateId);
        }

        public static DamageInfo Get(long exchangeRateId)
        {
            return DamageDAL.Get(exchangeRateId);
        }

        public static DamageInfoCollection GetAll()
        {
            return DamageDAL.GetAll();
        }

        public static DamageInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DamageDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
