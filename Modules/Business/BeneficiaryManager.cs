using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class BeneficiaryManager
    {
        public static byte Insert(BeneficiaryInfo beneficiaryInfo)
        {
            return BeneficiaryDAL.Insert(beneficiaryInfo);
        }

        public static byte Update(BeneficiaryInfo beneficiaryInfo)
        {
            return BeneficiaryDAL.Update(beneficiaryInfo);
        }

        public static byte Delete(long beneficiaryId)
        {
            return BeneficiaryDAL.Delete(beneficiaryId);
        }

        public static BeneficiaryInfo Get(long beneficiaryId)
        {
            return BeneficiaryDAL.Get(beneficiaryId);
        }

        public static BeneficiaryInfoCollection GetAll()
        {
            return BeneficiaryDAL.GetAll();
        }

        public static BeneficiaryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return BeneficiaryDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
