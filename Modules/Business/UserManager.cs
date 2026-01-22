using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class UserManager
    {
        public static byte Insert(UserInfo userInfo) 
        {
            return UserDAL.Insert(userInfo);
        }

        public static byte Update(UserInfo userInfo) 
        {
            return UserDAL.Update(userInfo);
        }

        public static bool Delete(long id) 
        {
            return UserDAL.Delele(id);
        }

        public static UserInfo Get(long id) 
        {
            return UserDAL.Get(id);
        }

        public static UserInfoCollection GetAll()
        {
            return UserDAL.GetAll();
        }

        public static UserInfoCollection GetByRoleId(long roleId)
        {
            return UserDAL.GetByRoleId(roleId);
        }

        public static UserInfo GetByUserName(string userName)
        {
            return UserDAL.GetByUseName(userName);
        }
    }
}
