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
using ZadHolding.Utilities;
using System.Drawing.Imaging;

public partial class admin_Flow_Photo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "Create Photo";

        if (!IsPostBack)
        {
           
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        cvPassportImage.Validate();
        cvPassportImageSize.Validate();
       

        if (!Page.IsValid)
        return;

        PhotoInfo userInfo = new PhotoInfo();

        long value = 0;
       
        userInfo.Id = value;
        userInfo.Name = txtMake.Text.Trim();


        userInfo.Photo = FileManager.GenerateUniqueName(FileType.Passport);
       

        if (PhotoManager.Insert(userInfo))
        {
            SaveFiles(userInfo);
            Response.Redirect("Photo.aspx");
        }
    }

    private void SaveFiles(PhotoInfo userInfo)
    {
        try
        {
            FileManager.SaveFile(fupPassport, FileType.Photo, userInfo.Photo);
           

            //ImageManager.GenerateThumbnails(ImageManager.GetImage(fupImage), FileType.Photo, userInfo.Photo);
            //ImageManager.GenerateThumbnails(ImageManager.GetImage(fupPassport), FileType.Passport, userInfo.Passport);
            //ImageManager.GenerateThumbnails(ImageManager.GetImage(fupRP), FileType.RP, userInfo.RP);
        }
        catch
        { }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("List.aspx");
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

   
}