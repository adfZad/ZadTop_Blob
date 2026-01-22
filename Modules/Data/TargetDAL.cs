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
    public class TargetDAL
    {
        public static byte Insert(TargetInfo targetInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsTarget");

                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, targetInfo.CompanyId);
                db.AddInParameter(dbCommand, "TargetQuantity", DbType.Int64, targetInfo.TargetQuantity);
                db.AddInParameter(dbCommand, "Limit", DbType.Decimal, targetInfo.Limit);
                db.AddInParameter(dbCommand, "Description", DbType.String, targetInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                targetInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long targetId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelTarget");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, targetId);
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

        public static byte Update(TargetInfo targetInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdTarget");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, targetInfo.Id);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, targetInfo.CompanyId);
                db.AddInParameter(dbCommand, "TargetQuantity", DbType.Int64, targetInfo.TargetQuantity);
                db.AddInParameter(dbCommand, "Limit", DbType.Decimal, targetInfo.Limit);
                db.AddInParameter(dbCommand, "Description", DbType.String, targetInfo.Description);

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

        public static TargetInfoCollection GetAll()
        {
            TargetInfoCollection targetInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTarget");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    targetInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return targetInfoCollection;
        }

        public static TargetInfo Get(long targetId)
        {
            TargetInfoCollection targetInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTarget");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, targetId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    targetInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (targetInfoCollection != null && targetInfoCollection.Count > 0) ? targetInfoCollection[0] : null;
        }

        public static TargetInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            TargetInfoCollection targetInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchTarget");

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
                    targetInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return targetInfoCollection;
        }

        private static TargetInfoCollection GetAsList(IDataReader dataReader)
        {
            TargetInfoCollection targetInfoCollection = new TargetInfoCollection();

            while (dataReader.Read())
            {
                TargetInfo targetInfo = new TargetInfo();
                targetInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                targetInfo.CompanyId = DataHelper.GetSafeLong(dataReader, "CompanyId", default(long));
                targetInfo.CompanyName = DataHelper.GetSafeString(dataReader, "CompanyName", default(string));
                targetInfo.TargetQuantity = DataHelper.GetSafeLong(dataReader, "TargetQuantity", default(long));
                targetInfo.Limit = DataHelper.GetSafeDecimal(dataReader, "Limit", default(decimal));
                targetInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (targetInfoCollection == null)
                    targetInfoCollection = new TargetInfoCollection();

                targetInfoCollection.Add(targetInfo);
            }

            return targetInfoCollection;
        }
    }
}
