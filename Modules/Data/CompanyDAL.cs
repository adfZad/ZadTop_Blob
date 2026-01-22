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
    public class CompanyDAL
    {
        public static byte Insert(CompanyInfo companyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCompany");

                db.AddInParameter(dbCommand, "Name", DbType.String, companyInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, companyInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, companyInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, companyInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, companyInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, companyInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, companyInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, companyInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                companyInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long companyId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCompany");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, companyId);
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

        public static byte Update(CompanyInfo companyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCompany");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, companyInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, companyInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, companyInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, companyInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, companyInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, companyInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, companyInfo.UpdatedTime);

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

        public static CompanyInfo Get(long CompanyId)
        {
            CompanyInfoCollection companyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCompany");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CompanyId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    companyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (companyInfoCollection != null && companyInfoCollection.Count > 0) ? companyInfoCollection[0] : null;
        }

        public static CompanyInfoCollection GetAll()
        {
            CompanyInfoCollection companyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCompany");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    companyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return companyInfoCollection;
        }

        public static CompanyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CompanyInfoCollection companyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCompany");

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
                    companyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return companyInfoCollection;
        }

        public static CompanyInfoCollection Search(CompanySearchParams companySearchParams, out long totalRows)
        {
            CompanyInfoCollection companyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCompany");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, companySearchParams.CurrentPage);

                if (companySearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, companySearchParams.PageSize);

                if (string.IsNullOrEmpty(companySearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, companySearchParams.SortColumn);

                if (string.IsNullOrEmpty(companySearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, companySearchParams.SortOrder);


                if (companySearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, companySearchParams.CreatedId);

                if (companySearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, companySearchParams.UpdateId);

                if (string.IsNullOrEmpty(companySearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, companySearchParams.Name);

                if (string.IsNullOrEmpty(companySearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, companySearchParams.Description);

                if (string.IsNullOrEmpty(companySearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, companySearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    companyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return companyInfoCollection;
        }

        private static CompanyInfoCollection GetAsList(IDataReader dataReader)
        {
            CompanyInfoCollection companyInfoCollection = null;

            while (dataReader.Read())
            {
                CompanyInfo companyInfo = new CompanyInfo();
                companyInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                companyInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                companyInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                companyInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                companyInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                companyInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                companyInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                companyInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                companyInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                companyInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                companyInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (companyInfoCollection == null)
                    companyInfoCollection = new CompanyInfoCollection();

                companyInfoCollection.Add(companyInfo);
            }

            return companyInfoCollection;
        }

     }
}
