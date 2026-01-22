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
    public class TypeDAL
    {
        public static byte Insert(TypeInfo typeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsType");

                db.AddInParameter(dbCommand, "Name", DbType.String, typeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, typeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, typeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, typeInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, typeInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, typeInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, typeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, typeInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                typeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long typeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, typeId);
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

        public static byte Update(TypeInfo typeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, typeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, typeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, typeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, typeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, typeInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, typeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, typeInfo.UpdatedTime);

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

        public static TypeInfo Get(long TypeId)
        {
            TypeInfoCollection typeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, TypeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    typeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (typeInfoCollection != null && typeInfoCollection.Count > 0) ? typeInfoCollection[0] : null;
        }

        public static TypeInfoCollection GetAll()
        {
            TypeInfoCollection typeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    typeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return typeInfoCollection;
        }

        public static TypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            TypeInfoCollection typeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchType");

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
                    typeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return typeInfoCollection;
        }

        public static TypeInfoCollection Search(TypeSearchParams typeSearchParams, out long totalRows)
        {
            TypeInfoCollection typeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchType");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, typeSearchParams.CurrentPage);

                if (typeSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, typeSearchParams.PageSize);

                if (string.IsNullOrEmpty(typeSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, typeSearchParams.SortColumn);

                if (string.IsNullOrEmpty(typeSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, typeSearchParams.SortOrder);


                if (typeSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, typeSearchParams.CreatedId);

                if (typeSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, typeSearchParams.UpdateId);

                if (string.IsNullOrEmpty(typeSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, typeSearchParams.Name);

                if (string.IsNullOrEmpty(typeSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, typeSearchParams.Description);

                if (string.IsNullOrEmpty(typeSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, typeSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    typeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return typeInfoCollection;
        }

        private static TypeInfoCollection GetAsList(IDataReader dataReader)
        {
            TypeInfoCollection typeInfoCollection = null;

            while (dataReader.Read())
            {
                TypeInfo typeInfo = new TypeInfo();
                typeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                typeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                typeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                typeInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                typeInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                typeInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                typeInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                typeInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                typeInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                typeInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                typeInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (typeInfoCollection == null)
                    typeInfoCollection = new TypeInfoCollection();

                typeInfoCollection.Add(typeInfo);
            }

            return typeInfoCollection;
        }
    }
}
