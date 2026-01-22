using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace ZadHolding.Data
{
    public class ExchangeRateDAL
    {
        public static byte Insert(ExchangeRateInfo exchangeRateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsExchangeRate");

                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, exchangeRateInfo.CreatedDate);
                db.AddInParameter(dbCommand, "Rate", DbType.Decimal, exchangeRateInfo.Rate);
                db.AddInParameter(dbCommand, "Currency", DbType.String, exchangeRateInfo.Currency); 
                db.AddInParameter(dbCommand, "Description", DbType.String, exchangeRateInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                exchangeRateInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte InsertLast(ExchangeRateInfo exchangeRateInfo, long userId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsExchangeRateLast");

                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, exchangeRateInfo.CreatedDate);
                db.AddInParameter(dbCommand, "Rate", DbType.Decimal, exchangeRateInfo.Rate);
                db.AddInParameter(dbCommand, "CurrencyCode", DbType.String, exchangeRateInfo.CurrencyCode); 
                db.AddInParameter(dbCommand, "Description", DbType.String, exchangeRateInfo.Description);
                db.AddInParameter(dbCommand, "InsertedBy", DbType.Int64, userId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                exchangeRateInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        
        public static DataTable SearchLast()
        {
            DataSet ds = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetExchangeRateNew");

                ds = db.ExecuteDataSet(dbCommand);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }

        public static bool Delete(long exchangeRateId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelExchangeRate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, exchangeRateId);
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

        public static bool Update(ExchangeRateInfo exchangeRateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdExchangeRate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, exchangeRateInfo.Id);
                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, exchangeRateInfo.CreatedDate);
                db.AddInParameter(dbCommand, "Rate", DbType.Decimal, exchangeRateInfo.Rate);
                db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, exchangeRateInfo.CurrencyId);
                db.AddInParameter(dbCommand, "Currency", DbType.String, exchangeRateInfo.Currency); 
                db.AddInParameter(dbCommand, "Description", DbType.String, exchangeRateInfo.Description);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result==1;
        }

        public static ExchangeRateInfoCollection GetAll()
        {
            ExchangeRateInfoCollection exchangeRateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetExchangeRate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    exchangeRateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exchangeRateInfoCollection;
        }
        
        public static DataTable  GetAllReport()
        {
            DataTable dt = new DataTable();
            
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetExchangeRate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    dt.Load(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataTable SearchApproveValues()
        {
            DataSet ds = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetExchangeRateApprove");

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }
        
        public static ExchangeRateInfo Get(long exchangeRateId)
        {
            ExchangeRateInfoCollection exchangeRateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetExchangeRate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, exchangeRateId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    exchangeRateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (exchangeRateInfoCollection != null && exchangeRateInfoCollection.Count > 0) ? exchangeRateInfoCollection[0] : null;
        }

        public static ExchangeRateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ExchangeRateInfoCollection exchangeRateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchExchangeRate");

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
                    exchangeRateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exchangeRateInfoCollection;
        }

        public static ExchangeRateInfoCollection Search(ExchangeRateParams exchangeRateParams, out long totalRows)
        {
            ExchangeRateInfoCollection exchangeRateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchExchangeRate");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, exchangeRateParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, exchangeRateParams.PageSize);

                if (string.IsNullOrEmpty(exchangeRateParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, exchangeRateParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, exchangeRateParams.SortOrder);

                if (exchangeRateParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, exchangeRateParams.DateFrom);

                if (exchangeRateParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, exchangeRateParams.DateTo);

                if (exchangeRateParams.CurrencyId == 0)
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, exchangeRateParams.CurrencyId);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    exchangeRateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exchangeRateInfoCollection;
        }

        public static bool UpdateApproval(long id, long userId) 
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdExchangeRateApproval");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);
                db.AddInParameter(dbCommand, "ApprovedBy", DbType.Int64, userId);

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

        private static ExchangeRateInfoCollection GetAsList(IDataReader dataReader)
        {
            ExchangeRateInfoCollection exchangeRateInfoCollection = new ExchangeRateInfoCollection();

            while (dataReader.Read())
            {
                ExchangeRateInfo exchangeRateInfo = new ExchangeRateInfo();
                exchangeRateInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                exchangeRateInfo.CreatedDate = DataHelper.GetSafeDateTime(dataReader, "CreatedDate", default(DateTime));
                exchangeRateInfo.Rate = DataHelper.GetSafeDecimal(dataReader, "Rate", default(decimal));
                exchangeRateInfo.CurrencyId = DataHelper.GetSafeLong(dataReader, "CurrencyId", default(long));
                exchangeRateInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
                exchangeRateInfo.Symbol = DataHelper.GetSafeString(dataReader, "Symbol", default(string));
                exchangeRateInfo.CurrencyCode = DataHelper.GetSafeString(dataReader, "Symbol", default(string));
                exchangeRateInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (exchangeRateInfoCollection == null)
                    exchangeRateInfoCollection = new ExchangeRateInfoCollection();

                exchangeRateInfoCollection.Add(exchangeRateInfo);
            }

            return exchangeRateInfoCollection;
        }
    }
}
