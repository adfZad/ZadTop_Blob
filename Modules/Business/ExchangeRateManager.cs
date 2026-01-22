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
    public class ExchangeRateManager
    {
        public static byte Insert(ExchangeRateInfo exchangeRateInfo)
        {
            return ExchangeRateDAL.Insert(exchangeRateInfo);
        }

        public static byte InsertLast(ExchangeRateInfo exchangeRateInfo, long userId)
        {
            return ExchangeRateDAL.InsertLast(exchangeRateInfo, userId);
        }

        public static bool Update(ExchangeRateInfo exchangeRateInfo)
        {
            return ExchangeRateDAL.Update(exchangeRateInfo);
        }

        public static bool Delete(long exchangeRateId)
        {
            return ExchangeRateDAL.Delete(exchangeRateId);
        }

        public static ExchangeRateInfo Get(long exchangeRateId)
        {
            return ExchangeRateDAL.Get(exchangeRateId);
        }

        public static ExchangeRateInfoCollection GetAll()
        {
            return ExchangeRateDAL.GetAll();
        }

        public static ExchangeRateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ExchangeRateDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }

        public static DataTable SearchLast()
        {
            DataTable dt = ExchangeRateDAL.SearchLast();

            dt.Columns.Add(new DataColumn("IsUpdated", typeof(bool)));

            DataColumn[] dcKey = new DataColumn[1];
            dcKey[0] = dt.Columns["Id"];
            dt.PrimaryKey = dcKey;

            dt.Columns.Add("Status", typeof(string));

            return dt;
        }

        public static ExchangeRateInfoCollection Search(ExchangeRateParams exchangeRateParams, out long totalRows)
        {
            return ExchangeRateDAL.Search(exchangeRateParams, out totalRows);
        }

        public static DataTable SearchApproveValues()
        {
            DataTable dt = ExchangeRateDAL.SearchApproveValues();
            DataColumn[] dcKey = new DataColumn[1];
            dcKey[0] = dt.Columns["Id"];
            dt.PrimaryKey = dcKey;

            dt.Columns.Add("Status", typeof(string));

            return dt;
        }

        public static bool UpdateApproval(long id, long userId) 
        {
            return ExchangeRateDAL.UpdateApproval(id, userId);
        }
    }
}
