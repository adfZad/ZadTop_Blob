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

public partial class admin_Flow_Flow : System.Web.UI.Page
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

    protected void rptFlow_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptFlow_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Flow.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Flow.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long lvalue = 0;
        FlowInfo flowInfo = new FlowInfo();
        flowInfo.Name = txtFlow.Text.Trim();
        long.TryParse(ddlDivision.SelectedItem.Value, out lvalue);
        flowInfo.DivisionId = lvalue;
        long.TryParse(ddlDepartment.SelectedItem.Value, out lvalue);
        flowInfo.DepartmentId = lvalue;
        flowInfo.Description = txtDesc.Text.Trim();
        flowInfo.AltDescription = txtAltDesc.Text.Trim();
        flowInfo.Active = chkActive.Checked;
        flowInfo.CreatedId = PortalUser.Current.UserId;
        flowInfo.CreatedTime = DateTime.Now;
        flowInfo.UpdatedId = PortalUser.Current.UserId;
        flowInfo.UpdatedTime = DateTime.Now;


        byte result = FlowManager.Insert(flowInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Flow.aspx?pg={0}", 1));
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
        long lvalue = 0;
        long.TryParse(hdnId.Value, out id);

        FlowInfo flowInfo = new FlowInfo();
        flowInfo.Id = id;
        flowInfo.Name = txtFlow.Text.Trim();
        long.TryParse(ddlDivision.SelectedItem.Value, out lvalue);
        flowInfo.DivisionId = lvalue;

        long.TryParse(ddlDepartment.SelectedItem.Value, out lvalue);
        flowInfo.DepartmentId = lvalue;

        flowInfo.Description = txtDesc.Text.Trim();
        flowInfo.AltDescription = txtAltDesc.Text.Trim();
        flowInfo.Active = chkActive.Checked;
        flowInfo.UpdatedId = PortalUser.Current.UserId;
        flowInfo.UpdatedTime = DateTime.Now;

        byte result = FlowManager.Update(flowInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Flow.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Flow.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        FlowInfoCollection flowInfoCollection = FlowManager.GetAll();
        rptFlow.DataSource = flowInfoCollection;
        rptFlow.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    BindDivision();
                    BindDepartment();
                    hdnId.Value = string.Empty;
                    txtFlow.Text = string.Empty;
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

                    FlowInfo flowInfo = FlowManager.Get(id);
                    if (flowInfo != null)
                    {
                        BindDivision();
                        BindDepartment();
                        hdnId.Value = flowInfo.Id.ToString();
                        ddlDivision.SelectedValue = flowInfo.DivisionId.ToString();
                        ddlDepartment.SelectedValue = flowInfo.DepartmentId.ToString(); 
                        txtFlow.Text = flowInfo.Name;
                        txtDesc.Text = flowInfo.Description;
                        txtAltDesc.Text = flowInfo.AltDescription;
                        chkActive.Checked = flowInfo.Active;
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

    private void BindDivision()
    {
        DivisionInfoCollection divisionInfoCollection = DivisionManager.GetAll();
        ddlDivision.DataSource = divisionInfoCollection;
        ddlDivision.DataTextField = "Name";
        ddlDivision.DataValueField = "Id";
        ddlDivision.DataBind();

        ddlDivision.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        ddlDivision.SelectedIndex = 0;
    }

    private void BindDepartment()
    {
        DepartmentInfoCollection departmentInfoCollection = DepartmentManager.GetAll();
        ddlDepartment.DataSource = departmentInfoCollection;
        ddlDepartment.DataTextField = "Name";
        ddlDepartment.DataValueField = "Id";
        ddlDepartment.DataBind();

        ddlDepartment.Items.Insert(0, new System.Web.UI.WebControls.ListItem(""));
        ddlDepartment.SelectedIndex = 0;
    }
}