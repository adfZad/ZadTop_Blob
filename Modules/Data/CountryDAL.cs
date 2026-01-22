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
    public class CountryDAL
    {
        public static byte Insert(CountryInfo countryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsCountry");

                db.AddInParameter(dbCommand, "Name", DbType.String, countryInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, countryInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, countryInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, countryInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, countryInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, countryInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, countryInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, countryInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                countryInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long countryId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelCountry");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, countryId);
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

        public static byte Update(CountryInfo countryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdCountry");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, countryInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, countryInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, countryInfo.Description);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, countryInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, countryInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, countryInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, countryInfo.UpdatedTime);

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

        public static CountryInfo Get(long CountryId)
        {
            CountryInfoCollection countryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCountry");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, CountryId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    countryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (countryInfoCollection != null && countryInfoCollection.Count > 0) ? countryInfoCollection[0] : null;
        }

        public static CountryInfoCollection GetAll()
        {
            CountryInfoCollection countryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCountry");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    countryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return countryInfoCollection;
        }

        public static CountryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            CountryInfoCollection countryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCountry");

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
                    countryInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return countryInfoCollection;
        }

        public static CountryInfoCollection Search(CountrySearchParams countrySearchParams, out long totalRows)
        {
            CountryInfoCollection countryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchCountry");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, countrySearchParams.CurrentPage);

                if (countrySearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, countrySearchParams.PageSize);

                if (string.IsNullOrEmpty(countrySearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, countrySearchParams.SortColumn);

                if (string.IsNullOrEmpty(countrySearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, countrySearchParams.SortOrder);


                if (countrySearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, countrySearchParams.CreatedId);

                if (countrySearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, countrySearchParams.UpdateId);

                if (string.IsNullOrEmpty(countrySearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, countrySearchParams.Name);

                if (string.IsNullOrEmpty(countrySearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, countrySearchParams.Description);

                if (string.IsNullOrEmpty(countrySearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, countrySearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    countryInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return countryInfoCollection;
        }

        private static CountryInfoCollection GetAsList(IDataReader dataReader)
        {
            CountryInfoCollection countryInfoCollection = null;

            while (dataReader.Read())
            {
                CountryInfo countryInfo = new CountryInfo();
                countryInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                countryInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                countryInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                countryInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                countryInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                countryInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                countryInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                countryInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                countryInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                countryInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                countryInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (countryInfoCollection == null)
                    countryInfoCollection = new CountryInfoCollection();

                countryInfoCollection.Add(countryInfo);
            }

            return countryInfoCollection;
        }
    }
}
