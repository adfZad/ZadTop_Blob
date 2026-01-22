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
    public class HandlerDAL
    {
        public static byte Insert(HandlerInfo handlerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsHandler");

                db.AddInParameter(dbCommand, "Name", DbType.String, handlerInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, handlerInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                handlerInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long handlerId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelHandler");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, handlerId);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);
                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Update(HandlerInfo handlerInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdHandler");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, handlerInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, handlerInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, handlerInfo.Description);

                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static HandlerInfo Get(long HandlerId)
        {
            HandlerInfoCollection handlerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetHandler");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, HandlerId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    handlerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (handlerInfoCollection != null && handlerInfoCollection.Count > 0) ? handlerInfoCollection[0] : null;
        }

        public static HandlerInfoCollection GetAll()
        {
            HandlerInfoCollection handlerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetHandler");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    handlerInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return handlerInfoCollection;
        }

        public static HandlerInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            HandlerInfoCollection handlerInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchHandler");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, currentPage);

                if (pageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, pageSize);

                if (string.IsNullOrEmpty(sortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, sortColumn);

                if (string.IsNullOrEmpty(sortColumn))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, sortOrder);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    handlerInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return handlerInfoCollection;
        }

        private static HandlerInfoCollection GetAsList(IDataReader dataReader)
        {
            HandlerInfoCollection handlerInfoCollection = null;

            while (dataReader.Read())
            {
                HandlerInfo handlerInfo = new HandlerInfo();
                handlerInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                handlerInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                handlerInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (handlerInfoCollection == null)
                    handlerInfoCollection = new HandlerInfoCollection();

                handlerInfoCollection.Add(handlerInfo);
            }

            return handlerInfoCollection;
        }
    }
}
