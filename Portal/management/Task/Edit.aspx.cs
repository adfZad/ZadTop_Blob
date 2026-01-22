using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Collection;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.Collections.Specialized;
using System.IO;
using ZadHolding.Utilities;
using System.Drawing.Imaging;
using System.Net.Mail;

using System.Text;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;


public partial class management_Task_Edit : System.Web.UI.Page
{   
    
    public string MainDocPathString = null;
    public string ApproveDocPathString = null;
    public string MainDocNameString = null;
    public string OneDocPathString = null;
    public string OneDocNameString = null;
    public string TwoDocPathString = null;
    public string TwoDocNameString = null;
    public string ThreeDocPathString = null;
    public string ThreeDocNameString = null;
    public string FourDocPathString = null;
    public string FourDocNameString = null;
    public string FiveDocPathString = null;
    public string FiveDocNameString = null;
    public string SixDocPathString = null;
    public string SixDocNameString = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        btnCancel.Attributes.Add("onclick", "return askConfirm();");
        btnReturn.Attributes.Add("onclick", "return askConfirm();");
              
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                long id = 0;
                long.TryParse(Request.QueryString["id"], out id);
                BindData(id);
            }
        }

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text != "")
        {
            long id = 0;
            long.TryParse(hdnId.Value, out id);
            TaskInfo currentTaskInfo = TaskManager.Get(id);
            if (currentTaskInfo.TaskStatusId == 1 && currentTaskInfo.UserId == PortalUser.Current.UserId)
            {

                TaskInfo taskInfo = new TaskInfo();
                long l = 0;
                long.TryParse(hdnDocumentId.Value, out l);
                DocumentInfo documentInfo = new DocumentInfo();
                taskInfo.Id = id;
                taskInfo.TaskStatusId = 3;
                taskInfo.ApprovalStatusId = 4;
                taskInfo.UpdatedTime = DateTime.Now;
                taskInfo.AltDescription = txtRemarks.Text.ToString();
                byte result = TaskManager.Update_End_Time(taskInfo);
                if (result == 1)
                {
                    documentInfo = new DocumentInfo();
                    documentInfo.Id = l;
                    documentInfo.DocumentStatusId = 4;
                    documentInfo.UpdatedId = PortalUser.Current.UserId;
                    documentInfo.UpdatedTime = DateTime.Now;
                    result = DocumentManager.UpdateDocumentStatus(documentInfo);
                    if (result == 1)
                    {
                        documentInfo = DocumentManager.Get(l);
                        textwriter(documentInfo);
                        Response.Write("<script type='text/javascript'>");
                        Response.Write("alert('Document Returned ! " + documentInfo.CreatedName + "');");
                        Response.Write("document.location.href='../Tools/DashBoard.aspx';");
                        Response.Write("</script>");
                    }
                }
                DocumentInfo newDocumentInfo = new DocumentInfo();
                newDocumentInfo.Name = documentInfo.Name;
                newDocumentInfo.FlowId = documentInfo.FlowId;
                newDocumentInfo.TypeId = documentInfo.TypeId;
                newDocumentInfo.Active = true;
                newDocumentInfo.Description = txtRemarks.Text.ToString();
                newDocumentInfo.Purpose = documentInfo.Purpose;
                newDocumentInfo.Amount = documentInfo.Amount;
                newDocumentInfo.Rate = documentInfo.Rate;
                newDocumentInfo.OtherAmount = documentInfo.OtherAmount;
                newDocumentInfo.Currency = documentInfo.Currency;
                newDocumentInfo.AltDescription = documentInfo.Description;
                newDocumentInfo.CreatedId = currentTaskInfo.DocumentCreatedId;
                newDocumentInfo.CreatedTime = DateTime.Now;
                newDocumentInfo.UpdatedId = currentTaskInfo.DocumentCreatedId;
                newDocumentInfo.UpdatedTime = DateTime.Now;
                newDocumentInfo.DocumentStatusId = 5;
                newDocumentInfo.AmendNo = documentInfo.AmendNo + 1;
                newDocumentInfo.Primary = documentInfo.Primary;
                newDocumentInfo.One = documentInfo.One;
                newDocumentInfo.Two = documentInfo.Two;
                newDocumentInfo.Three = documentInfo.Three;
                newDocumentInfo.Four = documentInfo.Four;
                newDocumentInfo.Five = documentInfo.Five;
                newDocumentInfo.Six = documentInfo.Six;
                newDocumentInfo.Primary = documentInfo.Primary;
                newDocumentInfo.OneName = documentInfo.OneName;
                newDocumentInfo.TwoName = documentInfo.TwoName;
                newDocumentInfo.ThreeName = documentInfo.ThreeName;
                newDocumentInfo.FourName = documentInfo.FourName;
                newDocumentInfo.FiveName = documentInfo.FiveName;
                newDocumentInfo.SixName = documentInfo.SixName;
                newDocumentInfo.AltDescription = documentInfo.AltDescription;

                result = DocumentManager.Insert(newDocumentInfo);
                if (result == 1)
                {
                    TaskInfo createTaskInfo = new TaskInfo();
                    createTaskInfo.LevelId = 1;
                    createTaskInfo.DocumentId = newDocumentInfo.Id;
                    createTaskInfo.UserId = documentInfo.CreatedId;
                    createTaskInfo.Description = "Return Auto Created Task";
                    createTaskInfo.AltDescription = "Return Auto Created Task";
                    createTaskInfo.TaskStatusId = 1;
                    createTaskInfo.TaskTypeId = 2;
                    createTaskInfo.ApprovalStatusId = 3;
                    createTaskInfo.CreatedTime = DateTime.Now;
                    createTaskInfo.UpdatedTime = DateTime.Now;
                    createTaskInfo.Active = true;
                    TaskManager.Insert(createTaskInfo);
                }
            }
        }
        else
        {
            Response.Write("<script type='text/javascript'>");
            Response.Write("alert('Comments are mandatory ! ');");
            //Response.Write("document.location.href='../Tools/DashBoard.aspx';");
            Response.Write("</script>");
            return;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text != "")
        {
         long id = 0;
         long.TryParse(hdnId.Value, out id);
         TaskInfo currentTaskInfo = TaskManager.Get(id);
         if (currentTaskInfo.TaskStatusId == 1 && currentTaskInfo.UserId == PortalUser.Current.UserId)
         {

             TaskInfo taskInfo = new TaskInfo();
             taskInfo.Id = id;
             taskInfo.TaskStatusId = 3;
             taskInfo.ApprovalStatusId = 2;
             taskInfo.UpdatedTime = DateTime.Now;
             taskInfo.AltDescription = txtRemarks.Text.ToString();
             byte result = TaskManager.Update_End_Time(taskInfo);
             if (result == 1)
             {
                 long l = 0;
                 long.TryParse(hdnDocumentId.Value, out l);
                 DocumentInfo documentInfo = new DocumentInfo();
                 documentInfo.Id = l;
                 documentInfo.DocumentStatusId = 3;
                 documentInfo.UpdatedId = PortalUser.Current.UserId;
                 documentInfo.UpdatedTime = DateTime.Now;
                 result = DocumentManager.UpdateDocumentStatus(documentInfo);
                 if (result == 1)
                 {
                     documentInfo = DocumentManager.Get(l);
                     textwriter(documentInfo);
                     Response.Write("<script type='text/javascript'>");
                     Response.Write("alert('Document Rejected ! " + PortalUser.Current.UserName + "');");
                     Response.Write("document.location.href='../Tools/DashBoard.aspx';");
                     Response.Write("</script>");
                 }
             }
         }
        }
        else
        {
            Response.Write("<script type='text/javascript'>");
            Response.Write("alert('Comments are mandatory ! ');");
            //Response.Write("document.location.href='../Tools/DashBoard.aspx';");
            Response.Write("</script>");
            return;
        }
    }
    
    protected void BindData(long id)
    {
        TaskInfo taskInfo = TaskManager.Get(id);

        if (taskInfo != null)
        {
            hdnId.Value = taskInfo.Id.ToString();
            hdnDocumentId.Value = taskInfo.DocumentId.ToString();
            lblDocumentName.Text = taskInfo.DocumentName;
            lblDocumentTypeName.Text = taskInfo.DocumentTypeName;
            lblDocumentPurpose.Text = taskInfo.DocumentPurpose;
            lblDocumentRemarks.Text = taskInfo.DocumentDescription;
            TaskInfoCollection taskInfoCollection = TaskManager.GetTaskByDocument(taskInfo.DocumentId);
            rptTask.DataSource = taskInfoCollection;
            rptTask.DataBind();
            DocumentInfo documentInfo = DocumentManager.Get(taskInfo.DocumentId);
            if (documentInfo != null)
            {
                MainDocPathString = DocPathMethod(documentInfo.Primary);
                MainDocNameString = documentInfo.Name;
                OneDocPathString = DocPathMethod(documentInfo.One);
                OneDocNameString = documentInfo.OneName;
                TwoDocPathString = DocPathMethod(documentInfo.Two);
                TwoDocNameString = documentInfo.TwoName;
                ThreeDocPathString = DocPathMethod(documentInfo.Three);
                ThreeDocNameString = documentInfo.ThreeName;
                FourDocPathString = DocPathMethod(documentInfo.Four);
                FourDocNameString = documentInfo.FourName;
                FiveDocPathString = DocPathMethod(documentInfo.Five);
                FiveDocNameString = documentInfo.FiveName;
                SixDocPathString = DocPathMethod(documentInfo.Six);
                SixDocNameString = documentInfo.SixName;
                ApproveDocPathString = DocPathMethod(documentInfo.AltDescription);
                myframe.Attributes["src"] = ApproveDocPathString;
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
         long id = 0;
         long.TryParse(hdnId.Value, out id);
         TaskInfo currentTaskInfo = TaskManager.Get(id);
         if (currentTaskInfo.TaskStatusId == 1 && currentTaskInfo.UserId == PortalUser.Current.UserId)
         {
             TaskInfo taskInfo = new TaskInfo();

             taskInfo.Id = id;
             taskInfo.TaskStatusId = 3;
             taskInfo.ApprovalStatusId = 1;
             taskInfo.UpdatedTime = DateTime.Now;
             taskInfo.AltDescription = txtRemarks.Text.ToString();
             byte result = TaskManager.Update_End_Time(taskInfo);
             if (result == 1)
             {

                 long.TryParse(hdnDocumentId.Value, out id);
                 TaskInfo nextTask = TaskManager.GetNextTask(id);
                 if (nextTask != null)
                 {
                     nextTask.TaskStatusId = 1;
                     nextTask.ApprovalStatusId = 3;
                     nextTask.CreatedTime = DateTime.Now;
                     result = TaskManager.Update_Start_Time(nextTask);
                     if (result == 1)
                     {

                         long l = 0;
                         long.TryParse(hdnDocumentId.Value, out l);
                         DocumentInfo documentInfo = new DocumentInfo();
                         documentInfo.Id = l;
                         documentInfo.DocumentStatusId = 2;
                         documentInfo.UpdatedId = PortalUser.Current.UserId;
                         documentInfo.UpdatedTime = DateTime.Now;
                         result = DocumentManager.UpdateDocumentStatus(documentInfo);
                         if (result == 1)
                         {
                             documentInfo = DocumentManager.Get(l);
                             textwriter(documentInfo);
                             Response.Write("<script type='text/javascript'>");
                             Response.Write("alert('Successfully Completed! Document Moved to " + nextTask.UserName + "');");
                             Response.Write("document.location.href='../Tools/DashBoard.aspx';");
                             Response.Write("</script>");
                         }
                     }
                 }
                 else
                 {
                     long l = 0;
                     long.TryParse(hdnDocumentId.Value, out l);
                     DocumentInfo documentInfo = new DocumentInfo();
                     documentInfo.Id = l;
                     documentInfo.DocumentStatusId = 1;
                     documentInfo.UpdatedId = PortalUser.Current.UserId;
                     documentInfo.UpdatedTime = DateTime.Now;
                     result = DocumentManager.UpdateDocumentStatus(documentInfo);
                     if (result == 1)
                     {
                         documentInfo = DocumentManager.Get(l);
                         textwriter(documentInfo);
                         //sendMessage(documentInfo);
                         Response.Write("<script type='text/javascript'>");
                         Response.Write("alert('Successfully Approved ! " + PortalUser.Current.UserName + "');");
                         Response.Write("document.location.href='../Tools/DashBoard.aspx';");
                         Response.Write("</script>");
                     }
                 }
             }

         }
         else
         {
             Response.Write("<script type='text/javascript'>");
             Response.Write("alert('Successfully Approved ! );");
             Response.Write("</script>");
         }
    }

    protected string DocPathMethod(string fileName)
    {
        string path = "#";
        string localPath = "";

        if (!string.IsNullOrEmpty(fileName))
            {
                path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                    WebConfigKeys.UploadImageRootDirectory,
                    FileType.Passport,
                    fileName.Substring(0, 1),
                    fileName.Substring(0, 2),
                    fileName);

                localPath = Server.MapPath(path);

                if (!File.Exists(localPath))
                {
                    path = "#";
                }
            }
      
        return path;

    }

    protected string LocalDocPathMethod(string fileName)
    {
        string path = "#";
        string localPath = "";

        if (!string.IsNullOrEmpty(fileName))
        {
            path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                WebConfigKeys.UploadImageRootDirectory,
                FileType.Passport,
                fileName.Substring(0, 1),
                fileName.Substring(0, 2),
                fileName);

            localPath = Server.MapPath(path);

            if (!File.Exists(localPath))
            {
                path = "#";
            }
            else
            {
                path = localPath;
            }
        }

        return path;

    }

    public void textwriter(DocumentInfo documentInfo)
    {
        string fileNameExisting = LocalDocPathMethod(documentInfo.Primary);
        string fileNameNew = LocalDocPathMethod(documentInfo.AltDescription);
        string approvalString = "";
        long totalRows = 0;
        TaskSearchParams taskSearchParams = new TaskSearchParams();
        taskSearchParams.CurrentPage = 1;
        taskSearchParams.PageSize = 100;
        taskSearchParams.SortColumn = "";
        taskSearchParams.SortOrder = "";
        taskSearchParams.DocumentId = documentInfo.Id;
        taskSearchParams.TaskStatusId = 3;
        TaskInfoCollection finishedTaskInfoCollection = TaskManager.Search(taskSearchParams, out totalRows);
        System.Drawing.Point p = new System.Drawing.Point(5, 580);
        AddTextToPdf(fileNameExisting, fileNameNew, approvalString, p, finishedTaskInfoCollection, totalRows);
       
    }

    public void AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, System.Drawing.Point point, TaskInfoCollection taskInfoCollection, long totalRows)
    {
      
        string pathin = inputPdfPath;
        string pathout = outputPdfPath;
        int i = Convert.ToInt32(totalRows);
        using (PdfReader reader = new PdfReader(pathin))
        using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
        {
          
            reader.SelectPages("1");
            var pageSize = reader.GetPageSize(1);
            string path = "../../Fonts/AAutoSignature-1GD9j.ttf";
            string lpath = getLocalPath(path);
           
            PdfContentByte pbover = stamper.GetOverContent(1);
            Font sigFont = new Font(BaseFont.CreateFont(lpath, BaseFont.CP1257, BaseFont.EMBEDDED), 6);
            Font regFont = new Font(BaseFont.CreateFont("../../Fonts/TIMES.TTF", BaseFont.MACROMAN, BaseFont.EMBEDDED), 6);

            PdfPTable table = new PdfPTable(i);


            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.ApprovalStatusId == 2)
                {

                    Font font = new Font();
                    font.Size = 15;
                    font.Color = BaseColor.RED;
                    font.IsBold();
                    //setting up the X and Y coordinates of the document
                    int x = point.X;
                    int y = point.Y;
                    x = (int)(pageSize.Width - 60);
                    y = (int)(pageSize.Height - 18);

                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase("Rejected", font), x, y, 0);
                    
                }
                DocumentInfo documentInfo = DocumentManager.Get(taskInfo.DocumentId);
                if (taskInfo.ApprovalStatusId == 1 && documentInfo.DocumentStatusId == 1)
                {

                    Font font = new Font();
                    font.Size = 15;
                    font.Color = BaseColor.GREEN;
                    font.IsBold();
                    //setting up the X and Y coordinates of the document
                    int x = point.X;
                    int y = point.Y;
                    x = (int)(pageSize.Width - 60);
                    y = (int)(pageSize.Height - 18);
                    string s = DateTime.Now.ToString("yyyyMMdd") +"-"+documentInfo.Id.ToString("0000");
                    ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(s, font), x, y, 0);

                }
                    if (taskInfo.ApprovalStatusId == 4)
                    {

                        //int pageCount = reader.NumberOfPages;

                        //// Create New Layer for Watermark
                        //PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                        //// Loop through each Page
                        //for (int j = 1; j <= pageCount; j++)
                        //{
                        //    // Getting the Page Size
                        //    Rectangle rect = reader.GetPageSize(j);

                        //    // Get the ContentByte object
                        //    PdfContentByte cb = stamper.GetUnderContent(j);

                        //    // Tell the cb that the next commands should be "bound" to this new layer
                        //    cb.BeginLayer(layer);
                        //    cb.SetFontAndSize(BaseFont.CreateFont(
                        //      BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);

                        //    PdfGState gState = new PdfGState();
                        //    gState.FillOpacity = 0.25f;
                        //    cb.SetGState(gState);

                        //    cb.SetColorFill(BaseColor.BLACK);
                        //    cb.BeginText();
                        //    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "RETUNED", rect.Width / 2, rect.Height / 2, 45f);
                        //    cb.EndText();

                        //    // Close the layer
                        //    cb.EndLayer();
                        //    //    //add content to the page using ColumnText
                        //    //Font font = new Font();
                        //    //font.Size = 100;
                        //    //font.Color = BaseColor.RED;

                        //    ////setting up the X and Y coordinates of the document
                        //    //int x = point.X;
                        //    //int y = point.Y;

                        //    //y = (int) (pageSize.Height - y);

                        //    //ColumnText.ShowTextAligned(pbover, Element., new Phrase("RETURNED", font), 500, 500, 0);

                        //}



                        Font font = new Font();
                        font.Size = 15;
                        font.Color = BaseColor.RED;
                        font.IsBold(); 
                        //setting up the X and Y coordinates of the document
                        int x = point.X;
                        int y = point.Y;
                        x  = (int)(pageSize.Width-60);
                        y = (int)(pageSize.Height - 18);

                        ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase("Returned", font), x, y, 0);
                    }
                 
               }


            //foreach (TaskInfo taskInfo in taskInfoCollection)
            //{
            //    if (taskInfo.LevelId == 1)
            //    {
            //        PdfPCell cell = new PdfPCell(new Phrase("Created By", regFont));
            //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //        cell.Border = Rectangle.NO_BORDER;
            //        table.AddCell(cell);
            //    }
            //    else
            //    {
            //        if (taskInfo.ApprovalStatusId == 2)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase("Rejected By", regFont));
            //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //            cell.Border = Rectangle.NO_BORDER;
            //            table.AddCell(cell);
            //        }
            //        if (taskInfo.ApprovalStatusId == 4)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase("Returned By", regFont));
            //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //            cell.Border = Rectangle.NO_BORDER;
            //            table.AddCell(cell);
            //        }
            //        if (taskInfo.ApprovalStatusId == 1)
            //        {
            //            PdfPCell cell = new PdfPCell(new Phrase("Approved By", regFont));
            //            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //            cell.Border = Rectangle.NO_BORDER;
            //            table.AddCell(cell);
            //        }
            //    }
        
            //}

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.Active == true)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDescription, sigFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserAltDescription, sigFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }

  
            }

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
                if (taskInfo.Active == true)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserFullName, regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase(" ", regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
            }

          
            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.Active == true)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDesignationName, regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase(" ", regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                 
            }
          
            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                if (taskInfo.Active == true)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UpdatedTime.ToShortDateString(), regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                else
                {
                    PdfPCell cell = new PdfPCell(new Phrase(" ", regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
              
            }

       
            table.TotalWidth = 765f;
          
            table.WriteSelectedRows(0, -1, 15, 60, pbover);
            stamper.FormFlattening = true;
           
        }

          
        }

    
    public string getLocalPath(string path)
    {
        return Server.MapPath(path);
    }

    protected void sendMessage(DocumentInfo documentInfo)
    {
        try
        {
            String userName = "lijo@zad.qa";
            String password = "Pogo1234";
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress("nazam@zad.qa"));
            msg.From = new MailAddress(userName);
            msg.Subject = documentInfo.Name.ToString()+" has been Approved";
            msg.Body = documentInfo.Name.ToString()+"Approved";
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
            ex.ToString();
        }
    }

   
    }

