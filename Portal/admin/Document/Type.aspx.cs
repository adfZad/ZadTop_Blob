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

public partial class admin_Document_Type : System.Web.UI.Page
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

    protected void rptType_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptType_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Type.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Type.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        TypeInfo typeInfo = new TypeInfo();
        typeInfo.Name = txtType.Text.Trim();
        typeInfo.Description = txtDesc.Text.Trim();
        typeInfo.AltDescription = txtAltDesc.Text.Trim();
        typeInfo.Active = chkActive.Checked;
        typeInfo.CreatedId = PortalUser.Current.UserId;
        typeInfo.CreatedTime = DateTime.Now;
        typeInfo.UpdatedId = PortalUser.Current.UserId;
        typeInfo.UpdatedTime = DateTime.Now;


        byte result = TypeManager.Insert(typeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Type.aspx?pg={0}", 1));
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

        TypeInfo typeInfo = new TypeInfo();
        typeInfo.Id = id;
        typeInfo.Name = txtType.Text.Trim();
        typeInfo.Description = txtDesc.Text.Trim();
        typeInfo.AltDescription = txtAltDesc.Text.Trim();
        typeInfo.Active = chkActive.Checked;
        typeInfo.UpdatedId = PortalUser.Current.UserId;
        typeInfo.UpdatedTime = DateTime.Now;

        byte result = TypeManager.Update(typeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Type.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Type.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        TypeInfoCollection typeInfoCollection = TypeManager.GetAll();
        rptType.DataSource = typeInfoCollection;
        rptType.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtType.Text = string.Empty;
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

                    TypeInfo typeInfo = TypeManager.Get(id);
                    if (typeInfo != null)
                    {
                        hdnId.Value = typeInfo.Id.ToString();
                        txtType.Text = typeInfo.Name;
                        txtDesc.Text = typeInfo.Description;
                        txtAltDesc.Text = typeInfo.AltDescription;
                        chkActive.Checked = typeInfo.Active;
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