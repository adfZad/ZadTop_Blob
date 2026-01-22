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
    public class CurrencyDAL
    {
        public static byte Insert(CurrencyInfo currencyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCurrency");

                db.AddInParameter(dbCommand, "Name", DbType.String, currencyInfo.Name);
                db.AddInParameter(dbCommand, "Symbol", DbType.String, currencyInfo.Symbol);
                db.AddInParameter(dbCommand, "Description", DbType.String, currencyInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                currencyInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long currencyId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCurrency");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, currencyId);
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

        public static byte Update(CurrencyInfo currencyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCurrency");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, currencyInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, currencyInfo.Name);
                db.AddInParameter(dbCommand, "Symbol", DbType.String, currencyInfo.Symbol);
                db.AddInParameter(dbCommand, "Description", DbType.String, currencyInfo.Description);

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

        public static CurrencyInfoCollection GetAll()
        {
            CurrencyInfoCollection currencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCurrency");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    currencyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return currencyInfoCollection;
        }

        public static CurrencyInfo Get(long currencyId)
        {
            CurrencyInfoCollection currencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCurrency");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, currencyId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    currencyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (currencyInfoCollection != null && currencyInfoCollection.Count > 0) ? currencyInfoCollection[0] : null;
        }

        public static CurrencyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CurrencyInfoCollection currencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCurrency");

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
                    currencyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return currencyInfoCollection;
        }

        private static CurrencyInfoCollection GetAsList(IDataReader dataReader)
        {
            CurrencyInfoCollection currencyInfoCollection = new CurrencyInfoCollection();

            while (dataReader.Read())
            {
                CurrencyInfo currencyInfo = new CurrencyInfo();
                currencyInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                currencyInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                currencyInfo.Symbol = DataHelper.GetSafeString(dataReader, "Symbol", default(string));
                currencyInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (currencyInfoCollection == null)
                    currencyInfoCollection = new CurrencyInfoCollection();

                currencyInfoCollection.Add(currencyInfo);
            }

            return currencyInfoCollection;
        }
    }
}
