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
    public class FlowDetailDAL
    {
        public static byte Insert(FlowDetailInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsFlowDetail");

                db.AddInParameter(dbCommand, "Name", DbType.String, makeInfo.Name);
                db.AddInParameter(dbCommand, "FlowTemplateId", DbType.Int64, makeInfo.FlowTemplateId);
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
                DbCommand dbCommand = db.GetStoredProcCommand("DelFlowDetail");

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

        public static byte Update(FlowDetailInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdFlowDetail");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, makeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, makeInfo.Name);
                db.AddInParameter(dbCommand, "FlowTemplateId", DbType.Int64, makeInfo.FlowTemplateId);
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

        public static FlowDetailInfo Get(long MakeId)
        {
            FlowDetailInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlowDetail");
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

        public static FlowDetailInfoCollection GetAll()
        {
            FlowDetailInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetFlowDetail");
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

        public static FlowDetailInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            FlowDetailInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlowDetails");

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

        public static FlowDetailInfoCollection Search(FlowDetailSearchParams calldetailSearchParams, out long totalRows)
        {
            FlowDetailInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchFlowDetails");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, calldetailSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, calldetailSearchParams.PageSize);

                if (string.IsNullOrEmpty(calldetailSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, calldetailSearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, calldetailSearchParams.SortOrder);

                if (calldetailSearchParams.FlowTemplateId == 0)
                    db.AddInParameter(dbCommand, "FlowTemplateId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "FlowTemplateId", DbType.Int64, calldetailSearchParams.FlowTemplateId);

                if (calldetailSearchParams.FlowId == 0)
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, calldetailSearchParams.FlowId);

                if (calldetailSearchParams.UserId == 0)
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, calldetailSearchParams.UserId);

                if (calldetailSearchParams.LevelId == 0)
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, calldetailSearchParams.LevelId);


                if (calldetailSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, calldetailSearchParams.CreatedId);



               


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

        private static FlowDetailInfoCollection GetAsList(IDataReader dataReader)
        {
            FlowDetailInfoCollection makeInfoCollection = null;

            while (dataReader.Read())
            {
                FlowDetailInfo makeInfo = new FlowDetailInfo();
                makeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                makeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));

                makeInfo.FlowTemplateId = DataHelper.GetSafeLong(dataReader, "FlowTemplateId", default(long));
                makeInfo.FlowTemplate = DataHelper.GetSafeString(dataReader, "FlowTemplate", default(string));

                makeInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                makeInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));

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
                    makeInfoCollection = new FlowDetailInfoCollection();

                makeInfoCollection.Add(makeInfo);
            }

            return makeInfoCollection;
        }
    }
}
