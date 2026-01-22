using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ZadHolding.Data.Utilities;

namespace ZadHolding.Data
{
    public class TotalCostingDAL
    {
        public static TotalCostingInfoCollection GetTotalCostingData()
        {
            TotalCostingInfoCollection totalCostingCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetTotalCostingData");

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    totalCostingCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return totalCostingCollection;
        }
        private static TotalCostingInfoCollection GetAsList(IDataReader dataReader)
        {
            TotalCostingInfoCollection totalCostingInfoCollection = null;

            while (dataReader.Read())
            {
                TotalCostingInfo totalCostingInfo = new TotalCostingInfo();
                totalCostingInfo.Company = DataHelper.GetSafeString(dataReader, "Company", default(string));
                totalCostingInfo.Sector = DataHelper.GetSafeString(dataReader, "Sector", default(string));
                totalCostingInfo.Country = DataHelper.GetSafeString(dataReader, "Country", default(string));
                totalCostingInfo.Quantity = DataHelper.GetSafeLong(dataReader, "Quantity", default(long));
                totalCostingInfo.AvgCost = DataHelper.GetSafeDecimal(dataReader, "AvgCost",default(decimal));
                totalCostingInfo.TotalCost = DataHelper.GetSafeDecimal(dataReader, "TotalCost", default(decimal));
                if (totalCostingInfoCollection == null)
                    totalCostingInfoCollection = new TotalCostingInfoCollection();

                totalCostingInfoCollection.Add(totalCostingInfo);
            }
            return totalCostingInfoCollection;
        }
    }
}
