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
    public class TemplateDAL
    {
        public static byte Insert(TemplateInfo templateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsTemplate");

               
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, templateInfo.FlowId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, templateInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, templateInfo.LevelId);
                db.AddInParameter(dbCommand, "Description", DbType.String, templateInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, templateInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, templateInfo.Active);
                db.AddInParameter(dbCommand, "ApproveId", DbType.Int64, templateInfo.ApproveId);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, templateInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, templateInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, templateInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, templateInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                templateInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long templateId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelTemplate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, templateId);
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

        public static byte Update(TemplateInfo templateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdTemplate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, templateInfo.Id);
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, templateInfo.FlowId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, templateInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, templateInfo.LevelId);
                db.AddInParameter(dbCommand, "Description", DbType.String, templateInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, templateInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, templateInfo.Active);
                db.AddInParameter(dbCommand, "ApproveId", DbType.Int64, templateInfo.ApproveId);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, templateInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, templateInfo.UpdatedTime);

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

        public static TemplateInfo Get(long TemplateId)
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTemplate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, TemplateId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    templateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (templateInfoCollection != null && templateInfoCollection.Count > 0) ? templateInfoCollection[0] : null;
        }

        public static TemplateInfoCollection GetAll()
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTemplate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    templateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return templateInfoCollection;
        }

        public static TemplateInfoCollection GetTemplateByFlow(long flowId)
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTemplateByFlowId");
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, flowId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    templateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return templateInfoCollection;
        }

        public static TemplateInfoCollection GetTemplateByDocumentId(long documentId)
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTemplateByDocumentId");
                db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, documentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    templateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return templateInfoCollection;
        }

        public static TemplateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchTemplate");

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
                    templateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return templateInfoCollection;
        }

        public static TemplateInfoCollection Search(TemplateSearchParams templateSearchParams, out long totalRows)
        {
            TemplateInfoCollection templateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchTemplate");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, templateSearchParams.CurrentPage);

                if (templateSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, templateSearchParams.PageSize);

                if (string.IsNullOrEmpty(templateSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, templateSearchParams.SortColumn);

                if (string.IsNullOrEmpty(templateSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, templateSearchParams.SortOrder);

                if (templateSearchParams.FlowId == 0)
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "FlowId", DbType.Int64, templateSearchParams.FlowId);

                if (templateSearchParams.UserId == 0)
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, templateSearchParams.UserId);

                if (templateSearchParams.LevelId == 0)
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, templateSearchParams.LevelId);


                if (templateSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, templateSearchParams.CreatedId);

                if (templateSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, templateSearchParams.UpdateId);

             
                if (string.IsNullOrEmpty(templateSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, templateSearchParams.Description);

                if (string.IsNullOrEmpty(templateSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, templateSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    templateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return templateInfoCollection;
        }

        private static TemplateInfoCollection GetAsList(IDataReader dataReader)
        {
            TemplateInfoCollection templateInfoCollection = null;

            while (dataReader.Read())
            {
                TemplateInfo templateInfo = new TemplateInfo();
                templateInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
               

                templateInfo.FlowId = DataHelper.GetSafeLong(dataReader, "FlowId", default(long));
                templateInfo.FlowName = DataHelper.GetSafeString(dataReader, "FlowName", default(string));

                templateInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                templateInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));

                templateInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                templateInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));

                templateInfo.UserId = DataHelper.GetSafeLong(dataReader, "UserId", default(long));
                templateInfo.UserName = DataHelper.GetSafeString(dataReader, "UserName", default(string));

                templateInfo.LevelId = DataHelper.GetSafeLong(dataReader, "LevelId", default(long));
                templateInfo.LevelName = DataHelper.GetSafeString(dataReader, "LevelName", default(string));

                templateInfo.ApproveId = DataHelper.GetSafeLong(dataReader, "ApproveId", default(long));
                templateInfo.ApproveName = DataHelper.GetSafeString(dataReader, "ApproveName", default(string));



                templateInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                templateInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                templateInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                templateInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                templateInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                templateInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                templateInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                templateInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                templateInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (templateInfoCollection == null)
                    templateInfoCollection = new TemplateInfoCollection();

                templateInfoCollection.Add(templateInfo);
            }

            return templateInfoCollection;
        }
    }
}
