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
    public class ModelDAL
    {
        public static byte Insert(ModelInfo modelInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsModel");

                db.AddInParameter(dbCommand, "Name", DbType.String, modelInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, modelInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, modelInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, modelInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, modelInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, modelInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, modelInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, modelInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "MakeId", DbType.Int64, modelInfo.MakeId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                modelInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long modelId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelModel");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, modelId);
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

        public static byte Update(ModelInfo modelInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdModel");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, modelInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, modelInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, modelInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, modelInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, modelInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, modelInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, modelInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "MakeId", DbType.Int64, modelInfo.MakeId);

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

        public static ModelInfoCollection GetAll()
        {
            ModelInfoCollection modelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetModel");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    modelInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelInfoCollection;
        }

        public static ModelInfo Get(long modelId)
        {
            ModelInfoCollection modelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetModel");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, modelId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    modelInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (modelInfoCollection != null && modelInfoCollection.Count > 0) ? modelInfoCollection[0] : null;
        }

        public static ModelInfoCollection GetByMakeId(long makeId)
        {
            ModelInfoCollection modelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetModelByMakeId");
                db.AddInParameter(dbCommand, "MakeId", DbType.Int64, makeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    modelInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelInfoCollection;
        }

        public static ModelInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ModelInfoCollection modelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchModel");

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
                    modelInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelInfoCollection;
        }

        public static ModelInfoCollection Search(ModelSearchParams modelSearchParams, out long totalRows)
        {
            ModelInfoCollection modelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchModel");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, modelSearchParams.CurrentPage);

                if (modelSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, modelSearchParams.PageSize);

                if (string.IsNullOrEmpty(modelSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, modelSearchParams.SortColumn);

                if (string.IsNullOrEmpty(modelSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, modelSearchParams.SortOrder);


                if (modelSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, modelSearchParams.CreatedId);

                if (modelSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, modelSearchParams.UpdateId);

                if (modelSearchParams.MakeId == 0)
                    db.AddInParameter(dbCommand, "MakeId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "MakeId", DbType.Int64, modelSearchParams.MakeId);

                if (string.IsNullOrEmpty(modelSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, modelSearchParams.Name);

                if (string.IsNullOrEmpty(modelSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, modelSearchParams.Description);

                if (string.IsNullOrEmpty(modelSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, modelSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    modelInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return modelInfoCollection;
        }

        private static ModelInfoCollection GetAsList(IDataReader dataReader)
        {
            ModelInfoCollection modelInfoCollection = null;

            while (dataReader.Read())
            {
                ModelInfo modelInfo = new ModelInfo();
                modelInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                modelInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                modelInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                modelInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                modelInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                modelInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                modelInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                modelInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                modelInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                modelInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                modelInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));
                modelInfo.MakeId = DataHelper.GetSafeLong(dataReader, "MakeId", default(long));
                modelInfo.MakeName = DataHelper.GetSafeString(dataReader, "MakeName", default(string));
                modelInfo.MakeDescription = DataHelper.GetSafeString(dataReader, "MakeDescription", default(string));
                modelInfo.MakeAltDescription = DataHelper.GetSafeString(dataReader, "MakeAltDescription", default(string));

                if (modelInfoCollection == null)
                    modelInfoCollection = new ModelInfoCollection();

                modelInfoCollection.Add(modelInfo);
            }

            return modelInfoCollection;
        }
    }
}
