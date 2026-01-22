using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class TestManager
    {
        public static byte Insert(TestInfo testInfo)
        {
            return TestDAL.Insert(testInfo);
        }

        public static byte Update(TestInfo testInfo)
        {
            return TestDAL.Update(testInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return TestDAL.Delete(exchangeRateId);
        }

        public static TestInfo Get(long exchangeRateId)
        {
            return TestDAL.Get(exchangeRateId);
        }

        public static TestInfoCollection GetAll()
        {
            return TestDAL.GetAll();
        }

        public static TestInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return TestDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
