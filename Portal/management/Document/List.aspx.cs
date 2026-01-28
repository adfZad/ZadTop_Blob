using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ZadHolding.Been;
using ZadHolding.Been.Enums;
using ZadHolding.Business;
using ZadHolding.Been.Collection;
using ZadHolding.PortalBase;
using ZadHolding.Data;

using System.Collections.Specialized;
using System.IO;
using ZadHolding.Utilities;
using System.Drawing.Imaging;

public partial class management_Document_List : System.Web.UI.Page
{
    int rowIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "Document Details";



        if (!IsPostBack)
        {
            BindData();
        }
    }
        
    private void BindData()
    {
        DocumentInfoCollection documentInfoCollection = DocumentManager.GetAll();


        long totalRows = 0;
        DocumentSearchParams documentSearchParams = new DocumentSearchParams();
        documentSearchParams.CurrentPage = 1;
        documentSearchParams.PageSize = 5000;
        documentSearchParams.SortColumn = "";
        documentSearchParams.SortOrder = "";
        //documentSearchParams.DivisionId = PortalUser.Current.DivisionId;
        //documentSearchParams.DepartmentId = PortalUser.Current.DepartmentId;
        //DocumentInfoCollection documentInfoCollection = DocumentManager.Search(documentSearchParams, out totalRows);
        //DocumentInfoCollection documentInfoCollection = DocumentManager.GetDocumentByUserId(PortalUser.Current.UserId);

        if (documentInfoCollection != null)
        {
            foreach (DocumentInfo documentInfo in documentInfoCollection)
            {
                if (documentInfo.AltDescription != null)
                {
                    documentInfo.AltDescription = DocPathMethod(documentInfo.AltDescription);
                }
            }
            rptDocument.DataSource = documentInfoCollection;
            rptDocument.DataBind();
        }
    }

    protected string DocPathMethod(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return "#";

        if (FileManager.FileExists(FileType.Passport, fileName))
        {
            return FileManager.GetSourceDirectory(FileType.Passport, fileName);
        }

        return "#";
    }
    
}