using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class CustomerManager
    {
        public static byte Insert(CustomerInfo customerInfo)
        {
            return CustomerDAL.Insert(customerInfo);
        }

        public static byte Update(CustomerInfo customerInfo)
        {
            return CustomerDAL.Update(customerInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return CustomerDAL.Delete(exchangeRateId);
        }

        public static CustomerInfo Get(long exchangeRateId)
        {
            return CustomerDAL.Get(exchangeRateId);
        }

        public static CustomerInfoCollection GetAll()
        {
            return CustomerDAL.GetAll();
        }

        public static CustomerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return CustomerDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static CustomerInfoCollection Search(CustomerSearchParams customerSearchParams, out long totalRows)
        {
            return CustomerDAL.Search(customerSearchParams, out totalRows);
        }
    }
}
