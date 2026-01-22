using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ZadHolding.Data
{
    public class BeneficiaryDAL
    {

        public static byte Insert(BeneficiaryInfo beneficiaryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsBeneficiary");

                db.AddInParameter(dbCommand, "Name", DbType.String, beneficiaryInfo.Name);
                db.AddInParameter(dbCommand, "Email", DbType.String, beneficiaryInfo.Email);
                db.AddInParameter(dbCommand, "Description", DbType.String, beneficiaryInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                beneficiaryInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long beneficiaryId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelBeneficiary");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, beneficiaryId);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);
                db.ExecuteNonQuery(dbCommand);

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (result);
        }

        public static byte Update(BeneficiaryInfo beneficiaryInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdBeneficiary");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, beneficiaryInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, beneficiaryInfo.Name);
                db.AddInParameter(dbCommand, "Email", DbType.String, beneficiaryInfo.Email);
                db.AddInParameter(dbCommand, "Description", DbType.String, beneficiaryInfo.Description);

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

        public static BeneficiaryInfoCollection GetAll()
        {
            BeneficiaryInfoCollection beneficiaryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetBeneficiary");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    beneficiaryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beneficiaryInfoCollection;
        }

        public static BeneficiaryInfo Get(long beneficiaryId)
        {
            BeneficiaryInfoCollection beneficiaryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetBeneficiary");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, beneficiaryId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    beneficiaryInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (beneficiaryInfoCollection != null && beneficiaryInfoCollection.Count > 0) ? beneficiaryInfoCollection[0] : null;
        }

        public static BeneficiaryInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            BeneficiaryInfoCollection beneficiaryInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchBeneficiary");

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
                    beneficiaryInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return beneficiaryInfoCollection;
        }

        private static BeneficiaryInfoCollection GetAsList(IDataReader dataReader)
        {
            BeneficiaryInfoCollection beneficiaryInfoCollection = new BeneficiaryInfoCollection();

            while (dataReader.Read())
            {
                BeneficiaryInfo beneficiaryInfo = new BeneficiaryInfo();
                beneficiaryInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                beneficiaryInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                beneficiaryInfo.Email = DataHelper.GetSafeString(dataReader, "Email", default(string));
                beneficiaryInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));

                if (beneficiaryInfoCollection == null)
                    beneficiaryInfoCollection = new BeneficiaryInfoCollection();

                beneficiaryInfoCollection.Add(beneficiaryInfo);
            }

            return beneficiaryInfoCollection;
        }
    }
}
