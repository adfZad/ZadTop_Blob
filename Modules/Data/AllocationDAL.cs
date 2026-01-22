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
    public class AllocationDAL
    {
        public static byte Insert(AllocationInfo AllocationInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsAllocation");

                db.AddInParameter(dbCommand, "ClaimId", DbType.Int64, AllocationInfo.ClaimId);
                db.AddInParameter(dbCommand, "RepairId", DbType.Int64, AllocationInfo.RepairId);
                db.AddInParameter(dbCommand, "AllocationDate", DbType.DateTime, AllocationInfo.AllocationDate);
                db.AddInParameter(dbCommand, "Description", DbType.String, AllocationInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                AllocationInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long AllocationId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelAllocation");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, AllocationId);
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

        public static byte Update(AllocationInfo AllocationInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdAllocation");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, AllocationInfo.Id);
                db.AddInParameter(dbCommand, "ClaimId", DbType.Int64, AllocationInfo.ClaimId);
                db.AddInParameter(dbCommand, "RepairId", DbType.Int64, AllocationInfo.RepairId);
                db.AddInParameter(dbCommand, "AllocationDate", DbType.DateTime, AllocationInfo.AllocationDate);
                db.AddInParameter(dbCommand, "Description", DbType.String, AllocationInfo.Description);

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

        public static AllocationInfo Get(long AllocationId)
        {
            AllocationInfoCollection AllocationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAllocation");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, AllocationId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    AllocationInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (AllocationInfoCollection != null && AllocationInfoCollection.Count > 0) ? AllocationInfoCollection[0] : null;
        }

        public static AllocationInfoCollection GetAll()
        {
            AllocationInfoCollection AllocationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAllocation");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    AllocationInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AllocationInfoCollection;
        }

        public static AllocationInfo GetAllocationByClaimId(long vehicleId)
        {
            AllocationInfoCollection AllocationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetAllocationByClaimId");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, vehicleId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    AllocationInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (AllocationInfoCollection != null && AllocationInfoCollection.Count > 0) ? AllocationInfoCollection[0] : null;
        }

        public static AllocationInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            AllocationInfoCollection AllocationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchAllocation");

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
                    AllocationInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AllocationInfoCollection;
        }

        public static AllocationInfoCollection Search(AllocationSearchParams allocationSearchParams, out long totalRows)
        {
            AllocationInfoCollection allocationInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchAllocation");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, allocationSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, allocationSearchParams.PageSize);

                if (string.IsNullOrEmpty(allocationSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, allocationSearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, allocationSearchParams.SortOrder);

                          
                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    allocationInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return allocationInfoCollection;
        }

        private static AllocationInfoCollection GetAsList(IDataReader dataReader)
        {
            AllocationInfoCollection AllocationInfoCollection = null;

            while (dataReader.Read())
            {
                AllocationInfo AllocationInfo = new AllocationInfo();
                AllocationInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                AllocationInfo.ClaimId = DataHelper.GetSafeLong(dataReader, "ClaimId", default(long));
                AllocationInfo.Claim = DataHelper.GetSafeString(dataReader, "Claim", default(string));
                AllocationInfo.RepairId = DataHelper.GetSafeLong(dataReader, "RepairId", default(long));
                AllocationInfo.Repair = DataHelper.GetSafeString(dataReader, "Repair", default(string));
                AllocationInfo.AllocationDate = DataHelper.GetSafeDateTime(dataReader, "AllocationDate", default(DateTime));
                AllocationInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (AllocationInfoCollection == null)
                    AllocationInfoCollection = new AllocationInfoCollection();

                AllocationInfoCollection.Add(AllocationInfo);
            }

            return AllocationInfoCollection;
        }
    }
}
