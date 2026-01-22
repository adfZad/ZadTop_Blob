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
using System.Web.UI.HtmlControls;

public partial class management_Document_StatusList : System.Web.UI.Page
{
    int rowIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label lbl = this.Master.FindControl("lblMenuName") as Label;
        lbl.Text = "Document Details";



        if (!IsPostBack)
        {
            BindData();
            BindDivisionName();
            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {


        BindDatas();

    }

    protected void pager_Command(object sender, CommandEventArgs e)
    {
        int currentPageIndex = 0;
        int.TryParse(e.CommandArgument.ToString(), out currentPageIndex);
        pager.CurrentIndex = currentPageIndex;



        BindDatas();




    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {
            foreach (RepeaterItem item in rptDocument.Items)
            {
                CheckBox chkItem = (CheckBox)item.FindControl("chkActive");
                long PlValue = 0;
                if (chkItem.Checked == true)
                {
                    DocumentsStatusInfo asseststatusInfo = new DocumentsStatusInfo();
                    HiddenField hdnDocumentId = (HiddenField)item.FindControl("hdnDocumentId");
                    long.TryParse(hdnDocumentId.Value, out PlValue);
                    asseststatusInfo.DocumentId = PlValue;
                    HiddenField hdnIds = (HiddenField)item.FindControl("hdnIds");
                    long.TryParse(hdnIds.Value, out PlValue);
                    asseststatusInfo.Id = PlValue;
                    asseststatusInfo.StatusId = 2;
                    DocumentsStatusManager.Insert(asseststatusInfo);


                }
            }


            Response.Redirect("StatusList.aspx");
        }
    }

    private void BindDatas()
    {

        long value = 0;

        rowIndex = ((pager.CurrentIndex - 1) * 15000);

        DocumentSearchParams legalSearchParams = new DocumentSearchParams();
        legalSearchParams.CurrentPage = pager.CurrentIndex;
        legalSearchParams.PageSize = 15000;


        DateTime date = DateTime.MinValue;


        long.TryParse(ddlDivisionName.SelectedItem.Value, out value);
        legalSearchParams.DivisionId = value;


           





        if (!string.IsNullOrEmpty(hdnId.Value))
        {
            string[] vals = hdnId.Value.Split(new char[] { '|' });
            if (vals.Length == 2)
            {
                legalSearchParams.SortColumn = vals[0];
                legalSearchParams.SortOrder = vals[1];
            }
        }

        rowIndex = (pager.CurrentIndex - 1) * 15000;
        long totalRows = 0;
		
		


        DocumentInfoCollection userInfoCollection = DocumentManager.SearchAll(legalSearchParams, out totalRows);
        rptDocument.DataSource = userInfoCollection;
        rptDocument.DataBind();


        pager.ItemCount = totalRows;
		
		 if (userInfoCollection != null)
        {
            foreach (DocumentInfo documentInfo in userInfoCollection)
            {
                if (documentInfo.AltDescription != null)
                {
                    documentInfo.AltDescription = DocPathMethod(documentInfo.AltDescription);
                }
            }
            rptDocument.DataSource = userInfoCollection;
            rptDocument.DataBind();
        }
    }

    private void BindData()
    {
        DocumentInfoCollection documentInfoCollection = DocumentManager.GetApprovedAll();


        long totalRows = 0;
        DocumentSearchParams documentSearchParams = new DocumentSearchParams();
        documentSearchParams.CurrentPage = 1;
        documentSearchParams.PageSize = 5000;
        documentSearchParams.SortColumn = "";
        documentSearchParams.SortOrder = "";
        //documentSearchParams.DivisionId = PortalUser.Current.DivisionId;
        //documentSearchParams.DepartmentId = PortalUser.Current.DepartmentId;
        //DocumentInfoCollection documentInfoCollection = DocumentManager.Search(documentSearchParams, out totalRows);



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

    private void BindDivisionName()
    {
        DivisionInfoCollection cityInfoCollection = DivisionManager.GetAll();
        ddlDivisionName.DataSource = cityInfoCollection;
        ddlDivisionName.DataTextField = "Name";
        ddlDivisionName.DataValueField = "Id";
        ddlDivisionName.DataBind();

        ddlDivisionName.DataBind();
        ddlDivisionName.Items.Insert(0, new ListItem(""));
        ddlDivisionName.SelectedIndex = 0;




    }

   
    
}