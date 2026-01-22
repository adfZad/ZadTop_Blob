using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;



namespace ZadHolding.Data
{
    public class ApproveDAL
    {
        public static byte Insert(ApproveInfo approveInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsApprove");

                db.AddInParameter(dbCommand, "Name", DbType.String, approveInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, approveInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, approveInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, approveInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, approveInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, approveInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, approveInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, approveInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                approveInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long approveId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelApprove");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, approveId);
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

        public static byte Update(ApproveInfo approveInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdApprove");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, approveInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, approveInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, approveInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, approveInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, approveInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, approveInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, approveInfo.UpdatedTime);

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

        public static ApproveInfo Get(long ApproveId)
        {
            ApproveInfoCollection approveInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetApprove");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, ApproveId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    approveInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (approveInfoCollection != null && approveInfoCollection.Count > 0) ? approveInfoCollection[0] : null;
        }

        public static ApproveInfoCollection GetAll()
        {
            ApproveInfoCollection approveInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetApprove");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    approveInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return approveInfoCollection;
        }

        public static ApproveInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ApproveInfoCollection approveInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchApprove");

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
                    approveInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return approveInfoCollection;
        }

        public static ApproveInfoCollection Search(ApproveSearchParams approveSearchParams, out long totalRows)
        {
            ApproveInfoCollection approveInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchApprove");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, approveSearchParams.CurrentPage);

                if (approveSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, approveSearchParams.PageSize);

                if (string.IsNullOrEmpty(approveSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, approveSearchParams.SortColumn);

                if (string.IsNullOrEmpty(approveSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, approveSearchParams.SortOrder);


                if (approveSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, approveSearchParams.CreatedId);

                if (approveSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, approveSearchParams.UpdateId);

                if (string.IsNullOrEmpty(approveSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, approveSearchParams.Name);

                if (string.IsNullOrEmpty(approveSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, approveSearchParams.Description);

                if (string.IsNullOrEmpty(approveSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, approveSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    approveInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return approveInfoCollection;
        }

        private static ApproveInfoCollection GetAsList(IDataReader dataReader)
        {
            ApproveInfoCollection approveInfoCollection = null;

            while (dataReader.Read())
            {
                ApproveInfo approveInfo = new ApproveInfo();
                approveInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                approveInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                approveInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                approveInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                approveInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                approveInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                approveInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                approveInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                approveInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                approveInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                approveInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (approveInfoCollection == null)
                    approveInfoCollection = new ApproveInfoCollection();

                approveInfoCollection.Add(approveInfo);
            }

            return approveInfoCollection;
        }
    }
}

