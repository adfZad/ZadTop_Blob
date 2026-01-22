using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

namespace ZadHolding.Data
{
    public class DividendDAL
    {
        public static bool Insert(DividendInfo dividendInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDividend");
                if (dividendInfo.CreatedDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, DBNull.Value);
                else
                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, dividendInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, dividendInfo.CompanyId);
                db.AddInParameter(dbCommand, "ExchangeRate", DbType.Decimal, dividendInfo.ExchangeRate);
                db.AddInParameter(dbCommand, "Amount", DbType.Decimal, dividendInfo.Amount);
                db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, dividendInfo.BeneficiaryId);
                db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, dividendInfo.BrokerId);
                db.AddInParameter(dbCommand, "Description", DbType.String, dividendInfo.Description);
                

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                dividendInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (result == 1);
        }

        public static bool Delete(long dividendId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDividend");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, dividendId);
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

        public static bool Update(DividendInfo dividendInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDividend");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, dividendInfo.Id);
                if (dividendInfo.CreatedDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, dividendInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, dividendInfo.CompanyId);
                db.AddInParameter(dbCommand, "ExchangeRate", DbType.Decimal, dividendInfo.ExchangeRate);
                db.AddInParameter(dbCommand, "Amount", DbType.Decimal, dividendInfo.Amount);
                db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, dividendInfo.BeneficiaryId);
                db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, dividendInfo.BrokerId);
                db.AddInParameter(dbCommand, "Description", DbType.String, dividendInfo.Description);

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

        public static DividendInfoCollection GetAll()
        {
            DividendInfoCollection dividendInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDividend");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dividendInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dividendInfoCollection;
        }

        public static DividendInfo Get(long dividendId)
        {
            DividendInfoCollection dividendInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDividend");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, dividendId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dividendInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (dividendInfoCollection != null && dividendInfoCollection.Count > 0) ? dividendInfoCollection[0] : null;
        }

        public static DividendInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DividendInfoCollection dividendInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDividend");

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
                    dividendInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dividendInfoCollection;
        }

        public static DividendInfoCollection Search(CallSearchParams callSearchParams, out long totalRows)
        {
            DividendInfoCollection dividendInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDividend");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, callSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, callSearchParams.PageSize);

                if (string.IsNullOrEmpty(callSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, callSearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, callSearchParams.SortOrder);

                if (callSearchParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, callSearchParams.DateFrom);

                if (callSearchParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, callSearchParams.DateTo);

                if (callSearchParams.CompanyId == 0)
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, callSearchParams.CompanyId);

                if (callSearchParams.BeneficiaryId == 0)
                    db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, callSearchParams.BeneficiaryId);

                if (callSearchParams.BrokerId == 0)
                    db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, callSearchParams.BrokerId);

                if (callSearchParams.CurrencyId == 0)
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, callSearchParams.CurrencyId);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dividendInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dividendInfoCollection;
        }
       
        private static DividendInfoCollection GetAsList(IDataReader dataReader)
        {
            DividendInfoCollection dividendInfoCollection = new DividendInfoCollection();

            while (dataReader.Read())
            {
                DividendInfo dividendInfo = new DividendInfo();
                dividendInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                dividendInfo.CreatedDate = DataHelper.GetSafeDateTime(dataReader, "CreatedDate", default(DateTime));
                dividendInfo.CompanyId = DataHelper.GetSafeLong(dataReader, "CompanyId", default(long));
                dividendInfo.Company = DataHelper.GetSafeString(dataReader, "Company", default(string));
                dividendInfo.ExchangeRate = DataHelper.GetSafeDecimal(dataReader, "ExchangeRate", default(decimal));
                dividendInfo.Amount = DataHelper.GetSafeDecimal(dataReader, "Amount", default(decimal));
                dividendInfo.BeneficiaryId = DataHelper.GetSafeLong(dataReader, "BeneficiaryId", default(long));
                dividendInfo.Beneficiary = DataHelper.GetSafeString(dataReader, "Beneficiary", default(string));
                dividendInfo.BrokerId = DataHelper.GetSafeLong(dataReader, "BrokerId", default(long));
                dividendInfo.Broker = DataHelper.GetSafeString(dataReader, "Broker", default(string));
                dividendInfo.CurrencyId = DataHelper.GetSafeLong(dataReader, "CurrencyId", default(long));
                dividendInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
                dividendInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
               
                if (dividendInfoCollection == null)
                    dividendInfoCollection = new DividendInfoCollection();

                dividendInfoCollection.Add(dividendInfo);
            }

            return dividendInfoCollection;
        }
    }
}
