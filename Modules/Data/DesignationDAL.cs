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
    public class DesignationDAL
    {
        public static byte Insert(DesignationInfo designationInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDesignation");

                db.AddInParameter(dbCommand, "Name", DbType.String, designationInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, designationInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, designationInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, designationInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, designationInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, designationInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, designationInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, designationInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                designationInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long designationId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDesignation");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, designationId);
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

        public static byte Update(DesignationInfo designationInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDesignation");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, designationInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, designationInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, designationInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, designationInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, designationInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, designationInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, designationInfo.UpdatedTime);

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

        public static DesignationInfo Get(long DesignationId)
        {
            DesignationInfoCollection designationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDesignation");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DesignationId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    designationInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (designationInfoCollection != null && designationInfoCollection.Count > 0) ? designationInfoCollection[0] : null;
        }

        public static DesignationInfoCollection GetAll()
        {
            DesignationInfoCollection designationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDesignation");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    designationInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return designationInfoCollection;
        }

        public static DesignationInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DesignationInfoCollection designationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDesignation");

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
                    designationInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return designationInfoCollection;
        }

        public static DesignationInfoCollection Search(DesignationSearchParams designationSearchParams, out long totalRows)
        {
            DesignationInfoCollection designationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDesignation");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, designationSearchParams.CurrentPage);

                if (designationSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, designationSearchParams.PageSize);

                if (string.IsNullOrEmpty(designationSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, designationSearchParams.SortColumn);

                if (string.IsNullOrEmpty(designationSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, designationSearchParams.SortOrder);


                if (designationSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, designationSearchParams.CreatedId);

                if (designationSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, designationSearchParams.UpdateId);

                if (string.IsNullOrEmpty(designationSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, designationSearchParams.Name);

                if (string.IsNullOrEmpty(designationSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, designationSearchParams.Description);

                if (string.IsNullOrEmpty(designationSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, designationSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    designationInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return designationInfoCollection;
        }

        private static DesignationInfoCollection GetAsList(IDataReader dataReader)
        {
            DesignationInfoCollection designationInfoCollection = null;

            while (dataReader.Read())
            {
                DesignationInfo designationInfo = new DesignationInfo();
                designationInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                designationInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                designationInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                designationInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                designationInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                designationInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                designationInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                designationInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                designationInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                designationInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                designationInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (designationInfoCollection == null)
                    designationInfoCollection = new DesignationInfoCollection();

                designationInfoCollection.Add(designationInfo);
            }

            return designationInfoCollection;
        }
    }
}
