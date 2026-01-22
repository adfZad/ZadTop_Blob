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
    public class FlowTemplateDAL
    {
        public static byte Insert(FlowTemplateInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsFlowTemplate");

                db.AddInParameter(dbCommand, "Name", DbType.String, makeInfo.Name);
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, makeInfo.FlowId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, makeInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, makeInfo.LevelId);
                db.AddInParameter(dbCommand, "Description", DbType.String, makeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, makeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, makeInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, makeInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, makeInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, makeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, makeInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                makeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long makeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelFlowTemplate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, makeId);
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

        public static byte Update(FlowTemplateInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdFlowTemplate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, makeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, makeInfo.Name);
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, makeInfo.FlowId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, makeInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, makeInfo.LevelId);
                db.AddInParameter(dbCommand, "Description", DbType.String, makeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, makeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, makeInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, makeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, makeInfo.UpdatedTime);

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

        public static FlowTemplateInfo Get(long MakeId)
        {
            FlowTemplateInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlowTemplate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, MakeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    makeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (makeInfoCollection != null && makeInfoCollection.Count > 0) ? makeInfoCollection[0] : null;
        }

        public static FlowTemplateInfoCollection GetAll()
        {
            FlowTemplateInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlowTemplate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    makeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return makeInfoCollection;
        }

        public static FlowTemplateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            FlowTemplateInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlowTemplate");

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
                    makeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return makeInfoCollection;
        }

        public static FlowTemplateInfoCollection Search(FlowTemplateSearchParams makeSearchParams, out long totalRows)
        {
            FlowTemplateInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlowTemplate");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, makeSearchParams.CurrentPage);

                if (makeSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, makeSearchParams.PageSize);

                if (string.IsNullOrEmpty(makeSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, makeSearchParams.SortColumn);

                if (string.IsNullOrEmpty(makeSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, makeSearchParams.SortOrder);

                if (makeSearchParams.FlowId == 0)
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, makeSearchParams.FlowId);

                if (makeSearchParams.UserId == 0)
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, makeSearchParams.UserId);

                if (makeSearchParams.LevelId == 0)
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, makeSearchParams.LevelId);


                if (makeSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, makeSearchParams.CreatedId);

                if (makeSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, makeSearchParams.UpdateId);

                if (string.IsNullOrEmpty(makeSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, makeSearchParams.Name);

                if (string.IsNullOrEmpty(makeSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, makeSearchParams.Description);

                if (string.IsNullOrEmpty(makeSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, makeSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    makeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return makeInfoCollection;
        }

        private static FlowTemplateInfoCollection GetAsList(IDataReader dataReader)
        {
            FlowTemplateInfoCollection makeInfoCollection = null;

            while (dataReader.Read())
            {
                FlowTemplateInfo makeInfo = new FlowTemplateInfo();
                makeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                makeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));

                makeInfo.FlowId = DataHelper.GetSafeLong(dataReader, "FlowId", default(long));
                makeInfo.FlowName = DataHelper.GetSafeString(dataReader, "FlowName", default(string));

                makeInfo.UserId = DataHelper.GetSafeLong(dataReader, "UserId", default(long));
                makeInfo.UserName = DataHelper.GetSafeString(dataReader, "UserName", default(string));

                makeInfo.LevelId = DataHelper.GetSafeLong(dataReader, "LevelId", default(long));
                makeInfo.LevelName = DataHelper.GetSafeString(dataReader, "LevelName", default(string));



                makeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                makeInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                makeInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                makeInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                makeInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                makeInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                makeInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                makeInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                makeInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (makeInfoCollection == null)
                    makeInfoCollection = new FlowTemplateInfoCollection();

                makeInfoCollection.Add(makeInfo);
            }

            return makeInfoCollection;
        }
    }
}
