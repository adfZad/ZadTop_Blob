using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class TemplateManager
    {
        public static byte Insert(TemplateInfo templateInfo)
        {
            return TemplateDAL.Insert(templateInfo);
        }

        public static byte Update(TemplateInfo templateInfo)
        {
            return TemplateDAL.Update(templateInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return TemplateDAL.Delete(exchangeRateId);
        }

        public static TemplateInfo Get(long exchangeRateId)
        {
            return TemplateDAL.Get(exchangeRateId);
        }

        public static TemplateInfoCollection GetAll()
        {
            return TemplateDAL.GetAll();
        }
        public static TemplateInfoCollection GetTemplateByFlow(long flowId)
        {
            return TemplateDAL.GetTemplateByFlow(flowId);
        }
        public static TemplateInfoCollection GetTemplateByDocumentId(long documentId)
        {
            return TemplateDAL.GetTemplateByDocumentId(documentId);
        }
        public static TemplateInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return TemplateDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static TemplateInfoCollection Search(TemplateSearchParams templateSearchParams, out long totalRows)
        {
            return TemplateDAL.Search(templateSearchParams, out totalRows);
        }
    }
}

