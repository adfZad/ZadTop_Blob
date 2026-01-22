using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class HandlerManager
    {
        public static byte Insert(HandlerInfo handlerInfo)
        {
            return HandlerDAL.Insert(handlerInfo);
        }

        public static byte Update(HandlerInfo handlerInfo)
        {
            return HandlerDAL.Update(handlerInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return HandlerDAL.Delete(exchangeRateId);
        }

        public static HandlerInfo Get(long exchangeRateId)
        {
            return HandlerDAL.Get(exchangeRateId);
        }

        public static HandlerInfoCollection GetAll()
        {
            return HandlerDAL.GetAll();
        }

        public static HandlerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return HandlerDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
