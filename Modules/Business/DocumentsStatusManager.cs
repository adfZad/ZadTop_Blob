using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class DocumentsStatusManager
    {
        public static long Insert(DocumentsStatusInfo asseststatusInfo)
        {
            return DocumentsStatusDAL.Insert(asseststatusInfo);
        }

        public static byte Update(DocumentsStatusInfo asseststatusInfo)
        {
            return DocumentsStatusDAL.Update(asseststatusInfo);
        }

        public static bool Delete(long assestId)
        {
            return DocumentsStatusDAL.Delete(assestId);
        }

        public static DocumentsStatusInfo Get(long assestId)
        {
            return DocumentsStatusDAL.Get(assestId);
        }     

        public static DocumentsStatusInfoCollection GetAll()
        {
            return DocumentsStatusDAL.GetAll();
        }

        public static DocumentsStatusInfoCollection SearchAll(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DocumentsStatusDAL.SearchAll(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DocumentsStatusInfoCollection SearchAll(DocumentsStatusSearchParams documentSearchParams, out long totalRows)
        {
            return DocumentsStatusDAL.SearchAll(documentSearchParams, out totalRows);
        }


       
    }
}
