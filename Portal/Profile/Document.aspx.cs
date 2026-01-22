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
using System.IO;
using ZadHolding.Utilities;
using System.Drawing.Imaging;

public partial class admin_Profile_Document : System.Web.UI.Page
{
    public DocumentInfo documentInfo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Label lbl = this.Master.FindControl("lblMenuName") as Label;
        //lbl.Text = "Document Details";

        if (Request.QueryString["id"] != null)
        {
            long id = 0;
            long.TryParse(Request.QueryString["id"], out id);
            documentInfo = DocumentManager.Get(id);
            if (documentInfo != null)
            {
                TaskInfoCollection taskInfoCollection = TaskManager.GetTaskByDocument(documentInfo.Id);
                rptTask.DataSource = taskInfoCollection;
                rptTask.DataBind();
            }

        }
    }


 
    public string MainDocPath
    {
        get
        {
            string path = "#";
            string localPath = "";

            if (!string.IsNullOrEmpty(documentInfo.Primary))
            {
                path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                    WebConfigKeys.UploadImageRootDirectory,
                    FileType.Passport,
                    documentInfo.Primary.Substring(0, 1),
                    documentInfo.Primary.Substring(0, 2),
                    documentInfo.Primary);

                localPath = Server.MapPath(path);

                if (!File.Exists(localPath))
                {
                    path = "#";
                }
            }

            return path;
        }
    }

    public string OneDocPath
    {
        get
        {
            string path = "#";
            string localPath = "";

            if (!string.IsNullOrEmpty(documentInfo.One))
            {
                path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                    WebConfigKeys.UploadImageRootDirectory,
                    FileType.Passport,
                    documentInfo.One.Substring(0, 1),
                    documentInfo.One.Substring(0, 2),
                    documentInfo.One);

                localPath = Server.MapPath(path);

                if (!File.Exists(localPath))
                {
                    path = "#";
                }
            }

            return path;
        }
    }

    public string TwoDocPath
    {
        get
        {
            string path = "#";
            string localPath = "";

            if (!string.IsNullOrEmpty(documentInfo.Two))
            {
                path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                    WebConfigKeys.UploadImageRootDirectory,
                    FileType.Passport,
                    documentInfo.Two.Substring(0, 1),
                    documentInfo.Two.Substring(0, 2),
                    documentInfo.Two);

                localPath = Server.MapPath(path);

                if (!File.Exists(localPath))
                {
                    path = "#";
                }
            }

            return path;
        }
    }

    public string ThreeDocPath
    {
        get
        {
            string path = "#";
            string localPath = "";

            if (!string.IsNullOrEmpty(documentInfo.Three))
            {
                path = string.Format("../../{0}/{1}/{2}/{3}/{4}",
                    WebConfigKeys.UploadImageRootDirectory,
                    FileType.Passport,
                    documentInfo.Three.Substring(0, 1),
                    documentInfo.Three.Substring(0, 2),
                    documentInfo.Three);

                localPath = Server.MapPath(path);

                if (!File.Exists(localPath))
                {
                    path = "#";
                }
            }

            return path;
        }
    }


}