using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class UserRoleManager
    {
        public static byte Insert(UserRoleInfo userRoleInfo)
        {
            return UserRoleDAL.Insert(userRoleInfo);
        }

        public static byte Update(UserRoleInfo userRoleInfo)
        {
            return UserRoleDAL.Update(userRoleInfo);
        }

        public static bool Delete(long userRoleId)
        {
            return UserRoleDAL.Delete(userRoleId);
        }

        public static UserRoleInfo Get(long userRoleId)
        {
            return UserRoleDAL.Get(userRoleId);
        }

        public static UserRoleInfoCollection GetAll()
        {
            return UserRoleDAL.GetAll();
        }

        public static UserRoleInfoCollection GetByDepartmentId(long departmentId) 
        {
            return UserRoleDAL.GetByDepartmentId(departmentId);
        }

        public static UserRoleInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return UserRoleDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
