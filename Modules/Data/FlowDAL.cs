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
    public class FlowDAL
    {
        public static byte Insert(FlowInfo flowInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsFlow");

                db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, flowInfo.DivisionId);
                db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, flowInfo.DepartmentId);
                db.AddInParameter(dbCommand, "Name", DbType.String, flowInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, flowInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, flowInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, flowInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, flowInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, flowInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, flowInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, flowInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                flowInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long flowId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelFlow");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, flowId);
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

        public static byte Update(FlowInfo flowInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdFlow");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, flowInfo.Id);
                db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, flowInfo.DivisionId);
                db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, flowInfo.DepartmentId);
                db.AddInParameter(dbCommand, "Name", DbType.String, flowInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, flowInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, flowInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, flowInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, flowInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, flowInfo.UpdatedTime);

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

        public static FlowInfo Get(long FlowId)
        {
            FlowInfoCollection flowInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlow");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, FlowId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    flowInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (flowInfoCollection != null && flowInfoCollection.Count > 0) ? flowInfoCollection[0] : null;
        }

        public static FlowInfoCollection GetAll()
        {
            FlowInfoCollection flowInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlow");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    flowInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flowInfoCollection;
        }

        public static FlowInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            FlowInfoCollection flowInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlow");

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
                    flowInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flowInfoCollection;
        }

        public static FlowInfoCollection Search(FlowSearchParams flowSearchParams, out long totalRows)
        {
            FlowInfoCollection flowInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlow");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, flowSearchParams.CurrentPage);

                if (flowSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, flowSearchParams.PageSize);

                if (string.IsNullOrEmpty(flowSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, flowSearchParams.SortColumn);

                if (string.IsNullOrEmpty(flowSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, flowSearchParams.SortOrder);


                if (flowSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, flowSearchParams.CreatedId);

                if (flowSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, flowSearchParams.UpdateId);

                if (flowSearchParams.DivisionId == 0)
                    db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, flowSearchParams.DivisionId);

                if (flowSearchParams.DepartmentId == 0)
                    db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, flowSearchParams.DepartmentId);

                if (string.IsNullOrEmpty(flowSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, flowSearchParams.Name);

                if (string.IsNullOrEmpty(flowSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, flowSearchParams.Description);

                if (string.IsNullOrEmpty(flowSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, flowSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    flowInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return flowInfoCollection;
        }

        private static FlowInfoCollection GetAsList(IDataReader dataReader)
        {
            FlowInfoCollection flowInfoCollection = null;

            while (dataReader.Read())
            {
                FlowInfo flowInfo = new FlowInfo();
                flowInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                flowInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                flowInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                flowInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));
                flowInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                flowInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));
                flowInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                flowInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                flowInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                flowInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                flowInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                flowInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                flowInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                flowInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                flowInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (flowInfoCollection == null)
                    flowInfoCollection = new FlowInfoCollection();

                flowInfoCollection.Add(flowInfo);
            }

            return flowInfoCollection;
        }
    }
}

