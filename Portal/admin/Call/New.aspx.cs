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

public partial class admin_Call_New : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindUser();
            BindType();
            BindAssign();
            BindDivision();
        }
    }

    private void SaveFiles(DocumentInfo documentInfo)
    {
        try
        {

            FileManager.SaveFile(fupImage, FileType.Photo, documentInfo.Primary);
            //FileManager.SaveFile(fupImage, FileType.Photo, documentInfo.AltDescription);
            if (fupOne.HasFile)
                FileManager.SaveFile(fupOne, FileType.Photo, documentInfo.One);
            if (fupTwo.HasFile)
                FileManager.SaveFile(fupTwo, FileType.Photo, documentInfo.Two);
            if (fupThree.HasFile)
                FileManager.SaveFile(fupThree, FileType.Photo, documentInfo.Three);
            if (fupFour.HasFile)
                FileManager.SaveFile(fupFour, FileType.Photo, documentInfo.Four);
            if (fupFive.HasFile)
                FileManager.SaveFile(fupFive, FileType.Photo, documentInfo.Five);
            if (fupSix.HasFile)
                FileManager.SaveFile(fupSix, FileType.Photo, documentInfo.Six);

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

        if (upload.PostedFile.ContentType.ToString().Contains("image"))
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
            

            documentInfo.Name = "ddadas123";

            long.TryParse(ddlUser.SelectedItem.Value, out lvalue);
            documentInfo.FlowId = lvalue;

            long.TryParse(ddlType.SelectedItem.Value, out lvalue);
            documentInfo.TypeId = lvalue;

            long.TryParse(ddlAssign.SelectedItem.Value, out lvalue);
            documentInfo.AmendNo = lvalue;

            //decimal.TryParse(txtAmount.Text.Trim(), out dvalue);
            documentInfo.Amount = 0;

            documentInfo.Active = true;
            documentInfo.Description = txtDesc.InnerText.Trim();
            //documentInfo.Purpose = txtPurpose.Text.Trim();
            //documentInfo.AltDescription = txtDesc.InnerText.Trim();
            documentInfo.CreatedId = PortalUser.Current.UserId;
            documentInfo.CreatedTime = DateTime.Now;
            documentInfo.UpdatedId = PortalUser.Current.UserId;
            documentInfo.UpdatedTime = DateTime.Now;
            documentInfo.DocumentStatusId = 2;
            //documentInfo.AmendNo = 0;
           
            //documentInfo.Primary = FileManager.GenerateUniqueName(FileType.Passport);
            //documentInfo.AltDescription = FileManager.GenerateUniqueName(FileType.Passport);
            if (fupImage.HasFile)
            {
                
                documentInfo.Primary = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.AltDescription = txtMain.Text.ToString(); 
            }

            if (fupOne.HasFile)
            {
                documentInfo.One = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.OneName = txtOne.Text.ToString();
            }
            if (fupTwo.HasFile)
            {
                documentInfo.Two = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.TwoName = txtTwo.Text.ToString();
            }
            if (fupThree.HasFile)
            {
                documentInfo.Three = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.ThreeName = txtThree.Text.ToString();
            }
            if (fupFour.HasFile)
            {
                documentInfo.Four = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.FourName = txtFour.Text.ToString();
            }
            if (fupFive.HasFile)
            {
                documentInfo.Five = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.FiveName = txtFive.Text.ToString();
            }
            if (fupSix.HasFile)
            {
                documentInfo.Six = FileManager.GenerateUniqueName(FileType.Photo);
                documentInfo.SixName = txtSix.Text.ToString();
            }

            byte result = DocumentManager.Insert(documentInfo);
            if (result == 1)
            {
                SaveFiles(documentInfo);
                Response.Write("<script type='text/javascript'>");
                Response.Write("alert('Successfully Created! Document NO " + documentInfo.Name + "');");
                Response.Write("document.location.href='../Document/List.aspx';");
                Response.Write("</script>");
               
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
    }

    private void BindUser()
    {
        UserInfoCollection userInfoCollection = UserManager.GetAll();
        ddlUser.DataSource = userInfoCollection;
        ddlUser.DataTextField = "Name";
        ddlUser.DataValueField = "Id";
        ddlUser.DataBind();


        ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
        ddlUser.SelectedIndex = 0;
    }

    private void BindAssign()
    {
        UserInfoCollection userInfoCollection = UserManager.GetByRoleId(WebConfigKeys.ITAdminRoleId);
        ddlAssign.DataSource = userInfoCollection;
        ddlAssign.DataTextField = "Name";
        ddlAssign.DataValueField = "Id";
        ddlAssign.DataBind();
        ddlAssign.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
        ddlAssign.SelectedIndex = 0;
    }

    private void BindDivision()
    {
        DivisionInfoCollection divisionInfoCollection = DivisionManager.GetAll();
        ddlDivision.DataSource = divisionInfoCollection;
        ddlDivision.DataTextField = "Name";
        ddlDivision.DataValueField = "Id";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
        ddlDivision.SelectedIndex = 0;

    }

    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUser.SelectedIndex > 0)
        {
            long userId = 0;
            long.TryParse(ddlUser.SelectedItem.Value, out userId);
            UserInfo userInfo = UserManager.Get(userId);
            if (userInfo.DivisionId != 0)
                ddlDivision.SelectedValue = userInfo.DivisionId.ToString();
        }
    }

    private void BindType()
    {
        TypeInfoCollection typeInfoCollection = TypeManager.GetAll();
        ddlType.DataSource = typeInfoCollection;
        ddlType.DataTextField = "Name";
        ddlType.DataValueField = "Id";
        ddlType.DataBind();
        ddlType.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
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

    public string getLocalPath(string path)
    {
        return Server.MapPath(path);
    }
}