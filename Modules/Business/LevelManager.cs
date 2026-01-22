using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class LevelManager
    {
        public static byte Insert(LevelInfo levelInfo)
        {
            return LevelDAL.Insert(levelInfo);
        }

        public static byte Update(LevelInfo levelInfo)
        {
            return LevelDAL.Update(levelInfo);
        }

        public static byte Delete(long exchangeRateId)
        {
            return LevelDAL.Delete(exchangeRateId);
        }

        public static LevelInfo Get(long exchangeRateId)
        {
            return LevelDAL.Get(exchangeRateId);
        }

        public static LevelInfoCollection GetAll()
        {
            return LevelDAL.GetAll();
        }

        public static LevelInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return LevelDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static LevelInfoCollection Search(LevelSearchParams levelSearchParams, out long totalRows)
        {
            return LevelDAL.Search(levelSearchParams, out totalRows);
        }
    }
}

