using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class VariantManager
    {
        public static byte Insert(VariantInfo variantInfo)
        {
            return VariantDAL.Insert(variantInfo);
        }

        public static byte Update(VariantInfo variantInfo)
        {
            return VariantDAL.Update(variantInfo);
        }

        public static bool Delete(long variantId)
        {
            return VariantDAL.Delete(variantId);
        }

        public static VariantInfo Get(long variantId)
        {
            return VariantDAL.Get(variantId);
        }

        public static VariantInfoCollection GetAll()
        {
            return VariantDAL.GetAll();
        }

        public static VariantInfoCollection GetByModelId(long modelId)
        {
            return VariantDAL.GetByModelId(modelId);
        }

        public static VariantInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return VariantDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
