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

public partial class admin_Flow_Template : System.Web.UI.Page
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

    protected void rptTemplate_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            //LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            //button.Attributes.Add("onClick", "return askConfirm();");

            LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptTemplate_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Template.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);

                TemplateManager.Delete(id);
                Response.Redirect("Template.aspx");

                break;


        }
    }

   


    private void BindFlow()
    {
        FlowInfoCollection manufactureInfoCollection = FlowManager.GetAll();
        ddlFlow.DataSource = manufactureInfoCollection;
        ddlFlow.DataTextField = "NameDivision";
        ddlFlow.DataValueField = "Id";
        ddlFlow.DataBind();

        ddlFlow.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlFlow.SelectedIndex = 0;
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

    private void BindApprove()
    {
        ApproveInfoCollection approveInfoCollection = ApproveManager.GetAll();
        ddlApprove.DataSource = approveInfoCollection;
        ddlApprove.DataTextField = "Name";
        ddlApprove.DataValueField = "Id";
        ddlApprove.DataBind();

        ddlApprove.Items.Insert(0, new ListItem("[ -- Select -- ]"));
        ddlApprove.SelectedIndex = 0;
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
        Response.Redirect(string.Format("Template.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        long id = 0;
        long lvalue = 0;
        long.TryParse(hdnId.Value, out id);
            
        TemplateInfo templateInfo = new TemplateInfo();
       
        long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
        templateInfo.FlowId = lvalue;
        long.TryParse(ddlUser.SelectedItem.Value, out lvalue);
        templateInfo.UserId = lvalue;
        long.TryParse(ddlLevel.SelectedItem.Value, out lvalue);
        templateInfo.LevelId = lvalue;
        long.TryParse(ddlApprove.SelectedItem.Value, out lvalue);
        templateInfo.ApproveId = lvalue;
        templateInfo.Description = txtDesc.Text.Trim();
        templateInfo.AltDescription = txtAltDesc.Text.Trim();
        templateInfo.Active = chkActive.Checked;
        templateInfo.CreatedId = PortalUser.Current.UserId;
        templateInfo.CreatedTime = DateTime.Now;
        templateInfo.UpdatedId = PortalUser.Current.UserId;
        templateInfo.UpdatedTime = DateTime.Now;


        byte result = TemplateManager.Insert(templateInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Template.aspx?pg={0}", 1));
        }
        else if (result == 2)
        {
            lblMessage.Text = "Level is already assign";
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

        TemplateInfo templateInfo = new TemplateInfo();
        templateInfo.Id = id;
       
        long.TryParse(ddlFlow.SelectedItem.Value, out lvalue);
        templateInfo.FlowId = lvalue;
        long.TryParse(ddlUser.SelectedItem.Value, out lvalue);
        templateInfo.UserId = lvalue;
        long.TryParse(ddlLevel.SelectedItem.Value, out lvalue);
        templateInfo.LevelId = lvalue;
        long.TryParse(ddlApprove.SelectedItem.Value, out lvalue);
        templateInfo.ApproveId = lvalue;
        templateInfo.Description = txtDesc.Text.Trim();
        templateInfo.AltDescription = txtAltDesc.Text.Trim();
        templateInfo.Active = chkActive.Checked;
        templateInfo.UpdatedId = PortalUser.Current.UserId;
        templateInfo.UpdatedTime = DateTime.Now;

        byte result = TemplateManager.Update(templateInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Template.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Template.aspx?pg={0}", 1));
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {

       
        TemplateInfoCollection templateInfoCollection = TemplateManager.GetAll();
        rptTemplate.DataSource = templateInfoCollection;
        rptTemplate.DataBind();


        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                   BindFlow();
                   BindUserName();
                   BindLevel();
                   BindApprove();
                    hdnId.Value = string.Empty;
                  
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

                    TemplateInfo templateInfo = TemplateManager.Get(id);
                    if (templateInfo != null)
                    {
                        BindFlow();
                        BindUserName();
                        BindLevel();
                        BindApprove();
                        hdnId.Value = templateInfo.Id.ToString();
                       
                        ddlFlow.SelectedValue = templateInfo.FlowId.ToString();
                        ddlUser.SelectedValue = templateInfo.UserId.ToString();
                        ddlLevel.SelectedValue = templateInfo.LevelId.ToString();
                        ddlApprove.SelectedValue = templateInfo.ApproveId.ToString();
                        txtDesc.Text = templateInfo.Description;
                        txtAltDesc.Text = templateInfo.AltDescription;
                        chkActive.Checked = templateInfo.Active;
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
            valtxtEnDescription.Enabled = false;
            valArDescription.Enabled = false;
        }
    }
}