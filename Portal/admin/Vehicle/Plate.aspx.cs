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

public partial class admin_Vehicle_Plate : System.Web.UI.Page
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

    protected void rptPlate_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptPlate_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Plate.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, 1);
                Response.Redirect(redirectUrl);
                break;
           
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Plate.aspx?action=add&pg={0}", 1));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        PlateInfo plateInfo = new PlateInfo();
        plateInfo.Name = txtPlate.Text.Trim();
        plateInfo.Description = txtDesc.Text.Trim();
        plateInfo.AltDescription = txtAltDesc.Text.Trim();
        plateInfo.Active = chkActive.Checked;
        plateInfo.CreatedId = PortalUser.Current.UserId;
        plateInfo.CreatedTime = DateTime.Now;
        plateInfo.UpdatedId= PortalUser.Current.UserId;
        plateInfo.UpdatedTime = DateTime.Now;


        byte result = PlateManager.Insert(plateInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Plate.aspx?pg={0}",1));
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

        PlateInfo plateInfo = new PlateInfo();
        plateInfo.Id = id;
        plateInfo.Name = txtPlate.Text.Trim();
        plateInfo.Description = txtDesc.Text.Trim();
        plateInfo.AltDescription = txtAltDesc.Text.Trim();
        plateInfo.Active = chkActive.Checked;
        plateInfo.UpdatedId = PortalUser.Current.UserId;
        plateInfo.UpdatedTime = DateTime.Now;

        byte result = PlateManager.Update(plateInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Plate.aspx?pg={0}", 1));
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
        Response.Redirect(string.Format("Plate.aspx?pg={0}", 1));
    }

  
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        PlateInfoCollection plateInfoCollection = PlateManager.GetAll();
        rptPlate.DataSource = plateInfoCollection;
        rptPlate.DataBind();
                

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtPlate.Text = string.Empty;
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

                    PlateInfo plateInfo = PlateManager.Get(id);
                    if (plateInfo != null)
                    {
                        hdnId.Value = plateInfo.Id.ToString();
                        txtPlate.Text = plateInfo.Name;
                        txtDesc.Text = plateInfo.Description;
                        txtAltDesc.Text = plateInfo.AltDescription;
                        chkActive.Checked = plateInfo.Active;
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