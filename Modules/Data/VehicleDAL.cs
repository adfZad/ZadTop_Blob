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
    public class VehicleDAL
    {
        public static byte Insert(VehicleInfo vehicleInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsVehicle");

                db.AddInParameter(dbCommand, "Name", DbType.String, vehicleInfo.Name);
                db.AddInParameter(dbCommand, "ModelId", DbType.Int64, vehicleInfo.ModelId);
                db.AddInParameter(dbCommand, "ShapeId", DbType.Int64, vehicleInfo.ShapeId);
                db.AddInParameter(dbCommand, "ColorId", DbType.Int64, vehicleInfo.ColorId);
                db.AddInParameter(dbCommand, "CustomerId", DbType.Int64, vehicleInfo.CustomerId);
                db.AddInParameter(dbCommand, "ModelYear", DbType.Int64, vehicleInfo.ModelYear);
           
                db.AddInParameter(dbCommand, "ChassisNo", DbType.String, vehicleInfo.ChassisNo);
                db.AddInParameter(dbCommand, "EngineNo", DbType.String, vehicleInfo.EngineNo);
                db.AddInParameter(dbCommand, "Cylinders", DbType.Int64, vehicleInfo.Cylinders);
                db.AddInParameter(dbCommand, "Passengers", DbType.Int64, vehicleInfo.Passengers);

                if (vehicleInfo.PurchaseDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "PurchaseDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PurchaseDate", DbType.DateTime, vehicleInfo.PurchaseDate);
                
                db.AddInParameter(dbCommand, "PurchaseCost", DbType.Decimal, vehicleInfo.PurchaseCost);
              
                db.AddInParameter(dbCommand, "Description", DbType.String, vehicleInfo.Description);
                db.AddInParameter(dbCommand, "Photo", DbType.String, vehicleInfo.Photo);
                db.AddInParameter(dbCommand, "Registration", DbType.String, vehicleInfo.Registration);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, vehicleInfo.Active);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, vehicleInfo.AltDescription);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, vehicleInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, vehicleInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, vehicleInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, vehicleInfo.UpdatedTime);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                vehicleInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long vehicleId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelVehicle");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, vehicleId);
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

        public static byte Update(VehicleInfo vehicleInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdVehicle");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, vehicleInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, vehicleInfo.Name);
                db.AddInParameter(dbCommand, "ModelId", DbType.Int64, vehicleInfo.ModelId);
                db.AddInParameter(dbCommand, "ShapeId", DbType.Int64, vehicleInfo.ShapeId);
                db.AddInParameter(dbCommand, "ColorId", DbType.Int64, vehicleInfo.ColorId);
                db.AddInParameter(dbCommand, "CustomerId", DbType.Int64, vehicleInfo.CustomerId);
                db.AddInParameter(dbCommand, "ModelYear", DbType.Int64, vehicleInfo.ModelYear);

                db.AddInParameter(dbCommand, "ChassisNo", DbType.String, vehicleInfo.ChassisNo);
                db.AddInParameter(dbCommand, "EngineNo", DbType.String, vehicleInfo.EngineNo);
                db.AddInParameter(dbCommand, "Cylinders", DbType.Int64, vehicleInfo.Cylinders);
                db.AddInParameter(dbCommand, "Passengers", DbType.Int64, vehicleInfo.Passengers);

                if (vehicleInfo.PurchaseDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "PurchaseDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PurchaseDate", DbType.DateTime, vehicleInfo.PurchaseDate);

                db.AddInParameter(dbCommand, "PurchaseCost", DbType.Decimal, vehicleInfo.PurchaseCost);

                db.AddInParameter(dbCommand, "Description", DbType.String, vehicleInfo.Description);
                db.AddInParameter(dbCommand, "Photo", DbType.String, vehicleInfo.Photo);
                db.AddInParameter(dbCommand, "Registration", DbType.String, vehicleInfo.Registration);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, vehicleInfo.Active);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, vehicleInfo.AltDescription);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, vehicleInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, vehicleInfo.UpdatedTime);

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

        public static VehicleInfo Get(long VehicleId)
        {
            VehicleInfoCollection vehicleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetVehicle");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, VehicleId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    vehicleInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (vehicleInfoCollection != null && vehicleInfoCollection.Count > 0) ? vehicleInfoCollection[0] : null;
        }

        public static VehicleInfoCollection GetAll()
        {
            VehicleInfoCollection vehicleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetVehicle");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    vehicleInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vehicleInfoCollection;
        }

        public static VehicleInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            VehicleInfoCollection vehicleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchVehicle");

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
                    vehicleInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vehicleInfoCollection;
        }

        public static VehicleInfoCollection Search(VehicleSearchParams vehicleSearchParams, out long totalRows)
        {
            VehicleInfoCollection vehicleInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchVehicle");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, vehicleSearchParams.CurrentPage);

                if (vehicleSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, vehicleSearchParams.PageSize);

                if (string.IsNullOrEmpty(vehicleSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, vehicleSearchParams.SortColumn);

                if (string.IsNullOrEmpty(vehicleSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, vehicleSearchParams.SortOrder);


                if (vehicleSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, vehicleSearchParams.CreatedId);

                if (vehicleSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, vehicleSearchParams.UpdateId);

                if (string.IsNullOrEmpty(vehicleSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, vehicleSearchParams.Name);

                if (string.IsNullOrEmpty(vehicleSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, vehicleSearchParams.Description);

                if (string.IsNullOrEmpty(vehicleSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, vehicleSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    vehicleInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return vehicleInfoCollection;
        }

        private static VehicleInfoCollection GetAsList(IDataReader dataReader)
        {
            VehicleInfoCollection vehicleInfoCollection = null;

            while (dataReader.Read())
            {
                VehicleInfo vehicleInfo = new VehicleInfo();
                vehicleInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                vehicleInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));

                vehicleInfo.ModelId = DataHelper.GetSafeLong(dataReader, "ModelId", default(long));
                vehicleInfo.MakeId = DataHelper.GetSafeLong(dataReader, "MakeId", default(long));
                vehicleInfo.MakeName = DataHelper.GetSafeString(dataReader, "MakeName", default(string));
                vehicleInfo.MakeDescription = DataHelper.GetSafeString(dataReader, "MakeDescription", default(string));
                vehicleInfo.MakeAltDescription = DataHelper.GetSafeString(dataReader, "MakeAltDescription", default(string));
                vehicleInfo.ModelName = DataHelper.GetSafeString(dataReader, "ModelName", default(string));
                vehicleInfo.ModelDescription = DataHelper.GetSafeString(dataReader, "ModelDescription", default(string));
                vehicleInfo.ModelAltDescription = DataHelper.GetSafeString(dataReader, "ModelAltDescription", default(string));
               
                
                
                vehicleInfo.ShapeId = DataHelper.GetSafeLong(dataReader, "ShapeId", default(long));
                vehicleInfo.TypeId = DataHelper.GetSafeLong(dataReader, "TypeId", default(long));
                vehicleInfo.TypeName = DataHelper.GetSafeString(dataReader, "TypeName", default(string));
                vehicleInfo.TypeDescription = DataHelper.GetSafeString(dataReader, "TypeDescription", default(string));
                vehicleInfo.TypeAltDescription = DataHelper.GetSafeString(dataReader, "TypeAltDescription", default(string));
                vehicleInfo.ShapeName = DataHelper.GetSafeString(dataReader, "ShapeName", default(string));
                vehicleInfo.ShapeDescription = DataHelper.GetSafeString(dataReader, "ShapeDescription", default(string));
                vehicleInfo.ShapeAltDescription = DataHelper.GetSafeString(dataReader, "ShapeAltDescription", default(string));

                vehicleInfo.ColorId = DataHelper.GetSafeLong(dataReader, "ColorId", default(long));
                vehicleInfo.ColorName = DataHelper.GetSafeString(dataReader, "ColorName", default(string));
                vehicleInfo.ColorDescription = DataHelper.GetSafeString(dataReader, "ColorDescription", default(string));
                vehicleInfo.ColorAltDescription = DataHelper.GetSafeString(dataReader, "ColorAltDescription", default(string));

                vehicleInfo.CustomerId = DataHelper.GetSafeLong(dataReader, "CustomerId", default(long));
                vehicleInfo.CustomerCode = DataHelper.GetSafeString(dataReader, "CustomerCode", default(string));
                vehicleInfo.CustomerName = DataHelper.GetSafeString(dataReader, "CustomerName", default(string));
                vehicleInfo.CustomerQID = DataHelper.GetSafeString(dataReader, "CustomerQID", default(string));

                vehicleInfo.CustomerCountryID = DataHelper.GetSafeLong(dataReader, "CustomerCountryID", default(long));
                vehicleInfo.CustomerCountryName = DataHelper.GetSafeString(dataReader, "CustomerCountryName", default(string));
                vehicleInfo.CustomerCountryDescription = DataHelper.GetSafeString(dataReader, "CustomerCountryDescription", default(string));
                vehicleInfo.CustomerCountryAltDescription = DataHelper.GetSafeString(dataReader, "CustomerCountryAltDescription", default(string));
                vehicleInfo.CustomerPassportNo = DataHelper.GetSafeString(dataReader, "CustomerPassportNo", default(string));

                vehicleInfo.ModelYear = DataHelper.GetSafeLong(dataReader, "ModelYear", default(long));
                vehicleInfo.ChassisNo = DataHelper.GetSafeString(dataReader, "ChassisNo", default(string));
                vehicleInfo.EngineNo = DataHelper.GetSafeString(dataReader, "EngineNo", default(string));
                vehicleInfo.Cylinders = DataHelper.GetSafeLong(dataReader, "Cylinders", default(long));
                vehicleInfo.Passengers = DataHelper.GetSafeLong(dataReader, "Passengers", default(long));
                vehicleInfo.PurchaseDate = DataHelper.GetSafeDateTime(dataReader, "PurchaseDate", default(DateTime));
                vehicleInfo.PurchaseCost = DataHelper.GetSafeDecimal(dataReader, "PurchaseCost", default(decimal));
                vehicleInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));                              
                vehicleInfo.Photo = DataHelper.GetSafeString(dataReader, "Photo", default(string));
                vehicleInfo.Registration = DataHelper.GetSafeString(dataReader, "Registration", default(string));
                vehicleInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));
                vehicleInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));                              
                vehicleInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
       
                  
                vehicleInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                vehicleInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                vehicleInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                vehicleInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                vehicleInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));
                                
                if (vehicleInfoCollection == null)
                    vehicleInfoCollection = new VehicleInfoCollection();

                vehicleInfoCollection.Add(vehicleInfo);
            }

            return vehicleInfoCollection;
        }
    }
}
