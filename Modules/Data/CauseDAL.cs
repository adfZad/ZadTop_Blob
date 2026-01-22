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
    public class CauseDAL
    {
        public static byte Insert(CauseInfo causeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCause");

                db.AddInParameter(dbCommand, "Name", DbType.String, causeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, causeInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                causeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long causeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCause");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, causeId);
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

        public static byte Update(CauseInfo causeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCause");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, causeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, causeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, causeInfo.Description);

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

        public static CauseInfo Get(long CauseId)
        {
            CauseInfoCollection causeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCause");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CauseId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    causeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (causeInfoCollection != null && causeInfoCollection.Count > 0) ? causeInfoCollection[0] : null;
        }

        public static CauseInfoCollection GetAll()
        {
            CauseInfoCollection causeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCause");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    causeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return causeInfoCollection;
        }

        public static CauseInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CauseInfoCollection causeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCause");

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
                    causeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return causeInfoCollection;
        }

        private static CauseInfoCollection GetAsList(IDataReader dataReader)
        {
            CauseInfoCollection causeInfoCollection = null;

            while (dataReader.Read())
            {
                CauseInfo causeInfo = new CauseInfo();
                causeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                causeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                causeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (causeInfoCollection == null)
                    causeInfoCollection = new CauseInfoCollection();

                causeInfoCollection.Add(causeInfo);
            }

            return causeInfoCollection;
        }
    }
}
