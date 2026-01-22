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
    public class PolicyTypeDAL
    {
        public static byte Insert(PolicyTypeInfo policyTypeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsPolicyType");

                db.AddInParameter(dbCommand, "Name", DbType.String, policyTypeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, policyTypeInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                policyTypeInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long policyTypeId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelPolicyType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, policyTypeId);
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

        public static byte Update(PolicyTypeInfo policyTypeInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdPolicyType");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, policyTypeInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, policyTypeInfo.Name);
                db.AddInParameter(dbCommand, "Description", DbType.String, policyTypeInfo.Description);

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

        public static PolicyTypeInfo Get(long PolicyTypeId)
        {
            PolicyTypeInfoCollection policyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPolicyType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, PolicyTypeId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    policyTypeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (policyTypeInfoCollection != null && policyTypeInfoCollection.Count > 0) ? policyTypeInfoCollection[0] : null;
        }

        public static PolicyTypeInfoCollection GetAll()
        {
            PolicyTypeInfoCollection policyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetPolicyType");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    policyTypeInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return policyTypeInfoCollection;
        }

        public static PolicyTypeInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            PolicyTypeInfoCollection policyTypeInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchPolicyType");

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
                    policyTypeInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return policyTypeInfoCollection;
        }

        private static PolicyTypeInfoCollection GetAsList(IDataReader dataReader)
        {
            PolicyTypeInfoCollection policyTypeInfoCollection = null;

            while (dataReader.Read())
            {
                PolicyTypeInfo policyTypeInfo = new PolicyTypeInfo();
                policyTypeInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                policyTypeInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                policyTypeInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (policyTypeInfoCollection == null)
                    policyTypeInfoCollection = new PolicyTypeInfoCollection();

                policyTypeInfoCollection.Add(policyTypeInfo);
            }

            return policyTypeInfoCollection;
        }
    }
}
