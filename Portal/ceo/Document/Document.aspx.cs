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

public partial class ceo_Document_Document : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {

        btnSave.Attributes.Add("onclick", "return askConfirm();");


        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void rptDocument_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptDocument_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Document.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Document.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        DocumentInfo documentInfo = new DocumentInfo();
        documentInfo.Name = txtDocument.Text.Trim();
        documentInfo.Description = txtDesc.Text.Trim();
        documentInfo.AltDescription = txtAltDesc.Text.Trim();
        documentInfo.Active = chkActive.Checked;
        documentInfo.CreatedId = PortalUser.Current.UserId;
        documentInfo.CreatedTime = DateTime.Now;
        documentInfo.UpdatedId = PortalUser.Current.UserId;
        documentInfo.UpdatedTime = DateTime.Now;


        byte result = DocumentManager.Insert(documentInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Document.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long.TryParse(hdnId.Value, out id);

        DocumentInfo documentInfo = new DocumentInfo();
        documentInfo.Id = id;
        documentInfo.Name = txtDocument.Text.Trim();
        documentInfo.Description = txtDesc.Text.Trim();
        documentInfo.AltDescription = txtAltDesc.Text.Trim();
        documentInfo.Active = chkActive.Checked;
        documentInfo.UpdatedId = PortalUser.Current.UserId;
        documentInfo.UpdatedTime = DateTime.Now;

        byte result = DocumentManager.Update(documentInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Document.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Duplicate values not allowed.";
        }
        else
        {
            lblMessage.Text = "Error occured. Try again.";
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Document.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DocumentInfoCollection documentInfoCollection = DocumentManager.GetAll();
        rptDocument.DataSource = documentInfoCollection;
        rptDocument.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtDocument.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                    txtAltDesc.Text = string.Empty;
                    chkActive.Checked = true;
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);

                    DocumentInfo documentInfo = DocumentManager.Get(id);
                    if (documentInfo != null)
                    {
                        hdnId.Value = documentInfo.Id.ToString();
                        txtDocument.Text = documentInfo.Name;
                        txtDesc.Text = documentInfo.Description;
                        txtAltDesc.Text = documentInfo.AltDescription;
                        chkActive.Checked = documentInfo.Active;
                    }

                    dvInput.Style.Remove("display");
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;

                    break;
                default:
                    dvInput.Style.Add("display", "none");

                    break;
            }
        }
        else
        {
            dvInput.Style.Add("display", "none");
            valtxtCode.Enabled = false;
            valtxtEnDescription.Enabled = false;
            valArDescription.Enabled = false;
        }
    }
}