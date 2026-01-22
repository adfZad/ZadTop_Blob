using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;
using System.Data;

namespace ZadHolding.Business
{
    public class MarketPriceManager
    {
        public static byte Insert(MarketPriceInfo marketPriceInfo, long userId)
        {
            return MarketPriceDAL.Insert(marketPriceInfo, userId);
        }

        public static byte InsertLast(MarketPriceInfo marketPriceInfo, long userId)
        {
            return MarketPriceDAL.InsertLast(marketPriceInfo, userId);
        }

        public static byte UpdateApproval(long id, long userId)
        {
            return MarketPriceDAL.UpdateApproval(id, userId);
        }

        public static bool Update(MarketPriceInfo marketPriceInfo)
        {
            return MarketPriceDAL.Update(marketPriceInfo);
        }

        public static bool Delete(long marketPriceId)
        {
            return MarketPriceDAL.Delete(marketPriceId);
        }

        public static MarketPriceInfo Get(long marketPriceId)
        {
            return MarketPriceDAL.Get(marketPriceId);
        }

        public static MarketPriceInfoCollection GetAll()
        {
            return MarketPriceDAL.GetAll();
        }

        public static MarketPriceInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return MarketPriceDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }

        public static DataTable SearchLast()
        {
            DataTable dt = MarketPriceDAL.SearchLast();

            dt.Columns.Add(new DataColumn("IsUpdated", typeof(bool)));

            DataColumn[] dcKey = new DataColumn[1];
            dcKey[0] = dt.Columns["Id"];
            dt.PrimaryKey = dcKey;

            dt.Columns.Add("Status", typeof(string));

            return dt;
        }

        public static DataTable SearchApproveValues()
        {
            DataTable dt = MarketPriceDAL.SearchApproveValues();
            DataColumn[] dcKey = new DataColumn[1];
            dcKey[0] = dt.Columns["Id"];
            dt.PrimaryKey = dcKey;

            dt.Columns.Add("Status", typeof(string));

            return dt;
        }

        public static MarketPriceInfoCollection Search(MarketParams marketParams, out long totalRows)
        {
            return MarketPriceDAL.Search(marketParams, out totalRows);
        }
    }
}
