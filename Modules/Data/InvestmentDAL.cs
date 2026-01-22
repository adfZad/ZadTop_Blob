using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

namespace ZadHolding.Data
{
    public class InvestmentDAL
    {
        public static bool Insert(InvestmentInfo investmentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsInvestment");
                if (investmentInfo.CreatedDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, investmentInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, investmentInfo.CompanyId);
                db.AddInParameter(dbCommand, "Quantity", DbType.Int64, investmentInfo.Quantity);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, investmentInfo.Price);
                db.AddInParameter(dbCommand, "Brokerage", DbType.Decimal, investmentInfo.Brokerage);
                db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, investmentInfo.BeneficiaryId);
                db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, investmentInfo.BrokerId);
                db.AddInParameter(dbCommand, "ExchangeRate", DbType.Decimal, investmentInfo.ExchangeRate);
                db.AddInParameter(dbCommand, "Type", DbType.Byte, (byte)investmentInfo.Type);
                db.AddInParameter(dbCommand, "TCQAR", DbType.Decimal, investmentInfo.TCQAR);
                db.AddInParameter(dbCommand, "TCLocal", DbType.Decimal, investmentInfo.TCLocal);
                db.AddInParameter(dbCommand, "Remarks", DbType.String, investmentInfo.Remarks);
                db.AddInParameter(dbCommand, "ReferenceNo", DbType.String, investmentInfo.ReferenceNo);

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                investmentInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (result == 1);
        }

        public static byte InsertForApproval(InvestmentInfo investmentInfo, long userId) 
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");

                DbCommand dbCommand = db.GetStoredProcCommand("InsInvestmentForApproval");

                db.AddInParameter(dbCommand, "InvestmentId", DbType.Int64, investmentInfo.Id);

                if (investmentInfo.CreatedDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, investmentInfo.CreatedDate);

                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, investmentInfo.CompanyId);
                db.AddInParameter(dbCommand, "Quantity", DbType.Int64, investmentInfo.Quantity);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, investmentInfo.Price);
                db.AddInParameter(dbCommand, "Brokerage", DbType.Decimal, investmentInfo.Brokerage);
                db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, investmentInfo.BeneficiaryId);
                db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, investmentInfo.BrokerId);
                db.AddInParameter(dbCommand, "ExchangeRate", DbType.Decimal, investmentInfo.ExchangeRate);
                db.AddInParameter(dbCommand, "Type", DbType.Byte, (byte)investmentInfo.Type);
                db.AddInParameter(dbCommand, "TCQAR", DbType.Decimal, investmentInfo.TCQAR);
                db.AddInParameter(dbCommand, "TCLocal", DbType.Decimal, investmentInfo.TCLocal);
                db.AddInParameter(dbCommand, "Remarks", DbType.String, investmentInfo.Remarks);
                db.AddInParameter(dbCommand, "ReferenceNo", DbType.String, investmentInfo.ReferenceNo);
                db.AddInParameter(dbCommand, "UpdatedBy", DbType.Int64, userId);

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

        public static bool Delete(long investmentId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelInvestment");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, investmentId);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);
                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }

        public static bool Update(InvestmentInfo investmentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdInvestment");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, investmentInfo.Id);
                if (investmentInfo.CreatedDate == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedDate", DbType.DateTime, investmentInfo.CreatedDate);
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, investmentInfo.CompanyId);
                db.AddInParameter(dbCommand, "Quantity", DbType.Int64, investmentInfo.Quantity);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, investmentInfo.Price);
                db.AddInParameter(dbCommand, "Brokerage", DbType.Decimal, investmentInfo.Brokerage);
                db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, investmentInfo.BeneficiaryId);
                db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, investmentInfo.BrokerId);
                db.AddInParameter(dbCommand, "ExchangeRate", DbType.Decimal, investmentInfo.ExchangeRate);
                db.AddInParameter(dbCommand, "Type", DbType.Byte, (byte)investmentInfo.Type);
                db.AddInParameter(dbCommand, "TCQAR", DbType.Decimal, investmentInfo.TCQAR);
                db.AddInParameter(dbCommand, "TCLocal", DbType.Decimal, investmentInfo.TCLocal);
                db.AddInParameter(dbCommand, "Remarks", DbType.String, investmentInfo.Remarks);
                db.AddInParameter(dbCommand, "ReferenceNo", DbType.String, investmentInfo.ReferenceNo);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }

        public static bool UpdateApproval(long id, long userId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdInvestmentApproval");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);
                db.AddInParameter(dbCommand, "ApprovedBy", DbType.Int64, userId);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }
        
        public static InvestmentInfoCollection GetAll()
        {
            InvestmentInfoCollection investmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetInvestment");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    investmentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investmentInfoCollection;
        }

        public static InvestmentInfo Get(long investmentId)
        {
            InvestmentInfoCollection investmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetInvestment");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, investmentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    investmentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (investmentInfoCollection != null && investmentInfoCollection.Count > 0) ? investmentInfoCollection[0] : null;
        }

        public static InvestmentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            InvestmentInfoCollection investmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchInvestment");

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
                    investmentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investmentInfoCollection;
        }

        public static InvestmentInfoCollection Search(CallSearchParams callSearchParams, out long totalRows)
        {
            InvestmentInfoCollection investmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchInvestment");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, callSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, callSearchParams.PageSize);

                if (string.IsNullOrEmpty(callSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, callSearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, callSearchParams.SortOrder);

                if (callSearchParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, callSearchParams.DateFrom);

                if (callSearchParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, callSearchParams.DateTo);

                if (callSearchParams.CompanyId == 0)
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, callSearchParams.CompanyId);

                if (callSearchParams.SectorId == 0)
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, callSearchParams.SectorId);

                if (callSearchParams.CountryId == 0)
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, callSearchParams.CountryId);

                if (callSearchParams.Type == TransactionType.None)
                    db.AddInParameter(dbCommand, "Type", DbType.Byte, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Type", DbType.Byte, (byte)callSearchParams.Type);

                
                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    investmentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investmentInfoCollection;
        }

        public static DataTable SearchApproveValues()
        {
            DataSet ds = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetInvestmentApprove");

                ds = db.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        }

        public static DataTable SearchDt(CallSearchParams callSearchParams, out long totalRows)
        {
            DataSet dsRedult = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchInvestment");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, callSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, callSearchParams.PageSize);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, callSearchParams.SortColumn);
                db.AddInParameter(dbCommand, "SortOrder", DbType.String, callSearchParams.SortOrder);

                if (callSearchParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, callSearchParams.DateFrom);

                if (callSearchParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, callSearchParams.DateTo);

                if (callSearchParams.CompanyId == 0)
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, callSearchParams.CompanyId);

                if (callSearchParams.SectorId == 0)
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, callSearchParams.SectorId);

                if (callSearchParams.CountryId == 0)
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, callSearchParams.CountryId);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                dsRedult = db.ExecuteDataSet(dbCommand);

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (dsRedult != null && dsRedult.Tables.Count > 0) ? dsRedult.Tables[0] : null;
        }

        public static InvestmentInfoCollection SearchDtExcludePaging(CallSearchParams callSearchParams, out long totalRows)
        {
            InvestmentInfoCollection investmentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchInvestmentExcludePaging");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);

                if (callSearchParams.DateFrom == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, callSearchParams.DateFrom);

                if (callSearchParams.DateTo == DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, callSearchParams.DateTo);

                if (callSearchParams.CompanyId == 0)
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, callSearchParams.CompanyId);

                if (callSearchParams.SectorId == 0)
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SectorId", DbType.Int64, callSearchParams.SectorId);

                if (callSearchParams.CountryId == 0)
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CountryId", DbType.Int64, callSearchParams.CountryId);

                if (callSearchParams.Type == TransactionType.None)
                    db.AddInParameter(dbCommand, "Type", DbType.Byte, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Type", DbType.Byte, (byte)callSearchParams.Type);

                if (callSearchParams.BeneficiaryId == 0)
                    db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "BeneficiaryId", DbType.Int64, callSearchParams.BeneficiaryId);

                if (callSearchParams.BrokerId == 0)
                    db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "BrokerId", DbType.Int64, callSearchParams.BrokerId);

                if (callSearchParams.CurrencyId == 0)
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CurrencyId", DbType.Int64, callSearchParams.CurrencyId);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    investmentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return investmentInfoCollection;
        }

        private static InvestmentInfoCollection GetAsList(IDataReader dataReader)
        {
            InvestmentInfoCollection investmentInfoCollection = new InvestmentInfoCollection();

            while (dataReader.Read())
            {
                InvestmentInfo investmentInfo = new InvestmentInfo();
                investmentInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                investmentInfo.CreatedDate = DataHelper.GetSafeDateTime(dataReader, "CreatedDate", default(DateTime));
                investmentInfo.CompanyId = DataHelper.GetSafeLong(dataReader, "CompanyId", default(long));
                investmentInfo.Company = DataHelper.GetSafeString(dataReader, "Company", default(string));
                investmentInfo.SectorId = DataHelper.GetSafeLong(dataReader, "SectorId", default(long));
                investmentInfo.Sector = DataHelper.GetSafeString(dataReader, "Sector", default(string));
                investmentInfo.CountryId = DataHelper.GetSafeLong(dataReader, "CountryId", default(long));
                investmentInfo.Country = DataHelper.GetSafeString(dataReader, "Country", default(string));
                investmentInfo.CurrencyId = DataHelper.GetSafeLong(dataReader, "CurrencyId", default(long));
                investmentInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
                investmentInfo.Symbol = DataHelper.GetSafeString(dataReader, "Symbol", default(string));
                investmentInfo.ExchangeRate = DataHelper.GetSafeDecimal(dataReader, "ExchangeRate", default(decimal));
                investmentInfo.BeneficiaryId = DataHelper.GetSafeLong(dataReader, "BeneficiaryId", default(long));
                investmentInfo.Beneficiary = DataHelper.GetSafeString(dataReader, "Beneficiary", default(string));
                investmentInfo.BrokerId = DataHelper.GetSafeLong(dataReader, "BrokerId", default(long));
                investmentInfo.Broker = DataHelper.GetSafeString(dataReader, "Broker", default(string));
                investmentInfo.Quantity = DataHelper.GetSafeLong(dataReader, "Quantity", default(long));
                investmentInfo.Price = DataHelper.GetSafeDecimal(dataReader, "Price", default(decimal));
                investmentInfo.Brokerage = DataHelper.GetSafeDecimal(dataReader, "Brokerage", default(decimal));
                investmentInfo.Type = (TransactionType)DataHelper.GetSafeByte(dataReader, "Type", default(byte));
                investmentInfo.TCQAR = DataHelper.GetSafeDecimal(dataReader, "TCQAR", default(decimal));
                investmentInfo.TCLocal = DataHelper.GetSafeDecimal(dataReader, "TCLocal", default(decimal));
                investmentInfo.Remarks = DataHelper.GetSafeString(dataReader, "Remarks", default(string));
                investmentInfo.ReferenceNo = DataHelper.GetSafeString(dataReader, "ReferenceNo", default(string));

                if (investmentInfoCollection == null)
                    investmentInfoCollection = new InvestmentInfoCollection();

                investmentInfoCollection.Add(investmentInfo);
            }

            return investmentInfoCollection;
        }
    }
}

