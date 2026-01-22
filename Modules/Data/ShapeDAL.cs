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
    public class ShapeDAL
    {
        public static byte Insert(ShapeInfo shapeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsShape");

                db.AddInParameter(dbCommand, "Name", DbType.String, shapeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, shapeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, shapeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, shapeInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, shapeInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, shapeInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, shapeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, shapeInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "TypeId", DbType.Int64, shapeInfo.TypeId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                shapeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long shapeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelShape");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, shapeId);
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

        public static byte Update(ShapeInfo shapeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdShape");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, shapeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, shapeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, shapeInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, shapeInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, shapeInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, shapeInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, shapeInfo.UpdatedTime);
                db.AddInParameter(dbCommand, "TypeId", DbType.Int64, shapeInfo.TypeId);

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

        public static ShapeInfoCollection GetAll()
        {
            ShapeInfoCollection shapeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetShape");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    shapeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shapeInfoCollection;
        }

        public static ShapeInfo Get(long shapeId)
        {
            ShapeInfoCollection shapeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetShape");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, shapeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    shapeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (shapeInfoCollection != null && shapeInfoCollection.Count > 0) ? shapeInfoCollection[0] : null;
        }

        public static ShapeInfoCollection GetByTypeId(long typeId)
        {
            ShapeInfoCollection shapeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetShapeByTypeId");
                db.AddInParameter(dbCommand, "TypeId", DbType.Int64, typeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    shapeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shapeInfoCollection;
        }

        public static ShapeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ShapeInfoCollection shapeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchShape");

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
                    shapeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shapeInfoCollection;
        }

        public static ShapeInfoCollection Search(ShapeSearchParams shapeSearchParams, out long totalRows)
        {
            ShapeInfoCollection shapeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchShape");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, shapeSearchParams.CurrentPage);

                if (shapeSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, shapeSearchParams.PageSize);

                if (string.IsNullOrEmpty(shapeSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, shapeSearchParams.SortColumn);

                if (string.IsNullOrEmpty(shapeSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, shapeSearchParams.SortOrder);


                if (shapeSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, shapeSearchParams.CreatedId);

                if (shapeSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, shapeSearchParams.UpdateId);

                if (shapeSearchParams.TypeId == 0)
                    db.AddInParameter(dbCommand, "TypeId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "TypeId", DbType.Int64, shapeSearchParams.TypeId);

                if (string.IsNullOrEmpty(shapeSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, shapeSearchParams.Name);

                if (string.IsNullOrEmpty(shapeSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, shapeSearchParams.Description);

                if (string.IsNullOrEmpty(shapeSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, shapeSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    shapeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return shapeInfoCollection;
        }

        private static ShapeInfoCollection GetAsList(IDataReader dataReader)
        {
            ShapeInfoCollection shapeInfoCollection = null;

            while (dataReader.Read())
            {
                ShapeInfo shapeInfo = new ShapeInfo();
                shapeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                shapeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                shapeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                shapeInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                shapeInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                shapeInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                shapeInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                shapeInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                shapeInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                shapeInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                shapeInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));
                shapeInfo.TypeId = DataHelper.GetSafeLong(dataReader, "TypeId", default(long));
                shapeInfo.TypeName = DataHelper.GetSafeString(dataReader, "TypeName", default(string));
                shapeInfo.TypeDescription = DataHelper.GetSafeString(dataReader, "TypeDescription", default(string));
                shapeInfo.TypeAltDescription = DataHelper.GetSafeString(dataReader, "TypeAltDescription", default(string));

                if (shapeInfoCollection == null)
                    shapeInfoCollection = new ShapeInfoCollection();

                shapeInfoCollection.Add(shapeInfo);
            }

            return shapeInfoCollection;
        }
    }
}
