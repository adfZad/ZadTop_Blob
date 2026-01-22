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

public partial class admin_Vehicle_Model : System.Web.UI.Page
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

    protected void rptModel_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
            //LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            //button.Attributes.Add("onClick", "return askConfirm();");

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptModel_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Model.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);
                byte result = ModelManager.Delete(id);
                if (result == 1)
                {
                    Response.Redirect(string.Format("Model.aspx?pg={0}", 1));
                }
                else
                {
                    lblDelMessage.Text = "Corresponding Model had more references";
                    //BindData(); 
                }
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Model.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        ModelInfo modelInfo         = new ModelInfo();
        modelInfo.Name              = txtModel.Text.Trim();
        modelInfo.Description       = txtDesc.Text.Trim();
        modelInfo.AltDescription    = txtAltDesc.Text.Trim();
        modelInfo.Active            = chkActive.Checked;
        modelInfo.CreatedId         = PortalUser.Current.UserId;
        modelInfo.CreatedTime       = DateTime.Now;
        modelInfo.UpdatedId         = PortalUser.Current.UserId;
        modelInfo.UpdatedTime       = DateTime.Now;


        long makeId = 0;
        long.TryParse(ddlMake.SelectedItem.Value, out makeId);

        modelInfo.MakeId = makeId;

        byte result = ModelManager.Insert(modelInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Model.aspx?pg={0}", 1));
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

        ModelInfo modelInfo = new ModelInfo();
        modelInfo.Id = id;
        modelInfo.Name = txtModel.Text.Trim();
        modelInfo.Description = txtDesc.Text.Trim();
        modelInfo.AltDescription = txtAltDesc.Text.Trim();
        modelInfo.Active = chkActive.Checked;
        modelInfo.UpdatedId = PortalUser.Current.UserId;
        modelInfo.UpdatedTime = DateTime.Now;

        long makeId = 0;
        long.TryParse(ddlMake.SelectedItem.Value, out makeId);

        modelInfo.MakeId = makeId;

        byte result = ModelManager.Update(modelInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Model.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Model.aspx?pg={0}", 1));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {

        ModelInfoCollection modelInfoCollection = ModelManager.GetAll();
        rptModel.DataSource = modelInfoCollection;
        rptModel.DataBind();
           

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtModel.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                    txtAltDesc.Text = string.Empty;
                    chkActive.Checked = true;

                    BindMake();
                    dvInput.Style.Remove("display");
                    btnSave.Visible = true;
                    btnUpdate.Visible = false;
                    break;
                case "edit":
                    long id = 0;
                    long.TryParse(Request.QueryString["id"], out id);
                    BindMake();
                    ModelInfo modelInfo = ModelManager.Get(id);
                    if (modelInfo != null)
                    {
                        hdnId.Value = modelInfo.Id.ToString();
                        txtModel.Text = modelInfo.Name;
                        txtDesc.Text = modelInfo.Description;
                        txtAltDesc.Text = modelInfo.AltDescription;
                        chkActive.Checked = modelInfo.Active;
                        ddlMake.SelectedValue = modelInfo.MakeId.ToString();
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

    private void BindMake()
    {
        MakeInfoCollection makeInfoCollection = MakeManager.GetAll();
        ddlMake.DataSource = makeInfoCollection;
        ddlMake.DataTextField = "Name";
        ddlMake.DataValueField = "Id";
        ddlMake.DataBind();
        ddlMake.Items.Insert(0, new ListItem(""));
        ddlMake.SelectedIndex = 0;
    }
    
}