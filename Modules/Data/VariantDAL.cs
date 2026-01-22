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
    public class VariantDAL
    {
        public static byte Insert(VariantInfo variantInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsVariant");

                db.AddInParameter(dbCommand, "Name", DbType.String, variantInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, variantInfo.Description);
                db.AddInParameter(dbCommand, "ModelId", DbType.Int64, variantInfo.ModelId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                variantInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static bool Delete(long variantId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelVariant");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, variantId);
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

        public static byte Update(VariantInfo variantInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdVariant");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, variantInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, variantInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, variantInfo.Description);
                db.AddInParameter(dbCommand, "ModelId", DbType.Int64, variantInfo.ModelId);

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

        public static VariantInfoCollection GetAll()
        {
            VariantInfoCollection variantInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetVariant");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    variantInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return variantInfoCollection;
        }

        public static VariantInfo Get(long variantId)
        {
            VariantInfoCollection variantInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetVariant");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, variantId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    variantInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (variantInfoCollection != null && variantInfoCollection.Count > 0) ? variantInfoCollection[0] : null;
        }

        public static VariantInfoCollection GetByModelId(long modelId)
        {
            VariantInfoCollection variantInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetVariantByModelId");
                db.AddInParameter(dbCommand, "ModelId", DbType.Int64, modelId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    variantInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return variantInfoCollection;
        }

        public static VariantInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            VariantInfoCollection variantInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchVariant");

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
                    variantInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return variantInfoCollection;
        }

        private static VariantInfoCollection GetAsList(IDataReader dataReader)
        {
            VariantInfoCollection variantInfoCollection = null;

            while (dataReader.Read())
            {
                VariantInfo variantInfo = new VariantInfo();
                variantInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                variantInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                variantInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                variantInfo.ModelId = DataHelper.GetSafeLong(dataReader, "ModelId", default(long));
                variantInfo.Model = DataHelper.GetSafeString(dataReader, "Model", default(string));

                if (variantInfoCollection == null)
                    variantInfoCollection = new VariantInfoCollection();

                variantInfoCollection.Add(variantInfo);
            }

            return variantInfoCollection;
        }
    }
}
