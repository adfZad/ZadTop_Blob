using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;

using ZadHolding.Been;
using ZadHolding.Data.Utilities;

namespace ZadHolding.Data
{
    public class UserDAL
    {
        public static byte Insert(UserInfo userInfo)
        {
            byte result = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsUser");
                db.AddInParameter(dbCommand, "UserName", DbType.String, userInfo.UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, userInfo.Password);
                db.AddInParameter(dbCommand, "FirstName", DbType.String, userInfo.FirstName);
                db.AddInParameter(dbCommand, "LastName", DbType.String, userInfo.LastName);
                db.AddInParameter(dbCommand, "RoleId", DbType.Int64, userInfo.RoleId);
                db.AddInParameter(dbCommand, "DivisionId", DbType.Int32, userInfo.DivisionId);
                db.AddInParameter(dbCommand, "DepartmentId", DbType.Int32, userInfo.DepartmentId);
                db.AddInParameter(dbCommand, "DesignationId", DbType.Int32, userInfo.DesignationId);
                db.AddInParameter(dbCommand, "Description", DbType.String, userInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, userInfo.AltDescription);

                db.AddInParameter(dbCommand, "Active", DbType.Boolean, userInfo.Active);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);    

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result);
        }

        public static byte Update(UserInfo userInfo)
        {
            byte result = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");

                DbCommand dbCommand = db.GetStoredProcCommand("UpdUser");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, userInfo.Id);
                db.AddInParameter(dbCommand, "UserName", DbType.String, userInfo.UserName);
                db.AddInParameter(dbCommand, "Password", DbType.String, userInfo.Password);
                db.AddInParameter(dbCommand, "FirstName", DbType.String, userInfo.FirstName);
                db.AddInParameter(dbCommand, "LastName", DbType.String, userInfo.LastName);
                db.AddInParameter(dbCommand, "RoleId", DbType.Int64, userInfo.RoleId);
                db.AddInParameter(dbCommand, "DivisionId", DbType.Int32, userInfo.DivisionId);
                db.AddInParameter(dbCommand, "DepartmentId", DbType.Int32, userInfo.DepartmentId);
                db.AddInParameter(dbCommand, "DesignationId", DbType.Int32, userInfo.DesignationId);
                db.AddInParameter(dbCommand, "Description", DbType.String, userInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, userInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, userInfo.Active);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result);
        }

        public static bool Delele(long id) 
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelUser");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);

            }
            catch (Exception ex) 
            {
                throw ex;
            }

            return (result == 1);
        }

        public static UserInfo Get(long id) 
        {
            UserInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUser");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand)) 
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }

            if (userInfoCollection != null && userInfoCollection.Count > 0)
                return userInfoCollection[0];
            else
                return null;
        }

        public static UserInfoCollection GetAll()
        {
            UserInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUser");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userInfoCollection;
        }

        public static UserInfo GetByUseName(string userName)
        {
            UserInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUserByUserName");
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (userInfoCollection != null && userInfoCollection.Count > 0)
                return userInfoCollection[0];
            else
                return null;
        }

        public static UserInfoCollection GetByRoleId(long roleId) 
        {
            UserInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUserByRoleId");
                db.AddInParameter(dbCommand, "RoleId", DbType.Int64, roleId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userInfoCollection;
        }

        private static UserInfoCollection GetAsList(IDataReader dataReader) 
        {
            UserInfoCollection userInfoCollection = null;

            while (dataReader.Read()) 
            {
                UserInfo userInfo = new UserInfo();
                userInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                userInfo.UserName = DataHelper.GetSafeString(dataReader, "UserName", default(string));
                userInfo.Password = DataHelper.GetSafeString(dataReader, "Password", default(string));
                userInfo.FirstName = DataHelper.GetSafeString(dataReader, "FirstName", default(string));
                userInfo.LastName = DataHelper.GetSafeString(dataReader, "LastName", default(string));
                userInfo.RoleId = DataHelper.GetSafeLong(dataReader, "RoleId", default(long));
                userInfo.RoleName = DataHelper.GetSafeString(dataReader, "RoleName", default(string));
                userInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                userInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));
                userInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                userInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));
                userInfo.DesignationId = DataHelper.GetSafeLong(dataReader, "DesignationId", default(long));
                userInfo.DesignationName = DataHelper.GetSafeString(dataReader, "DesignationName", default(string));
                userInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                userInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                userInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                if (userInfoCollection == null)
                    userInfoCollection = new UserInfoCollection();

                userInfoCollection.Add(userInfo);
            }

            return userInfoCollection;
        }
    }
}
