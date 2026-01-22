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
    public class DivisionDAL
    {
        public static byte Insert(DivisionInfo divisionInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDivision");

                db.AddInParameter(dbCommand, "Name", DbType.String, divisionInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, divisionInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, divisionInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, divisionInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, divisionInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, divisionInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, divisionInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, divisionInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                divisionInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long divisionId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDivision");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, divisionId);
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

        public static byte Update(DivisionInfo divisionInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDivision");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, divisionInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, divisionInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, divisionInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, divisionInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, divisionInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, divisionInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, divisionInfo.UpdatedTime);

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

        public static DivisionInfo Get(long DivisionId)
        {
            DivisionInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDivision");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DivisionId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (divisionInfoCollection != null && divisionInfoCollection.Count > 0) ? divisionInfoCollection[0] : null;
        }

        public static DivisionInfoCollection GetAll()
        {
            DivisionInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDivision");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        public static DivisionInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DivisionInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDivision");

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
                    divisionInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        public static DivisionInfoCollection Search(DivisionSearchParams divisionSearchParams, out long totalRows)
        {
            DivisionInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDivision");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, divisionSearchParams.CurrentPage);

                if (divisionSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, divisionSearchParams.PageSize);

                if (string.IsNullOrEmpty(divisionSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, divisionSearchParams.SortColumn);

                if (string.IsNullOrEmpty(divisionSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, divisionSearchParams.SortOrder);


                if (divisionSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, divisionSearchParams.CreatedId);

                if (divisionSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, divisionSearchParams.UpdateId);

                if (string.IsNullOrEmpty(divisionSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, divisionSearchParams.Name);

                if (string.IsNullOrEmpty(divisionSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, divisionSearchParams.Description);

                if (string.IsNullOrEmpty(divisionSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, divisionSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        private static DivisionInfoCollection GetAsList(IDataReader dataReader)
        {
            DivisionInfoCollection divisionInfoCollection = null;

            while (dataReader.Read())
            {
                DivisionInfo divisionInfo = new DivisionInfo();
                divisionInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                divisionInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                divisionInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                divisionInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                divisionInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                divisionInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                divisionInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                divisionInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                divisionInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                divisionInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                divisionInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (divisionInfoCollection == null)
                    divisionInfoCollection = new DivisionInfoCollection();

                divisionInfoCollection.Add(divisionInfo);
            }

            return divisionInfoCollection;
        }
    }
}

