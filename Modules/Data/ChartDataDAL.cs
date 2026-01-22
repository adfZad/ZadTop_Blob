using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ZadHolding.Data.Utilities;


namespace ZadHolding.Data
{
    public class ChartDataDAL
    {
        public static ChartDataInfoCollection GetSectorChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetSectorChartData");

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCountryBySector(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCountryBySector");
                db.AddInParameter(dbCommand, "SectorId", DbType.Int64,id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCompanyBySector(long sId,long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCompanyBySector");
                db.AddInParameter(dbCommand, "SectorId", DbType.Int64, sId);
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCompanyChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCompanyChartData");

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetSectorByCompany(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetSectorByCompany");
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCountryByCompany(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCountryByCompany");
                db.AddInParameter(dbCommand, "CompanyId", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }     
        public static ChartDataInfoCollection GetCountryChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCountryChartData");

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }

        public static ChartDataInfoCollection GetSectorByCountry(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetSectorByCountry");
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }

        public static ChartDataInfoCollection GetCompanyByCountry(long cId,long sId)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCompanyByCountry");
                db.AddInParameter(dbCommand, "CountryId", DbType.Int64, cId);
                db.AddInParameter(dbCommand, "SectorId", DbType.Int64, sId);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }

        public static ChartDataInfoCollection GetCostingChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetCostingChartData");

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    chartDataInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return chartDataInfoCollection;
        }
        private static ChartDataInfoCollection GetAsList(IDataReader dataReader)
        {
            ChartDataInfoCollection chartDataInfoCollection = null;

            while (dataReader.Read())
            {
                ChartDataInfo chartDataInfo = new ChartDataInfo();
                chartDataInfo.XValue = DataHelper.GetSafeString(dataReader, "XValue", string.Empty);
                chartDataInfo.YValue = dataReader.GetValue(dataReader.GetOrdinal("YValue"));
                chartDataInfo.SeriesKey = DataHelper.GetSafeString(dataReader, "SeriesKey", string.Empty);

                if (chartDataInfoCollection == null)
                    chartDataInfoCollection = new ChartDataInfoCollection();

                chartDataInfoCollection.Add(chartDataInfo);
            }

            return chartDataInfoCollection;
        }
    }

     }
