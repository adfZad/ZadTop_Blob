using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;



namespace ZadHolding.Data
{
    public class DepartmentDAL
    {
        public static byte Insert(DepartmentInfo departmentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDepartment");

                db.AddInParameter(dbCommand, "Name", DbType.String, departmentInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, departmentInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, departmentInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, departmentInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, departmentInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, departmentInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, departmentInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, departmentInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                departmentInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long departmentId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDepartment");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, departmentId);
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

        public static byte Update(DepartmentInfo departmentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDepartment");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, departmentInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, departmentInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, departmentInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, departmentInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, departmentInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, departmentInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, departmentInfo.UpdatedTime);

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

        public static DepartmentInfo Get(long DepartmentId)
        {
            DepartmentInfoCollection departmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDepartment");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DepartmentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    departmentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (departmentInfoCollection != null && departmentInfoCollection.Count > 0) ? departmentInfoCollection[0] : null;
        }

        public static DepartmentInfoCollection GetAll()
        {
            DepartmentInfoCollection departmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDepartment");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    departmentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return departmentInfoCollection;
        }

        public static DepartmentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DepartmentInfoCollection departmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDepartment");

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
                    departmentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return departmentInfoCollection;
        }

        public static DepartmentInfoCollection Search(DepartmentSearchParams departmentSearchParams, out long totalRows)
        {
            DepartmentInfoCollection departmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDepartment");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, departmentSearchParams.CurrentPage);

                if (departmentSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, departmentSearchParams.PageSize);

                if (string.IsNullOrEmpty(departmentSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, departmentSearchParams.SortColumn);

                if (string.IsNullOrEmpty(departmentSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, departmentSearchParams.SortOrder);


                if (departmentSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, departmentSearchParams.CreatedId);

                if (departmentSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, departmentSearchParams.UpdateId);

                if (string.IsNullOrEmpty(departmentSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, departmentSearchParams.Name);

                if (string.IsNullOrEmpty(departmentSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, departmentSearchParams.Description);

                if (string.IsNullOrEmpty(departmentSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, departmentSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    departmentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return departmentInfoCollection;
        }

        private static DepartmentInfoCollection GetAsList(IDataReader dataReader)
        {
            DepartmentInfoCollection departmentInfoCollection = null;

            while (dataReader.Read())
            {
                DepartmentInfo departmentInfo = new DepartmentInfo();
                departmentInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                departmentInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                departmentInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                departmentInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                departmentInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                departmentInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                departmentInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                departmentInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                departmentInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                departmentInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                departmentInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (departmentInfoCollection == null)
                    departmentInfoCollection = new DepartmentInfoCollection();

                departmentInfoCollection.Add(departmentInfo);
            }

            return departmentInfoCollection;
        }
    }
}

