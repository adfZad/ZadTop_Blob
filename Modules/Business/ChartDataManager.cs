using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class ChartDataManager
    {
        public static ChartDataInfoCollection GetSectorChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetSectorChartData();

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Total Cost by Sector";

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCountryBySector(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCountryBySector(id);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Country By Sector";

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCompanyBySector(long sId,long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCompanyBySector(sId,id);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Company By Sector";

            return chartDataInfoCollection;
        }
        
        public static ChartDataInfoCollection GetCountryChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCountryChartData();

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Total Cost by Country";

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetSectorByCountry(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetSectorByCountry(id);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Sectors";

            return chartDataInfoCollection;
        }
        public static ChartDataInfoCollection GetCompanyByCountry(long cId,long sId)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCompanyByCountry(cId, sId);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Companies";

            return chartDataInfoCollection;
        }


        public static ChartDataInfoCollection GetCompanyChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCompanyChartData();

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Total by Company";

            return chartDataInfoCollection; 
        }
        
        public static ChartDataInfoCollection GetSectorByCompany(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetSectorByCompany(id);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Sector By Company";

            return chartDataInfoCollection;
        }

        public static ChartDataInfoCollection GetCountryByCompany(long id)
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCountryByCompany(id);

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Country By Company";

            return chartDataInfoCollection;
        }

        public static ChartDataInfoCollection GetCostingChartData()
        {
            ChartDataInfoCollection chartDataInfoCollection = ChartDataDAL.GetCostingChartData();

            if (chartDataInfoCollection != null)
                chartDataInfoCollection.ChartTitle = "Avg Costing per Company";

            return chartDataInfoCollection;
        }
    }
}
