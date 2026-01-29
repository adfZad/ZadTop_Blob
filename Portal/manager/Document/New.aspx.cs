using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Been.Collection;
using ZadHolding.Business;
using ZadHolding.PortalBase;
using System.Collections.Specialized;
using ZadHolding.Utilities;
using System.Drawing.Imaging;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Data;
using System.IO;


public partial class manager_Document_New : System.Web.UI.Page
{
    //public static TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            BindFlow();
            BindType();
        }
    }

    private void SaveFiles(DocumentInfo documentInfo)
    {
        try
        {
            var metadata = new Dictionary<string, string>
            {
                { "ReferenceNo", documentInfo.Name },
                { "DocumentType", ddlType.SelectedItem.Text },
                { "RequestDate", DateTime.Now.ToString("yyyy-MM-dd") },
                { "RequestStatus", "Pending" },
                { "UploadedBy", PortalUser.Current.UserId.ToString() }
            };

            FileManager.SaveFile(fupRegistration, FileType.Passport, documentInfo.Primary, metadata);
            FileManager.SaveFile(fupRegistration, FileType.Passport, documentInfo.AltDescription, metadata);
            if (fupOne.HasFile)
              FileManager.SaveFile(fupOne, FileType.Passport, documentInfo.One, metadata);
            if (fupTwo.HasFile)
            FileManager.SaveFile(fupTwo, FileType.Passport, documentInfo.Two, metadata);
            if (fupThree.HasFile)
            FileManager.SaveFile(fupThree, FileType.Passport, documentInfo.Three, metadata);
            if (fupFour.HasFile)
                FileManager.SaveFile(fupFour, FileType.Passport, documentInfo.Four, metadata);
            if (fupFive.HasFile)
                FileManager.SaveFile(fupFive, FileType.Passport, documentInfo.Five, metadata);
            if (fupSix.HasFile)
                FileManager.SaveFile(fupSix, FileType.Passport, documentInfo.Six, metadata);
          
        }
        catch
        { }
    }

    protected void cvImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator validator = (CustomValidator)source;

        FileUpload upload = (FileUpload)this.pnlCreate.FindControl(validator.ControlToValidate);

        bool flag = true;
        List<string> allowedExtensions = null;

        if (upload.ID.ToString().Contains("image"))
            allowedExtensions = new List<string>() { ".jpg", ".jpeg", ".png" };
        else
            allowedExtensions = new List<string>() { ".pdf" };

        if (upload.HasFile)
        {
            string fileName = upload.PostedFile.FileName;
            string fileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1, fileName.Length - (fileName.LastIndexOf('.') + 1));
            flag = string.IsNullOrEmpty(allowedExtensions.Find(ext => (ext == fileExtension)));
        }

        validator.IsValid = flag;
        args.IsValid = flag;
    }

    protected void cvFileSize_ServerValidate(object source, ServerValidateEventArgs args)
    {
        CustomValidator validator = (CustomValidator)source;
        FileUpload upload = (FileUpload)this.pnlCreate.FindControl(validator.ControlToValidate);
        if (!string.IsNullOrEmpty(upload.FileName) && (upload.PostedFile.ContentLength <= WebConfigKeys.MaxImageSize))
        {
            validator.IsValid = true;
            args.IsValid = true;
        }
        else
        {
            validator.IsValid = false;
            args.IsValid = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DocumentInfo documentInfo = new DocumentInfo();

            long lvalue = 0;
            decimal dvalue = 0;

            documentInfo.Name = txtName.Text.Trim();
            long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
            documentInfo.FlowId = lvalue;
            long.TryParse(ddlType.SelectedItem.Value, out lvalue);
            documentInfo.TypeId = lvalue;
            documentInfo.Active = true;
            documentInfo.Description = txtDesc.Text.Trim();
            documentInfo.Purpose = txtPurpose.Text.Trim();
            documentInfo.AltDescription = txtDesc.Text.Trim();
            documentInfo.Currency = ddlCurrency.SelectedItem.Text.Trim();
            documentInfo.CreatedId = PortalUser.Current.UserId;
            documentInfo.CreatedTime = DateTime.Now;
            documentInfo.UpdatedId = PortalUser.Current.UserId;
            documentInfo.UpdatedTime = DateTime.Now;
            documentInfo.DocumentStatusId = 2;
            documentInfo.AmendNo = 0;

            decimal.TryParse(txtAmount.Text.Trim(), out dvalue);
            documentInfo.Amount = dvalue;

           

            decimal.TryParse(txtExchangeRate.Text.Trim(), out dvalue);
            documentInfo.Rate = dvalue;

            decimal.TryParse(txtOtherAmount.Text.Trim(), out dvalue);
            documentInfo.OtherAmount = dvalue;



            documentInfo.Primary = FileManager.GenerateUniqueName(FileType.Passport);
            documentInfo.AltDescription = FileManager.GenerateUniqueName(FileType.Passport);

            if (fupOne.HasFile)
            {
                documentInfo.One = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.OneName = txtOne.Text.ToString();
            }
            if (fupTwo.HasFile)
            {
                documentInfo.Two = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.TwoName = txtTwo.Text.ToString();
            }
            if (fupThree.HasFile)
            {
                documentInfo.Three = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.ThreeName = txtThree.Text.ToString();
            }
            if (fupFour.HasFile)
            {
                documentInfo.Four = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.FourName = txtFour.Text.ToString();
            }
            if (fupFive.HasFile)
            {
                documentInfo.Five = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.FiveName = txtFive.Text.ToString();
            }
            if (fupSix.HasFile)
            {
                documentInfo.Six = FileManager.GenerateUniqueName(FileType.Passport);
                documentInfo.SixName = txtSix.Text.ToString();
            }

            byte result = DocumentManager.Insert(documentInfo);
            if (result == 1)
            {
                SaveFiles(documentInfo);
                Insert_Create_Task(documentInfo.Id);
                long flowId = 0;
                long.TryParse(ddlFlow.SelectedItem.Value, out flowId);
                TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
                templateInfoCollection = TemplateManager.GetTemplateByFlow(flowId);
                foreach (TemplateInfo templateInfo in templateInfoCollection)
                {
                    TaskInfo taskInfo = new TaskInfo();
                    taskInfo.LevelId = templateInfo.LevelId;
                    taskInfo.DocumentId = documentInfo.Id;
                    taskInfo.TaskStatusId = 2;
                    taskInfo.TaskTypeId = 1;
                    taskInfo.ApprovalStatusId = 3;
                    taskInfo.UserId = templateInfo.UserId;
                    taskInfo.Description = "Auto Generated Task";
                    taskInfo.AltDescription = "Auto Generated Task";
                    taskInfo.Active = templateInfo.Active;
                    TaskManager.Insert(taskInfo);
                }
                
                TaskInfo nextTask =  TaskManager.GetNextTask(documentInfo.Id);
                if (nextTask != null)
                {
                    nextTask.TaskStatusId = 1;
                    nextTask.ApprovalStatusId = 3;
                    nextTask.CreatedTime = DateTime.Now;
                    TaskManager.Update_Start_Time(nextTask);
                }
                Response.Write("<script type='text/javascript'>");
                Response.Write("alert('Successfully Created! Document NO " + documentInfo.Name+ "');");
                Response.Write("document.location.href='../Document/List.aspx';");
                Response.Write("</script>");
                textwriter(documentInfo);
            }
        }
    }

    protected void Insert_Create_Task(long documentId)
    {
        TaskInfo taskInfo = new TaskInfo();
        taskInfo.LevelId =1;
        taskInfo.DocumentId = documentId;
        taskInfo.UserId = PortalUser.Current.UserId;
        taskInfo.Description = "Creation Task";
        taskInfo.AltDescription = "Creation Task";
        taskInfo.Active = true;
        taskInfo.TaskStatusId = 3;
        taskInfo.TaskTypeId = 1;
        taskInfo.ApprovalStatusId = 1;
        taskInfo.CreatedTime = DateTime.Now;
        taskInfo.UpdatedTime = DateTime.Now;
        TaskManager.Insert(taskInfo);
    }

    protected void Insert_Flow_Task(TaskInfo taskInfo)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }
	
	 private void BindFlow()
    {
        FlowInfoCollection flowInfoCollection = FlowManager.GetAll();
        ddlFlow.DataSource = flowInfoCollection;
        ddlFlow.DataTextField = "NameDivision";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();


        ddlFlow.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        ddlFlow.SelectedIndex = 0;
    }
    
	
	

    //private void BindFlow()
    //{
        //long totalRows = 0;
        //FlowSearchParams flowSearchParams = new FlowSearchParams();
        //flowSearchParams.CurrentPage = 1;
        //flowSearchParams.PageSize = 5000;
        //flowSearchParams.SortColumn = "";
        //flowSearchParams.SortOrder = "";
        //flowSearchParams.DivisionId = PortalUser.Current.DivisionId;
		
        //FlowInfoCollection flowInfoCollection = FlowManager.Search(flowSearchParams, out totalRows);
        //ddlFlow.DataSource = flowInfoCollection;
        //ddlFlow.DataTextField = "NameDivision";
        //ddlFlow.DataValueField = "Id";
        //ddlFlow.DataBind();


        //ddlFlow.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        //ddlFlow.SelectedIndex = 0;
    //}
    
    protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFlow.SelectedIndex > 0)
        {
            long flowId = 0;
            long.TryParse(ddlFlow.SelectedItem.Value, out flowId);
            TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();
            templateInfoCollection  = TemplateManager.GetTemplateByFlow(flowId);
            rptTemplate.DataSource = templateInfoCollection;
            rptTemplate.DataBind();
        }
    }

    private void BindType()
    {
        TypeInfoCollection typeInfoCollection = TypeManager.GetAll();
        ddlType.DataSource = typeInfoCollection;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Id";
        ddlType.DataBind();

        //ddlType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        //ddlType.SelectedIndex = 0;


        ddlType.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        ddlType.SelectedIndex = 0;

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

        if (string.IsNullOrEmpty(pathin) || pathin == "#" || !File.Exists(pathin))
        {
            return;
        }

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
 
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDescription, sigFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);

            }

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserFullName, regFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
            }


            foreach (TaskInfo taskInfo in taskInfoCollection)
            {
               
                    PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UserDesignationName, regFont));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);

            }

            foreach (TaskInfo taskInfo in taskInfoCollection)
            {

                PdfPCell cell = new PdfPCell(new Phrase(taskInfo.UpdatedTime.ToShortDateString(), regFont));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

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
   
}