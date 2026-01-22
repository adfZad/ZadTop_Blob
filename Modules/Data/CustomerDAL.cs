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
    public class CustomerDAL
    {
        public static byte Insert(CustomerInfo customerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCustomer");

                db.AddInParameter(dbCommand, "Name", DbType.String, customerInfo.Name);
                db.AddInParameter(dbCommand, "QID", DbType.String, customerInfo.QID);
                db.AddInParameter(dbCommand, "CategoryId", DbType.Int64, customerInfo.CategoryId);
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, customerInfo.CountryId);
                db.AddInParameter(dbCommand, "Contact", DbType.String, customerInfo.Contact);
                db.AddInParameter(dbCommand, "PassportNo", DbType.String, customerInfo.PassportNo);
                db.AddInParameter(dbCommand, "Email", DbType.String, customerInfo.Email);
                db.AddInParameter(dbCommand, "POBox", DbType.String, customerInfo.POBox);
                db.AddInParameter(dbCommand, "Mobile", DbType.String, customerInfo.Mobile);
                db.AddInParameter(dbCommand, "Land", DbType.String, customerInfo.Land);
                db.AddInParameter(dbCommand, "Address", DbType.String, customerInfo.Address);
                db.AddInParameter(dbCommand, "Description", DbType.String, customerInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, customerInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, customerInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, customerInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, customerInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, customerInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, customerInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                customerInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long customerId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCustomer");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, customerId);
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

        public static byte Update(CustomerInfo customerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCustomer");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, customerInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, customerInfo.Name);
                db.AddInParameter(dbCommand, "QID", DbType.String, customerInfo.QID);
                db.AddInParameter(dbCommand, "CategoryId", DbType.Int64, customerInfo.CategoryId);
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, customerInfo.CountryId);
                db.AddInParameter(dbCommand, "Contact", DbType.String, customerInfo.Contact);
                db.AddInParameter(dbCommand, "PassportNo", DbType.String, customerInfo.PassportNo);
                db.AddInParameter(dbCommand, "Email", DbType.String, customerInfo.Email);
                db.AddInParameter(dbCommand, "POBox", DbType.String, customerInfo.POBox);
                db.AddInParameter(dbCommand, "Mobile", DbType.String, customerInfo.Mobile);
                db.AddInParameter(dbCommand, "Land", DbType.String, customerInfo.Land);
                db.AddInParameter(dbCommand, "Address", DbType.String, customerInfo.Address);
                db.AddInParameter(dbCommand, "Description", DbType.String, customerInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, customerInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, customerInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, customerInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, customerInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, customerInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, customerInfo.UpdatedTime);

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

        public static CustomerInfo Get(long CustomerId)
        {
            CustomerInfoCollection customerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCustomer");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CustomerId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    customerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (customerInfoCollection != null && customerInfoCollection.Count > 0) ? customerInfoCollection[0] : null;
        }

        public static CustomerInfoCollection GetAll()
        {
            CustomerInfoCollection customerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCustomer");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    customerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerInfoCollection;
        }

        public static CustomerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CustomerInfoCollection customerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCustomer");

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
                    customerInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerInfoCollection;
        }

        public static CustomerInfoCollection Search(CustomerSearchParams customerSearchParams, out long totalRows)
        {
            CustomerInfoCollection customerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCustomer");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, customerSearchParams.CurrentPage);

                if (customerSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, customerSearchParams.PageSize);

                if (string.IsNullOrEmpty(customerSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, customerSearchParams.SortColumn);

                if (string.IsNullOrEmpty(customerSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, customerSearchParams.SortOrder);


                if (customerSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, customerSearchParams.CreatedId);

                if (customerSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, customerSearchParams.UpdateId);

                if (string.IsNullOrEmpty(customerSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, customerSearchParams.Name);

                if (string.IsNullOrEmpty(customerSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, customerSearchParams.Description);

                if (string.IsNullOrEmpty(customerSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, customerSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    customerInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerInfoCollection;
        }

        private static CustomerInfoCollection GetAsList(IDataReader dataReader)
        {
            CustomerInfoCollection customerInfoCollection = null;

            while (dataReader.Read())
            {
                CustomerInfo customerInfo = new CustomerInfo();
                customerInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                customerInfo.Code = DataHelper.GetSafeString(dataReader, "Code", default(string));
                customerInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                customerInfo.QID = DataHelper.GetSafeString(dataReader, "QID", default(string));

                customerInfo.CategoryId = DataHelper.GetSafeLong(dataReader, "CategoryId", default(long));
                customerInfo.CategoryName = DataHelper.GetSafeString(dataReader, "CategoryName", default(string));
                customerInfo.CategoryDescription = DataHelper.GetSafeString(dataReader, "CategoryDescription", default(string));
                customerInfo.CategoryAltDescription = DataHelper.GetSafeString(dataReader, "CategoryAltDescription", default(string));

                customerInfo.CountryId = DataHelper.GetSafeLong(dataReader, "CountryId", default(long));
                customerInfo.CountryName = DataHelper.GetSafeString(dataReader, "CountryName", default(string));
                customerInfo.CountryDescription = DataHelper.GetSafeString(dataReader, "CountryDescription", default(string));
                customerInfo.CountryAltDescription = DataHelper.GetSafeString(dataReader, "CountryAltDescription", default(string));

                customerInfo.Contact = DataHelper.GetSafeString(dataReader, "Contact", default(string));
                customerInfo.PassportNo = DataHelper.GetSafeString(dataReader, "PassportNo", default(string));
                customerInfo.Email = DataHelper.GetSafeString(dataReader, "Email", default(string));
                customerInfo.POBox = DataHelper.GetSafeString(dataReader, "POBox", default(string));
                customerInfo.Mobile = DataHelper.GetSafeString(dataReader, "Mobile", default(string));
                customerInfo.Land = DataHelper.GetSafeString(dataReader, "Land", default(string));
                customerInfo.Address = DataHelper.GetSafeString(dataReader, "Address", default(string));
                customerInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                customerInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                customerInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                customerInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                customerInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                customerInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                customerInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                customerInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                customerInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));
                if (customerInfoCollection == null)
                    customerInfoCollection = new CustomerInfoCollection();

                customerInfoCollection.Add(customerInfo);
            }

            return customerInfoCollection;
        }
    }
}

 