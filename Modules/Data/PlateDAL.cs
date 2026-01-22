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
    public class PlateDAL
    {
        public static byte Insert(PlateInfo plateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsPlate");

                db.AddInParameter(dbCommand, "Name", DbType.String,  plateInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, plateInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, plateInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, plateInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, plateInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, plateInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, plateInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, plateInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                plateInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long plateId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelPlate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, plateId);
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

        public static byte Update(PlateInfo plateInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdPlate");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, plateInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, plateInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, plateInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, plateInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, plateInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, plateInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, plateInfo.UpdatedTime);

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

        public static PlateInfo Get(long PlateId)
        {
            PlateInfoCollection plateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPlate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, PlateId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    plateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (plateInfoCollection != null && plateInfoCollection.Count > 0) ? plateInfoCollection[0] : null;
        }

        public static PlateInfoCollection GetAll()
        {
            PlateInfoCollection plateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPlate");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    plateInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plateInfoCollection;
        }

        public static PlateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            PlateInfoCollection plateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchPlate");

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
                    plateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plateInfoCollection;
        }

        public static PlateInfoCollection Search(PlateSearchParams plateSearchParams, out long totalRows)
        {
            PlateInfoCollection plateInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchPlate");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, plateSearchParams.CurrentPage);

                if (plateSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, plateSearchParams.PageSize);

                if (string.IsNullOrEmpty(plateSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String,plateSearchParams.SortColumn);

                if (string.IsNullOrEmpty(plateSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, plateSearchParams.SortOrder);
                             

                if (plateSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, plateSearchParams.CreatedId);

                if (plateSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, plateSearchParams.UpdateId);

                if (string.IsNullOrEmpty(plateSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, plateSearchParams.Name);

                if (string.IsNullOrEmpty(plateSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, plateSearchParams.Description);

                if (string.IsNullOrEmpty(plateSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, plateSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                   plateInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return plateInfoCollection;
        }

        private static PlateInfoCollection GetAsList(IDataReader dataReader)
        {
            PlateInfoCollection plateInfoCollection = null;

            while (dataReader.Read())
            {
                PlateInfo plateInfo = new PlateInfo();
                plateInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                plateInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                plateInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                plateInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                plateInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                plateInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                plateInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                plateInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                plateInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                plateInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                plateInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (plateInfoCollection == null)
                    plateInfoCollection = new PlateInfoCollection();

                plateInfoCollection.Add(plateInfo);
            }

            return plateInfoCollection;
        }
    }
}
