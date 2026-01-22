using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class FlowTemplateManager
    {
        public static byte Insert(FlowTemplateInfo makeInfo)
        {
            return FlowTemplateDAL.Insert(makeInfo);
        }

        public static byte Update(FlowTemplateInfo makeInfo)
        {
            return FlowTemplateDAL.Update(makeInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return FlowTemplateDAL.Delete(exchangeRateId);
        }

        public static FlowTemplateInfo Get(long exchangeRateId)
        {
            return FlowTemplateDAL.Get(exchangeRateId);
        }

        public static FlowTemplateInfoCollection GetAll()
        {
            return FlowTemplateDAL.GetAll();
        }

        public static FlowTemplateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return FlowTemplateDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static FlowTemplateInfoCollection Search(FlowTemplateSearchParams makeSearchParams, out long totalRows)
        {
            return FlowTemplateDAL.Search(makeSearchParams, out totalRows);
        }
    }
}

