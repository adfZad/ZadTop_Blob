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
    public class SectorDAL
    {
        public static byte Insert(SectorInfo sectorInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsSector");

                db.AddInParameter(dbCommand, "Name", DbType.String, sectorInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, sectorInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                sectorInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static  byte Delete(long sectorId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelSector");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, sectorId);
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

        public static byte Update(SectorInfo sectorInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdSector");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, sectorInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, sectorInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, sectorInfo.Description);

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

        public static SectorInfoCollection GetAll()
        {
            SectorInfoCollection sectorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetSector");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    sectorInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sectorInfoCollection;
        }

        public static SectorInfo Get(long sectorId)
        {
            SectorInfoCollection sectorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetSector");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, sectorId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    sectorInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (sectorInfoCollection != null && sectorInfoCollection.Count > 0) ? sectorInfoCollection[0] : null;
        }

        public static SectorInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            SectorInfoCollection sectorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchSector");

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
                    sectorInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sectorInfoCollection;
        }

        private static SectorInfoCollection GetAsList(IDataReader dataReader)
        {
            SectorInfoCollection sectorInfoCollection = new SectorInfoCollection();

            while (dataReader.Read())
            {
                SectorInfo sectorInfo = new SectorInfo();
                sectorInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                sectorInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                sectorInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (sectorInfoCollection == null)
                    sectorInfoCollection = new SectorInfoCollection();

                sectorInfoCollection.Add(sectorInfo);
            }

            return sectorInfoCollection;
        }
    }
}
