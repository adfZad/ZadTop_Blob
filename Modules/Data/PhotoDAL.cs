using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using Microsoft.Practices.EnterpriseLibrary.Data;

using ZadHolding.Been;
using ZadHolding.Data.Utilities;

namespace ZadHolding.Data
{
    public class PhotoDAL
    {
        public static bool Insert(PhotoInfo userInfo)
        {
            byte result = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsPhoto");
                db.AddInParameter(dbCommand, "Name", DbType.String, userInfo.Name);
                db.AddInParameter(dbCommand, "Photo", DbType.String, userInfo.Photo);
               

                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }

        public static bool Update(PhotoInfo userInfo)
        {
            byte result = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");

                DbCommand dbCommand = db.GetStoredProcCommand("UpdPhoto");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, userInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, userInfo.Name);
                db.AddInParameter(dbCommand, "Photo", DbType.String, userInfo.Photo);
               

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }

        public static bool Delele(long id)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelPhoto");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result == 1);
        }

        public static PhotoInfo Get(long id)
        {
            PhotoInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPhoto");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, id);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (userInfoCollection != null && userInfoCollection.Count > 0)
                return userInfoCollection[0];
            else
                return null;
        }

        public static PhotoInfoCollection GetAll()
        {
            PhotoInfoCollection userInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPhoto");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    userInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return userInfoCollection;
        }

      

      

      

       

        private static PhotoInfoCollection GetAsList(IDataReader dataReader)
        {
            PhotoInfoCollection userInfoCollection = null;

            while (dataReader.Read())
            {
                PhotoInfo userInfo = new PhotoInfo();
                userInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                userInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                userInfo.Photo = DataHelper.GetSafeString(dataReader, "Photo", default(string));
                

                if (userInfoCollection == null)
                    userInfoCollection = new PhotoInfoCollection();
                userInfoCollection.Add(userInfo);
            }

            return userInfoCollection;
        }
    }
}
