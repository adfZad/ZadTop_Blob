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

public partial class admin_Flow_FlowDetail : System.Web.UI.Page
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

    protected void rptMake_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptMake_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("FlowDetail.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;

        }
    }




    private void BindFlow()
    {
        FlowInfoCollection manufactureInfoCollection = FlowManager.GetAll();
        ddlFlow.DataSource = manufactureInfoCollection;
        ddlFlow.DataTextField = "Name";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();

        ddlFlow.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlFlow.SelectedIndex = 0;
    }

    private void BindFlowTemplate()
    {
        FlowTemplateInfoCollection manufactureInfoCollection = FlowTemplateManager.GetAll();
        ddlFlowTemplate.DataSource = manufactureInfoCollection;
        ddlFlowTemplate.DataTextField = "Name";
        ddlFlowTemplate.DataValueField = "Id";
        ddlFlowTemplate.DataBind();

        ddlFlowTemplate.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlFlowTemplate.SelectedIndex = 0;
    }

    private void BindUserName()
    {
        UserInfoCollection manufactureInfoCollection = UserManager.GetAll();
        ddlUser.DataSource = manufactureInfoCollection;
        ddlUser.DataTextField = "UserName";
        ddlUser.DataValueField = "Id";
        ddlUser.DataBind();

        ddlUser.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlUser.SelectedIndex = 0;
    }


    private void BindLevel()
    {
        LevelInfoCollection manufactureInfoCollection = LevelManager.GetAll();
        ddlLevel.DataSource = manufactureInfoCollection;
        ddlLevel.DataTextField = "Name";
        ddlLevel.DataValueField = "Id";
        ddlLevel.DataBind();

        ddlLevel.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlLevel.SelectedIndex = 0;
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("FlowDetail.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long lvalue = 0;
        long.TryParse(hdnId.Value, out id);

        FlowDetailInfo makeInfo = new FlowDetailInfo();
        makeInfo.Name = txtMake.Text.Trim();
        long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
        makeInfo.FlowId = lvalue;
        long.TryParse(ddlFlowTemplate.SelectedItem.Value, out lvalue);
        makeInfo.FlowTemplateId = lvalue;
        long.TryParse(ddlUser.SelectedItem.Value, out lvalue);
        makeInfo.UserId = lvalue;
        long.TryParse(ddlLevel.SelectedItem.Value, out lvalue);
        makeInfo.LevelId = lvalue;
        makeInfo.Description = txtDesc.Text.Trim();
        makeInfo.AltDescription = txtAltDesc.Text.Trim();
        makeInfo.Active = chkActive.Checked;
        makeInfo.CreatedId = PortalUser.Current.UserId;
        makeInfo.CreatedTime = DateTime.Now;
        makeInfo.UpdatedId = PortalUser.Current.UserId;
        makeInfo.UpdatedTime = DateTime.Now;


        byte result = FlowDetailManager.Insert(makeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("FlowDetail.aspx?pg={0}", 1));
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

        FlowDetailInfo makeInfo = new FlowDetailInfo();
        makeInfo.Id = id;
        makeInfo.Name = txtMake.Text.Trim();
        long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
        makeInfo.FlowId = lvalue;
        long.TryParse(ddlFlowTemplate.SelectedItem.Value, out lvalue);
        makeInfo.FlowTemplateId = lvalue;
        long.TryParse(ddlUser.SelectedItem.Value, out lvalue);
        makeInfo.UserId = lvalue;
        long.TryParse(ddlLevel.SelectedItem.Value, out lvalue);
        makeInfo.LevelId = lvalue;
        makeInfo.Description = txtDesc.Text.Trim();
        makeInfo.AltDescription = txtAltDesc.Text.Trim();
        makeInfo.Active = chkActive.Checked;
        makeInfo.UpdatedId = PortalUser.Current.UserId;
        makeInfo.UpdatedTime = DateTime.Now;

        byte result = FlowDetailManager.Update(makeInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("FlowDetail.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("FlowDetail.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {


        FlowDetailInfoCollection makeInfoCollection = FlowDetailManager.GetAll();
        rptMake.DataSource = makeInfoCollection;
        rptMake.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    BindFlow();
                    BindFlowTemplate();
                    BindUserName();
                    BindLevel();
                    hdnId.Value = string.Empty;
                    txtMake.Text = string.Empty;
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

                    FlowDetailInfo makeInfo = FlowDetailManager.Get(id);
                    if (makeInfo != null)
                    {
                        BindFlow();
                        BindFlowTemplate();
                        BindUserName();
                        BindLevel();
                        hdnId.Value = makeInfo.Id.ToString();
                        txtMake.Text = makeInfo.Name;
                        ddlFlow.SelectedValue = makeInfo.FlowId.ToString();
                        ddlFlowTemplate.SelectedValue = makeInfo.FlowTemplateId.ToString();
                        ddlUser.SelectedValue = makeInfo.UserId.ToString();
                        ddlLevel.SelectedValue = makeInfo.LevelId.ToString();
                        txtDesc.Text = makeInfo.Description;
                        txtAltDesc.Text = makeInfo.AltDescription;
                        chkActive.Checked = makeInfo.Active;
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