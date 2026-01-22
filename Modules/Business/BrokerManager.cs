using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class BrokerManager
    {
        public static byte Insert(BrokerInfo brokerInfo)
        {
            return BrokerDAL.Insert(brokerInfo);
        }

        public static byte Update(BrokerInfo brokerInfo)
        {
            return BrokerDAL.Update(brokerInfo);
        }

        public static byte Delete(long brokerId)
        {
            return BrokerDAL.Delete(brokerId);
        }

        public static BrokerInfo Get(long brokerId)
        {
            return BrokerDAL.Get(brokerId);
        }

        public static BrokerInfoCollection GetAll()
        {
            return BrokerDAL.GetAll();
        }

        public static BrokerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return BrokerDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
