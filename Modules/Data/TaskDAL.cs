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
    public class TaskDAL
    {
        public static byte Insert(TaskInfo taskInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsTask");


                db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, taskInfo.DocumentId);
                db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskInfo.TaskStatusId);
                db.AddInParameter(dbCommand, "TaskTypeId", DbType.Int64, taskInfo.TaskTypeId);
                db.AddInParameter(dbCommand, "ApprovalStatusId", DbType.Int64, taskInfo.ApprovalStatusId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, taskInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, taskInfo.LevelId);
                db.AddInParameter(dbCommand, "ApproveId", DbType.Int64, taskInfo.ApproveId);
                db.AddInParameter(dbCommand, "Description", DbType.String, taskInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, taskInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, taskInfo.Active);

                if (taskInfo.CreatedTime == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, taskInfo.CreatedTime);

                if (taskInfo.UpdatedTime == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, taskInfo.UpdatedTime);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                taskInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long taskId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelTask");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, taskId);
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

        public static byte Update(TaskInfo taskInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdTask");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, taskInfo.Id);
                db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, taskInfo.DocumentId);
                db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskInfo.TaskStatusId);
                db.AddInParameter(dbCommand, "TaskTypeId", DbType.Int64, taskInfo.TaskTypeId);
                db.AddInParameter(dbCommand, "ApprovalStatusId", DbType.Int64, taskInfo.ApprovalStatusId);
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, taskInfo.UserId);
                db.AddInParameter(dbCommand, "LevelId", DbType.Int64, taskInfo.LevelId);
                db.AddInParameter(dbCommand, "ApproveId", DbType.Int64, taskInfo.ApproveId);
                db.AddInParameter(dbCommand, "Description", DbType.String, taskInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, taskInfo.AltDescription);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, taskInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, taskInfo.Active);

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

        public static byte Update_Start_Time(TaskInfo taskInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdTask_Start_Time");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, taskInfo.Id);
                db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskInfo.TaskStatusId);
                db.AddInParameter(dbCommand, "ApprovalStatusId", DbType.Int64, taskInfo.ApprovalStatusId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, taskInfo.CreatedTime);

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

        public static byte Update_End_Time(TaskInfo taskInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdTask_End_Time");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, taskInfo.Id);
                db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskInfo.TaskStatusId);
                db.AddInParameter(dbCommand, "ApprovalStatusId", DbType.Int64, taskInfo.ApprovalStatusId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, taskInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, taskInfo.AltDescription);

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

        public static byte Update_Return_End_Time(TaskInfo taskInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdReturnTask_End_Time");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, taskInfo.Id);
                db.AddInParameter(dbCommand, "TaskTypeId", DbType.Int64, taskInfo.TaskTypeId);
                db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskInfo.TaskStatusId);
                db.AddInParameter(dbCommand, "ApprovalStatusId", DbType.Int64, taskInfo.ApprovalStatusId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, taskInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, taskInfo.AltDescription);

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

        public static TaskInfo Get(long TaskId)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTask");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, TaskId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (taskInfoCollection != null && taskInfoCollection.Count > 0) ? taskInfoCollection[0] : null;
        }

        public static TaskInfo GetNextTask(long documentId)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetNextTask");
                db.AddInParameter(dbCommand, "DocumemtId", DbType.Int64, documentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (taskInfoCollection != null && taskInfoCollection.Count > 0) ? taskInfoCollection[0] : null;
        }

        public static TaskInfoCollection GetAll()
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTask");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskInfoCollection;
        }

        public static TaskInfoCollection GetTaskByFlow(long flowId)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTaskByFlowId");
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, flowId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskInfoCollection;
        }

        public static TaskInfoCollection GetTaskByDocument(long documentId)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTaskByDocumentId");
                db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, documentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskInfoCollection;
        }

        public static TaskInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchTask");

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
                    taskInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskInfoCollection;
        }

        public static TaskInfoCollection Search(TaskSearchParams taskSearchParams, out long totalRows)
        {
            TaskInfoCollection taskInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchTask");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, taskSearchParams.CurrentPage);

                if (taskSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, taskSearchParams.PageSize);

                if (string.IsNullOrEmpty(taskSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, taskSearchParams.SortColumn);

                if (string.IsNullOrEmpty(taskSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, taskSearchParams.SortOrder);

                if (taskSearchParams.DocumentId == 0)
                    db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, taskSearchParams.DocumentId);

                if (taskSearchParams.UserId == 0)
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UserId", DbType.Int64, taskSearchParams.UserId);

                if (taskSearchParams.LevelId == 0)
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "LevelId", DbType.Int64, taskSearchParams.LevelId);


                if (taskSearchParams.TaskStatusId == 0)
                    db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "TaskStatusId", DbType.Int64, taskSearchParams.TaskStatusId);


                if (taskSearchParams.TaskTypeId == 0)
                    db.AddInParameter(dbCommand, "TaskTypeId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "TaskTypeId", DbType.Int64, taskSearchParams.TaskTypeId);

                                         
             
                if (string.IsNullOrEmpty(taskSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, taskSearchParams.Description);

                if (string.IsNullOrEmpty(taskSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, taskSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    taskInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return taskInfoCollection;
        }

        private static TaskInfoCollection GetAsList(IDataReader dataReader)
        {
            TaskInfoCollection taskInfoCollection = null;

            while (dataReader.Read())
            {
                TaskInfo taskInfo = new TaskInfo();
                taskInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));


                taskInfo.DocumentId = DataHelper.GetSafeLong(dataReader, "DocumentId", default(long));
                taskInfo.DocumentName = DataHelper.GetSafeString(dataReader, "DocumentName", default(string));

                taskInfo.DocumentTypeId = DataHelper.GetSafeLong(dataReader, "DocumentTypeId", default(long));
                taskInfo.DocumentTypeName = DataHelper.GetSafeString(dataReader, "DocumentTypeName", default(string));
                taskInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                taskInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));
                taskInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                taskInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));

                taskInfo.ApproveId = DataHelper.GetSafeLong(dataReader, "ApproveId", default(long));
                taskInfo.ApproveName = DataHelper.GetSafeString(dataReader, "ApproveName", default(string));

                taskInfo.DocumentPurpose = DataHelper.GetSafeString(dataReader, "DocumentPurpose", default(string));
                taskInfo.DocumentDescription = DataHelper.GetSafeString(dataReader, "DocumentDescription", default(string));
                taskInfo.DocumentAltDescription = DataHelper.GetSafeString(dataReader, "DocumentAltDescription", default(string));

                taskInfo.DocumentAmount = DataHelper.GetSafeDecimal(dataReader, "DocumentAmount", default(decimal));
                taskInfo.OtherAmount = DataHelper.GetSafeDecimal(dataReader, "OtherAmount", default(decimal));
                taskInfo.Rate = DataHelper.GetSafeDecimal(dataReader, "Rate", default(decimal));
                taskInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
               

                taskInfo.DocumentCreatedId = DataHelper.GetSafeLong(dataReader, "DocumentCreatedId", default(long));
                taskInfo.DocumentCreatedName = DataHelper.GetSafeString(dataReader, "DocumentCreatedName", default(string));

                taskInfo.TaskStatusId = DataHelper.GetSafeLong(dataReader, "TaskStatusId", default(long));
                taskInfo.TaskStatusName = DataHelper.GetSafeString(dataReader, "TaskStatusName", default(string));

                taskInfo.TaskTypeId = DataHelper.GetSafeLong(dataReader, "TaskTypeId", default(long));
                taskInfo.TaskTypeName = DataHelper.GetSafeString(dataReader, "TaskTypeName", default(string));

                taskInfo.ApprovalStatusId = DataHelper.GetSafeLong(dataReader, "ApprovalStatusId", default(long));
                taskInfo.ApprovalStatusName = DataHelper.GetSafeString(dataReader, "ApprovalStatusName", default(string));

                taskInfo.UserId = DataHelper.GetSafeLong(dataReader, "UserId", default(long));
                taskInfo.UserName = DataHelper.GetSafeString(dataReader, "UserName", default(string));
                taskInfo.UserFirstName = DataHelper.GetSafeString(dataReader, "UserFirstName", default(string));
                taskInfo.UserLastName = DataHelper.GetSafeString(dataReader, "UserLastName", default(string));
                taskInfo.UserDesignationId = DataHelper.GetSafeLong(dataReader, "UserDesignationId", default(long));
                taskInfo.UserDesignationName = DataHelper.GetSafeString(dataReader, "UserDesignationName", default(string));
                taskInfo.UserDescription = DataHelper.GetSafeString(dataReader, "UserDescription", default(string));
                taskInfo.UserAltDescription = DataHelper.GetSafeString(dataReader, "UserAltDescription", default(string));

                taskInfo.LevelId = DataHelper.GetSafeLong(dataReader, "LevelId", default(long));
                taskInfo.LevelName = DataHelper.GetSafeString(dataReader, "LevelName", default(string));



                taskInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                taskInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                taskInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));
                
                taskInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));
               
                taskInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (taskInfoCollection == null)
                    taskInfoCollection = new TaskInfoCollection();

                taskInfoCollection.Add(taskInfo);
            }

            return taskInfoCollection;
        }
    }
}
