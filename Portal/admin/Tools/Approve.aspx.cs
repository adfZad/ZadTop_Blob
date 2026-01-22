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

public partial class admin_Tools_Approve : System.Web.UI.Page
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

    protected void rptApprove_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptApprove_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Approve.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Approve.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        ApproveInfo approveInfo = new ApproveInfo();
        approveInfo.Name = txtApprove.Text.Trim();
        approveInfo.Description = txtDesc.Text.Trim();
        approveInfo.AltDescription = txtAltDesc.Text.Trim();
        approveInfo.Active = chkActive.Checked;
        approveInfo.CreatedId = PortalUser.Current.UserId;
        approveInfo.CreatedTime = DateTime.Now;
        approveInfo.UpdatedId = PortalUser.Current.UserId;
        approveInfo.UpdatedTime = DateTime.Now;


        byte result = ApproveManager.Insert(approveInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Approve.aspx?pg={0}", 1));
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

        ApproveInfo approveInfo = new ApproveInfo();
        approveInfo.Id = id;
        approveInfo.Name = txtApprove.Text.Trim();
        approveInfo.Description = txtDesc.Text.Trim();
        approveInfo.AltDescription = txtAltDesc.Text.Trim();
        approveInfo.Active = chkActive.Checked;
        approveInfo.UpdatedId = PortalUser.Current.UserId;
        approveInfo.UpdatedTime = DateTime.Now;

        byte result = ApproveManager.Update(approveInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Approve.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Approve.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        ApproveInfoCollection approveInfoCollection = ApproveManager.GetAll();
        rptApprove.DataSource = approveInfoCollection;
        rptApprove.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtApprove.Text = string.Empty;
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

                    ApproveInfo approveInfo = ApproveManager.Get(id);
                    if (approveInfo != null)
                    {
                        hdnId.Value = approveInfo.Id.ToString();
                        txtApprove.Text = approveInfo.Name;
                        txtDesc.Text = approveInfo.Description;
                        txtAltDesc.Text = approveInfo.AltDescription;
                        chkActive.Checked = approveInfo.Active;
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