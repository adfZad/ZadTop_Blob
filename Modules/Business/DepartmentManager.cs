using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class DepartmentManager
    {
        public static byte Insert(DepartmentInfo departmentInfo)
        {
            return DepartmentDAL.Insert(departmentInfo);
        }

        public static byte Update(DepartmentInfo departmentInfo)
        {
            return DepartmentDAL.Update(departmentInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return DepartmentDAL.Delete(exchangeRateId);
        }

        public static DepartmentInfo Get(long exchangeRateId)
        {
            return DepartmentDAL.Get(exchangeRateId);
        }

        public static DepartmentInfoCollection GetAll()
        {
            return DepartmentDAL.GetAll();
        }

        public static DepartmentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DepartmentDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DepartmentInfoCollection Search(DepartmentSearchParams departmentSearchParams, out long totalRows)
        {
            return DepartmentDAL.Search(departmentSearchParams, out totalRows);
        }
    }
}
