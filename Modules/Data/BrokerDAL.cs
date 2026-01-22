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
    public class BrokerDAL
    {
         public static byte Insert(BrokerInfo brokerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsBroker");

                db.AddInParameter(dbCommand, "Name", DbType.String, brokerInfo.Name);
                db.AddInParameter(dbCommand, "Address", DbType.String, brokerInfo.Address);
                db.AddInParameter(dbCommand, "Website", DbType.String, brokerInfo.Website);
                db.AddInParameter(dbCommand, "Email", DbType.String, brokerInfo.Email);
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, brokerInfo.CountryId);
                db.AddInParameter(dbCommand, "Description", DbType.String, brokerInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                brokerInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long brokerId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelBroker");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, brokerId);
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

        public static byte Update(BrokerInfo brokerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdBroker");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, brokerInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, brokerInfo.Name);
                db.AddInParameter(dbCommand, "Address", DbType.String, brokerInfo.Address);
                db.AddInParameter(dbCommand, "Website", DbType.String, brokerInfo.Website);
                db.AddInParameter(dbCommand, "Email", DbType.String, brokerInfo.Email);
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, brokerInfo.CountryId);
                db.AddInParameter(dbCommand, "Description", DbType.String, brokerInfo.Description);

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

        public static BrokerInfoCollection GetAll()
        {
            BrokerInfoCollection brokerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetBroker");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    brokerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return brokerInfoCollection;
        }

        public static BrokerInfo Get(long brokerId)
        {
            BrokerInfoCollection brokerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetBroker");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, brokerId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    brokerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (brokerInfoCollection != null && brokerInfoCollection.Count > 0) ? brokerInfoCollection[0] : null;
        }

        public static BrokerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            BrokerInfoCollection brokerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchBroker");

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
                    brokerInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return brokerInfoCollection;
        }

        private static BrokerInfoCollection GetAsList(IDataReader dataReader)
        {
            BrokerInfoCollection brokerInfoCollection = new BrokerInfoCollection();

            while (dataReader.Read())
            {
                BrokerInfo brokerInfo = new BrokerInfo();
                brokerInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                brokerInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                brokerInfo.Address = DataHelper.GetSafeString(dataReader, "Address", default(string));
                brokerInfo.Website = DataHelper.GetSafeString(dataReader, "Website", default(string));
                brokerInfo.Email = DataHelper.GetSafeString(dataReader, "Email", default(string));
                brokerInfo.CountryId = DataHelper.GetSafeLong(dataReader, "CountryId", default(long));
                brokerInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (brokerInfoCollection == null)
                    brokerInfoCollection = new BrokerInfoCollection();

                brokerInfoCollection.Add(brokerInfo);
            }

            return brokerInfoCollection;
        }
    }
}
