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

public partial class admin_Tools_Division : System.Web.UI.Page
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

    protected void rptDivision_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptDivision_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Division.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Division.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        DivisionInfo divisionInfo = new DivisionInfo();
        divisionInfo.Name = txtDivision.Text.Trim();
        divisionInfo.Description = txtDesc.Text.Trim();
        divisionInfo.AltDescription = txtAltDesc.Text.Trim();
        divisionInfo.Active = chkActive.Checked;
        divisionInfo.CreatedId = PortalUser.Current.UserId;
        divisionInfo.CreatedTime = DateTime.Now;
        divisionInfo.UpdatedId = PortalUser.Current.UserId;
        divisionInfo.UpdatedTime = DateTime.Now;


        byte result = DivisionManager.Insert(divisionInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Division.aspx?pg={0}", 1));
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

        DivisionInfo divisionInfo = new DivisionInfo();
        divisionInfo.Id = id;
        divisionInfo.Name = txtDivision.Text.Trim();
        divisionInfo.Description = txtDesc.Text.Trim();
        divisionInfo.AltDescription = txtAltDesc.Text.Trim();
        divisionInfo.Active = chkActive.Checked;
        divisionInfo.UpdatedId = PortalUser.Current.UserId;
        divisionInfo.UpdatedTime = DateTime.Now;

        byte result = DivisionManager.Update(divisionInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Division.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Division.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DivisionInfoCollection divisionInfoCollection = DivisionManager.GetAll();
        rptDivision.DataSource = divisionInfoCollection;
        rptDivision.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtDivision.Text = string.Empty;
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

                    DivisionInfo divisionInfo = DivisionManager.Get(id);
                    if (divisionInfo != null)
                    {
                        hdnId.Value = divisionInfo.Id.ToString();
                        txtDivision.Text = divisionInfo.Name;
                        txtDesc.Text = divisionInfo.Description;
                        txtAltDesc.Text = divisionInfo.AltDescription;
                        chkActive.Checked = divisionInfo.Active;
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