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
    public class CoverageDAL
    {
        public static byte Insert(CoverageInfo coverageInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCoverage");

                db.AddInParameter(dbCommand, "Name", DbType.String, coverageInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, coverageInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                coverageInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long coverageId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCoverage");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, coverageId);
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

        public static byte Update(CoverageInfo coverageInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCoverage");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, coverageInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, coverageInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, coverageInfo.Description);

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

        public static CoverageInfo Get(long CoverageId)
        {
            CoverageInfoCollection coverageInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCoverage");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CoverageId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    coverageInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (coverageInfoCollection != null && coverageInfoCollection.Count > 0) ? coverageInfoCollection[0] : null;
        }

        public static CoverageInfoCollection GetAll()
        {
            CoverageInfoCollection coverageInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCoverage");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    coverageInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return coverageInfoCollection;
        }

        public static CoverageInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CoverageInfoCollection coverageInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCoverage");

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
                    coverageInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return coverageInfoCollection;
        }

        private static CoverageInfoCollection GetAsList(IDataReader dataReader)
        {
            CoverageInfoCollection coverageInfoCollection = null;

            while (dataReader.Read())
            {
                CoverageInfo coverageInfo = new CoverageInfo();
                coverageInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                coverageInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                coverageInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (coverageInfoCollection == null)
                    coverageInfoCollection = new CoverageInfoCollection();

                coverageInfoCollection.Add(coverageInfo);
            }

            return coverageInfoCollection;
        }
    }
}
