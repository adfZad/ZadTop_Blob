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
    public class DocumentStatusDAL
    {

        public static DocumentStatusInfo Get(long DivisionId)
        {
            DocumentStatusInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocumentStatus");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DivisionId);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (divisionInfoCollection != null && divisionInfoCollection.Count > 0) ? divisionInfoCollection[0] : null;
        }

        public static DocumentStatusInfoCollection GetAll()
        {
            DocumentStatusInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("GetDocumentStatus");
                db.AddInParameter(dbCommand, "Id", DbType.Int64, DBNull.Value);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        public static DocumentStatusInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            DocumentStatusInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocumentStatus");

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
                    divisionInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        public static DocumentStatusInfoCollection Search(DocumentStatusSearchParams divisionSearchParams, out long totalRows)
        {
            DocumentStatusInfoCollection divisionInfoCollection = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase("ZadHoldingDB");
                DbCommand dbCommand = db.GetStoredProcCommand("SearchDocumentStatus");

                db.AddInParameter(dbCommand, "CurrentPage", DbType.Int32, divisionSearchParams.CurrentPage);

                if (divisionSearchParams.PageSize == 0)
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "PageSize", DbType.Int32, divisionSearchParams.PageSize);

                if (string.IsNullOrEmpty(divisionSearchParams.SortColumn))
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortColumn", DbType.String, divisionSearchParams.SortColumn);

                if (string.IsNullOrEmpty(divisionSearchParams.SortOrder))
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "SortOrder", DbType.String, divisionSearchParams.SortOrder);


                if (divisionSearchParams.CreatedId == 0)
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "CreatedId", DbType.Int64, divisionSearchParams.CreatedId);

                if (divisionSearchParams.UpdateId == 0)
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "UpdatedId", DbType.Int64, divisionSearchParams.UpdateId);

                if (string.IsNullOrEmpty(divisionSearchParams.Name))
                    db.AddInParameter(dbCommand, "Name", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Name", DbType.String, divisionSearchParams.Name);

                if (string.IsNullOrEmpty(divisionSearchParams.Description))
                    db.AddInParameter(dbCommand, "Description", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "Description", DbType.String, divisionSearchParams.Description);

                if (string.IsNullOrEmpty(divisionSearchParams.AltDescription))
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, DBNull.Value);
                else
                    db.AddInParameter(dbCommand, "AltDescription", DbType.String, divisionSearchParams.AltDescription);


                db.AddOutParameter(dbCommand, "TotalRows", DbType.Int64, 1);

                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    divisionInfoCollection = GetAsList(dataReader);
                }

                long.TryParse(db.GetParameterValue(dbCommand, "TotalRows").ToString(), out totalRows);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return divisionInfoCollection;
        }

        private static DocumentStatusInfoCollection GetAsList(IDataReader dataReader)
        {
            DocumentStatusInfoCollection divisionInfoCollection = null;

            while (dataReader.Read())
            {
                DocumentStatusInfo divisionInfo = new DocumentStatusInfo();
                divisionInfo.Id = DataHelper.GetSafeLong(dataReader, "Id", default(long));
                divisionInfo.Name = DataHelper.GetSafeString(dataReader, "Name", default(string));
                divisionInfo.Description = DataHelper.GetSafeString(dataReader, "Description", default(string));
                divisionInfo.AltDescription = DataHelper.GetSafeString(dataReader, "AltDescription", default(string));
                divisionInfo.Active = DataHelper.GetSafeBool(dataReader, "Active", default(bool));

                divisionInfo.CreatedId = DataHelper.GetSafeLong(dataReader, "CreatedId", default(long));
                divisionInfo.CreatedName = DataHelper.GetSafeString(dataReader, "CreatedName", default(string));
                divisionInfo.CreatedTime = DataHelper.GetSafeDateTime(dataReader, "CreatedTime", default(DateTime));

                divisionInfo.UpdatedId = DataHelper.GetSafeLong(dataReader, "UpdatedId", default(long));
                divisionInfo.UpdatedName = DataHelper.GetSafeString(dataReader, "UpdatedName", default(string));
                divisionInfo.UpdatedTime = DataHelper.GetSafeDateTime(dataReader, "UpdatedTime", default(DateTime));

                if (divisionInfoCollection == null)
                    divisionInfoCollection = new DocumentStatusInfoCollection();

                divisionInfoCollection.Add(divisionInfo);
            }

            return divisionInfoCollection;
        }
    }
}

