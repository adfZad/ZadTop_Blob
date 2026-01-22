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
    public class AgencyTypeDAL
    {
        public static byte Insert(AgencyTypeInfo agencyTypeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsAgencyType");

                db.AddInParameter(dbCommand, "Name", DbType.String, agencyTypeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, agencyTypeInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                agencyTypeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long agencyTypeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelAgencyType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, agencyTypeId);
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

        public static byte Update(AgencyTypeInfo agencyTypeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdAgencyType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, agencyTypeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, agencyTypeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, agencyTypeInfo.Description);

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

        public static AgencyTypeInfo Get(long AgencyTypeId)
        {
            AgencyTypeInfoCollection agencyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAgencyType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, AgencyTypeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    agencyTypeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (agencyTypeInfoCollection != null && agencyTypeInfoCollection.Count > 0) ? agencyTypeInfoCollection[0] : null;
        }

        public static AgencyTypeInfoCollection GetAll()
        {
            AgencyTypeInfoCollection agencyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAgencyType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    agencyTypeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return agencyTypeInfoCollection;
        }

        public static AgencyTypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            AgencyTypeInfoCollection agencyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchAgencyType");

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
                    agencyTypeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return agencyTypeInfoCollection;
        }

        private static AgencyTypeInfoCollection GetAsList(IDataReader dataReader)
        {
            AgencyTypeInfoCollection agencyTypeInfoCollection = null;

            while (dataReader.Read())
            {
                AgencyTypeInfo agencyTypeInfo = new AgencyTypeInfo();
                agencyTypeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                agencyTypeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                agencyTypeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (agencyTypeInfoCollection == null)
                    agencyTypeInfoCollection = new AgencyTypeInfoCollection();

                agencyTypeInfoCollection.Add(agencyTypeInfo);
            }

            return agencyTypeInfoCollection;
        }
    }
}
