using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class DocumentStatusManager
    {


        public static DocumentStatusInfo Get(long exchangeRateId)
        {
            return DocumentStatusDAL.Get(exchangeRateId);
        }

        public static DocumentStatusInfoCollection GetAll()
        {
            return DocumentStatusDAL.GetAll();
        }

        public static DocumentStatusInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DocumentStatusDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DocumentStatusInfoCollection Search(DocumentStatusSearchParams divisionSearchParams, out long totalRows)
        {
            return DocumentStatusDAL.Search(divisionSearchParams, out totalRows);
        }
    }
}
