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
    public class PolicyDAL
    {
        public static byte Insert(PolicyInfo PolicyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsPolicy");

               
                db.AddInParameter(dbCommand, "Amendment_No", DbType.Int64, PolicyInfo.Amendment_No);

                if (PolicyInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "IssueDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "IssueDate", DbType.DateTime, PolicyInfo.IssueDate);

                if (PolicyInfo.PaymentDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "PaymentDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PaymentDate", DbType.DateTime, PolicyInfo.PaymentDate);

                if (PolicyInfo.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, PolicyInfo.DateFrom);

                if (PolicyInfo.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, PolicyInfo.DateFrom);

                db.AddInParameter(dbCommand, "RegFlag", DbType.Boolean, PolicyInfo.RegFlag);

                db.AddInParameter(dbCommand, "InsuredValue", DbType.Decimal, PolicyInfo.InsuredValue);
                db.AddInParameter(dbCommand, "Premium", DbType.Decimal, PolicyInfo.Premium);
                db.AddInParameter(dbCommand, "Deductibles", DbType.Decimal, PolicyInfo.Deductibles);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, PolicyInfo.Active);
                db.AddInParameter(dbCommand, "Description", DbType.String, PolicyInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, PolicyInfo.AltDescription);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, PolicyInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, PolicyInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, PolicyInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, PolicyInfo.UpdatedTime);

                db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, PolicyInfo.VehicleId);
                db.AddInParameter(dbCommand, "CoverageId", DbType.Int64, PolicyInfo.CoverageId);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, PolicyInfo.CompanyId);
            
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                PolicyInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long PolicyId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelPolicy");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, PolicyId);
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

        public static byte Update(PolicyInfo PolicyInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdPolicy");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, PolicyInfo.Id);
                db.AddInParameter(dbCommand, "Amendment_No", DbType.Int64, PolicyInfo.Amendment_No);

                if (PolicyInfo.IssueDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "IssueDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "IssueDate", DbType.DateTime, PolicyInfo.IssueDate);

                if (PolicyInfo.PaymentDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "PaymentDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PaymentDate", DbType.DateTime, PolicyInfo.PaymentDate);

                if (PolicyInfo.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, PolicyInfo.DateFrom);

                if (PolicyInfo.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, PolicyInfo.DateFrom);

                db.AddInParameter(dbCommand, "RegFlag", DbType.Boolean, PolicyInfo.RegFlag);

                db.AddInParameter(dbCommand, "InsuredValue", DbType.Decimal, PolicyInfo.InsuredValue);
                db.AddInParameter(dbCommand, "Premium", DbType.Decimal, PolicyInfo.Premium);
                db.AddInParameter(dbCommand, "Deductibles", DbType.Decimal, PolicyInfo.Deductibles);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, PolicyInfo.Active);
                db.AddInParameter(dbCommand, "Description", DbType.String, PolicyInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, PolicyInfo.AltDescription);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, PolicyInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, PolicyInfo.UpdatedTime);

                db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, PolicyInfo.VehicleId);
                db.AddInParameter(dbCommand, "CoverageId", DbType.Int64, PolicyInfo.CoverageId);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, PolicyInfo.CompanyId);

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

        public static PolicyInfo Get(long PolicyId)
        {
            PolicyInfoCollection PolicyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPolicy");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, PolicyId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    PolicyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (PolicyInfoCollection != null && PolicyInfoCollection.Count > 0) ? PolicyInfoCollection[0] : null;
        }

        public static PolicyInfoCollection GetAll()
        {
            PolicyInfoCollection PolicyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPolicy");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    PolicyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PolicyInfoCollection;
        }

        public static PolicyInfo GetPolicyByVehicleId(long vehicleId)
        {
            PolicyInfoCollection PolicyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPolicyByVehicleId");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, vehicleId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    PolicyInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (PolicyInfoCollection != null && PolicyInfoCollection.Count > 0) ? PolicyInfoCollection[0] : null;
        }

        public static PolicyInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            PolicyInfoCollection PolicyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchPolicy");

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
                    PolicyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return PolicyInfoCollection;
        }

        public static PolicyInfoCollection Search(PolicySearchParams policySearchParams, out long totalRows)
        {
            PolicyInfoCollection policyInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchPolicy");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, policySearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, policySearchParams.PageSize);

                if (string.IsNullOrEmpty(policySearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, policySearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, policySearchParams.SortOrder);

                if (policySearchParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, policySearchParams.DateFrom);

                if (policySearchParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, policySearchParams.DateTo);

                if (policySearchParams.PolicyId == 0)
                    db.AddInParameter(dbCommand, "PolicyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PolicyId", DbType.Int64, policySearchParams.PolicyId);

                if (policySearchParams.CustomerId == 0)
                    db.AddInParameter(dbCommand, "CustomerId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CustomerId", DbType.Int64, policySearchParams.CustomerId);

                if (policySearchParams.VehicleId == 0)
                    db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, policySearchParams.VehicleId);

                if (policySearchParams.MakeId == 0)
                    db.AddInParameter(dbCommand, "MakeId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "MakeId", DbType.Int64, policySearchParams.MakeId);

                if (policySearchParams.ModelId == 0)
                    db.AddInParameter(dbCommand, "ModelId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "ModelId", DbType.Int64, policySearchParams.ModelId);

                if (policySearchParams.VariantId == 0)
                    db.AddInParameter(dbCommand, "VariantId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "VariantId", DbType.Int64, policySearchParams.VariantId);

                if (string.IsNullOrEmpty(policySearchParams.ChassisNo))
                    db.AddInParameter(dbCommand, "ChassisNo", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "ChassisNo", DbType.String, policySearchParams.ChassisNo);

                if (string.IsNullOrEmpty(policySearchParams.EngineNo))
                    db.AddInParameter(dbCommand, "EngineNo", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "EngineNo", DbType.String, policySearchParams.EngineNo);

              
                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    policyInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return policyInfoCollection;
        }

        private static PolicyInfoCollection GetAsList(IDataReader dataReader)
        {
            PolicyInfoCollection PolicyInfoCollection = null;

            while (dataReader.Read())
            {
                PolicyInfo PolicyInfo = new PolicyInfo();
                PolicyInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                PolicyInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                PolicyInfo.Amendment_No = DataHelper.GetSafeLong(dataReader, "Amendment_No", default(long));
                PolicyInfo.IssueDate = DataHelper.GetSafeDateTime(dataReader, "IssueDate", default(DateTime));
                PolicyInfo.PaymentDate = DataHelper.GetSafeDateTime(dataReader, "PaymentDate", default(DateTime));
                PolicyInfo.DateFrom = DataHelper.GetSafeDateTime(dataReader, "DateFrom", default(DateTime));
                PolicyInfo.DateTo = DataHelper.GetSafeDateTime(dataReader, "DateTo", default(DateTime));
                PolicyInfo.RegFlag = DataHelper.GetSafeBool(dataReader, "RegFlag", default(bool));
                PolicyInfo.InsuredValue = DataHelper.GetSafeDecimal(dataReader, "InsuredValue", default(decimal));
                PolicyInfo.Premium = DataHelper.GetSafeDecimal(dataReader, "Premium", default(decimal));
                PolicyInfo.Deductibles = DataHelper.GetSafeDecimal(dataReader, "Deductibles", default(decimal));

                PolicyInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));
                PolicyInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                PolicyInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));


                PolicyInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                PolicyInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                PolicyInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                PolicyInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                PolicyInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                PolicyInfo.VehicleId = DataHelper.GetSafeLong(dataReader, "VehicleId", default(long));

                PolicyInfo.VehicleName = DataHelper.GetSafeString(dataReader, "VehicleName", default(string));

                PolicyInfo.ModelId = DataHelper.GetSafeLong(dataReader, "ModelId", default(long));
                PolicyInfo.MakeId = DataHelper.GetSafeLong(dataReader, "MakeId", default(long));
                PolicyInfo.MakeName = DataHelper.GetSafeString(dataReader, "MakeName", default(string));
                PolicyInfo.MakeDescription = DataHelper.GetSafeString(dataReader, "MakeDescription", default(string));
                PolicyInfo.MakeAltDescription = DataHelper.GetSafeString(dataReader, "MakeAltDescription", default(string));
                PolicyInfo.ModelName = DataHelper.GetSafeString(dataReader, "ModelName", default(string));
                PolicyInfo.ModelDescription = DataHelper.GetSafeString(dataReader, "ModelDescription", default(string));
                PolicyInfo.ModelAltDescription = DataHelper.GetSafeString(dataReader, "ModelAltDescription", default(string));



                PolicyInfo.ShapeId = DataHelper.GetSafeLong(dataReader, "ShapeId", default(long));
                PolicyInfo.TypeId = DataHelper.GetSafeLong(dataReader, "TypeId", default(long));
                PolicyInfo.TypeName = DataHelper.GetSafeString(dataReader, "TypeName", default(string));
                PolicyInfo.TypeDescription = DataHelper.GetSafeString(dataReader, "TypeDescription", default(string));
                PolicyInfo.TypeAltDescription = DataHelper.GetSafeString(dataReader, "TypeAltDescription", default(string));
                PolicyInfo.ShapeName = DataHelper.GetSafeString(dataReader, "ShapeName", default(string));
                PolicyInfo.ShapeDescription = DataHelper.GetSafeString(dataReader, "ShapeDescription", default(string));
                PolicyInfo.ShapeAltDescription = DataHelper.GetSafeString(dataReader, "ShapeAltDescription", default(string));

                PolicyInfo.ColorId = DataHelper.GetSafeLong(dataReader, "ColorId", default(long));
                PolicyInfo.ColorName = DataHelper.GetSafeString(dataReader, "ColorName", default(string));
                PolicyInfo.ColorDescription = DataHelper.GetSafeString(dataReader, "ColorDescription", default(string));
                PolicyInfo.ColorAltDescription = DataHelper.GetSafeString(dataReader, "ColorAltDescription", default(string));

                PolicyInfo.CustomerId = DataHelper.GetSafeLong(dataReader, "CustomerId", default(long));
                PolicyInfo.CustomerCode = DataHelper.GetSafeString(dataReader, "CustomerCode", default(string));
                PolicyInfo.CustomerName = DataHelper.GetSafeString(dataReader, "CustomerName", default(string));
                PolicyInfo.CustomerQID = DataHelper.GetSafeString(dataReader, "CustomerQID", default(string));

                PolicyInfo.CustomerCountryID = DataHelper.GetSafeLong(dataReader, "CustomerCountryID", default(long));
                PolicyInfo.CustomerCountryName = DataHelper.GetSafeString(dataReader, "CustomerCountryName", default(string));
                PolicyInfo.CustomerCountryDescription = DataHelper.GetSafeString(dataReader, "CustomerCountryDescription", default(string));
                PolicyInfo.CustomerCountryAltDescription = DataHelper.GetSafeString(dataReader, "CustomerCountryAltDescription", default(string));
                PolicyInfo.CustomerPassportNo = DataHelper.GetSafeString(dataReader, "CustomerPassportNo", default(string));

                PolicyInfo.ModelYear = DataHelper.GetSafeLong(dataReader, "ModelYear", default(long));
                PolicyInfo.ChassisNo = DataHelper.GetSafeString(dataReader, "ChassisNo", default(string));
                PolicyInfo.EngineNo = DataHelper.GetSafeString(dataReader, "EngineNo", default(string));
                PolicyInfo.Cylinders = DataHelper.GetSafeLong(dataReader, "Cylinders", default(long));
                PolicyInfo.Passengers = DataHelper.GetSafeLong(dataReader, "Passengers", default(long));
                PolicyInfo.PurchaseDate = DataHelper.GetSafeDateTime(dataReader, "PurchaseDate", default(DateTime));
                PolicyInfo.PurchaseCost = DataHelper.GetSafeDecimal(dataReader, "PurchaseCost", default(decimal));
                              
                PolicyInfo.CoverageId = DataHelper.GetSafeLong(dataReader, "CoverageId", default(long));
                PolicyInfo.CoverageName = DataHelper.GetSafeString(dataReader, "CoverageName", default(string));

                PolicyInfo.CompanyId = DataHelper.GetSafeLong(dataReader, "CompanyId", default(long));
                PolicyInfo.CompanyName = DataHelper.GetSafeString(dataReader, "CompanyName", default(string));
               
              
               
              
                
                if (PolicyInfoCollection == null)
                    PolicyInfoCollection = new PolicyInfoCollection();

                PolicyInfoCollection.Add(PolicyInfo);
            }

            return PolicyInfoCollection;
        }
    }
}

 