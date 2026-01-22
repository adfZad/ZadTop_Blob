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
    public class MakeDAL
    {
        public static byte Insert(MakeInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsMake");

                db.AddInParameter(dbCommand, "Name", DbType.String,  makeInfo.Name);
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
                DbCommand dbCommand = db.GetStoredProcCommand("DelMake");

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

        public static byte Update(MakeInfo makeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdMake");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, makeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, makeInfo.Name);
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

        public static MakeInfo Get(long MakeId)
        {
            MakeInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMake");
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

        public static MakeInfoCollection GetAll()
        {
            MakeInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetMake");
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

        public static MakeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            MakeInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchMake");

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

        public static MakeInfoCollection Search(MakeSearchParams makeSearchParams, out long totalRows)
        {
            MakeInfoCollection makeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchMake");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, makeSearchParams.CurrentPage);

                if (makeSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, makeSearchParams.PageSize);

                if (string.IsNullOrEmpty(makeSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String,makeSearchParams.SortColumn);

                if (string.IsNullOrEmpty(makeSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, makeSearchParams.SortOrder);
                             

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

        private static MakeInfoCollection GetAsList(IDataReader dataReader)
        {
            MakeInfoCollection makeInfoCollection = null;

            while (dataReader.Read())
            {
                MakeInfo makeInfo = new MakeInfo();
                makeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                makeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
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
                    makeInfoCollection = new MakeInfoCollection();

                makeInfoCollection.Add(makeInfo);
            }

            return makeInfoCollection;
        }
    }
}
