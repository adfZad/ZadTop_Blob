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
    public class DocumentDAL
    {
        public static byte Insert(DocumentInfo documentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("InsDocument");

                db.AddInParameter(dbCommand, "Name", DbType.String, documentInfo.Name);
                db.AddInParameter(dbCommand, "AmendNo", DbType.Int64, documentInfo.AmendNo);
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, documentInfo.FlowId);
                db.AddInParameter(dbCommand, "TypeId", DbType.Int64, documentInfo.TypeId);
                db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, documentInfo.DocumentStatusId);
                db.AddInParameter(dbCommand, "Amount", DbType.Decimal, documentInfo.Amount);
                db.AddInParameter(dbCommand, "OtherAmount", DbType.Decimal, documentInfo.OtherAmount);
                db.AddInParameter(dbCommand, "Currency", DbType.String, documentInfo.Currency);
                db.AddInParameter(dbCommand, "Rate", DbType.Decimal, documentInfo.Rate);
                db.AddInParameter(dbCommand, "Primary", DbType.String, documentInfo.Primary);
                db.AddInParameter(dbCommand, "One", DbType.String, documentInfo.One);
                db.AddInParameter(dbCommand, "Two", DbType.String, documentInfo.Two);
                db.AddInParameter(dbCommand, "Three", DbType.String, documentInfo.Three);
                db.AddInParameter(dbCommand, "Four", DbType.String, documentInfo.Four);
                db.AddInParameter(dbCommand, "Five", DbType.String, documentInfo.Five);
                db.AddInParameter(dbCommand, "Six", DbType.String, documentInfo.Six);
                db.AddInParameter(dbCommand, "OneName", DbType.String, documentInfo.OneName);
                db.AddInParameter(dbCommand, "TwoName", DbType.String, documentInfo.TwoName);
                db.AddInParameter(dbCommand, "ThreeName", DbType.String, documentInfo.ThreeName);
                db.AddInParameter(dbCommand, "FourName", DbType.String, documentInfo.FourName);
                db.AddInParameter(dbCommand, "FiveName", DbType.String, documentInfo.FiveName);
                db.AddInParameter(dbCommand, "SixName", DbType.String, documentInfo.SixName);
                db.AddInParameter(dbCommand, "Description", DbType.String, documentInfo.Description);
                db.AddInParameter(dbCommand, "Purpose", DbType.String, documentInfo.Purpose);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, documentInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, documentInfo.Active);
                db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, documentInfo.CreatedId);
                db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, documentInfo.CreatedTime);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, documentInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, documentInfo.UpdatedTime);
                db.AddOutParameter(dbCommand, "Id", DbType.Int64, 1);
                db.AddOutParameter(dbCommand, "Result", DbType.Byte, 1);

                db.ExecuteNonQuery(dbCommand);

                long id = 0;
                long.TryParse(db.GetParameterValue(dbCommand, "Id").ToString(), out id);
                documentInfo.Id = id;

                byte.TryParse(db.GetParameterValue(dbCommand, "Result").ToString(), out result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static byte Delete(long documentId)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("DelDocument");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, documentId);
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


        public static byte Update(DocumentInfo documentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDocument");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, documentInfo.Id);
                db.AddInParameter(dbCommand, "AmendNo", DbType.Int64, documentInfo.AmendNo);
                db.AddInParameter(dbCommand, "Name", DbType.String, documentInfo.Name);
                db.AddInParameter(dbCommand, "FlowId", DbType.Int64, documentInfo.FlowId);
                db.AddInParameter(dbCommand, "TypeId", DbType.Int64, documentInfo.TypeId);
                db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, documentInfo.DocumentStatusId);
                db.AddInParameter(dbCommand, "Amount", DbType.Decimal, documentInfo.Amount);
                db.AddInParameter(dbCommand, "OtherAmount", DbType.Decimal, documentInfo.OtherAmount);
                db.AddInParameter(dbCommand, "Currency", DbType.String, documentInfo.Currency);
                db.AddInParameter(dbCommand, "Rate", DbType.Decimal, documentInfo.Rate);
                db.AddInParameter(dbCommand, "Primary", DbType.String, documentInfo.Primary);
                db.AddInParameter(dbCommand, "One", DbType.String, documentInfo.One);
                db.AddInParameter(dbCommand, "Two", DbType.String, documentInfo.Two);
                db.AddInParameter(dbCommand, "Three", DbType.String, documentInfo.Three);
                db.AddInParameter(dbCommand, "Four", DbType.String, documentInfo.Four);
                db.AddInParameter(dbCommand, "Five", DbType.String, documentInfo.Five);
                db.AddInParameter(dbCommand, "Six", DbType.String, documentInfo.Six);
                db.AddInParameter(dbCommand, "OneName", DbType.String, documentInfo.OneName);
                db.AddInParameter(dbCommand, "TwoName", DbType.String, documentInfo.TwoName);
                db.AddInParameter(dbCommand, "ThreeName", DbType.String, documentInfo.ThreeName);
                db.AddInParameter(dbCommand, "FourName", DbType.String, documentInfo.FourName);
                db.AddInParameter(dbCommand, "FiveName", DbType.String, documentInfo.FiveName);
                db.AddInParameter(dbCommand, "SixName", DbType.String, documentInfo.SixName);
                db.AddInParameter(dbCommand, "Description", DbType.String, documentInfo.Description);
                db.AddInParameter(dbCommand, "Purpose", DbType.String, documentInfo.Purpose);
                db.AddInParameter(dbCommand, "AltDescription", DbType.String, documentInfo.AltDescription);
                db.AddInParameter(dbCommand, "Active", DbType.Boolean, documentInfo.Active);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, documentInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, documentInfo.UpdatedTime);

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

        public static byte UpdateDocumentStatusId(DocumentInfo documentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDocumentStatus");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, documentInfo.Id);
                db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, documentInfo.DocumentStatusId);
               

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

        public static byte UpdateDocumentStatus(DocumentInfo documentInfo)
        {
            byte result = 0;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("UpdDocumentStatusId");

                db.AddInParameter(dbCommand, "Id", DbType.Int64, documentInfo.Id);
                db.AddInParameter(dbCommand, "DocumentStatusId", DbType.Int64, documentInfo.DocumentStatusId);
                db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, documentInfo.UpdatedId);
                db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, documentInfo.UpdatedTime);
             
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

        public static DocumentInfo Get(long DocumentId)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocument");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DocumentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (documentInfoCollection != null && documentInfoCollection.Count > 0) ? documentInfoCollection[0] : null;
        }

        public static DocumentInfoCollection GetAll()
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocument");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documentInfoCollection;
        }

        public static DocumentInfo GetApproved(long DocumentId)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetApprovedDocument");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DocumentId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (documentInfoCollection != null && documentInfoCollection.Count > 0) ? documentInfoCollection[0] : null;
        }

        public static DocumentInfoCollection GetApprovedAll()
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetApprovedDocument");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documentInfoCollection;
        }

        public static DocumentInfoCollection GetDocumentByUserId(long UserId)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocumentByUserId");
                db.AddInParameter(dbCommand, "UserId", DbType.Int64, UserId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    documentInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return documentInfoCollection;
        }

        public static DocumentInfoCollection SearchAll(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocumentAll");

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

        public static DocumentInfoCollection SearchAll(DocumentSearchParams documentSearchParams, out long totalRows)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocumentAll");

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

        public static DocumentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocument");

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

        public static DocumentInfoCollection Search(DocumentSearchParams documentSearchParams, out long totalRows)
        {
            DocumentInfoCollection documentInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocument");

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

        private static DocumentInfoCollection GetAsList(IDataReader dataReader)
        {
            DocumentInfoCollection documentInfoCollection = null;

            while (dataReader.Read())
            {
                DocumentInfo documentInfo = new DocumentInfo();
                documentInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                documentInfo.DocumentId = DataHelper.GetSafeLong(dataReader, "DocumentId", default(long));
                documentInfo.AmendNo = DataHelper.GetSafeLong(dataReader, "AmendNo", default(long));
                documentInfo.Code = DataHelper.GetSafeString(dataReader, "Code", default(string));
                documentInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                documentInfo.FlowId = DataHelper.GetSafeLong(dataReader, "FlowId", default(long));
                documentInfo.FlowName = DataHelper.GetSafeString(dataReader, "FlowName", default(string));
                documentInfo.DivisionId = DataHelper.GetSafeLong(dataReader, "DivisionId", default(long));
                documentInfo.DivisionName = DataHelper.GetSafeString(dataReader, "DivisionName", default(string));
                documentInfo.DepartmentId = DataHelper.GetSafeLong(dataReader, "DepartmentId", default(long));
                documentInfo.DepartmentName = DataHelper.GetSafeString(dataReader, "DepartmentName", default(string));
                documentInfo.TypeId = DataHelper.GetSafeLong(dataReader, "TypeId", default(long));
                documentInfo.TypeName = DataHelper.GetSafeString(dataReader, "TypeName", default(string));
                documentInfo.DocumentStatusId = DataHelper.GetSafeLong(dataReader, "DocumentStatusId", default(long));
                documentInfo.DocumentStatusName = DataHelper.GetSafeString(dataReader, "DocumentStatusName", default(string));
                documentInfo.Amount = DataHelper.GetSafeDecimal(dataReader, "Amount", default(decimal));
                documentInfo.OtherAmount = DataHelper.GetSafeDecimal(dataReader, "OtherAmount", default(decimal));
                documentInfo.Currency = DataHelper.GetSafeString(dataReader, "Currency", default(string));
                documentInfo.Rate = DataHelper.GetSafeDecimal(dataReader, "Rate", default(decimal));
                documentInfo.Primary = DataHelper.GetSafeString(dataReader, "Primary", default(string));
                documentInfo.One = DataHelper.GetSafeString(dataReader, "One", default(string));
                documentInfo.Two = DataHelper.GetSafeString(dataReader, "Two", default(string));
                documentInfo.Three = DataHelper.GetSafeString(dataReader, "Three", default(string));
                documentInfo.Four = DataHelper.GetSafeString(dataReader, "Four", default(string));
                documentInfo.Five = DataHelper.GetSafeString(dataReader, "Five", default(string));
                documentInfo.Six = DataHelper.GetSafeString(dataReader, "Six", default(string));
                documentInfo.OneName = DataHelper.GetSafeString(dataReader, "OneName", default(string));
                documentInfo.TwoName = DataHelper.GetSafeString(dataReader, "TwoName", default(string));
                documentInfo.ThreeName = DataHelper.GetSafeString(dataReader, "ThreeName", default(string));
                documentInfo.FourName = DataHelper.GetSafeString(dataReader, "FourName", default(string));
                documentInfo.FiveName = DataHelper.GetSafeString(dataReader, "FiveName", default(string));
                documentInfo.SixName = DataHelper.GetSafeString(dataReader, "SixName", default(string));
                documentInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                documentInfo.Purpose = DataHelper.GetSafeString(dataReader, "Purpose", default(string));
                documentInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                documentInfo.Date = DataHelper.GetSafeString(dataReader, "Date", default(string));
                documentInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                documentInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                documentInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                documentInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                documentInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                documentInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                documentInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (documentInfoCollection == null)
                    documentInfoCollection = new DocumentInfoCollection();

                documentInfoCollection.Add(documentInfo);
            }

            return documentInfoCollection;
        }
    }
}

