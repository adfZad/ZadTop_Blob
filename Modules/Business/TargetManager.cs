using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Data;
using ZadHolding.Been.Collection;

namespace ZadHolding.Business
{
    public class TargetManager
    {
        public static byte Insert(TargetInfo targetInfo)
        {
            return TargetDAL.Insert(targetInfo);
        }

        public static byte Update(TargetInfo targetInfo)
        {
            return TargetDAL.Update(targetInfo);
        }

        public static byte Delete(long targetId)
        {
            return TargetDAL.Delete(targetId);
        }

        public static TargetInfo Get(long targetId)
        {
            return TargetDAL.Get(targetId);
        }

        public static TargetInfoCollection GetAll()
        {
            return TargetDAL.GetAll();
        }

        public static TargetInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return TargetDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
