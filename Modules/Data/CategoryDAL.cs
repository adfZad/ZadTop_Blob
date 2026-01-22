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
    public class CategoryDAL
    {
        public static byte Insert(CategoryInfo categoryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCategory");

                db.AddInParameter(dbCommand, "Name", DbType.String, categoryInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, categoryInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, categoryInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, categoryInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, categoryInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, categoryInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, categoryInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, categoryInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                categoryInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long categoryId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCategory");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, categoryId);
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

        public static byte Update(CategoryInfo categoryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCategory");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, categoryInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, categoryInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, categoryInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, categoryInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, categoryInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, categoryInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, categoryInfo.UpdatedTime);

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

        public static CategoryInfo Get(long CategoryId)
        {
            CategoryInfoCollection categoryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCategory");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CategoryId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    categoryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (categoryInfoCollection != null && categoryInfoCollection.Count > 0) ? categoryInfoCollection[0] : null;
        }

        public static CategoryInfoCollection GetAll()
        {
            CategoryInfoCollection categoryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCategory");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    categoryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoryInfoCollection;
        }

        public static CategoryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CategoryInfoCollection categoryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCategory");

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
                    categoryInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoryInfoCollection;
        }

        public static CategoryInfoCollection Search(CategorySearchParams categorySearchParams, out long totalRows)
        {
            CategoryInfoCollection categoryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCategory");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, categorySearchParams.CurrentPage);

                if (categorySearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, categorySearchParams.PageSize);

                if (string.IsNullOrEmpty(categorySearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, categorySearchParams.SortColumn);

                if (string.IsNullOrEmpty(categorySearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, categorySearchParams.SortOrder);


                if (categorySearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, categorySearchParams.CreatedId);

                if (categorySearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, categorySearchParams.UpdateId);

                if (string.IsNullOrEmpty(categorySearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, categorySearchParams.Name);

                if (string.IsNullOrEmpty(categorySearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, categorySearchParams.Description);

                if (string.IsNullOrEmpty(categorySearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, categorySearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    categoryInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoryInfoCollection;
        }

        private static CategoryInfoCollection GetAsList(IDataReader dataReader)
        {
            CategoryInfoCollection categoryInfoCollection = null;

            while (dataReader.Read())
            {
                CategoryInfo categoryInfo = new CategoryInfo();
                categoryInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                categoryInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                categoryInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                categoryInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                categoryInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                categoryInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                categoryInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                categoryInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                categoryInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                categoryInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                categoryInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (categoryInfoCollection == null)
                    categoryInfoCollection = new CategoryInfoCollection();

                categoryInfoCollection.Add(categoryInfo);
            }

            return categoryInfoCollection;
        }
    }
}
