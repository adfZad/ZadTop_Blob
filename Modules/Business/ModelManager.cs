using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class ModelManager
    {
        public static byte Insert(ModelInfo modelInfo)
        {
            return ModelDAL.Insert(modelInfo);
        }

        public static byte Update(ModelInfo modelInfo)
        {
            return ModelDAL.Update(modelInfo);
        }

        public static byte Delete(long modelId)
        {
            return ModelDAL.Delete(modelId);
        }

        public static ModelInfo Get(long modelId)
        {
            return ModelDAL.Get(modelId);
        }

        public static ModelInfoCollection GetAll()
        {
            return ModelDAL.GetAll();
        }

        public static ModelInfoCollection GetByMakeId(long makeId)
        {
            return ModelDAL.GetByMakeId(makeId);
        }

        public static ModelInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return ModelDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static ModelInfoCollection Search(ModelSearchParams modelSearchParams, out long totalRows)
        {
            return ModelDAL.Search(modelSearchParams, out totalRows);
        }
    }
}
