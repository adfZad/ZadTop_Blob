using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;


namespace ZadHolding.Business
{
    public class PhotoManager
    {
        public static bool Insert(PhotoInfo makeInfo)
        {
            return PhotoDAL.Insert(makeInfo);
        }

        public static bool Update(PhotoInfo makeInfo)
        {
            return PhotoDAL.Update(makeInfo);
        }

        public static bool Delete(long exchangeRateId)
        {
            return PhotoDAL.Delele(exchangeRateId);
        }

        public static PhotoInfo Get(long exchangeRateId)
        {
            return PhotoDAL.Get(exchangeRateId);
        }

        public static PhotoInfoCollection GetAll()
        {
            return PhotoDAL.GetAll();
        }

        
    }
}
