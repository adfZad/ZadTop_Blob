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

public partial class admin_Document_Edit : System.Web.UI.Page
{
    public static TemplateInfoCollection templateInfoCollection = new TemplateInfoCollection();

    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void BindData(long id)
    {
        DocumentInfo documentInfo = DocumentManager.Get(id);

        if (documentInfo != null)
        {


            BindFlow();
            BindType();
            long flowId = 0;
            long.TryParse(documentInfo.FlowId.ToString(), out flowId);
            if (documentInfo.FlowId > 0)
                BindFlowTask(flowId);

            hdnId.Value = documentInfo.Id.ToString();
            hdnDocumentId.Value = documentInfo.Id.ToString();
            hdnPrimary.Value = documentInfo.Primary;
            hdnOne.Value = documentInfo.One;
            hdnTwo.Value = documentInfo.Two;
            hdnThree.Value = documentInfo.Three;
            hdnFour.Value = documentInfo.Four;
            hdnFive.Value = documentInfo.Five;
            hdnSix.Value = documentInfo.Six;
            txtName.Text = documentInfo.Name.ToString();
            txtPurpose.Text = documentInfo.Purpose.ToString();
            txtDesc.Text = documentInfo.Description.ToString();
            txtCurrency.Text = documentInfo.Currency.ToString();
            txtExchangeRate.Text = documentInfo.Rate.ToString();
            txtAmount.Text = documentInfo.Amount.ToString();
            txtOtherAmount.Text = documentInfo.OtherAmount.ToString();
            ddlFlow.SelectedValue = documentInfo.FlowId.ToString();
            ddlType.SelectedValue = documentInfo.TypeId.ToString();
        }
    }

    private void SaveFiles(DocumentInfo documentInfo)
    {
        try
        {
            

            FileManager.SaveFile(fupRegistration, FileType.Passport, documentInfo.Primary);
            FileManager.SaveFile(fupRegistration, FileType.Passport, documentInfo.AltDescription);
            if (fupOne.HasFile)
                FileManager.SaveFile(fupOne, FileType.Passport, documentInfo.One);
            if (fupTwo.HasFile)
                FileManager.SaveFile(fupTwo, FileType.Passport, documentInfo.Two);
            if (fupThree.HasFile)
                FileManager.SaveFile(fupThree, FileType.Passport, documentInfo.Three);
            if (fupFour.HasFile)
                FileManager.SaveFile(fupFour, FileType.Passport, documentInfo.Four);
            if (fupFive.HasFile)
                FileManager.SaveFile(fupFive, FileType.Passport, documentInfo.Five);
            if (fupSix.HasFile)
                FileManager.SaveFile(fupSix, FileType.Passport, documentInfo.Six);

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
            decimal dcvalue = 0;
            decimal devalue = 0;
            decimal dovalue = 0;
            long.TryParse(hdnDocumentId.Value.ToString(),out lvalue);
            decimal.TryParse(txtAmount.Text.ToString(), out dvalue);
            decimal.TryParse(txtCurrency.Text.ToString(), out dcvalue);
            decimal.TryParse(txtExchangeRate.Text.ToString(), out devalue);
            decimal.TryParse(txtOtherAmount.Text.ToString(), out dovalue);

            documentInfo.Id = lvalue;
            documentInfo.Name = txtName.Text.Trim();
            long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
            documentInfo.FlowId = lvalue;
            long.TryParse(ddlType.SelectedItem.Value, out lvalue);
            documentInfo.TypeId = lvalue;
            documentInfo.Active = true;
            documentInfo.Description = txtDesc.Text.Trim();
            documentInfo.Currency = txtCurrency.Text.Trim();
            documentInfo.Rate = devalue;
            documentInfo.Amount      = dvalue;
            documentInfo.OtherAmount = dovalue;
            documentInfo.Purpose = txtPurpose.Text.Trim();
            documentInfo.UpdatedId = PortalUser.Current.UserId;
            documentInfo.UpdatedTime = DateTime.Now;
            documentInfo.DocumentStatusId = 2;

           
                documentInfo.Primary = FileManager.GenerateUniqueName(FileType.Passport);
               documentInfo.AltDescription = FileManager.GenerateUniqueName(FileType.Passport);
          

            if (fupOne.HasFile)
                documentInfo.One = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.One = hdnOne.Value;

            if (fupTwo.HasFile)
                documentInfo.Two = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.Two = hdnTwo.Value;

            if (fupThree.HasFile)
                documentInfo.Three = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.Three = hdnThree.Value;

            if (fupFour.HasFile)
                documentInfo.Four = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.Four = hdnFour.Value;

            if (fupFive.HasFile)
                documentInfo.Five = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.Five = hdnFive.Value;

            if (fupSix.HasFile)
                documentInfo.Six = FileManager.GenerateUniqueName(FileType.Passport);
            else
                documentInfo.Six = hdnSix.Value;
           
            documentInfo.OneName = txtOne.Text.ToString();
            documentInfo.TwoName = txtTwo.Text.ToString();
            documentInfo.ThreeName = txtThree.Text.ToString();
            documentInfo.FourName = txtFour.Text.ToString();
            documentInfo.FiveName = txtFive.Text.ToString();
            documentInfo.SixName = txtSix.Text.ToString();


            byte result = DocumentManager.Update(documentInfo);
            if (result == 1)
            {
                SaveFiles(documentInfo);
                TaskInfoCollection inserttask = TaskManager.GetTaskByDocument(documentInfo.Id);
                if (inserttask != null)
                {
                    TaskInfo taskInfo = inserttask[0];
                    taskInfo.TaskStatusId = 3;
                    taskInfo.ApprovalStatusId = 1;
                    taskInfo.TaskTypeId = 1;
                    taskInfo.UpdatedTime = DateTime.Now;
                    TaskManager.Update_Return_End_Time(taskInfo);
                }
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
                    TaskManager.Insert(taskInfo);
                }

                TaskInfo nextTask = TaskManager.GetNextTask(documentInfo.Id);
                if (nextTask != null)
                {
                    nextTask.TaskStatusId = 1;
                    nextTask.ApprovalStatusId = 3;
                    nextTask.CreatedTime = DateTime.Now;
                    TaskManager.Update_Start_Time(nextTask);
                }
                Response.Redirect("List.aspx");
            }
        }
    }

    protected void Insert_Create_Task(long documentId)
    {
        TaskInfo taskInfo = new TaskInfo();
        taskInfo.LevelId = 1;
        taskInfo.DocumentId = documentId;
        taskInfo.UserId = PortalUser.Current.UserId;
        taskInfo.Description = "Creation Task";
        taskInfo.AltDescription = "Creation Task";
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
        ddlFlow.DataTextField = "Name";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();

        ddlFlow.Items.Insert(0, new ListItem(""));
        ddlFlow.SelectedIndex = 0;
    }

    private void BindFlowTask(long flowId)
    {
            templateInfoCollection = TemplateManager.GetTemplateByFlow(flowId);
            rptTemplate.DataSource = templateInfoCollection;
            rptTemplate.DataBind();
    }

    protected void ddlFlow_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFlow.SelectedIndex > 0)
        {
            long flowId = 0;
            long.TryParse(ddlFlow.SelectedItem.Value, out flowId);
            templateInfoCollection = TemplateManager.GetTemplateByFlow(flowId);
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

        ddlType.Items.Insert(0, new ListItem(""));
        ddlType.SelectedIndex = 0;
    }
}