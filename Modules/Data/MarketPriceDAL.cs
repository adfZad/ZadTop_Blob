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


namespace ZadHolding.Data
{
    public class MarketPriceDAL
    {
        public static byte Insert(MarketPriceInfo marketPriceInfo, long userId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsMarketPrice");

                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, marketPriceInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyCode", DbType.String, marketPriceInfo.CompanyCode);
                db.AddInParameter(dbCommand, "ClosingPrice", DbType.Decimal, marketPriceInfo.ClosingPrice);
                db.AddInParameter(dbCommand, "Volume", DbType.Int64, marketPriceInfo.Volume);
                db.AddInParameter(dbCommand, "Description", DbType.String, marketPriceInfo.Description);
                db.AddInParameter(dbCommand, "InsertedBy", DbType.Int64, userId);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                marketPriceInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static bool Delete(long marketPriceId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelMarketPrice");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, marketPriceId);
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

        public static bool Update(MarketPriceInfo marketPriceInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdMarketPrice");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, marketPriceInfo.Id);
                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, marketPriceInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyCode", DbType.String, marketPriceInfo.CompanyCode);
                db.AddInParameter(dbCommand, "ClosingPrice", DbType.Decimal, marketPriceInfo.ClosingPrice);
                db.AddInParameter(dbCommand, "Volume", DbType.Int64, marketPriceInfo.Volume);
                db.AddInParameter(dbCommand, "Description", DbType.String, marketPriceInfo.Description);

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

        public static byte InsertLast(MarketPriceInfo marketPriceInfo, long userId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsMarketPriceLast");

                db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, marketPriceInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyCode", DbType.String, marketPriceInfo.CompanyCode);
                db.AddInParameter(dbCommand, "ClosingPrice", DbType.Decimal, marketPriceInfo.ClosingPrice);
                db.AddInParameter(dbCommand, "Volume", DbType.Int64, marketPriceInfo.Volume);
                db.AddInParameter(dbCommand, "Description", DbType.String, marketPriceInfo.Description);
                db.AddInParameter(dbCommand, "InsertedBy", DbType.Int64, userId);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                marketPriceInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte UpdateApproval(long id, long userId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdMarketPriceApproval");
               
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

            return result;
        }

        public static MarketPriceInfoCollection GetAll()
        {
            MarketPriceInfoCollection marketPriceInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMarketPrice");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    marketPriceInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return marketPriceInfoCollection;
        }

        public static MarketPriceInfo Get(long marketPriceId)
        {
            MarketPriceInfoCollection marketPriceInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMarketPrice");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, marketPriceId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    marketPriceInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (marketPriceInfoCollection != null && marketPriceInfoCollection.Count > 0) ? marketPriceInfoCollection[0] : null;
        }

        public static MarketPriceInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            MarketPriceInfoCollection marketPriceInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchMarketPrice");

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
                    marketPriceInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return marketPriceInfoCollection;
        }
        public static MarketPriceInfoCollection Search(MarketParams marketParams, out long totalRows)
        {
            MarketPriceInfoCollection marketPriceInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchMarketPrice");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, marketParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, marketParams.PageSize);

                if (string.IsNullOrEmpty(marketParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, marketParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, marketParams.SortOrder);

                if (marketParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, marketParams.DateFrom);

                if (marketParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, marketParams.DateTo);

                if (marketParams.CompanyId == 0)
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, marketParams.CompanyId);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    marketPriceInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return marketPriceInfoCollection;
        }

        public static DataTable SearchLast()
        {
            DataSet ds = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMarketPriceNew");

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }

        public static DataTable SearchApproveValues()
        {
            DataSet ds = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMarketPriceApprove");

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }

        private static MarketPriceInfoCollection GetAsList(IDataReader dataReader)
        {
            MarketPriceInfoCollection marketPriceInfoCollection = new MarketPriceInfoCollection();

            while (dataReader.Read())
            {
                MarketPriceInfo marketPriceInfo = new MarketPriceInfo();
                marketPriceInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                marketPriceInfo.CreatedDate = DataHelper.GetSafeDateTime(dataReader, "CreatedDate", default(DateTime));
                marketPriceInfo.CompanyId = DataHelper.GetSafeLong(dataReader, "CompanyId", default(long));
                marketPriceInfo.CompanyCode = DataHelper.GetSafeString(dataReader, "CompanyCode", default(string));
                marketPriceInfo.ClosingPrice = DataHelper.GetSafeDecimal(dataReader, "ClosingPrice", default(decimal));
                marketPriceInfo.Volume = DataHelper.GetSafeLong(dataReader, "Volume", default(long));
                marketPriceInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (marketPriceInfoCollection == null)
                    marketPriceInfoCollection = new MarketPriceInfoCollection();

                marketPriceInfoCollection.Add(marketPriceInfo);
            }

            return marketPriceInfoCollection;
        }

    }
}

