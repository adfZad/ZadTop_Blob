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
    public class ColorDAL
    {
        public static byte Insert(ColorInfo colorInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsColor");

                db.AddInParameter(dbCommand, "Name", DbType.String, colorInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, colorInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, colorInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, colorInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, colorInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, colorInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, colorInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, colorInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                colorInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long colorId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelColor");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, colorId);
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

        public static byte Update(ColorInfo colorInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdColor");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, colorInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, colorInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, colorInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, colorInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, colorInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, colorInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, colorInfo.UpdatedTime);

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

        public static ColorInfo Get(long ColorId)
        {
            ColorInfoCollection colorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetColor");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, ColorId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    colorInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (colorInfoCollection != null && colorInfoCollection.Count > 0) ? colorInfoCollection[0] : null;
        }

        public static ColorInfoCollection GetAll()
        {
            ColorInfoCollection colorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetColor");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    colorInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colorInfoCollection;
        }

        public static ColorInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ColorInfoCollection colorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchColor");

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
                    colorInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colorInfoCollection;
        }

        public static ColorInfoCollection Search(ColorSearchParams colorSearchParams, out long totalRows)
        {
            ColorInfoCollection colorInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchColor");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, colorSearchParams.CurrentPage);

                if (colorSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, colorSearchParams.PageSize);

                if (string.IsNullOrEmpty(colorSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, colorSearchParams.SortColumn);

                if (string.IsNullOrEmpty(colorSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, colorSearchParams.SortOrder);


                if (colorSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, colorSearchParams.CreatedId);

                if (colorSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, colorSearchParams.UpdateId);

                if (string.IsNullOrEmpty(colorSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, colorSearchParams.Name);

                if (string.IsNullOrEmpty(colorSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, colorSearchParams.Description);

                if (string.IsNullOrEmpty(colorSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, colorSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    colorInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return colorInfoCollection;
        }

        private static ColorInfoCollection GetAsList(IDataReader dataReader)
        {
            ColorInfoCollection colorInfoCollection = null;

            while (dataReader.Read())
            {
                ColorInfo colorInfo = new ColorInfo();
                colorInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                colorInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                colorInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                colorInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                colorInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                colorInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                colorInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                colorInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                colorInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                colorInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                colorInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (colorInfoCollection == null)
                    colorInfoCollection = new ColorInfoCollection();

                colorInfoCollection.Add(colorInfo);
            }

            return colorInfoCollection;
        }
    }
}
