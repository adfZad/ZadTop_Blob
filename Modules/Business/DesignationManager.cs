using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class DesignationManager
    {
        public static byte Insert(DesignationInfo designationInfo)
        {
            return DesignationDAL.Insert(designationInfo);
        }

        public static byte Update(DesignationInfo designationInfo)
        {
            return DesignationDAL.Update(designationInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return DesignationDAL.Delete(exchangeRateId);
        }

        public static DesignationInfo Get(long exchangeRateId)
        {
            return DesignationDAL.Get(exchangeRateId);
        }

        public static DesignationInfoCollection GetAll()
        {
            return DesignationDAL.GetAll();
        }

        public static DesignationInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DesignationDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DesignationInfoCollection Search(DesignationSearchParams designationSearchParams, out long totalRows)
        {
            return DesignationDAL.Search(designationSearchParams, out totalRows);
        }
    }
}
