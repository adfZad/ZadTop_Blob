using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Data.Utilities;

using Microsoft.Practices.EnterpriseLibrary.Data;



namespace ZadHolding.Data
{
    public class DocumentsStatusDAL
    {

        public static long Insert(DocumentsStatusInfo asseststatusInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDocumentsStatus");

                db.AddInParameter(dbCommand, "DocumentId", DbType.Int64, asseststatusInfo.DocumentId);
                db.AddInParameter(dbCommand, "StatusId", DbType.Int64, asseststatusInfo.StatusId);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                asseststatusInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return asseststatusInfo.Id;
        }

        public static bool Delete(long assestId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDocumentsStatus");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, assestId);
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

        public static byte Update(DocumentsStatusInfo asseststatusInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDocumentsStatus");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, asseststatusInfo.Id);
                db.AddInParameter(dbCommand, "DocumentId", DbType.String, asseststatusInfo.DocumentId);
                db.AddInParameter(dbCommand, "StatusId", DbType.String, asseststatusInfo.StatusId);

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

        public static DocumentsStatusInfoCollection GetAll()
        {
            DocumentsStatusInfoCollection asseststatusInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocumentsStatus");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    asseststatusInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return asseststatusInfoCollection;
        }

        public static DocumentsStatusInfo Get(long assestId)
        {
            DocumentsStatusInfoCollection asseststatusInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocumentsStatus");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, assestId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    asseststatusInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (asseststatusInfoCollection != null && asseststatusInfoCollection.Count > 0) ? asseststatusInfoCollection[0] : null;
        }

        public static DocumentsStatusInfoCollection SearchAll(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DocumentsStatusInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchSignedDocumentAll");

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
                    documentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documentInfoCollection;
        }

        public static DocumentsStatusInfoCollection SearchAll(DocumentsStatusSearchParams documentSearchParams, out long totalRows)
        {
            DocumentsStatusInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchSignedDocumentAll");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, documentSearchParams.CurrentPage);

                if (documentSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, documentSearchParams.PageSize);

                if (string.IsNullOrEmpty(documentSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, documentSearchParams.SortColumn);

                if (string.IsNullOrEmpty(documentSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, documentSearchParams.SortOrder);


                if (documentSearchParams.TypeId == 0)
                    db.AddInParameter(dbCommand, "TypeId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "TypeId", DbType.Int64, documentSearchParams.TypeId);

                if (documentSearchParams.DocumentStatusId == 0)
                    db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, documentSearchParams.DocumentStatusId);

                if (documentSearchParams.DivisionId == 0)
                    db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DivisionId", DbType.Int64, documentSearchParams.DivisionId);

                if (documentSearchParams.DepartmentId == 0)
                    db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "DepartmentId", DbType.Int64, documentSearchParams.DepartmentId);


                if (documentSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, documentSearchParams.CreatedId);

                if (documentSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, documentSearchParams.UpdateId);

                if (string.IsNullOrEmpty(documentSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, documentSearchParams.Name);

                if (string.IsNullOrEmpty(documentSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, documentSearchParams.Description);

                if (string.IsNullOrEmpty(documentSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, documentSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documentInfoCollection;
        }

        private static DocumentsStatusInfoCollection GetAsList(IDataReader dataReader)
        {
            DocumentsStatusInfoCollection asseststatusInfoCollection = null;

            while (dataReader.Read())
            {
                DocumentsStatusInfo asseststatusInfo = new DocumentsStatusInfo();

                asseststatusInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                asseststatusInfo.DocumentId = DataHelper.GetSafeLong(dataReader, "DocumentId", default(long));
                asseststatusInfo.StatusId = DataHelper.GetSafeLong(dataReader, "StatusId", default(long));
                asseststatusInfo.StatusName = DataHelper.GetSafeString(dataReader, "StatusName", default(string));
                asseststatusInfo.AmendNo = DataHelper.GetSafeLong(dataReader, "AmendNo", default(long));
                asseststatusInfo.Code = DataHelper.GetSafeString(dataReader, "Code", default(string));
                asseststatusInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                asseststatusInfo.FlowId = DataHelper.GetSafeLong(dataReader, "FlowId", default(long));
                asseststatusInfo.FlowName = DataHelper.GetSafeString(dataReader, "FlowName", default(string));
                asseststatusInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                asseststatusInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));
                asseststatusInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                asseststatusInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));
                asseststatusInfo.TypeId = DataHelper.GetSafeLong(dataReader, "TypeId", default(long));
                asseststatusInfo.TypeName = DataHelper.GetSafeString(dataReader, "TypeName", default(string));
                asseststatusInfo.DocumentStatusId = DataHelper.GetSafeLong(dataReader, "DocumentStatusId", default(long));
                asseststatusInfo.DocumentStatusName = DataHelper.GetSafeString(dataReader, "DocumentStatusName", default(string));
                asseststatusInfo.Amount = DataHelper.GetSafeDecimal(dataReader, "Amount", default(decimal));
                asseststatusInfo.OtherAmount = DataHelper.GetSafeDecimal(dataReader, "OtherAmount", default(decimal));
                asseststatusInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
                asseststatusInfo.Rate = DataHelper.GetSafeDecimal(dataReader, "Rate", default(decimal));
                asseststatusInfo.Primary = DataHelper.GetSafeString(dataReader, "Primary", default(string));
                asseststatusInfo.One = DataHelper.GetSafeString(dataReader, "One", default(string));
                asseststatusInfo.Two = DataHelper.GetSafeString(dataReader, "Two", default(string));
                asseststatusInfo.Three = DataHelper.GetSafeString(dataReader, "Three", default(string));
                asseststatusInfo.Four = DataHelper.GetSafeString(dataReader, "Four", default(string));
                asseststatusInfo.Five = DataHelper.GetSafeString(dataReader, "Five", default(string));
                asseststatusInfo.Six = DataHelper.GetSafeString(dataReader, "Six", default(string));
                asseststatusInfo.OneName = DataHelper.GetSafeString(dataReader, "OneName", default(string));
                asseststatusInfo.TwoName = DataHelper.GetSafeString(dataReader, "TwoName", default(string));
                asseststatusInfo.ThreeName = DataHelper.GetSafeString(dataReader, "ThreeName", default(string));
                asseststatusInfo.FourName = DataHelper.GetSafeString(dataReader, "FourName", default(string));
                asseststatusInfo.FiveName = DataHelper.GetSafeString(dataReader, "FiveName", default(string));
                asseststatusInfo.SixName = DataHelper.GetSafeString(dataReader, "SixName", default(string));
                asseststatusInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                asseststatusInfo.Purpose = DataHelper.GetSafeString(dataReader, "Purpose", default(string));
                asseststatusInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                asseststatusInfo.Date = DataHelper.GetSafeString(dataReader, "Date", default(string));
                asseststatusInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                asseststatusInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                asseststatusInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                asseststatusInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                asseststatusInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                asseststatusInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                asseststatusInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));


               
               

                if (asseststatusInfoCollection == null)
                    asseststatusInfoCollection = new DocumentsStatusInfoCollection();

                asseststatusInfoCollection.Add(asseststatusInfo);
            }

            return asseststatusInfoCollection;
        }
    }
}

