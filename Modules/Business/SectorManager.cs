using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data;

namespace ZadHolding.Business
{
    public class SectorManager
    {
        public static byte Insert(SectorInfo sectorInfo)
        {
            return SectorDAL.Insert(sectorInfo);
        }

        public static byte Update(SectorInfo sectorInfo)
        {
            return SectorDAL.Update(sectorInfo);
        }

        public static byte Delete(long sectorId)
        {
            return SectorDAL.Delete(sectorId);
        }

        public static SectorInfo Get(long sectorId)
        {
            return SectorDAL.Get(sectorId);
        }

        public static SectorInfoCollection GetAll()
        {
            return SectorDAL.GetAll();
        }

        public static SectorInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return SectorDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
    }
}
