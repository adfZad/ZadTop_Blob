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

public partial class admin_Tools_Department : System.Web.UI.Page
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

    protected void rptDepartment_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptDepartment_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Department.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Department.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        DepartmentInfo departmentInfo = new DepartmentInfo();
        departmentInfo.Name = txtDepartment.Text.Trim();
        departmentInfo.Description = txtDesc.Text.Trim();
        departmentInfo.AltDescription = txtAltDesc.Text.Trim();
        departmentInfo.Active = chkActive.Checked;
        departmentInfo.CreatedId = PortalUser.Current.UserId;
        departmentInfo.CreatedTime = DateTime.Now;
        departmentInfo.UpdatedId = PortalUser.Current.UserId;
        departmentInfo.UpdatedTime = DateTime.Now;


        byte result = DepartmentManager.Insert(departmentInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Department.aspx?pg={0}", 1));
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

        DepartmentInfo departmentInfo = new DepartmentInfo();
        departmentInfo.Id = id;
        departmentInfo.Name = txtDepartment.Text.Trim();
        departmentInfo.Description = txtDesc.Text.Trim();
        departmentInfo.AltDescription = txtAltDesc.Text.Trim();
        departmentInfo.Active = chkActive.Checked;
        departmentInfo.UpdatedId = PortalUser.Current.UserId;
        departmentInfo.UpdatedTime = DateTime.Now;

        byte result = DepartmentManager.Update(departmentInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Department.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Department.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        DepartmentInfoCollection departmentInfoCollection = DepartmentManager.GetAll();
        rptDepartment.DataSource = departmentInfoCollection;
        rptDepartment.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtDepartment.Text = string.Empty;
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

                    DepartmentInfo departmentInfo = DepartmentManager.Get(id);
                    if (departmentInfo != null)
                    {
                        hdnId.Value = departmentInfo.Id.ToString();
                        txtDepartment.Text = departmentInfo.Name;
                        txtDesc.Text = departmentInfo.Description;
                        txtAltDesc.Text = departmentInfo.AltDescription;
                        chkActive.Checked = departmentInfo.Active;
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