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

public partial class admin_Vehicle_Color : System.Web.UI.Page
{
    int rowIndex = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Label lbl = this.Master.FindControl("lblMenuName") as Label;
        //lbl.Text = "Color";
        btnSave.Attributes.Add("onclick", "return askConfirm();");
        //btnAdd.Attributes.Add("onclick", "return askConfirm();");
        //btnCancel.Attributes.Add("onclick", "return askConfirm();");
        //btnUpdate.Attributes.Add("onclick", "return askConfirm();"); 
        pager.PageSize = WebConfigKeys.GridPageSize;

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void rptColor_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lbl = e.Item.FindControl("lblRowNo") as Label;
            lbl.Text = (rowIndex++).ToString();

            //LinkButton button = e.Item.FindControl("lnkDelete") as LinkButton;
            //button.Attributes.Add("onClick", "return askConfirm();");

            LinkButton button = e.Item.FindControl("lnkEdit") as LinkButton;
            button.Attributes.Add("onClick", "return askConfirm();");
        }
    }

    protected void rptColor_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                string redirectUrl = string.Format("Color.aspx?action=edit&id={0}&pg={1}", e.CommandArgument, pager.CurrentIndex);
                Response.Redirect(redirectUrl);
                break;
            case "delete":
                long id = 0;
                long.TryParse(e.CommandArgument.ToString(), out id);
                byte result = ColorManager.Delete(id);
                if (result == 1)
                {
                    Response.Redirect(string.Format("Color.aspx?pg={0}", pager.CurrentIndex));
                }
                else
                {
                    lblDelMessage.Text = "Corresponding Color had more references";
                    BindData(); 
                }
                break;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Color.aspx?action=add&pg={0}", pager.CurrentIndex));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;
        ColorInfo colorInfo = new ColorInfo();
        colorInfo.Name = txtColor.Text.Trim();
        colorInfo.Description = txtDesc.Text.Trim();
        colorInfo.AltDescription = txtAltDesc.Text.Trim();
        colorInfo.Active = chkActive.Checked;
        colorInfo.CreatedId = PortalUser.Current.UserId;
        colorInfo.CreatedTime = DateTime.Now;
        colorInfo.UpdatedId= PortalUser.Current.UserId;
        colorInfo.UpdatedTime = DateTime.Now;


        byte result = ColorManager.Insert(colorInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Color.aspx?pg={0}", pager.CurrentIndex));
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

        ColorInfo colorInfo = new ColorInfo();
        colorInfo.Id = id;
        colorInfo.Name = txtColor.Text.Trim();
        colorInfo.Description = txtDesc.Text.Trim();
        colorInfo.AltDescription = txtAltDesc.Text.Trim();
        colorInfo.Active = chkActive.Checked;
        colorInfo.UpdatedId = PortalUser.Current.UserId;
        colorInfo.UpdatedTime = DateTime.Now;

        byte result = ColorManager.Update(colorInfo);

        if (result == 1)
        {
            Response.Redirect(string.Format("Color.aspx?pg={0}", pager.CurrentIndex));
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
        Response.Redirect(string.Format("Color.aspx?pg={0}", pager.CurrentIndex));
    }

    protected void pager_Command(object sender, CommandEventArgs e)
    {
        int currentPageIndex = 0;
        int.TryParse(e.CommandArgument.ToString(), out currentPageIndex);
        pager.CurrentIndex = currentPageIndex;

        string queryString = string.Empty;
        string queryCon = string.Empty;

        if (string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            BindData();
            return;
        }

        foreach (string key in Request.QueryString.AllKeys)
        {
            if (key == "pg")
            {
                queryCon = string.IsNullOrEmpty(queryString) ? "?" : "&";
                queryString += string.Format("{0}{1}={2}", queryCon, key, pager.CurrentIndex);
            }
            else
            {
                queryCon = string.IsNullOrEmpty(queryString) ? "?" : "&";
                queryString += string.Format("{0}{1}={2}", queryCon, key, Request.QueryString[key]);
            }
        }

        Response.Redirect(string.Format("Color.aspx{0}", queryString));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        ColorSearchParams colorSearchParams = new ColorSearchParams();
        
        
        if (!string.IsNullOrEmpty(Request.QueryString["pg"]))
        {
            int pageIndex = 0;
            int.TryParse(Request.QueryString["pg"], out pageIndex);
            pager.CurrentIndex = pageIndex;
        }

        long totalRows = 0;
        rowIndex = ((pager.CurrentIndex - 1) * pager.PageSize) + 1;

        colorSearchParams.CurrentPage = pager.CurrentIndex;
        colorSearchParams.PageSize = pager.PageSize;
        colorSearchParams.Name = txtName.Text.ToString();
        colorSearchParams.Description = txtSDescription.Text.ToString();
        colorSearchParams.AltDescription = txtSAltDescription.Text.ToString();

        //ColorInfoCollection colorInfoCollection = ColorManager.Search(pager.CurrentIndex, pager.PageSize, "", "", out totalRows);
        ColorInfoCollection colorInfoCollection = ColorManager.Search(colorSearchParams, out totalRows);
        rptColor.DataSource = colorInfoCollection;
        rptColor.DataBind();

        pager.ItemCount = totalRows;

        if (!string.IsNullOrEmpty(Request.QueryString["action"]))
        {
            switch (Request.QueryString["action"].ToLower())
            {
                case "add":
                    hdnId.Value = string.Empty;
                    txtColor.Text = string.Empty;
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

                    ColorInfo colorInfo = ColorManager.Get(id);
                    if (colorInfo != null)
                    {
                        hdnId.Value = colorInfo.Id.ToString();
                        txtColor.Text = colorInfo.Name;
                        txtDesc.Text = colorInfo.Description;
                        txtAltDesc.Text = colorInfo.AltDescription;
                        chkActive.Checked = colorInfo.Active;
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