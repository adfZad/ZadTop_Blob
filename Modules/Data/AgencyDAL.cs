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
    public class AgencyDAL
    {
        public static byte Insert(AgencyInfo agencyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsAgency");

                db.AddInParameter(dbCommand, "Name", DbType.String, agencyInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, agencyInfo.Description);
                db.AddInParameter(dbCommand, "AgencyTypeId", DbType.Int64, agencyInfo.AgencyTypeId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                agencyInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long agencyId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelAgency");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, agencyId);
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

        public static byte Update(AgencyInfo agencyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdAgency");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, agencyInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, agencyInfo.Name);
                db.AddInParameter(dbCommand, "AgencyTypeId", DbType.Int64, agencyInfo.AgencyTypeId);
                db.AddInParameter(dbCommand, "Description", DbType.String, agencyInfo.Description);

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

        public static AgencyInfo Get(long AgencyId)
        {
            AgencyInfoCollection agencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAgency");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, AgencyId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    agencyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (agencyInfoCollection != null && agencyInfoCollection.Count > 0) ? agencyInfoCollection[0] : null;
        }

        public static AgencyInfoCollection GetAll()
        {
            AgencyInfoCollection agencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAgency");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    agencyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return agencyInfoCollection;
        }

        public static AgencyInfoCollection GetAgencyByType(long exchangeRateId)
        {
            AgencyInfoCollection agencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAgencyByType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, exchangeRateId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    agencyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return agencyInfoCollection;
        }

        public static AgencyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            AgencyInfoCollection agencyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchAgency");

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
                    agencyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return agencyInfoCollection;
        }

        private static AgencyInfoCollection GetAsList(IDataReader dataReader)
        {
            AgencyInfoCollection agencyInfoCollection = null;

            while (dataReader.Read())
            {
                AgencyInfo agencyInfo = new AgencyInfo();
                agencyInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                agencyInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                agencyInfo.AgencyTypeId = DataHelper.GetSafeLong(dataReader, "AgencyTypeId", default(long));
                agencyInfo.AgencyType = DataHelper.GetSafeString(dataReader, "AgencyType", default(string));
                agencyInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (agencyInfoCollection == null)
                    agencyInfoCollection = new AgencyInfoCollection();

                agencyInfoCollection.Add(agencyInfo);
            }

            return agencyInfoCollection;
        }
    }
}
