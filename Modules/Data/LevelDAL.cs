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
    public class LevelDAL
    {
        public static byte Insert(LevelInfo levelInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsLevel");

                db.AddInParameter(dbCommand, "Name", DbType.String, levelInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, levelInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, levelInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, levelInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, levelInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, levelInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, levelInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, levelInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                levelInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long levelId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelLevel");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, levelId);
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

        public static byte Update(LevelInfo levelInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdLevel");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, levelInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, levelInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, levelInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, levelInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, levelInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, levelInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, levelInfo.UpdatedTime);

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

        public static LevelInfo Get(long LevelId)
        {
            LevelInfoCollection levelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetLevel");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, LevelId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    levelInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (levelInfoCollection != null && levelInfoCollection.Count > 0) ? levelInfoCollection[0] : null;
        }

        public static LevelInfoCollection GetAll()
        {
            LevelInfoCollection levelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetLevel");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    levelInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return levelInfoCollection;
        }

        public static LevelInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            LevelInfoCollection levelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchLevel");

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
                    levelInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return levelInfoCollection;
        }

        public static LevelInfoCollection Search(LevelSearchParams levelSearchParams, out long totalRows)
        {
            LevelInfoCollection levelInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchLevel");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, levelSearchParams.CurrentPage);

                if (levelSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, levelSearchParams.PageSize);

                if (string.IsNullOrEmpty(levelSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, levelSearchParams.SortColumn);

                if (string.IsNullOrEmpty(levelSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, levelSearchParams.SortOrder);


                if (levelSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, levelSearchParams.CreatedId);

                if (levelSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, levelSearchParams.UpdateId);

                if (string.IsNullOrEmpty(levelSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, levelSearchParams.Name);

                if (string.IsNullOrEmpty(levelSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, levelSearchParams.Description);

                if (string.IsNullOrEmpty(levelSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, levelSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    levelInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return levelInfoCollection;
        }

        private static LevelInfoCollection GetAsList(IDataReader dataReader)
        {
            LevelInfoCollection levelInfoCollection = null;

            while (dataReader.Read())
            {
                LevelInfo levelInfo = new LevelInfo();
                levelInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                levelInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                levelInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                levelInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                levelInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                levelInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                levelInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                levelInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                levelInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                levelInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                levelInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (levelInfoCollection == null)
                    levelInfoCollection = new LevelInfoCollection();

                levelInfoCollection.Add(levelInfo);
            }

            return levelInfoCollection;
        }
    }
}


