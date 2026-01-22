using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ZadHolding.Data
{
    public class UserRoleDAL
    {
        public static byte Insert(UserRoleInfo userRoleInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsUserRole");

                db.AddInParameter(dbCommand, "RoleName", DbType.String, userRoleInfo.RoleName);
                db.AddInParameter(dbCommand, "Description", DbType.String, userRoleInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                userRoleInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static bool Delete(long userRoleId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelUserRole");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, userRoleId);
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

        public static byte Update(UserRoleInfo userRoleInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdUserRole");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, userRoleInfo.Id);
                db.AddInParameter(dbCommand, "RoleName", DbType.String, userRoleInfo.RoleName);
                db.AddInParameter(dbCommand, "Description", DbType.String, userRoleInfo.Description);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static UserRoleInfoCollection GetAll()
        {
            UserRoleInfoCollection userRoleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUserRole");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userRoleInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userRoleInfoCollection;
        }

        public static UserRoleInfo Get(long userRoleId)
        {
            UserRoleInfoCollection userRoleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUserRole");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, userRoleId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userRoleInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (userRoleInfoCollection != null && userRoleInfoCollection.Count > 0) ? userRoleInfoCollection[0] : null;
        }

        public static UserRoleInfoCollection GetByDepartmentId(long departmentId) 
        {
            UserRoleInfoCollection userRoleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetUserRoleByDeptId");
                db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, departmentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userRoleInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userRoleInfoCollection;
        }

        public static UserRoleInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            UserRoleInfoCollection userRoleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchUserRole");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, currentPage);

                if (pageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);

                if (string.IsNullOrEmpty(sortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, sortColumn);

                if (string.IsNullOrEmpty(sortColumn))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, sortOrder);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userRoleInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userRoleInfoCollection;
        }

        private static UserRoleInfoCollection GetAsList(IDataReader dataReader)
        {
            UserRoleInfoCollection userRoleInfoCollection = null;

            while (dataReader.Read())
            {
                UserRoleInfo userRoleInfo = new UserRoleInfo();
                userRoleInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", 0);
                userRoleInfo.RoleName = DataHelper.GetSafeString(dataReader, "RoleName", string.Empty);
                userRoleInfo.Description = DataHelper.GetSafeString(dataReader, "Description", string.Empty);

                if (userRoleInfoCollection == null)
                    userRoleInfoCollection = new UserRoleInfoCollection();

                userRoleInfoCollection.Add(userRoleInfo);
            }

            return userRoleInfoCollection;
        }
    }
}
