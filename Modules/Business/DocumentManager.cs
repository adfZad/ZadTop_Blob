using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZadHolding.Been;
using ZadHolding.Data;
using System.Net.Mail;


namespace ZadHolding.Business
{
    public class DocumentManager
    {
        public static byte Insert(DocumentInfo documentInfo)
        {
            return DocumentDAL.Insert(documentInfo);
        }

        public static byte Update(DocumentInfo documentInfo)
        {
            return DocumentDAL.Update(documentInfo);
        }

        public static byte UpdateDocumentStatus(DocumentInfo documentInfo)
        {
            return DocumentDAL.UpdateDocumentStatus(documentInfo);
        }

        public static byte UpdateDocumentStatusId(DocumentInfo documentInfo)
        {
            return DocumentDAL.UpdateDocumentStatusId(documentInfo);
        }
        public static byte Delete(long exchangeRateId)
        {
            return DocumentDAL.Delete(exchangeRateId);
        }

        public static DocumentInfo Get(long exchangeRateId)
        {
            return DocumentDAL.Get(exchangeRateId);
        }

        public static DocumentInfoCollection GetAll()
        {
            return DocumentDAL.GetAll();
        }

        public static DocumentInfo GetApproved(long exchangeRateId)
        {
            return DocumentDAL.GetApproved(exchangeRateId);
        }

        public static DocumentInfoCollection GetApprovedAll()
        {
            return DocumentDAL.GetApprovedAll();
        }

        public static DocumentInfoCollection GetDocumentByUserId(long UserId)
        {
            return DocumentDAL.GetDocumentByUserId(UserId);
        }
        public static DocumentInfoCollection Search(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DocumentDAL.Search(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DocumentInfoCollection Search(DocumentSearchParams documentSearchParams, out long totalRows)
        {
            return DocumentDAL.Search(documentSearchParams, out totalRows);
        }

        public static DocumentInfoCollection SearchAll(int currentPage, int pageSize, string sortColumn, string sortOrder, out long totalRows)
        {
            return DocumentDAL.SearchAll(currentPage, pageSize, sortColumn, sortOrder, out totalRows);
        }
        public static DocumentInfoCollection SearchAll(DocumentSearchParams documentSearchParams, out long totalRows)
        {
            return DocumentDAL.SearchAll(documentSearchParams, out totalRows);
        }

        public static void SendMail(DocumentInfo documentInfo)
        {
            try
            {
                String userName = "lijo@zad.qa";
                String password = "Pogo1234";
                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress("nazam@zad.qa"));
                msg.From = new MailAddress(userName);
                msg.Subject = documentInfo.Name.ToString() + " has been Approved";
                msg.Body = documentInfo.Name.ToString() + "Approved";
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.office365.com";
                client.Credentials = new System.Net.NetworkCredential(userName, password);
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                // Log exception if needed
            }
        }
    }
}
