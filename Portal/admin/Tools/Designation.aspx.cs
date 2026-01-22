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

public partial class admin_Tools_Designation : System.Web.UI.Page
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

    protected void rptDesignation_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptDesignation_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Designation.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Designation.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        DesignationInfo designationInfo = new DesignationInfo();
        designationInfo.Name = txtDesignation.Text.Trim();
        designationInfo.Description = txtDesc.Text.Trim();
        designationInfo.AltDescription = txtAltDesc.Text.Trim();
        designationInfo.Active = chkActive.Checked;
        designationInfo.CreatedId = PortalUser.Current.UserId;
        designationInfo.CreatedTime = DateTime.Now;
        designationInfo.UpdatedId = PortalUser.Current.UserId;
        designationInfo.UpdatedTime = DateTime.Now;


        byte result = DesignationManager.Insert(designationInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Designation.aspx?pg={0}", 1));
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

        DesignationInfo designationInfo = new DesignationInfo();
        designationInfo.Id = id;
        designationInfo.Name = txtDesignation.Text.Trim();
        designationInfo.Description = txtDesc.Text.Trim();
        designationInfo.AltDescription = txtAltDesc.Text.Trim();
        designationInfo.Active = chkActive.Checked;
        designationInfo.UpdatedId = PortalUser.Current.UserId;
        designationInfo.UpdatedTime = DateTime.Now;

        byte result = DesignationManager.Update(designationInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Designation.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Designation.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DesignationInfoCollection designationInfoCollection = DesignationManager.GetAll();
        rptDesignation.DataSource = designationInfoCollection;
        rptDesignation.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtDesignation.Text = string.Empty;
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

                    DesignationInfo designationInfo = DesignationManager.Get(id);
                    if (designationInfo != null)
                    {
                        hdnId.Value = designationInfo.Id.ToString();
                        txtDesignation.Text = designationInfo.Name;
                        txtDesc.Text = designationInfo.Description;
                        txtAltDesc.Text = designationInfo.AltDescription;
                        chkActive.Checked = designationInfo.Active;
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