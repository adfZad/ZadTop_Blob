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
    public class ClaimDAL
    {
        public static byte Insert(ClaimInfo claimInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsClaim");

                db.AddInParameter(dbCommand, "Name", DbType.String, claimInfo.Name);
                db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, claimInfo.VehicleId);
                db.AddInParameter(dbCommand, "CauseId", DbType.Int64, claimInfo.CauseId);
                db.AddInParameter(dbCommand, "HandlerId", DbType.Int64, claimInfo.HandlerId);
                db.AddInParameter(dbCommand, "AccidentDate", DbType.DateTime, claimInfo.AccidentDate);
                db.AddInParameter(dbCommand, "DeclareDate", DbType.DateTime, claimInfo.DeclareDate);
                db.AddInParameter(dbCommand, "Label", DbType.String, claimInfo.Label);
                db.AddInParameter(dbCommand, "EstimatedValue", DbType.Decimal, claimInfo.EstimatedValue);
                db.AddInParameter(dbCommand, "StatusId", DbType.Int64, claimInfo.StatusId);
                db.AddInParameter(dbCommand, "Description", DbType.String, claimInfo.Description);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                claimInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long claimId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelClaim");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, claimId);
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

        public static byte Update(ClaimInfo claimInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdClaim");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, claimInfo.Id);
                db.AddInParameter(dbCommand, "Name", DbType.String, claimInfo.Name);
                db.AddInParameter(dbCommand, "VehicleId", DbType.Int64, claimInfo.VehicleId);
                db.AddInParameter(dbCommand, "CauseId", DbType.Int64, claimInfo.CauseId);
                db.AddInParameter(dbCommand, "HandlerId", DbType.Int64, claimInfo.HandlerId);
                db.AddInParameter(dbCommand, "AccidentDate", DbType.DateTime, claimInfo.AccidentDate);
                db.AddInParameter(dbCommand, "DeclareDate", DbType.DateTime, claimInfo.DeclareDate);
                db.AddInParameter(dbCommand, "Label", DbType.String, claimInfo.Label);
                db.AddInParameter(dbCommand, "EstimatedValue", DbType.Decimal, claimInfo.EstimatedValue);
                db.AddInParameter(dbCommand, "StatusId", DbType.Int64, claimInfo.StatusId);
                db.AddInParameter(dbCommand, "Description", DbType.String, claimInfo.Description);

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

        public static byte UpdateClaimStatus(ClaimInfo claimInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdClaimStatus");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, claimInfo.Id);
                db.AddInParameter(dbCommand, "StatusId", DbType.Int64, claimInfo.StatusId);
       
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

        public static ClaimInfo Get(long ClaimId)
        {
            ClaimInfoCollection claimInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetClaim");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, ClaimId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    claimInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (claimInfoCollection != null && claimInfoCollection.Count > 0) ? claimInfoCollection[0] : null;
        }

        public static ClaimInfoCollection GetClaimByStatus(long ClaimId)
        {
            ClaimInfoCollection claimInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetClaimByStatus");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, ClaimId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    claimInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return claimInfoCollection;
        }

        public static ClaimInfoCollection GetAll()
        {
            ClaimInfoCollection claimInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetClaim");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    claimInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return claimInfoCollection;
        }

        public static ClaimInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            ClaimInfoCollection claimInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchClaim");

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
                    claimInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return claimInfoCollection;
        }

        public static ClaimInfoCollection Search(ClaimSearchParams claimSearchParams, out long totalRows)
        {
            ClaimInfoCollection claimInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchClaim");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, claimSearchParams.CurrentPage);
                db.AddInParameter(dbCommand, "PageSize", DbType.Int32, claimSearchParams.PageSize);

                if (string.IsNullOrEmpty(claimSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, claimSearchParams.SortColumn);

                db.AddInParameter(dbCommand, "SortOrder", DbType.String, claimSearchParams.SortOrder);

                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    claimInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return claimInfoCollection;
        }

        private static ClaimInfoCollection GetAsList(IDataReader dataReader)
        {
            ClaimInfoCollection claimInfoCollection = null;

            while (dataReader.Read())
            {
                ClaimInfo claimInfo = new ClaimInfo();
                claimInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                claimInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                claimInfo.VehicleId = DataHelper.GetSafeLong(dataReader, "VehicleId", default(long));
                claimInfo.Plate_No = DataHelper.GetSafeString(dataReader, "Plate_No", default(string));
                claimInfo.CauseId = DataHelper.GetSafeLong(dataReader, "CauseId", default(long));
                claimInfo.Cause = DataHelper.GetSafeString(dataReader, "Cause", default(string));
                claimInfo.HandlerId = DataHelper.GetSafeLong(dataReader, "HandlerId", default(long));
                claimInfo.Handler = DataHelper.GetSafeString(dataReader, "Handler", default(string));
                claimInfo.EstimatedValue = DataHelper.GetSafeDecimal(dataReader, "EstimatedValue", default(decimal));
                claimInfo.StatusId = DataHelper.GetSafeLong(dataReader, "StatusId", default(long));
                claimInfo.Status = DataHelper.GetSafeString(dataReader, "Status", default(string));
                claimInfo.AccidentDate = DataHelper.GetSafeDateTime(dataReader, "AccidentDate", default(DateTime));
                claimInfo.DeclareDate = DataHelper.GetSafeDateTime(dataReader, "DeclareDate", default(DateTime));
                claimInfo.Label = DataHelper.GetSafeString(dataReader, "Label", default(string));
                claimInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                if (claimInfoCollection == null)
                    claimInfoCollection = new ClaimInfoCollection();

                claimInfoCollection.Add(claimInfo);
            }

            return claimInfoCollection;
        }
    }
}
